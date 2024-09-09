Imports System
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO


Public Class frmOtkupZ

    Dim strUpit As String

    Dim strOrder As String
    Dim strNaslov As String

    Dim trenutniRed As DataGridViewRow 'red u gridu koji se azurira, kada kliknemo na grid ili kada program nadje da sifra postoji
    Dim intID As Long 'ID reda koji se azurira, kada kliknemo na grid ili kada program nadje da sifra postoji
    Dim strID As String 'naziv kolone u kojoj je ID

    Dim strTabela As String 'naziv tabele
    Dim intBrKol As Integer 'broj kolona u DataGridView komponenti(Ivan dao ime promenljivoj)
    Dim strNazKol() As String 'naziv kolona u DataGridView komponenti, prvo mora biti Rb a drugo ID
    Dim intSirineKolona() As Integer 'sirine kolona u DataGrieView komponenti(Ivan dodao promenljivu)
    Dim strFildGrid() As String 'naziv kolona iz tabele koje se ucitavaju u grid
    Dim strFild() As String 'naziv kolona u tabeli koje se azuriraju
    Dim intBrTexBoxova  'broj text boxova na formi
    Dim strLabelPoredText() As String 'nazivi labela pored text boxova
    Dim strSifra As String 'naziv kolone u kojoj je sifra(kolona posle id sloga) mora biti i primarni kljuc tabele

    Dim intSuma() As Integer 'niz za izracunavanje sume (1 ce biti u polju gde hocu da dobijem sumu)
    Dim strInic() As String 'inicijalne vrednosti textBoxova pri prvom unosu u bazu

    Dim valDatum() As TextBox 'niz TextBoxova koji primaju Date
    Dim valDecimal() As TextBox 'niz TextBoxova koji primaju Decimal(Currency in MS Access)
    Dim valInteger() As TextBox 'niz TextBoxova koji primaju Integer(Number in MS Access)

    Public Enum enmStavke
        nemaStavke
        stavkeID
        stavkeSifra
    End Enum

    Dim insStavke As enmStavke

    Dim boolLandscape As Boolean 'nacin stampanja

    Dim textBoxLista As New List(Of TextBox) 'lista textBoxova u groupBox-u 1
    Dim labelLista As New List(Of Label)     'lista labela u groupBox-u 1

    'za kreiranje labele koja korisniku opisuje šifru
    Dim imenikIzbor As New Dictionary(Of TextBox, Object())

    'ako je poslat ovaj TextBox preko konstruktora, znači da je u poslati TextBox potrebno upisati šifru novog upisa(nakon klika na dugme upiši)
    'i zatvoriti formu
    Dim textBoxPrenesiSifru As TextBox

    'promenljiva u kojoj se unosi Text za dugme stavke ukoliko stavke postoje.
    Dim txtDugmetaStavke As String = ""

    'podrazumevani konstruktor
    Public Sub New()
        'dizajner zahteva da se na pocetku svakog konstruktora forme pozove f-ja InitializeComponent()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' šifra dokumenta koja je OBAVEZNO dobijena iz konstruktora
    ''' </summary>
    Dim tmpDok As String = ""
    Dim tmpMag As String = ""

    ''' <summary>
    ''' šifra dokumenta
    ''' </summary>
    ''' <param name="dokument">šifra dokumenta koja će se upsivati i koristiti</param>
    Public Sub New(dokument As String, magacin As String)
        'dizajner zahteva da se na pocetku svakog konstruktora forme pozove f-ja InitializeComponent()
        InitializeComponent()
        tmpDok = dokument
        tmpMag = magacin
    End Sub
    'novi konstruktor ukoliko se šalje TextBox u koji treba da se upiše šifra nakon klika na dugme Upiši
    Public Sub New(tb As TextBox)
        'dizajner zahteva da se na početku svakog konstruktora forme pozove f-ja InitializeComponent()
        InitializeComponent()
        textBoxPrenesiSifru = tb
    End Sub

    Private Sub inicijalizujPromenljive()

        strNaslov = "Otkup poljoprivrednih proizvoda na prvom mestu " & "Dokument: " & tmpDok & " " & " Objekat: " & tmpMag
        Me.Text = strNaslov
        Me.lblNaslov.Text = strNaslov

        strTabela = "KalkVZ3"
        intID = 0
        strID = "Kmz_id"
        strSifra = "Kmz_Broj"

        'strUpit = "SELECT * FROM " + Me.strTabela + " WHERE 0=0 "
        strUpit = $"SELECT * FROM (((KalkVZ3 
                    LEFT JOIN Konta ON Konta.Kon_Sifra = KalkVZ3.Kmz_Kon_432)
                    LEFT JOIN MestOtk ON MestOtk.Mto_Sifra = KalkVZ3.Kmz_Mto_Sifra)
                    LEFT JOIN FaktZUslovi ON FaktZUslovi.FkzU_Sifra = KalkVZ3.Kmz_FkzN_Sifra)"

        strOrder = " ORDER BY " & strSifra

        intBrKol = 14
        intSirineKolona = New Integer() {50, 50, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        strNazKol = New String() {"ID", "RB", "Broj", "SifraDokumenta", "Datum", "Šif. Polj", "Poljoprivrednik", "Dostavnica", "Datum dostavnice", "PDV poljo.", "PDV nep.", "Ukupno", "Otk mes.", "Paleta"}
        strFildGrid = New String() {strID, "umesto rb", strSifra, "Kmz_Dok_Sifra", "Kmz_Datum", "Kmz_Kon_432", "Kon_Opis", "Kmz_Dostavnica", "Kmz_DatumD", "Kmz_OsnovicaPlj", "Kmz_PdvNepriznati", "Kmz_Ukupno", "Mto_Sifra", "Kmz_Paleta"}

        intBrTexBoxova = 6
        strFild = New String() {"Kmz_Broj", "Kmz_Datum", "Kmz_Mto_Sifra", "Kmz_Kon_432", "Kmz_FkzN_Sifra", "Kmz_Paleta"}
        strLabelPoredText = New String() {"Broj", "Datum", "Otk mes", "Poljo", "Rok za uplatu", "Paleta"}
        strInic = New String() {"", Date.Today, "", "", "1", ""}

        'inicijalizujem niz za izracunavanje sume
        intSuma = New Integer() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

        valDatum = New TextBox() {}
        valDecimal = New TextBox() {}
        valInteger = New TextBox() {}

        'popuna i formatiranje header-a grida
        modGrid.gridPrviRed(dgvPrikaz, intBrKol, strNazKol, intSirineKolona, Me.Name & Me.dgvPrikaz.Name & "Width")

        'popuna grida
        modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)

        'Odabir: 1. nemaStavke; 2. stavkeID, 3.stavkeSifra
        insStavke = enmStavke.stavkeID
        txtDugmetaStavke = "Nove stavke"

        'primer popune imenika ukoliko ima dugmadi za izbor(ovde ih ima dva)
        'na mesto {0} ce doci vrednost TextBox-a koji je definisan kao ključ, a upit se moze nastaviti i imati vise uslova
        'npr. kada je potrebno Kor_VrstaLookUp kao drugi uslov(zbog toga sam i promenio ovo)
        'imenikIzbor.Add(TextBox4, New Object() {lblOpis1, "SELECT Frm_Naziv FROM Firme WHERE Frm_Sifra = '{0}'"})
        imenikIzbor.Add(TextBox1, New Object() {lblOpis1, "SELECT Kon_Opis FROM Konta WHERE Kon_Sifra = '{0}'"})
        'imenikIzbor.Add(TextBox5, New Object() {lblOpis2, "SELECT Zap_Ime FROM Zaposleni WHERE Zap_Sifra = '{0}'"})
    End Sub

    Private Sub PatternForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            'da bi forma prepoznala događaj klika na dugme(Enter umesto Tab)
            Me.KeyPreview = True

            inicijalizujPromenljive()

            podesiTextBoxove()
            srediFormuPocetno()
            dodeliDogadjaj()
            implementirajDesign()

            dgvPrikaz.RowHeadersWidth = CInt(dgvPrikaz.RowHeadersWidth * 1.35)

            'ucitava se pozicija i velicina forme
            'modForma.citajPozicijuIzBaze(Me, boolLandscape)
            inicijalizujTextBoxove(False)
            PolozajToolStripMenuItem.Text = If(boolLandscape = True, "Polozeno", "Uspravno")
            'podesava se minimalna veličina forme
            Me.MinimumSize = New Size(870, 550)
            'ako nema stavke dugme tsbStavke se nece videti tokom celog izvrsavanja
            If insStavke = enmStavke.nemaStavke Then
                tsbStavke.Visible = False
            End If
        Catch ex As Exception
            MsgBox("Greška pri otvaranju forme " & Me.Text & " :" & ex.Message)
        End Try
    End Sub

    Private Sub btnUpisi_Click(sender As Object, e As EventArgs) Handles btnUpisi.Click
        'Vrši se provera ispravnosti datuma u nizu TextBoxova koji je definisan u f-ji inicijalizujPromenljive()
        'If modValidacija.ValidacijaDatuma(valDatum) <> True Then
        '    Exit Sub
        'End If

        'VAZNO!!! pri upisu novog sloga uzimaju se samo kolone u koje hoćemo da dodamo vrednost.
        'Samo tako će kolone u koje ne dodajemo vrednost dobiti DEFOULT vrednosti definisanu u bazi,
        'a i brže se izvršava kada se povlače samo potrebni podaci iz baze.
        Dim j As Integer
        Dim kolone As String = ""

        For j = 0 To strFild.Length - 1
            kolone = kolone & strFild(j) & " , "
        Next
        kolone = kolone.Remove(kolone.LastIndexOf(","))
        kolone = kolone & " , Kmz_Dok_Sifra, Kmz_Kon_132, Kmz_Kon_960, Kmz_VKpr_Sifra "

        Dim strSQL As String = "SELECT " & kolone & " FROM " + Me.strTabela
        Dim adapter As New OleDbDataAdapter(strSQL, adoCN)
        Dim tabela As New DataTable
        Dim red As DataRow
        Dim builder As New OleDbCommandBuilder(adapter)

        Try
            'iz tabele se ne uzimaju podaci jer nisu potrebni, preuzima se samo šema tabele
            adapter.FillSchema(tabela, SchemaType.Source)
            'kreira se novi red sa istom šemom kao i tabela
            red = tabela.NewRow
            'vrednosti iz textBoxova se upisujem u red, validacija datuma se izvršava u prvoj liniji tako da ovde više nije potrebna
            Dim i As Integer
            For i = 0 To intBrTexBoxova - 1
                red(strFild(i)) = textBoxLista(i).Text
            Next
            red("Kmz_Dok_Sifra") = "381"
            red("Kmz_Kon_132") = "1311"
            red("Kmz_Kon_960") = "/"
            red("Kmz_VKpr_Sifra") = "7"

            'tabeli dodeljujem red i update-ujem tabelu u bazi
            tabela.Rows.Add(red)
            adapter.Update(tabela)

            'provera da li je poslat TextBox iz druge forme u koji samo treba preneti šifru i zatvoriti trenutnu formu
            If Not IsNothing(textBoxPrenesiSifru) Then
                textBoxPrenesiSifru.Text = TextBox0.Text
                Me.Dispose()
                Exit Sub
            End If

            'podesavam vrednost status  labele
            lblStatus.ForeColor = Color.Red
            lblStatus.Text = "Slog sa šifrom " & TextBox0.Text & " je uspešno upisan. "
            'If dgvPrikaz.RowCount Then
            '    modGrid.dodajRedUGrid(dgvPrikaz, strFildGrid, strUpit, strID)
            'Else
            modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
            'End If

            inicijalizujTextBoxove(True)
            srediFormuPocetno()
            lblNaslov.Text = strNaslov

            'POZIV FORME STAVKE (UKOLIKO POSTOJI) JE OBAVEZNO NAKON F-JE DODAJ RED U GRID INAČE MOŽE DA DOĐE DO OZBILJNE GREŠKE!!!!!!!!!!!!!!!!!!!!!!!!!!
            'NAJBOLJE DA POZIV FORME STAVKE UVEK BUDE NA KRAJU OVE F-JE ISPOD OVOG KOMENTARA!!!
            'poziv forme za upis stavki
            modMain.strLinkPub = modOleDb.vratiJedVrednostUpit("SELECT @@identity")

            'STVAKE OTKUŠ SSSSSSSS
            frmOtkupS.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            frmOtkupS.ShowDialog()

            '12/23 Veljko, dodajemo da se iz prilikom Upisa novog Popisa otvori forma Stavke
            'If tmpDok = "691" Then
            '    dgvPrikaz.Rows(dgvPrikaz.RowCount - 2).Selected = True 'pokusaj selektovanja poslednjeg unetog reda
            '    otvoriFormuStavke()
            '    modGrid.prikaziPoslednjiRedUGridu(dgvPrikaz)
            'End If

        Catch ex As Exception
            MsgBox("Greška prilikom upisa podataka: " + ex.Message, 0, strNaslov)
        End Try
    End Sub

    Private Sub btnIzmeni_Click(sender As Object, e As EventArgs) Handles btnIzmeni.Click
        'Provera da li je knjizeno
        'If modNalog.VecKnjizeno(tmpDok, TextBox0.Text) Then Exit Sub
        ''Vrši se provera ispravnosti datuma u nizu TextBoxova koji je definisan u f-ji inicijalizujPromenljive()
        'If modValidacija.ValidacijaDatuma(valDatum) <> True Then
        '    Exit Sub
        'End If

        Dim strSQL As String = "SELECT * FROM " & strTabela & " WHERE " & strID & " = " & intID
        Dim adapter As New OleDbDataAdapter(strSQL, adoCN)
        Dim tabela As New DataTable
        Dim red As DataRow
        Dim builder As New OleDbCommandBuilder(adapter)
        Try
            'data adapter puni tabelu
            adapter.Fill(tabela)
            red = tabela.Rows(0)
            'vrednosti iz textBoxova se upisujem u red, validacija datuma se izvršava u prvoj liniji tako da ovde više nije potrebna
            Dim i As Integer
            For i = 0 To intBrTexBoxova - 1
                red(strFild(i)) = textBoxLista(i).Text
            Next
            'update-ujem tabelu u bazi
            adapter.Update(tabela)
            izmeniUGridu()
            'podesavam vrednost status  labele
            lblStatus.ForeColor = Color.Red
            lblStatus.Text = "Slog sa šifrom " & TextBox0.Text & " je uspešno izmenjen. "
            'modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
            inicijalizujTextBoxove(False)
            srediFormuPocetno()
            lblNaslov.Text = strNaslov
            trenutniRed.Selected = True
        Catch ex As Exception
            MsgBox("Greška prilikom izmene podataka: " + ex.Message, 0, strNaslov)
        End Try
    End Sub

    Private Sub btnBrisi_Click(sender As Object, e As EventArgs) Handles btnBrisi.Click
        'Provera da li je knjizeno
        'If modNalog.VecKnjizeno(tmpDok, TextBox0.Text) Then Exit Sub
        Dim pozicija As Integer = trenutniRed.Index
        Dim strSQL As String = "DELETE FROM " & strTabela & " WHERE " & strID & " = " & intID
        Try
            'poziva se f-ja koja izvršava komandu, i mora da vrati 1, što znači da je jedan slog izbrisan iz baze
            If modOleDb.izvrsiKomandu(strSQL) = 1 Then
                'podesavam vrednost status  labele
                lblStatus.ForeColor = Color.Red
                lblStatus.Text = "Slog sa šifrom " & TextBox0.Text & " je uspešno izbrisan. "
                'ne ucitavam ponovo grid vec brisem iz grida postojeci red
                Me.dgvPrikaz.Rows.Remove(trenutniRed)
                'modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol)
                'popuniKolonuKnjizeno()
                inicijalizujTextBoxove(False)
                srediFormuPocetno()
                lblNaslov.Text = strNaslov
                'selektujem red iznad izbrisanog reda ili ispod ukoliko brisem prvi red
                If dgvPrikaz.Rows.Count > 0 Then
                    If pozicija > 0 Then
                        dgvPrikaz.Rows.Item(pozicija - 1).Selected = True
                    Else
                        dgvPrikaz.Rows.Item(pozicija).Selected = True
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Greška prilikom brisanja podataka: " + ex.Message, 0, strNaslov)
        End Try
    End Sub

    Private Sub btnIsprazniPolja_Click(sender As Object, e As EventArgs) Handles btnIsprazniPolja.Click
        lblStatus.Text = ""
        inicijalizujTextBoxove(False)
        srediFormuPocetno()
    End Sub

    Public Sub implementirajDesign()
        'Me.BackColor = ColorTranslator.FromWin32(modDesign.bojaPozadineProzora)
    End Sub

    Private Sub podesiTextBoxove()

        'kreiram listu TextBox-ova
        textBoxLista.Insert(0, TextBox0)
        textBoxLista.Insert(1, TextBox1)
        textBoxLista.Insert(2, TextBox2)
        textBoxLista.Insert(3, TextBox3)
        textBoxLista.Insert(4, TextBox4)
        textBoxLista.Insert(5, TextBox5)
        'textBoxLista.Insert(6, TextBox5)


        'podesavam izabranu boju txtBoxa
        Dim i As Integer = 0
        For i = 0 To intBrTexBoxova - 1
            'textBoxLista(i).BackColor = ColorTranslator.FromWin32(modDesign.bojaTb)
        Next

        'kreiram listu Labela
        labelLista.Insert(0, Label0)
        labelLista.Insert(1, Label1)
        labelLista.Insert(2, Label2)
        labelLista.Insert(3, Label3)
        labelLista.Insert(4, Label4)
        labelLista.Insert(5, Label5)
        'labelLista.Insert(6, Label6)


        'ovo je u slucaju da ima isti broj text boxova i labela, ako ih nema isto onda verovatno mora rucno da se podesi
        For i = 0 To intBrTexBoxova - 1
            labelLista(i).Text = strLabelPoredText(i) & tackice 'text komponenta labele
            labelLista(i).Location = New Point(labelLista(i).Location.X, textBoxLista(i).Location.Y + textBoxLista(i).Height / 2 - 2)
        Next

        'podesavam duzinu labela lblNaslov i lblStatus
        lblNaslov.Width = StatusStrip1.Width - 20
        lblStatus.Width = StatusStrip2.Width - 20
    End Sub

    Private Sub srediFormuPocetno()
        TextBox0.Text = modMain.vratiSledecuSifru(strTabela, strSifra)
        'TextBox0.Text = modMain.vratiSledecuSifruUslov(strTabela, strSifra, "Edi_Dok_Sifra", tmpDok)

        'prvo pokušavam da stavim Fokus a ako to ne može onda koristim Select
        If TextBox0.CanFocus Then
            TextBox0.Focus()
        Else
            TextBox0.Select()
        End If

        isprazniSveOpisneLabele()

        btnUpisi.Enabled = True
        btnIzmeni.Enabled = False
        btnBrisi.Enabled = False
        btnIsprazniPolja.Enabled = True
        'u gridu ništa ne treba da bude selektovano
        Me.dgvPrikaz.ClearSelection()

        'Stavke nisu omogućene
        Me.tsbStavke.Text = txtDugmetaStavke
        Me.tsbStavke.Enabled = False
    End Sub

    Private Sub srediFormuPostojiSifra()
        popuniSveOpisneLabele()

        btnUpisi.Enabled = False
        btnIzmeni.Enabled = True
        btnBrisi.Enabled = True
        btnIsprazniPolja.Enabled = True

        'Stavke su omogućene
        Me.tsbStavke.Enabled = True
    End Sub

    Private Sub srediFormuNePostojiSifra()
        isprazniSveOpisneLabele()

        btnUpisi.Enabled = True
        btnIzmeni.Enabled = False
        btnBrisi.Enabled = False
        btnIsprazniPolja.Enabled = True
        btnZatvori.Enabled = True
        dgvPrikaz.ClearSelection()

        'Stavke nisu omogućene
        Me.tsbStavke.Text = txtDugmetaStavke
        Me.tsbStavke.Enabled = False
    End Sub

    ''' <summary>
    ''' f-ja inicijalizuje TextBox-ove vrednostima iz niza
    ''' </summary>
    ''' <param name="sacuvajTrenVr">da li čuva trenutne vrednosti pre inicijalizacije(kod f-je Upisi)</param>
    Private Sub inicijalizujTextBoxove(sacuvajTrenVr As Boolean)
        Dim i As Integer
        If sacuvajTrenVr Then
            'slučaj da čuva trenutne vrednosti, trenutne vrednosti se prepisuju u strInic gde je strInic različito od praznog stringa.
            For i = 0 To intBrTexBoxova - 1
                If strInic(i) <> "" Then
                    strInic(i) = textBoxLista(i).Text
                End If
            Next
        End If

        'u svakom slucaju se vrsi inicijalizacija textBoxova iz strInic niza
        For i = 1 To intBrTexBoxova - 1
            textBoxLista(i).Text = strInic(i)
        Next
    End Sub

    Private Sub izmeniUGridu()
        Dim reader As OleDbDataReader = Nothing
        'Dim strSQL As String = strUpit & " AND " & strID & " = " & intID
        Dim strSQL As String = strUpit & " WHERE " & strID & " = " & intID
        Dim komanda As New OleDbCommand(strSQL, adoCN)
        Try
            reader = komanda.ExecuteReader()
            If reader.Read() Then
                For i As Integer = 2 To strFildGrid.Count - 1

                    If IsDBNull(reader(strFildGrid(i))) Then
                        trenutniRed.Cells(i).Value = ""
                    Else
                        Select Case reader(strFildGrid(i)).GetType()
                            Case GetType(Int16) 'Number: Integer in MS Access
                                trenutniRed.Cells(i).Value = reader(strFildGrid(i))
                            Case GetType(Int32) 'Autonumber (Long Integer), Number: (Long Integer) in MS Access
                                trenutniRed.Cells(i).Value = reader(strFildGrid(i))
                            Case GetType(String) 'Text, Memo, Hyperlink in MS Access
                                trenutniRed.Cells(i).Value = reader(strFildGrid(i))
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                            Case GetType(Decimal) 'Currency, Number: Decimal in MS Access
                                trenutniRed.Cells(i).Value = Format(reader(strFildGrid(i)), "#,##0.00")
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            Case GetType(DateTime) 'Date/Time in MS Access
                                trenutniRed.Cells(i).Value = Format(reader(strFildGrid(i)), "dd.MM.yyyy")
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            Case Else
                                trenutniRed.Cells(i).Value = reader(strFildGrid(i))
                        End Select
                    End If
                Next
            Else
                MsgBox("Došlo je do greške prilikom ažuriranja reda u tabeli, red nije pronađen", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox("Greška prilikom ažuriranja reda u gridu: " & ex.Message)
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Sub


#Region "Region TextBox0.Leave"

    Private Sub TextBox0_Leave(sender As Object, e As EventArgs)
        Dim kljucPretrage As String
        kljucPretrage = TextBox0.Text
        If kljucPretrage <> "" Then
            If pronadjiSifru(kljucPretrage) = True Then
                srediFormuPostojiSifra()
                'selectujem red sa unesenom sifrom
                selectujRedUGridu(kljucPretrage)
                lblStatus.ForeColor = Color.Black
                lblStatus.Text = "Selektovan je slog sa šifrom " & TextBox0.Text & ". Mozete ga izmeniti i brisati. "
                'menjam naziv dugmeta tsbStavke samo ukoliko su stavke povezane sa zaglavljem preko šifre, pošto nema svrhe pisati ID.
                If insStavke = enmStavke.stavkeSifra Then
                    Me.tsbStavke.Text = txtDugmetaStavke & " " & TextBox0.Text
                End If
            Else
                lblStatus.ForeColor = Color.Black
                lblStatus.Text = "Novi slog sa šifrom " & TextBox0.Text & "  mozete upisati. "
                inicijalizujTextBoxove(False)
                srediFormuNePostojiSifra()
            End If
        Else
            srediFormuPocetno()
        End If
    End Sub

    Private Function pronadjiSifru(ByVal kljucPretrage As String) As Boolean
        'Dim strSQL As String = "SELECT * FROM " + strTabela + " WHERE " + strSifra + " = '" + kljucPretrage + "'"
        Dim strSQL As String = "SELECT * FROM " + strTabela + " WHERE " + strSifra + " = '" + kljucPretrage + "' AND Edi_Dok_Sifra = '" + tmpDok + "'"
        Dim command As New OleDbCommand(strSQL, adoCN)
        Dim reader As OleDbDataReader = Nothing
        Dim i As Integer
        Try
            reader = command.ExecuteReader()
            If reader.Read() Then
                'vrednosti iz polja u tabeli se upisuju u odgovarajuce textBox-ove
                For i = 0 To intBrTexBoxova - 1
                    'ako je vrednost u bazi DBNull onda u text boxove upisujem prazan string da ne bi doslo do greske
                    If IsDBNull(reader(strFild(i))) Then
                        textBoxLista(i).Text = ""
                    Else
                        textBoxLista(i).Text = reader(strFild(i))
                    End If
                Next
                'ovde je definisan ID pronadjenog upisa(pri napustanju text box-a ili pri kliku na DGV)
                intID = reader(strID)

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Greska prilikom pretrage podataka: " + ex.Message, 0, strNaslov)
            Return False
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Function

    Private Sub selectujRedUGridu(ByVal kljucPretrage As String)
        dgvPrikaz.ClearSelection()
        For Each row As DataGridViewRow In dgvPrikaz.Rows
            If (Convert.ToString(row.Cells(strNazKol(2)).Value).ToLower() = kljucPretrage.ToLower()) Then
                row.Selected = True
                'dodeljujem trenutno selektovan red
                trenutniRed = row
                'ako selektovani red nije prikazan onda ga prikazuje
                If row.Displayed = False Then
                    dgvPrikaz.FirstDisplayedScrollingRowIndex = row.Index
                End If
                Exit For 'kada pronadje i selektuje odgovarajuci red nema potrebe da nastavlja sa pretragom
            End If
        Next
    End Sub

#End Region

#Region "Region dgvPrikaz.Click"

    Private Sub dgvPrikaz_Click(sender As Object, e As EventArgs) Handles dgvPrikaz.Click
        redSelektovan()
    End Sub

    Private Sub redSelektovan()
        'upis u textBoxove vrsim iz baze, prvo proveravam da li je red uopšte selektovan
        If dgvPrikaz.SelectedRows.Count > 0 Then
            'dodeljujem trenutno selektovan red
            trenutniRed = dgvPrikaz.SelectedRows(0)

            If Convert.ToString(trenutniRed.Cells(strNazKol(0)).Value) <> "" Then
                intID = trenutniRed.Cells(strNazKol(0)).Value 'dodeljujem ID
                pronadjiID()
                'cim je red selektovan u bazi postoji upis, nema potrebe za proverom
                srediFormuPostojiSifra()
                'menjam naziv dugmeta tsbStavke samo ukoliko su stavke povezane sa zaglavljem preko šifre, pošto nema svrhe pisati ID.
                If insStavke = enmStavke.stavkeSifra Then
                    Me.tsbStavke.Text = txtDugmetaStavke & " " & trenutniRed.Cells(strNazKol(2)).Value
                End If
                napuniGridStav_UlMat(trenutniRed.Cells(strNazKol(0)).Value)

            Else
                lblStatus.Text = ""
                inicijalizujTextBoxove(False)
                srediFormuPocetno()
            End If
        End If
    End Sub

    Private Function pronadjiID() As Boolean
        Dim strSQL As String = "SELECT * FROM " & strTabela & " WHERE " & strID & " = " & intID
        Dim command As New OleDbCommand(strSQL, adoCN)
        Dim reader As OleDbDataReader = Nothing
        Dim i As Integer
        Try
            reader = command.ExecuteReader()
            If reader.Read() Then
                For i = 0 To intBrTexBoxova - 1
                    'ako je vrednost u bazi DBNull onda u text boxove upisujem prazan string da ne bi doslo do greske
                    If IsDBNull(reader(strFild(i))) Then
                        textBoxLista(i).Text = ""
                    Else
                        textBoxLista(i).Text = reader(strFild(i))
                    End If
                Next
                'ako je upis u labele uspesan, menjam text u status bar-u
                lblStatus.ForeColor = Color.Black
                lblStatus.Text = "Selektovan je slog sa šifrom " & TextBox0.Text & ". Mozete ga izmeniti i brisati. "
                Return True
            Else
                MsgBox("Doslo je do greske jer pri kliku na kolonu tabele, podaci moraju biti pronadjeni i upisani u polja!", 0, strNaslov)
                Return False
            End If
        Catch ex As Exception
            MsgBox("Greska prilikom citanja selektovane kolone iz baze: " & ex.Message, 0, strNaslov)
            Return False
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Function

#End Region

    Private Sub dodeliDogadjaj()

        'pri napuštanju svim textBoxova koji se inicijalizuju iz forme za izbor, poziva se događaj popuniOpisnuLabelu
        For Each stavka In imenikIzbor
            AddHandler stavka.Key.Leave, AddressOf popuniOpisnuLabelu
        Next

        'dodeljujem sve text boxova u groupBoxu1 događajima ulazniDogadjaj i izlazniDogadjaj
        For Each tb In textBoxLista
            AddHandler tb.Enter, AddressOf ulazniDogadjaj
            AddHandler tb.Leave, AddressOf izlazniDogadjaj
            AddHandler tb.Enter, AddressOf selektujText
        Next

        'svaki TextBox koji je naveden u nizu valDecimal dodeljujem događaju validacijaDecimal i dodajNulu koje se nalaze u modValidacija
        For Each tb In valDecimal
            'AddHandler tb.KeyPress, AddressOf modValidacija.validacijaDecimal
            'AddHandler tb.Leave, AddressOf modValidacija.dodajNulu
        Next

        'svaki TextBox koji je naveden u nizu valInteger dodeljujem događaju validacijaInteger i dodajNulu koje se nalaze u modValidacija
        For Each tb In valInteger
            'AddHandler tb.KeyPress, AddressOf modValidacija.validacijaInteger
            'AddHandler tb.Leave, AddressOf modValidacija.dodajNulu
        Next
    End Sub

    Private Sub ulazniDogadjaj(sender As Object, e As EventArgs) 'Handles TextBox1.Enter, TextBox2.Enter, cmbPretrazi.Enter
        'DirectCast(sender, Control).BackColor = ColorTranslator.FromWin32(modDesign.bojaAktivnogTb)
    End Sub

    Private Sub izlazniDogadjaj(sender As Object, e As EventArgs) 'Handles TextBox1.Leave, TextBox2.Leave, cmbPretrazi.Leave
        'DirectCast(sender, Control).BackColor = ColorTranslator.FromWin32(modDesign.bojaTb)
    End Sub

    Private Sub selektujText(sender As Object, e As EventArgs)
        BeginInvoke(DirectCast(Sub() sender.SelectAll(), Action))
    End Sub

    Private Sub dgvPrikaz_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPrikaz.CellDoubleClick
        If dgvPrikaz.SelectedRows(0).Cells(strNazKol(0)).Value <> "" Then
            'otvara se forma stavke samo ukoliko postoje stavke
            If insStavke <> enmStavke.nemaStavke Then
                otvoriFormuStavke()
            End If
        End If
    End Sub

    Private Sub tsbStavke_Click(sender As Object, e As EventArgs) Handles tsbStavke.Click
        'otvara se forma stavke samo ukoliko postoje stavke
        If insStavke <> enmStavke.nemaStavke Then
            otvoriFormuStavke()
        End If
    End Sub

    Private Sub otvoriFormuStavke()
        If dgvPrikaz.SelectedRows.Count = 1 Then
            trenutniRed = dgvPrikaz.SelectedRows(0)
            'treba paziti da li je zaglavlje sa stavkama povezano sa id ili sa sifrom (strNazKol(0) ili strNazKol(2))
            Select Case insStavke
                Case enmStavke.stavkeID
                    modMain.strLinkPub = trenutniRed.Cells(0).Value
                    frmOtkupS.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    frmOtkupS.ShowDialog()
                    'Update stavke na kojoj je rađeno, nako izlaska iz forme Stavke
                    izmeniUGridu()

                Case enmStavke.stavkeSifra
                    modMain.strLinkPub = trenutniRed.Cells(2).Value
                    frmOtkupS.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    frmOtkupS.ShowDialog()
                    'Update stavke na kojoj je rađeno, nako izlaska iz forme Stavke
                    izmeniUGridu()
            End Select
        Else
            MsgBox("Morate selektovati red u tabeli.", vbOKOnly, "Forma stavke")
        End If
    End Sub

#Region "Region Standardne opcije"

    Private Sub PregledŠtampeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PregledŠtampeToolStripMenuItem.Click
        'modStampaCR.stampajGridCR(Me.dgvPrikaz, lblNaslov.Text, boolLandscape, enmStampaExport.Print)
    End Sub

    Private Sub PDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFToolStripMenuItem.Click
        'modStampaCR.stampajGridCR(Me.dgvPrikaz, lblNaslov.Text, boolLandscape, enmStampaExport.Pdf)
    End Sub

    Private Sub WordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WordToolStripMenuItem.Click
        'modStampaCR.stampajGridCR(Me.dgvPrikaz, lblNaslov.Text, boolLandscape, enmStampaExport.Word)
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        'modStampaCR.stampajGridCR(Me.dgvPrikaz, lblNaslov.Text, boolLandscape, enmStampaExport.Excel)
    End Sub

    Private Sub UspravnoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PolozajToolStripMenuItem.Click
        If PolozajToolStripMenuItem.Text = "Uspravno" Then
            PolozajToolStripMenuItem.Text = "Polozeno"
            boolLandscape = True
        Else
            PolozajToolStripMenuItem.Text = "Uspravno"
            boolLandscape = False
        End If
    End Sub

    Private Sub PretragaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PretragaToolStripMenuItem.Click
        'modMain.kreirajFormuZaPretragu(Me, strNazKol, intBrKol, strUpit, strOrder, strFildGrid, strNaslov, intSuma, Me.lblNaslov)
    End Sub

    Private Sub PočetnoStanjeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PočetnoStanjeToolStripMenuItem.Click
        modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
        lblNaslov.Text = strNaslov
    End Sub

    Private Sub btnZatvori_Click(sender As Object, e As EventArgs) Handles btnZatvori.Click
        Me.Close()
    End Sub

#End Region

    Private Sub Pattern_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'dimenzije i lokacija prozora se upisuju u bazu samo ukoliko prozor nije minimiziran
        If Me.WindowState <> FormWindowState.Minimized Then
            'upisuje se u bazu trenutna pozicija i velicina forme 
            'modForma.upisiPozicijuUBazu(Me, boolLandscape)
        End If
        'upisuje se u bazu sirina kolona u gridu
        modGrid.Set_Grid_Width(Me.dgvPrikaz, Me.Name & Me.dgvPrikaz.Name & "Width")
        Me.Dispose()
    End Sub

    Private Sub Form_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub OtvorenIzborDatuma(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim picker = DirectCast(sender, DateTimePicker)

        'Select Case picker.Name
        '    Case DateTimePicker1.Name
        '        If modValidacija.proveriDatum(TextBox1.Text) Then
        '            DateTimePicker1.Value = Convert.ToDateTime(TextBox1.Text)
        '        End If
        'End Select
    End Sub

    Private Sub ZatvorenIzbotDatuma(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim picker = DirectCast(sender, DateTimePicker)

        Select Case picker.Name
            Case DateTimePicker1.Name 'izabran je prvi DateAndTime Picker za izbor datuma izrade
                TextBox1.Text = DateTimePicker1.Value.Date()
                TextBox1.Focus()
                'Case DateTimePicker2.Name
                '    TextBox2.Text = DateTimePicker2.Value.Date()
                '    TextBox2.Focus()
        End Select
    End Sub

    Private Sub btnIzborFirma_Click(sender As Object, e As EventArgs)
        modMain.kreirajFormuZaIzbor("SELECT Kon_Sifra, Kon_Opis FROM Konta WHERE Kon_Vrsta = 'A' ",
                                    New String() {"Konto", "Naziv"},
                                    New Integer() {5, 25},
                                    "Izbor kupca")
        If modMain.glbPrenesi <> "" Then
            TextBox1.Text = modMain.glbPrenesi
            modMain.glbPrenesi = ""
            TextBox1.Focus()
            'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
            TextBox2.Focus()
        Else
            TextBox1.Focus()
        End If
    End Sub

    Private Sub btnNovaFirma_Click(sender As Object, e As EventArgs)
        'Dim forma As New frmSmKTipSobe(TextBox4)
        'forma.ShowDialog()
        'TextBox4.Focus()
        'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
        'TextBox5.Focus()
    End Sub

    ''' <summary>
    ''' događaj popunjava opisnu labelu na osnovu šifre u poslatom TextBoxu-u.
    ''' </summary>
    Private Sub popuniOpisnuLabelu(sender As Object, e As EventArgs)
        Dim labela As Label
        Dim tb As TextBox = DirectCast(sender, TextBox)
        Dim rez As Object = Nothing

        If tb.Text <> "" Then
            Dim niz() As Object = imenikIzbor(sender)

            labela = DirectCast(niz(0), Label)
            'u f-ji String.Format() se upitu dodeljuje Text u odgovarajucem TextBox-u.
            rez = modOleDb.vratiJedVrednostUpit(String.Format(niz(1), tb.Text))
            'ukoliko je vrednost Nothing ili DBNull tada labela dobija vrednost praznog stringa
            If IsNothing(rez) Or IsDBNull(rez) Then
                labela.Text = ""
            Else
                labela.Text = rez
            End If
        End If
    End Sub

    ''' <summary>
    ''' f-ja popunjava opisne labele sa vrednostima u odgovarajućim TextBoxo-vima
    ''' </summary>
    Private Sub popuniSveOpisneLabele()
        For Each stavka In imenikIzbor
            popuniOpisnuLabelu(stavka.Key, New EventArgs())
        Next
    End Sub

    ''' <summary>
    ''' f-ja prazni sve opisne labele
    ''' </summary>
    Private Sub isprazniSveOpisneLabele()
        For Each stavka In imenikIzbor
            DirectCast(stavka.Value(0), Label).Text = ""
        Next
    End Sub

    Private Sub napuniGridStav_UlMat(lngId As Long)
        'Stavke za dokument na koji smo kliknuli. Ovo je isprogramirao Srdjan
        'lblNaslovStavke.Text = "Stavke prodaje: " ' & vratiJedVrednostUpit("SELECT Kon_Opis FROM Konta WHERE Kon_sifra = '" & strBroj & "' ")
        Dim strUpit1 As String
        '= "SELECT * " &
        '" FROM KalkMS3 " &
        '" INNER JOIN KontaR ON KontaR.Kor_Sifra = KalkMS3.Kms_Kor_Sifra AND KalkMS3.Kms_Kor_VrstaLookUp = KontaR.Kor_VrstaLookUp " &
        '" WHERE Kms_Kmz_id = " & lngId

        'strUpit1 = "SELECT * FROM EdiS, KontaR WHERE Eds_Edi_id  = " & lngId &
        '            " AND Kor_VrstaLookUp = Eds_Kor_VrstaLookUp AND Kor_Sifra = Eds_Kor_Sifra"
        strUpit1 = "SELECT * FROM KalkVS3, KontaR WHERE Kms_Kmz_id  = " & lngId &
                    " AND Kor_VrstaLookUp = Kms_Kor_VrstaLookUp AND Kor_Sifra = Kms_Kor_Sifra"


        Dim strOrder1 = " ORDER BY Eds_id "
        Dim intBrKol1 = 7
        Dim intSirineKolona1 = New Integer() {50, 50, 100, 100, 100, 100, 100}

        Dim strNazKol1 = New String() {"ID", "Rb", "Sifra", "Sifra", "Naziv artikla", "Količina", "Cena", "Ukupno Količina x Cena"}
        Dim strFildGrid1 = New String() {"Kms_id", "umesto rb", "Kms_Kor_Sifra", "Kor_Opis", "Kms_Kolicina", "Kms_CenaP", "Kms_VrednostP_PDV"}
        'inicijalizujem niz za izracunavanje sume
        Dim intSuma1 = New Integer() {0, 0, 0, 0, 0, 0, 1}
        'popuna i formatiranje header-a grida
        'modGrid.gridPrviRed(dgvStavke, intBrKol1, strNazKol1, intSirineKolona1, Me.Name & Me.dgvStavke.Name & "Width")

        'popuna grida
        'modGrid.napuniGrid(dgvStavke, strUpit1 & strOrder1, strFildGrid1, intBrKol1, intSuma1)
    End Sub

    Private Sub btnIzborFirma_Click_1(sender As Object, e As EventArgs) Handles btnIzborFirma.Click,
                                                                                btnIzborFirma1.Click,
                                                                                btnIzborFirma2.Click
        Select Case sender.name
            Case btnIzborFirma.Name
                modMain.kreirajFormuZaIzbor("SELECT Mto_Sifra, Mto_Naziv FROM MestOtk",
                                 New String() {"Šifra", "Naziv"},
                                 New Integer() {5, 25},
                                 "Izbor otkupnog mesta")
                If modMain.glbPrenesi <> "" Then
                    TextBox2.Text = modMain.glbPrenesi
                    modMain.glbPrenesi = ""
                    TextBox2.Focus()
                    'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
                    TextBox3.Focus()
                Else
                    TextBox2.Focus()
                End If
            Case btnIzborFirma1.Name
                modMain.kreirajFormuZaIzbor($"Select Kon_Sifra, Kon_Opis 
                            From Konta 
                            Left Join KontaR ON KontaR.Kor_id = Konta.Kon_Link_id 
                            WHERE Kon_Sifra Like '4358%' ORDER BY Kon_Opis",
                              New String() {"Šifra", "Naziv"},
                              New Integer() {5, 25},
                              "Izbor poljprivrednog privodžača")
                If modMain.glbPrenesi <> "" Then
                    TextBox3.Text = modMain.glbPrenesi
                    modMain.glbPrenesi = ""
                    TextBox3.Focus()
                    'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
                    TextBox4.Focus()
                Else
                    TextBox3.Focus()
                End If
            Case btnIzborFirma2.Name
                modMain.kreirajFormuZaIzbor("SELECT FkzU_Sifra, FkzU_Opis FROM FaktZUslovi",
                              New String() {"Šifra", "Naziv"},
                              New Integer() {5, 25},
                              "Izbor uslova prodaje")
                If modMain.glbPrenesi <> "" Then
                    TextBox4.Text = modMain.glbPrenesi
                    modMain.glbPrenesi = ""
                    TextBox4.Focus()
                    'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
                    TextBox5.Focus()
                Else
                    TextBox4.Focus()
                End If
        End Select

    End Sub
End Class
