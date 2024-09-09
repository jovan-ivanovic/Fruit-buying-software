Imports System
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Threading

'*************************** RAZLIKE I SLICNOSTI IZMEDJU FORME ZAGLAVLJA I OVE FORME STAVKI******************************************************

' 1. Formi zaglavlje primarni kljuc je uglavnom sifra, dok je formi stavki primarni kljuc je uvek id.
' 2. Forma stavke se sa formom zaglavlje povezuje preko id ili sifre zaglavlja(u zavisnosti sta je primarni kljuc),
'   a ta informacija stoji i promeljivoj strSifra
' 3. U formi stavke se u nulti TextBox upisuje polje koje se cuva u strSifra i onemogucen je pristup i menjanje prvog text boxa.
' 4. U formi Zaglavlje f-ja pronadjiSifru(sifra as Integer) se pozivala kada se napusti nulti TextBox u kome je sifra. F-ja je inicijalizovala intID
'    i textBoxove ukoliko se ispostavi da postoji upis sa trazenom sifrom, a koja je pozivala f-ju selektujRedUGridu(sifra as String)
'    koja selektuje red u gridu ako je upis pronadjen. Pri kliku na grid inicijalizovao se intID iz nulte kolone grida
'    i pozivala se f-ja pronadjiID() koja je popunjavala textBoxove za inicijalizovani intID. 
' 5. U formi Stavke ne postoji dogadjaj napustanja nultog TextBoxa(videti tacku 3.) pa samim tim ne postoje f-je pronadjiSifru(sifra as Integer) i
'    selektujRedUGridu(sifra as String),  a pri kliku na grid takodje se inicijalizuje id iz nulte kolone grida i poziva se f-ja pronadjiID(),
'    koja koja popunjava TextBoxova za inicijalizovani ID.
' 6. U obe forme su upis, citanje, izmena i brisanje podataka vrsi preko intID.
' 7. U formi Stavke su f-je za sredjivanje forme (srediFormuPocetno() i srediFormuPostojiSifra()) malo drugacije jer treba obezbediti da 
'    nulti TextBox bude onemogucen i inicijalizovan sa strSifra u svakom trenutku(videti tacku 3.). 
'    F-ja srediFormuNepostojiSifra() u formi Stavke ne postoji.
' 8. Forma Stavke je modalna sto znaci da se otvara na ShowDialog(), a da bi se oslobodila memorija mora se zatvoriti u dogadjaju
'    formClosing() na Me.Dispose(). Ne moze se pristupiti drugoj formi dok se forma stavke ne zatvori.

Public Class frmSifOtkMes

    Dim strUpit As String

    Dim strOrder As String
    Dim strNaslov As String

    Dim trenutniRed As DataGridViewRow 'red u gridu koji se azurira, kada kliknemo na grid ili kada program nadje da sifra postoji
    Dim intID As Long 'ID reda koji se azurira, kada kliknemo na grid ili kada program nadje da sifra postoji
    Dim strID As String 'naziv kolone u kojoj je ID

    Dim strTabela As String 'naziv tabele
    Dim intBrKol As Integer 'broj kolona u DataGridView komponenti(Ivan dao ime promenljivoj)
    Dim strNazKol(15) As String 'naziv kolona u DataGridView komponenti, prvo mora biti Rb a drugo ID
    Dim intSirineKolona() As Integer 'sirine kolona u DataGrieView komponenti(Ivan dodao promenljivu)
    Dim strFildGrid() As String 'naziv kolona iz tabele koje se ucitavaju u grid
    Dim strFild() As String 'naziv kolona u tabeli koje se azuriraju
    Dim intBrTexBoxova  'broj text boxova na formi
    Dim strLabelPoredText() As String 'nazivi labela pored text boxova
    Dim strSifra As String 'naziv kolone u kojoj je sifra

    Dim intSuma() As Integer ' niz za izracunavanje sume (1 ce biti u polju gde hocu da dobijem sumu)
    Dim strInic() As String 'inicijalne vrednosti textBoxova pri prvom unosu u bazu

    Dim boolLandscape As Boolean 'orijentacija stampanja

    Dim textBoxLista As New List(Of TextBox) 'lista textBoxova u groupBox-u 1
    Dim labelLista As New List(Of Label)     'lista labela u groupBox-u 1

    Dim valDatum() As TextBox 'niz TextBoxova koji primaju Date
    Dim valDecimal() As TextBox 'niz TextBoxova koji primaju Decimal(Currency in MS Access)
    Dim valInteger() As TextBox 'niz TextBoxova koji primaju Integer(Number in MS Access)

    Dim strLinkLok As String  'kopiram globalnu promeljivu za povezivanje sa formom zaglavlje


    '-------------- U svakoj formi stavki postoji Dokument i Broj iz tabele Zaglavlja --
    Dim strObjekat As String        'obično četvorocifrena sintetika iz državnog kontnog plana
    Dim strDokument As String
    Dim strBroj As String
    Dim strTmpDok As String    'za globalnu promenljivu sifra dokumenta. U FaktZ Fkz_Dok_Sifra u KalkVS3 Kmz_Dok_Sifra
    Dim strTmpBroj As String    'za globalnu promenljivu Broj dokumenta. U FaktZ Fkz_Broj u KalkVS3 Kmz_Broj

    Dim imenikIzbor As New Dictionary(Of TextBox, Object()) 'za kreiranje labele koja korisniku opisuje šifru

    'pravim tabelu koju ću čuvati u memoriji i neću svaki čas slati upit u bazu
    Dim kontaR_mem As DataTable = Nothing




    Protected Overridable Sub inicijalizujPromenljive()

        strNaslov = "Šifarnik otkupnih mesta "
        Me.Text = strNaslov
        Me.lblNaslov.Text = strNaslov



        strTabela = "MestOtk"
        intID = 0
        strID = "Mto_id"
        strSifra = "Mto_Sifra" 'ovde unosim polje za povezivanje sa tabelom zaglavlje(bilo da je to povezivanje preko id ili sifre)

        strUpit = $"SELECT * FROM MestOtk  "

        strOrder = " ORDER BY " & strID

        intBrKol = 5
        intSirineKolona = New Integer() {50, 50, 50, 100, 100}
        strNazKol = New String() {"ID", "Rb", "Sifra", "Naziv artikla", "Skraćeni naziv"}
        strFildGrid = New String() {strID, "umesto rb", "Mto_Sifra", "Mto_Naziv", "Mto_SkNaziv"}
        'strNazKol = New String() {"ID", "Rb", "Sifra", "Naziv artikla", "Knjigovodstveno stanje", "Popisno stanje", "Ulaz - povećanje", "Izlaz - smanjenje", "Cena", "Iznos"}
        'strFildGrid = New String() {strID, "umesto rb", "Eds_Kor_Sifra", "Kor_Opis", "Eds_Popust", "Eds_CenaPuna", "Eds_Ulaz", "Eds_Izlaz", "Eds_Cena", "Eds_Iznos"}

        intBrTexBoxova = 3
        strFild = New String() {"Mto_Sifra", "Mto_Naziv", "Mto_SkNaziv"}
        strLabelPoredText = New String() {"Šifra mesta troška", "Naziv mesta troška", "Skraćeni naziv"}
        strInic = New String() {"", "", ""}

        'inicijalizujem niz za izracunavanje sume
        intSuma = New Integer() {0, 0, 0, 0, 0}

        valDatum = New TextBox() {}
        valDecimal = New TextBox() {TextBox1, TextBox2}
        valInteger = New TextBox() {}

        'popuna i formatiranje header-a grida
        modGrid.gridPrviRed(dgvPrikaz, intBrKol, strNazKol, intSirineKolona, Me.Name & Me.dgvPrikaz.Name & "Width")

        'popuna grida
        modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)

        'primer popune imenika ukoliko ima dugmadi za izbor(ovde ih ima dva)
        'na mesto {0} ce doci vrednost TextBox-a koji je definisan kao ključ, a upit se moze nastaviti i imati vise uslova
        'npr. kada je potrebno Kor_VrstaLookUp kao drugi uslov(zbog toga sam i promenio ovo)
        imenikIzbor.Add(TextBox0, New Object() {lblOpis1, "SELECT Kor_Opis FROM KontaR WHERE Kor_VrstaLookUp = 'M' AND Kor_Sifra = '{0}'"})
        'imenikIzbor.Add(TextBox5, New Object() {lblOpis2, "SELECT Zap_Ime FROM Zaposleni WHERE Zap_Sifra = '{0}'"})
    End Sub

    Private Sub PatternForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'da bi forma prepoznala događaj klika na dugme(Enter umesto Tab)
            Me.KeyPreview = True
            inicijalizujPromenljive()
            lblStatus.ForeColor = Color.Black
            lblStatus.Text = "Možete upisati novi slog."

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
            Me.MinimumSize = New Size(800, 550)
        Catch ex As Exception
            MsgBox("Greška pri otvaranju forme " & Me.Text & " :" & ex.Message)
        End Try

    End Sub

    Private Sub btnUpisi_Click(sender As Object, e As EventArgs) Handles btnUpisi.Click
        'VAZNO!!! pri upisu novog sloga uzimaju se samo kolone u koje hoćemo da dodamo vrednost.
        'Samo tako će kolone u koje ne dodajemo vrednost dobiti DEFOULT vrednosti definisanu u bazi,
        'a i brže se izvršava kada se povlače samo potrebni podaci iz baze.
        Dim j As Integer
        Dim kolone As String = ""

        For j = 0 To strFild.Length - 1
            kolone = kolone & strFild(j) & " , "
        Next
        kolone = kolone.Remove(kolone.LastIndexOf(","))
        'ako se nešto upisuje u novu kolonu, a nije u TextBoxovima, nova kolona onda je ovde ispod treba dodati
        'VRLO VAŽNO!!! U stavkama se prvo dodaje kolona povezivanja sa formom zaglavlje, da bi sa automatski mogla upisati
        'kolone = kolone & " , " & strSifra
        kolone = kolone

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
            'može biti i M i MT odnosno piće i repro i onda ga nadjemo. Moglobi i bolje ali će raditi
            'Dim strVrstaLookUp As String
            'strVrstaLookUp = modOleDb.vratiJedVrednostUpit("SELECT Kor_VrstaLookUp FROM KontaR WHERE Kor_Sifra = '" & TextBox0.Text & "'  
            '                                             And (Kor_VrstaLookUp = 'M' OR Kor_VrstaLookUp = 'MT')  ")

            'tabeli dodeljujem red i update-ujem tabelu u bazi
            tabela.Rows.Add(red)
            adapter.Update(tabela)

            'podesavam vrednost status  labele
            lblStatus.ForeColor = Color.Red
            lblStatus.Text = "Slog je uspešno upisan. "
            modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
            inicijalizujTextBoxove(True)
            srediFormuPocetno()
        Catch ex As Exception
            MsgBox("Greška prilikom upisa podataka: " + ex.Message, 0, strNaslov)
        End Try
    End Sub

    Private Sub btnIzmeni_Click(sender As Object, e As EventArgs) Handles btnIzmeni.Click

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
            'red("Kor_Kor") = red("Kor_VrstaLookUp") & red("Kor_Sifra")
            'update-ujem tabelu u bazi
            adapter.Update(tabela)
            izmeniUGridu()
            'podesavam vrednost status  labele
            lblStatus.ForeColor = Color.Red
            lblStatus.Text = "Slog sa šifrom " & TextBox0.Text & " je uspešno izmenjen. "
            'modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol)
            inicijalizujTextBoxove(False)
            srediFormuPocetno()
            lblNaslov.Text = strNaslov
            Try
                trenutniRed.Selected = True
            Catch ex As Exception
            End Try
        Catch ex As Exception
            MsgBox("Greška prilikom izmene podataka: " + ex.Message, 0, strNaslov)
        End Try
    End Sub

    Private Sub btnBrisi_Click(sender As Object, e As EventArgs) Handles btnBrisi.Click
        Dim pozicija As Integer = trenutniRed.Index
        Dim strSQL As String = "DELETE  FROM " & strTabela & " WHERE " & strID & " = " & intID
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
        lblStatus.ForeColor = Color.Black
        lblStatus.Text = "Možete upisati novi slog."
        inicijalizujTextBoxove(True)
        srediFormuPocetno()
    End Sub

    Public Sub implementirajDesign()
        Me.BackColor = ColorTranslator.FromWin32(modDesign.bojaPozadineProzora)
    End Sub

    Private Sub podesiTextBoxove()

        'Ovde pravim objekte lista
        textBoxLista.Insert(0, TextBox0)
        textBoxLista.Insert(1, TextBox1)
        textBoxLista.Insert(2, TextBox2)
        'textBoxLista.Insert(3, TextBox3)
        'textBoxLista.Insert(4, TextBox4)
        'textBoxLista.Insert(5, TextBox5)
        'textBoxLista.Insert(6, TextBox6)
        'textBoxLista.Insert(7, TextBox7)

        Dim i As Integer = 0
        For i = 0 To intBrTexBoxova - 1
            'textBoxLista(i).BackColor = ColorTranslator.FromWin32(modDesign.bojaTb) 'podesavam izabranu boju txtBoxa
        Next

        labelLista.Insert(0, Label0)
        labelLista.Insert(1, Label1)
        labelLista.Insert(2, Label2)
        'labelLista.Insert(3, Label3)
        'labelLista.Insert(4, Label4)
        'labelLista.Insert(5, Label5)
        'labelLista.Insert(6, Label6)
        'labelLista.Insert(7, Label7)

        'ovo je u slucaju da ima isti broj text boxova i labela, ako ih nema isto onda će morati nekako drugačije
        For i = 0 To intBrTexBoxova - 1
            labelLista(i).Text = strLabelPoredText(i) & tackice 'text komponenta labele
            labelLista(i).Location = New Point(labelLista(i).Location.X, textBoxLista(i).Location.Y + textBoxLista(i).Height / 2 - 2)
        Next

    End Sub

    Private Sub srediFormuPocetno()
        TextBox0.Text = modMain.vratiSledecuSifru(strTabela, strSifra)

        'prvo pokušavam da stavim Fokus a ako to ne može onda koristim Select
        If TextBox0.CanFocus Then
            TextBox0.Focus()
        Else
            TextBox0.Select()
        End If

        isprazniSveOpisneLabele()
        'u gridu ništa ne treba da bude selektovano
        Me.dgvPrikaz.ClearSelection()

        btnUpisi.Enabled = True
        btnIzmeni.Enabled = False
        btnBrisi.Enabled = False
        btnIsprazniPolja.Enabled = True
    End Sub

    Private Sub srediFormuPostojiSifra()
        TextBox0.Select()

        popuniSveOpisneLabele()

        btnUpisi.Enabled = False
        btnIzmeni.Enabled = True
        btnBrisi.Enabled = True
        btnIsprazniPolja.Enabled = True
    End Sub

    ''' <summary>
    ''' f-ja inicijalizuje TextBox-ove vrednostima iz niza
    ''' </summary>
    ''' <param name="sacuvajTrenVr">da li čuva trenutne vrednosti pre inicijalizacije(kod f-je Upisi)</param>
    Private Sub inicijalizujTextBoxove(sacuvajTrenVr As Boolean)
        Dim i As Integer
        'If sacuvajTrenVr Then
        '    'slučaj da čuva trenutne vrednosti, trenutne vrednosti se prepisuju u strInic gde je strInic različito od praznog stringa.
        '    For i = 0 To intBrTexBoxova - 1
        '        If strInic(i) <> "" Then
        '            strInic(i) = textBoxLista(i).Text
        '        End If
        '    Next
        'End If

        'u svakom slucaju se vrsi inicijalizacija textBoxova iz strInic niza, u formi zaglavlje pocinje od 1, ali o formi stavke od 0(U STAVKAMA NEMA ŠIFRA)
        For i = 0 To intBrTexBoxova - 1
            textBoxLista(i).Text = strInic(i)
        Next
    End Sub

#Region "dgvPrikaz.Click"

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
                srediFormuPostojiSifra() 'cim je red selektovan u bazi postoji upis, nema potrebe za proverom
            Else
                lblStatus.ForeColor = Color.Black
                lblStatus.Text = "Možete upisati novi slog."
                inicijalizujTextBoxove(False)
                srediFormuPocetno()
            End If
        End If
    End Sub

    Private Function pronadjiID() As Boolean
        Dim i As Integer
        Dim strSQL As String = "SELECT * FROM " & strTabela & " WHERE " & strID & " = " & intID
        Dim command As New OleDbCommand(strSQL, adoCN)
        Dim reader As OleDbDataReader = Nothing
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
                'ako je upis u TextBoxove uspesan, menjam text u status bar-u
                lblStatus.ForeColor = Color.Black
                lblStatus.Text = "Slog je selektovan. Mozete ga izmeniti i brisati. "
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


    Private Sub selektujText(sender As Object, e As EventArgs)
        BeginInvoke(DirectCast(Sub() sender.SelectAll(), Action))
    End Sub

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

        'svaki TextBox koji je naveden u nizu valDecimal dodeljujem događaju validacijaDecimal koja se nalazi u modValidacija
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

    Private Sub Pattern_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'dimenzije i lokacija prozora se upisuju u bazu samo ukoliko prozor nije minimiziran
        If Me.WindowState <> FormWindowState.Minimized Then
            'upisuje se u bazu trenutna pozicija i velicina forme 
            'modForma.upisiPozicijuUBazu(Me, boolLandscape)
        End If
        'upisuje se u bazu sirina kolona u gridu
        modGrid.Set_Grid_Width(Me.dgvPrikaz, Me.Name & Me.dgvPrikaz.Name & "Width")
        'If dgvStavke.RowCount <> 0 Then modGrid.Set_Grid_Width(Me.dgvStavke, Me.Name & "_mali_grid")
        Me.Dispose()
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

    Private Sub PolozajToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PolozajToolStripMenuItem.Click
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

    Private Sub Form_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub izmeniUGridu()
        Dim reader As OleDbDataReader = Nothing
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


    Private Sub OtvorenIzborDatuma(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.DropDown
        'Dim picker = DirectCast(sender, DateTimePicker)

        'Select Case picker.Name
        '    Case DateTimePicker1.Name
        '        If modValidacija.proveriDatum(TextBox1.Text) Then
        '            DateTimePicker1.Value = Convert.ToDateTime(TextBox1.Text)
        '        End If
        'End Select
    End Sub

    Private Sub ZatvorenIzbotDatuma(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        'Dim picker = DirectCast(sender, DateTimePicker)

        'Select Case picker.Name
        '    Case DateTimePicker1.Name 'izabran je prvi DateAndTime Picker za izbor datuma izrade
        '        TextBox1.Text = DateTimePicker1.Value.Date()
        '        TextBox1.Focus()
        'End Select
    End Sub

    Private Sub btnIzborFirma_Click(sender As Object, e As EventArgs) Handles btnIzborFirma.Click
        'modMain.kreirajFormuZaIzbor_DataTableV2_sir_vis(kontaR_mem,
        '                                                New Integer() {15, 60, 30, 30, 25, 12},
        '                                                "Izbor artikla", TextBox0.Text, 600, 500)
        'If modMain.glbPrenesi <> "" Then
        '    TextBox0.Text = modMain.glbPrenesi
        '    modMain.glbPrenesi = ""
        '    TextBox0.Focus()
        '    'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
        '    TextBox1.Focus()
        'Else
        '    TextBox0.Focus()
        'End If

    End Sub

    Private Sub btnNovaFirma_Click(sender As Object, e As EventArgs) Handles btnNovaFirma.Click
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



End Class