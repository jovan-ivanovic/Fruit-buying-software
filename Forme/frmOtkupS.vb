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

Public Class frmOtkupS

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

        strObjekat = modOleDb.vratiJedVrednostUpit(String.Format("SELECT Kmz_Kon_132 FROM KalkVZ3 WHERE Kmz_id = {0}", strLinkLok))
        strTmpDok = modOleDb.vratiJedVrednostUpit(String.Format("SELECT Kmz_Dok_Sifra FROM KalkVZ3 WHERE Kmz_id = {0}", strLinkLok))
        strTmpBroj = modOleDb.vratiJedVrednostUpit(String.Format("SELECT Kmz_Broj FROM KalkVZ3 WHERE Kmz_id = {0}", strLinkLok))

        'strNaslov = "Stavke prodaje " & " Dokument: " & strTmpDok & " Broj: " & strTmpBroj

        Me.Text = "Stavke: dodavanje/izmena/brisanje"

        'Dim insDokument As New clsDokumenta
        strNaslov = $"Objekat: {strObjekat} -  Dokument:  {strTmpDok}  -  Broj: {strTmpBroj}"
        Me.lblNaslov.Text = strNaslov



        strTabela = "KalkVS3"
        intID = 0
        strID = "Kms_id"
        strSifra = "Kms_Kmz_id" 'ovde unosim polje za povezivanje sa tabelom zaglavlje(bilo da je to povezivanje preko id ili sifre)

        strUpit = "SELECT * FROM ((KalkVS3
                            LEFT JOIN Konta ON Konta.Kon_Sifra = KalkVS3.Kms_Kon_1320)
                            LEFT JOIN KontaR ON KontaR.Kor_Sifra = KalkVS3.Kms_Kor_Sifra)
                             WHERE Kms_Kmz_id = " & strLinkLok &
                    " AND Kor_VrstaLookUp = Kms_Kor_VrstaLookUp AND Kor_Sifra = Kms_Kor_Sifra"




        strOrder = " ORDER BY " & strID

        intBrKol = 15
        intSirineKolona = New Integer() {50, 50, 100, 100, 100, 100, 50, 100, 100, 100, 100, 100, 100, 100, 100}
        strNazKol = New String() {"ID", "Rb", "Sifra", "Naziv", "JM", "Količina", "Cena bez pdv-a", "Vrednost bez PDV-a", "PDV %", "Iznos PDV-a", "Nabavna vrednost", "Ukupno", "Cena sa PDV-om", "Razdužn. ambal.", "Zadužn. ambal."}
        strFildGrid = New String() {strID, "umesto rb", "Kor_Sifra", "Kor_Opis", "Kor_jm", "Kms_Kolicina", "Kms_CenaN", "Kms_VrednostN", "Kms_Pr_282", "Kms_Vr_282", "Kms_VrednostN", "Kms_VrednostP_PDV", "Kms_CenaP_PDV", "Kms_Kom_Amb_Vr", "Kms_Kom_Amb_Pr"}
        'strNazKol = New String() {"ID", "Rb", "Sifra", "Naziv artikla", "Knjigovodstveno stanje", "Popisno stanje", "Ulaz - povećanje", "Izlaz - smanjenje", "Cena", "Iznos"}
        'strFildGrid = New String() {strID, "umesto rb", "Eds_Kor_Sifra", "Kor_Opis", "Eds_Popust", "Eds_CenaPuna", "Eds_Ulaz", "Eds_Izlaz", "Eds_Cena", "Eds_Iznos"}

        intBrTexBoxova = 9
        strFild = New String() {"Kms_Kor_Sifra", "Kms_Sif_Amb", "Kms_Bruto", "Kms_Kom_Amb", "Kms_JedTez_Amb", "Kms_Kolicina", "Kms_CenaP", "Kms_Kom_Amb_Vr", "Kms_Kom_Amb_Pr"}
        strLabelPoredText = New String() {"Šifra artikla", "Ambalaža", "Bruto količina", "Komada ambalaže", "Jedinična težina", "Neto količina", "Fakturna cena sa PDV-om", "Razduženo", "Zaduženo"}
        strInic = New String() {"", "", "0", "0", "0", "0", "0", "0", "0"}

        'inicijalizujem niz za izracunavanje sume
        intSuma = New Integer() {0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1}

        valDatum = New TextBox() {}
        valDecimal = New TextBox() {}
        valInteger = New TextBox() {}

        'popuna i formatiranje header-a grida
        modGrid.gridPrviRed(dgvPrikaz, intBrKol, strNazKol, intSirineKolona, Me.Name & Me.dgvPrikaz.Name & "Width")

        'popuna grida
        modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)

        'primer popune imenika ukoliko ima dugmadi za izbor(ovde ih ima dva)
        'na mesto {0} ce doci vrednost TextBox-a koji je definisan kao ključ, a upit se moze nastaviti i imati vise uslova
        'npr. kada je potrebno Kor_VrstaLookUp kao drugi uslov(zbog toga sam i promenio ovo)
        'imenikIzbor.Add(TextBox0, New Object() {lblOpis1, "SELECT Kor_Opis FROM KontaR WHERE Kor_VrstaLookUp = 'M' AND Kor_Sifra = '{0}'"})
        'imenikIzbor.Add(TextBox5, New Object() {lblOpis2, "SELECT Zap_Ime FROM Zaposleni WHERE Zap_Sifra = '{0}'"})
    End Sub

    Private Sub PatternForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'da bi forma prepoznala događaj klika na dugme(Enter umesto Tab)
            Me.KeyPreview = True

            strLinkLok = modMain.strLinkPub
            strLinkPub = ""
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
            'If Not napuniKontaRMemorija() Then
            '    Dim msg As New MojMsgBox("Tabela sa artiklima nije učitana, proverite da li je tabelu otvorio neki drugi korisnik. " & vbCrLf &
            '           "Proverite da li vaša mreža radi ispravno " & vbCrLf &
            '           " 1. da li je uključen računar na kome se nalazi baza " & vbCrLf &
            '           " 2. da li su mrezni kablovi dobro utakniti." & vbCrLf &
            '           "Nakon toga pokrenite ponovo program.", MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
            '    msg.ShowDialog()
            '    Me.Close()
            'End If
        Catch ex As Exception
            MsgBox("Greška pri otvaranju forme " & Me.Text & " :" & ex.Message)
        End Try

    End Sub

    Private Sub btnUpisi_Click(sender As Object, e As EventArgs) Handles btnUpisi.Click
        'Vršimo proveru da li je knjiženo


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
        kolone = kolone & " , " & strSifra
        kolone = kolone & " , Kms_Kon_282,Kms_VrednostN,Kms_Pr_282,Kms_Iz_282,Kms_Vr_282,Kms_VrednostP_PDV, Kms_CenaP_PDV,Kms_Kor_VrstaLookUp, Kms_Kor_Kor,Kms_Kon_1320,Kms_VrednostP,Kms_CenaN,Kms_VrednostN2  "

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
            'VRLO BITNO!!! dodavanje polja povezivanja u tabelu stavke, ranije je bio prvi TextBox koji je bio onemogućen
            red(strSifra) = strLinkLok
            'može biti i M i MT odnosno piće i repro i onda ga nadjemo. Moglobi i bolje ali će raditi
            Dim strVrstaLookUp As String
            strVrstaLookUp = modOleDb.vratiJedVrednostUpit("SELECT Kor_VrstaLookUp FROM KontaR WHERE Kor_Sifra = '" & TextBox0.Text & "'  
                                                         And (Kor_VrstaLookUp = 'M' OR Kor_VrstaLookUp = 'MT')  ")
            'iznos rabata po jedinici proizvoda
            'red("Eds_Kon_1340") = red("Eds_Kor_Sifra")
            'red("Eds_Kolicina") = red("Eds_Ulaz") + red("Eds_Izlaz")    'Jedno je 0 pa je kolicina jednaka jednom od Izla ili Ulaz
            'red("Eds_Kolicina") = red("Eds_Izlaz")    'Jedno je 0 pa je kolicina jednaka jednom od Izla ili Ulaz
            red("Kms_VrednostP") = red("Kms_CenaP") * red("Kms_Kolicina")
            red("Kms_VrednostP_PDV") = Round(red("Kms_CenaP") * red("Kms_Kolicina"), 2)
            red("Kms_Kor_VrstaLookUp") = strVrstaLookUp ' "M"
            red("Kms_Kor_Kor") = red("Kms_Kor_VrstaLookUp") & red("Kms_Kor_Sifra")
            'red("Kms_Kon_1320") = "1311"
            red("Kms_Kon_282") = "2780"

            Dim pdvTarifa As String = modOleDb.vratiJedVrednostUpit($"SELECT Kor_Tar_Sifra FROM KontaR WHERE Kor_Sifra = '{TextBox0.Text}'  
            AND (Kor_VrstaLookUp = 'M' OR Kor_VrstaLookUp = 'MT')  ")
            red("Kms_Pr_282") = pdvTarifa


            red("Kms_CenaN") = Math.Round(red("Kms_CenaP") / (1 + (red("Kms_Pr_282") / 100)), 2)
            red("Kms_VrednostN") = red("Kms_CenaN") * red("Kms_Kolicina")

            red("Kms_Iz_282") = (red("Kms_CenaP") * red("Kms_Pr_282")) / (100 + red("Kms_Pr_282"))
            'Dim iznosPDVPoJedinici As Double = (prodajnaCena * pdvProcenat) / (100 + pdvProcenat)
            'red("Kms_CenaN") = red("Kms_CenaP") * red("Kms_Pr_282") / 100

            red("Kms_Vr_282") = red("Kms_Kolicina") * red("Kms_Iz_282")
            red("Kms_VrednostN2") = red("Kms_CenaN") * red("Kms_Kolicina") '+ red("Kms_Zav_Ukupno")
            red("Kms_CenaP_PDV") = red("Kms_CenaP")
            ' red("Kms_VrednostP") = red("Kms_CenaN") * red("Kms_Kolicina")
            'red("Kms_VrednostP_PDV") = red("Kms_CenaP") * red("Kms_Kolicina")
            'red("Kms_VrednostP") = red("Kms_CenaP") * red("Kms_Kolicina")
            'If pdvTarifa = "1" Then
            '    red("Eds_PDVStopa") = "20"
            'ElseIf pdvTarifa = "2" Then
            '    red("Eds_PDVStopa") = "10"
            'ElseIf pdvTarifa = "0" Then
            '    red("Eds_PDVStopa") = "0"
            'ElseIf pdvTarifa = 
            'End If
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
            'vrednosti iz textBoxova se upisujem u red
            Dim i As Integer
            For i = 0 To intBrTexBoxova - 1
                'vrednosti iz textBoxova se upisujem u red, validacija datuma se izvršava u prvoj liniji tako da ovde više nije potrebna
                red(strFild(i)) = textBoxLista(i).Text
            Next
            'VRLO BITNO!!! dodavanje polja povezivanja u tabelu stavke, ranije je bio prvi TextBox koji je bio onemogućen
            red(strSifra) = strLinkLok
            'može biti i M i MT odnosno piće i repro i onda ga nadjemo. Moglobi i bolje ali će raditi
            Dim strVrstaLookUp As String
            strVrstaLookUp = modOleDb.vratiJedVrednostUpit("SELECT Kor_VrstaLookUp FROM KontaR WHERE Kor_Sifra = '" & TextBox0.Text & "'  
                                                         And (Kor_VrstaLookUp = 'M' OR Kor_VrstaLookUp = 'MT')  ")
            red("Kms_VrednostP") = red("Kms_CenaP") * red("Kms_Kolicina")
            red("Kms_VrednostP_PDV") = Round(red("Kms_CenaP") * red("Kms_Kolicina"), 2)
            red("Kms_Kor_VrstaLookUp") = strVrstaLookUp ' "M"
            red("Kms_Kor_Kor") = red("Kms_Kor_VrstaLookUp") & red("Kms_Kor_Sifra")
            'red("Kms_Kon_1320") = "1311"
            red("Kms_Kon_282") = "2780"

            Dim pdvTarifa As String = modOleDb.vratiJedVrednostUpit($"SELECT Kor_Tar_Sifra FROM KontaR WHERE Kor_Sifra = '{TextBox0.Text}'  
            AND (Kor_VrstaLookUp = 'M' OR Kor_VrstaLookUp = 'MT')  ")
            red("Kms_Pr_282") = pdvTarifa


            red("Kms_CenaN") = Math.Round(red("Kms_CenaP") / (1 + (red("Kms_Pr_282") / 100)), 2)
            red("Kms_VrednostN") = red("Kms_CenaN") * red("Kms_Kolicina")

            red("Kms_Iz_282") = (red("Kms_CenaP") * red("Kms_Pr_282")) / (100 + red("Kms_Pr_282"))
            'Dim iznosPDVPoJedinici As Double = (prodajnaCena * pdvProcenat) / (100 + pdvProcenat)
            'red("Kms_CenaN") = red("Kms_CenaP") * red("Kms_Pr_282") / 100

            red("Kms_Vr_282") = red("Kms_Kolicina") * red("Kms_Iz_282")
            red("Kms_VrednostN2") = red("Kms_CenaN") * red("Kms_Kolicina") '+ red("Kms_Zav_Ukupno")
            red("Kms_CenaP_PDV") = red("Kms_CenaP")
            ' red("Kms_VrednostP") = red("Kms_CenaN") * red("Kms_Kolicina")
            'red("Kms_VrednostP_PDV") = red("Kms_CenaP") * red("Kms_Kolicina")
            'red("Kms_VrednostP") = red("Kms_CenaP") * red("Kms_Kolicina")
            'If pdvTarifa = "1" Then
            '    red("Eds_PDVStopa") = "20"
            'ElseIf pdvTarifa = "2" Then
            '    red("Eds_PDVStopa") = "10"
            'ElseIf pdvTarifa = "0" Then
            '    red("Eds_PDVStopa") = "0"
            'ElseIf pdvTarifa = 
            'End If
            adapter.Update(tabela)

            izmeniUGridu()
            'podesavam vrednost status  labele
            lblStatus.ForeColor = Color.Red
            lblStatus.Text = "Slog je uspešno izmenjen. "
            'modGrid.napuniGrid(dgvPrikaz, strUpit, strFildGrid, intBrKol, intSuma)
            inicijalizujTextBoxove(False)
            srediFormuPocetno()
            trenutniRed.Selected = True
            modGrid.azurirajSumeGrida(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
            'dgvPrikaz.Refresh()
        Catch ex As Exception
            MsgBox("Greška prilikom izmene podataka: " + ex.Message, 0, strNaslov)
        End Try
    End Sub

    Private Sub btnBrisi_Click(sender As Object, e As EventArgs) Handles btnBrisi.Click
        'Vršimo proveru da li je knjiženo

        Dim pozicija As Integer = trenutniRed.Index
        Dim strSQL As String = "DELETE FROM " & strTabela & " WHERE " & strID & " = " & intID
        Try
            'poziva se f-ja koja izvršava komandu, i mora da vrati 1, što znači da je jedan slog izbrisan iz baze
            If modOleDb.izvrsiKomandu(strSQL) = 1 Then
                'podesavam vrednost status  labele
                lblStatus.ForeColor = Color.Red
                lblStatus.Text = "Slog je uspešno izbrisan. "
                'ne ucitavam ponovo grid vec brisem iz grida postojeci red
                Me.dgvPrikaz.Rows.Remove(trenutniRed)
                'modGrid.napuniGrid(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
                inicijalizujTextBoxove(False)
                srediFormuPocetno()
                'selektujem red iznad izbrisanog reda ili ispod ukoliko brisem prvi red
                If dgvPrikaz.Rows.Count > 0 Then
                    If pozicija > 0 Then
                        dgvPrikaz.Rows.Item(pozicija - 1).Selected = True
                    Else
                        dgvPrikaz.Rows.Item(pozicija).Selected = True
                    End If
                End If
            End If
            modGrid.azurirajSumeGrida(dgvPrikaz, strUpit & strOrder, strFildGrid, intBrKol, intSuma)
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
        'Me.BackColor = ColorTranslator.FromWin32(modDesign.bojaPozadineProzora)
    End Sub

    Private Sub podesiTextBoxove()

        'Ovde pravim objekte lista
        textBoxLista.Insert(0, TextBox0)
        textBoxLista.Insert(1, TextBox1)
        textBoxLista.Insert(2, TextBox2)
        textBoxLista.Insert(3, TextBox3)
        textBoxLista.Insert(4, TextBox4)
        textBoxLista.Insert(5, TextBox5)
        textBoxLista.Insert(6, TextBox6)
        textBoxLista.Insert(7, TextBox7)
        textBoxLista.Insert(8, TextBox8)

        Dim i As Integer = 0
        For i = 0 To intBrTexBoxova - 1
            'textBoxLista(i).BackColor = ColorTranslator.FromWin32(modDesign.bojaTb) 'podesavam izabranu boju txtBoxa
        Next

        labelLista.Insert(0, Label0)
        labelLista.Insert(1, Label1)
        labelLista.Insert(2, Label2)
        labelLista.Insert(3, Label3)
        labelLista.Insert(4, Label4)
        labelLista.Insert(5, Label5)
        labelLista.Insert(6, Label6)
        labelLista.Insert(7, Label7)
        labelLista.Insert(8, Label8)


        'ovo je u slucaju da ima isti broj text boxova i labela, ako ih nema isto onda će morati nekako drugačije
        For i = 0 To intBrTexBoxova - 1
            labelLista(i).Text = strLabelPoredText(i) & tackice 'text komponenta labele
            labelLista(i).Location = New Point(labelLista(i).Location.X, textBoxLista(i).Location.Y + textBoxLista(i).Height / 2 - 2)
        Next

    End Sub

    Private Sub srediFormuPocetno()
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

        Next

        'svaki TextBox koji je naveden u nizu valInteger dodeljujem događaju validacijaInteger i dodajNulu koje se nalaze u modValidacija
        For Each tb In valInteger

        Next

    End Sub

    Private Sub ulazniDogadjaj(sender As Object, e As EventArgs) 'Handles TextBox1.Enter, TextBox2.Enter, cmbPretrazi.Enter
        'DirectCast(sender, Control).BackColor = ColorTranslator.FromWin32(modDesign.bojaAktivnogTb)
    End Sub

    Private Sub izlazniDogadjaj(sender As Object, e As EventArgs) 'Handles TextBox1.Leave, TextBox2.Leave, cmbPretrazi.Leave
        'DirectCast(sender, Control).BackColor = ColorTranslator.FromWin32(modDesign.bojaTb)
    End Sub


    'KADA BUDEM SKONTAO KAKO DA UPISUJEM SIRINE U BAZU 
    Private Sub Pattern_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'dimenzije i lokacija prozora se upisuju u bazu samo ukoliko prozor nije minimiziran
        'If Me.WindowState <> FormWindowState.Minimized Then
        '    'upisuje se u bazu trenutna pozicija i velicina forme 
        '    modForma.upisiPozicijuUBazu(Me, boolLandscape)
        'End If
        ''upisuje se u bazu sirina kolona u gridu
        'modGrid.Set_Grid_Width(Me.dgvPrikaz, Me.Name & Me.dgvPrikaz.Name & "Width")
        'If dgvStavke.RowCount <> 0 Then modGrid.Set_Grid_Width(Me.dgvStavke, Me.Name & "_mali_grid")
        'Me.Dispose()
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
        Dim strSQL As String = strUpit & " AND " & strID & " = " & intID
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

    Private Sub btnIzborFirma_Click(sender As Object, e As EventArgs) Handles btnIzborFirma.Click,
                                                                              btnIzborFirma1.Click
        Select Case sender.name
            Case btnIzborFirma.Name
                modMain.kreirajFormuZaIzbor("SELECT Kor_Sifra, Kor_Opis FROM KontaR WHERE Kor_Vrsta='MT'",
                                 New String() {"Šifra", "Naziv"},
                                 New Integer() {5, 25},
                                 "Izbor artikla")
                If modMain.glbPrenesi <> "" Then
                    TextBox0.Text = modMain.glbPrenesi
                    modMain.glbPrenesi = ""
                    TextBox0.Focus()
                    'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
                    TextBox1.Focus()
                Else
                    TextBox0.Focus()
                End If
            Case btnIzborFirma1.Name
                modMain.kreirajFormuZaIzbor("SELECT Kor_Sifra, Kor_Opis FROM KontaR WHERE Kor_Vrsta='AB'",
                        New String() {"Šifra", "Naziv"},
                        New Integer() {5, 25},
                        "Izbor artikla")
                If modMain.glbPrenesi <> "" Then
                    TextBox1.Text = modMain.glbPrenesi
                    modMain.glbPrenesi = ""
                    TextBox1.Focus()
                    'ovde se pali događaj TextBox.Leave koji mi je neophodan da bi se inicijalizovala opisna labela pored
                    TextBox2.Focus()
                Else
                    TextBox1.Focus()
                End If
        End Select



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


    Private Sub TextBox0_LostFocus(sender As Object, e As EventArgs) Handles TextBox0.LostFocus
        Dim strVrstaLookUp As String
        strVrstaLookUp = modOleDb.vratiJedVrednostUpit("SELECT Kor_VrstaLookUp FROM KontaR WHERE Kor_Sifra = '" & TextBox0.Text & "'  
                                                         And (Kor_VrstaLookUp = 'M' OR Kor_VrstaLookUp = 'MT')  ")
        TextBox6.Text = modArtikal.vratiCenuRobe(TextBox0.Text, strVrstaLookUp)
        'TextBox2.Text = modArtikal.vratiCenuRobe(TextBox0.Text, strVrstaLookUp)
        'Dim pdv As String = modArtikal.vratiPDVStopu(TextBox0.Text, strVrstaLookUp)
        'Dim stopaPreracunata As Decimal = modPDV.prerStopaPoreza(pdv)
        'TextBox3.Text = Math.Round(TextBox2.Text * (1 - stopaPreracunata / 100), 7)


        napuniGrid_Kartica(Me.dgvStavke, TextBox0.Text, strVrstaLookUp, Today.Date())
    End Sub



    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        Dim strVrstaLookUp As String
        strVrstaLookUp = modOleDb.vratiJedVrednostUpit("SELECT Kor_VrstaLookUp FROM KontaR WHERE Kor_Sifra = '" & TextBox1.Text & "'  
                                                         And (Kor_VrstaLookUp = 'AB' OR Kor_VrstaLookUp = 'AB')  ")
        TextBox4.Text = modArtikal.vratiTezGajbe(TextBox1.Text, strVrstaLookUp)
        'Dim razlika As Decimal = TextBox4.Text - TextBox5.Text
        'If razlika > 0 Then
        '    TextBox3.Text = "0"
        '    TextBox1.Text = razlika
        'ElseIf razlika < 0 Then
        '    TextBox3.Text = "0"
        '    TextBox1.Text = razlika
        'End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        'Dim strVrstaLookUp As String
        'strVrstaLookUp = modOleDb.vratiJedVrednostUpit("SELECT Kor_VrstaLookUp FROM KontaR WHERE Kor_Sifra = '" & TextBox1.Text & "'  
        '                                                 And (Kor_VrstaLookUp = 'AB' OR Kor_VrstaLookUp = 'AB')  ")
        'TextBox4.Text = modArtikal.vratiCenuRobe(TextBox1.Text, strVrstaLookUp)
        Dim netokol As Decimal = TextBox2.Text - (TextBox3.Text * TextBox4.Text)
        TextBox5.Text = netokol
        'If razlika > 0 Then
        '    TextBox3.Text = "0"
        '    TextBox1.Text = razlika
        'ElseIf razlika < 0 Then
        '    TextBox3.Text = "0"
        '    TextBox1.Text = razlika
        'End If
    End Sub









    'Private Sub TextBox5_Leave(sender As Object, e As EventArgs) Handles TextBox5.Leave
    '    Dim razlika As Decimal = TextBox4.Text - TextBox5.Text
    '    If razlika > 0 Then
    '        TextBox3.Text = "0"
    '        TextBox1.Text = razlika
    '    ElseIf razlika < 0 Then
    '        TextBox3.Text = "0"
    '        TextBox1.Text = razlika
    '    End If
    'End Sub


    Private Sub napuniGrid_Kartica(maliGrid As DataGridView, sifraProizvoda As String, lookUpProizvoda As String, doDatuma As Date)

        'Dim strSQL As String = String.Format("SELECT d.Dok_Sifra AS Dokument, sr1.Stv_Datum AS Datum,  n.Nlg_Broj AS Broj, 
        '                                            d.Dok_Opis AS [Naziv dokumenta], sr1.Stv_Ulaz AS Ulaz, sr1.Stv_Izlaz AS Izlaz
        '                                            FROM (((StavkeR AS sr1 
        '                                            INNER JOIN KontaR kr ON sr1.Stv_Kor_VrstaLookUp = kr.Kor_VrstaLookUp AND sr1.Stv_Kon_Sifra = kr.Kor_Sifra)
        '                                            INNER JOIN Nalozi n ON sr1.Stv_Nlg_id = n.Nlg_id)
        '                                            INNER JOIN Dokumenta d ON n.Nlg_Dok_Sifra = d.Dok_Sifra)
        '                                            WHERE sr1.Stv_DATUM <= {0} AND  kr.Kor_Sifra = '{1}'  AND kr.Kor_VrstaLookUp = '{2}' 
        '                                            ORDER BY kr.Kor_Kor, sr1.Stv_datum, sr1.Stv_id ", datumZaUpit(doDatuma), sifraProizvoda, lookUpProizvoda)

        'Dim tabela As DataTable = modOleDb.napuniTabelu(strSQL)

        ''[VERZIJA 1], ukoliko ne radi zakomentarišite i odkomentarišite [VERZIJU 2]
        ''tabela.Columns.Add("Stanje", GetType(Decimal), "SUM(Ulaz) - SUM(Izlaz)")
        ''modOleDb.dodajKolonuRB(tabela, 0)

        ''[VERZIJA 2]
        'tabela.Columns.Add("Stanje", GetType(Decimal))
        'Dim temp As Decimal = 0
        'For Each r As DataRow In tabela.Rows
        '    temp = temp + r("Ulaz") - r("Izlaz")
        '    r("Stanje") = temp
        'Next
        'modOleDb.dodajKolonuRB(tabela, 0)


        'lblNaslovStavke.Text = "Kartica artikla: " & modArtikal.vratiNazivArtikaIzKontaR(sifraProizvoda, "M") & " do datuma " & doDatuma

        'Dim sirKolona = New Integer() {100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        ''poziva se f-ja koja čita širine kolona iz baze i ukoliko postoje menja širine kolona poslate f-ji
        'citajSirineKolona(Me.Name & "_mali_grid", 12, sirKolona)

        'maliGrid.DataSource = modOleDb.zameniNuleSaNull(modOleDb.izracunajSumu(tabela, New Integer() {0, 0, 0, 0, 0, 1, 1, 0}))
        'modGrid.formatirajGridDataTable(maliGrid, sirKolona)



        'Exit Sub


        'Stavke za dokument na koji smo kliknuli. Ovo je isprogramirao Srdjan

        'Dim strSQL As String
        'strSQL = String.Format("SELECT d.Dok_Sifra AS Dokument, sr1.Stv_Datum AS Datum,  n.Nlg_Broj AS Broj, 
        '                                            d.Dok_Opis AS [Naziv dokumenta], iif(sr1.Stv_Ulaz=0, Null,sr1.Stv_Ulaz) AS Ulaz, sr1.Stv_Izlaz AS Izlaz
        '                                            FROM (((StavkeR AS sr1 
        '                                            INNER JOIN KontaR kr ON sr1.Stv_Kor_VrstaLookUp = kr.Kor_VrstaLookUp AND sr1.Stv_Kon_Sifra = kr.Kor_Sifra)
        '                                            INNER JOIN Nalozi n ON sr1.Stv_Nlg_id = n.Nlg_id)
        '                                            INNER JOIN Dokumenta d ON n.Nlg_Dok_Sifra = d.Dok_Sifra)
        '                                            WHERE sr1.Stv_DATUM <= {0} AND  kr.Kor_Sifra = '{1}'  AND kr.Kor_VrstaLookUp = '{2}' 
        '                                            ORDER BY kr.Kor_Kor, sr1.Stv_datum, sr1.Stv_id ", datumZaUpit(doDatuma), sifraProizvoda, lookUpProizvoda)

        'Dim tabela As DataTable = modOleDb.napuniTabelu(strSQL)

        ''[VERZIJA 1], ukoliko ne radi zakomentarišite i odkomentarišite [VERZIJU 2]
        ''tabela.Columns.Add("Stanje", GetType(Decimal), "SUM(Ulaz) - SUM(Izlaz)")
        ''modOleDb.dodajKolonuRB(tabela, 0)

        ''[VERZIJA 2]
        'tabela.Columns.Add("Stanje", GetType(Decimal))
        'Dim temp As Decimal = 0
        'For Each r As DataRow In tabela.Rows
        '    'temp = temp + r("Ulaz") - r("Izlaz")
        '    temp = temp + IIf(IsDBNull(r("Ulaz")), 0, r("Ulaz")) - r("Izlaz")
        '    r("Stanje") = temp
        '    'If r("Ulaz") = 0 Then r("Ulaz") = DBNull.Value
        '    If r("Izlaz") = 0 Then r("Izlaz") = DBNull.Value
        'Next
        'modOleDb.dodajKolonuRB(tabela, 0)


        'lblNaslovStavke.Text = "Kartica artikla: " & sifraProizvoda & "-" & modArtikal.vratiNazivArtikaIzKontaR(sifraProizvoda, lookUpProizvoda) & " do datuma " & doDatuma

        'Dim sirKolona() As Integer
        'sirKolona = New Integer() {100, 100, 100, 100, 100, 100, 100, 100}
        'citajSirineKolona(Me.Name & "_mali_grid", 8, sirKolona)
        ''maliGrid.DataSource = tabela ' modOleDb.izracunajSumu(tabela, New Integer() {0, 0, 0, 0, 0, 1, 1, 0})
        ''modGrid.formatirajGridDataTable(maliGrid, sirKolona)


        'Dim algKol = New Integer() {16, 32, 64, 16, 32, 64, 64, 64, 64, 64, 64, 64}
        'maliGrid.DataSource = modOleDb.izracunajSumu(tabela, New Integer() {0, 0, 0, 0, 0, 1, 1, 0})
        'modGrid.gridFormat_ver1(maliGrid, 8, {"R.b.", "Šifra dokumenta", "Datum", "Broj", "Naziv dokumenta", "Ulaz", "Izlaz", "Stanje", "", "", "", ""}, sirKolona,
        '                         algKol, {"####.", "", "dd.MM.yy", "", "", "#,##0.00", "#,##0.00", "#,##0.00", "", "", ""}, Me.Name & "_mali_grid")

    End Sub

    ''' <summary>
    ''' puni tabelu KontaR koja će se čuvati u memoriji i vraća true ili false u zavisnosti da li je punjenje uspelo
    ''' </summary>
    Private Function napuniKontaRMemorija() As Boolean
        Dim sql As String = "SELECT Kor_Sifra AS Sifra, Kor_Opis AS Opis, Kor_CenaD As Cena, Kor_CenaN as NabavnaCena, Kor_Ean AS BarKod, Kor_Tar_Sifra AS Tarifa FROM KontaR " &
                             "WHERE Kor_VrstaLookUp = 'M'"
        Try
            kontaR_mem = modOleDb.napuniTabelu(sql)
            'kolona Sifra je primarni ključ ove tabele
            kontaR_mem.PrimaryKey = New DataColumn() {kontaR_mem.Columns("Sifra")}
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub mnuOpcije_StampaPredracuna_Click(sender As Object, e As EventArgs) Handles btnOpcije_StampaPred_Uspravno.Click,
                                                                                           btnOpcije_StampaPred_Polozeno.Click
        Select Case sender.name
            Case btnOpcije_StampaPred_Uspravno.Name
                'stampaPredracuna_v1(strLinkLok, "Predračun broj: " & strTmpBroj, False, enmStampaExport.Print, "")
            Case btnOpcije_StampaPred_Polozeno.Name
                'stampaPredracuna_v1(strLinkLok, "Predračun broj: " & strTmpBroj, True, enmStampaExport.Print, "")
        End Select



    End Sub


    'Private Function set_EDI_u_FKZ(dokument As String, Broj As String) As Boolean
    '    '------ Funkcija kopira podatek iz tabele Kmz-Kms u Fkz-Fks kako bi se lakse stampao

    '    '  strFild(0) = "Fkz_Broj"
    '    '  strFild(1) = "Fkz_Datum"
    '    '  strFild(2) = "Fkz_MestoIzdavanja"
    '    '  strFild(3) = "Fkz_DatumOtp"
    '    '  strFild(4) = "Fkz_BrOtp"
    '    '  strFild(5) = "Fkz_Vri_Sifra"
    '    '  strFild(6) = "Fkz_Kon_201"
    '    '  strFild(7) = "Fkz_Valuta"
    '    '  strFild(8) = "Fkz_Vozilo"
    '    '  strFild(9) = "Fkz_FkzU_Sifra"
    '    '  strFild(10) = "Fkz_FkzN_Sifra"
    '    '  strFild(11) = "Fkz_AdresaIsporuke"        '---- Napomena uz racun
    '    '  strFild(12) = "Fkz_Vozac"
    '    '  strFild(13) = "Fkz_MestoIsporuke"

    '    '  strFildGrid(0) = "Edi_Dok_Sifra"
    '    '  strFildGrid(1) = "Edi_Broj"
    '    '  strFildGrid(2) = "Edi_Datum"
    '    '  strFildGrid(3) = "Edi_Kon_Sifra"
    '    '  strFildGrid(4) = "Edi_Kupac"
    '    '  strFildGrid(5) = "Kon_Opis"
    '    'Dim intSqlArtikli As Integer
    '    'Dim pb As New frmProgres
    '    'pb.ProgressBarSetup(intBrojArtikala, "Učitavanje artikala sa servera")

    '    Dim lngID As Long

    '    'proverimo da li vec tamo u FaktZ ima taj racun jer to zajebava maximalno -------
    '    Dim strFkz_Dok_Sifra As String
    '    'strFkz_Dok_Sifra = Get_INI("default_Fkz_Dok_Sifra")
    '    strFkz_Dok_Sifra = ReadIni(iniFile, "najcesce vrednosti", "default_Fkz_Dok_Sifra", "44")
    '    If strFkz_Dok_Sifra = "" Then strFkz_Dok_Sifra = "41"
    '    'Dim strSqlProvera = $"Select Fkz_Broj, Fkz_Dok_Sifra FROM FaktZ
    '    '          WHERE Fkz_Broj = '{Broj}'
    '    '          AND Fkz_Dok_Sifra = '{strFkz_Dok_Sifra}' "
    '    Dim strSqlProvera = $"SELECT Edi_BrojBlokRacuna FROM Edi 
    '                        WHERE Edi_Por = 'V' AND Edi_Broj = '{Broj}' AND Edi_Dok_Sifra = '{dokument}' "
    '    If modOleDb.RecordCountA(strSqlProvera) > 0 Then

    '        'msgObavest("Vec postoji faktura " & strFkz_Dok_Sifra & "/" & Broj & "!" & vbCrLf &
    '        '            "Izbrišite fakturu i opet prebacite predračun u račun.", "Postoji račun")
    '        'Return False
    '        'Exit Function
    '    End If
    '    Dim ediId As String = vratiJedVrednostUpit($"SELECT Edi_id FROM Edi WHERE Edi_Broj = '{Broj}' AND Edi_Dok_Sifra = '{dokument}'")
    '    Dim fkzId As String = modMain.vratiSledecuSifruUslov("FaktZ", "Fkz_Broj", "Fkz_Dok_Sifra", strFkz_Dok_Sifra)
    '    setKontaR_M_u_K(ediId)  '  f-ja koja nedostaje

    '    'Dim strSql As String = $"SELECT * FROM Edi, EdiS, KontaR 
    '    '            WHERE Edi_Broj = '{Broj}'
    '    '            And Edi_Dok_Sifra = '{dokument}'
    '    '            And Edi_id = Eds_Edi_id 
    '    '            And Eds_Kon_1340 = Kor_Sifra 
    '    '            And Eds_Kor_VrstaLookUp = Kor_VrstaLookUp 
    '    '            ORDER BY Eds_id "
    '    Dim strSql As String = $"SELECT * FROM Edi 
    '                WHERE Edi_Broj = '{Broj}'
    '                And Edi_Dok_Sifra = '{dokument}'
    '                ORDER BY Edi_id "
    '    Dim reader As OleDbDataReader = Nothing
    '    reader = modOleDb.vratiDataReader(strSql)
    '    While reader.Read()

    '        Dim kolone As String = "Fkz_Broj, Fkz_PozivNaBroj, Fkz_Dok_Sifra, Fkz_Datum, Fkz_Valuta, Fkz_DatumOtp, 
    '                                Fkz_Kon_201, Fkz_Kon_132, Fkz_Vri_Sifra, Fkz_MestoIzdavanja, Fkz_AdresaIsporuke"

    '        Dim strSQLupdate As String = $"SELECT  {kolone}  FROM FaktZ"
    '        Dim adapter As New OleDbDataAdapter(strSQLupdate, adoCN)
    '        Dim tabela As New DataTable
    '        Dim red As DataRow
    '        Dim builder As New OleDbCommandBuilder(adapter)

    '        Try

    '            'iz tabele se ne uzimaju podaci jer nisu potrebni, preuzima se samo šema tabele
    '            adapter.FillSchema(tabela, SchemaType.Source)
    '            'kreira se novi red sa istom šemom kao i tabela
    '            red = tabela.NewRow
    '            red("Fkz_Broj") = fkzId
    '            red("Fkz_PozivNaBroj") = fkzId
    '            red("Fkz_Dok_Sifra") = strFkz_Dok_Sifra
    '            red("Fkz_Datum") = reader("Edi_Datum")

    '            red("Fkz_Valuta") = reader("Edi_DatumUplate")
    '            red("Fkz_DatumOtp") = reader("Edi_Datum")
    '            red("Fkz_Kon_201") = reader("Edi_Kupac")
    '            red("Fkz_Kon_132") = "1321"

    '            red("Fkz_MestoIzdavanja") = ReadIni(iniFile, "Options", "mesto", "/") 'Get_INI("mesto")
    '            red("Fkz_AdresaIsporuke") = reader("Edi_Opis")
    '            red("Fkz_Vri_Sifra") = "1"

    '            'tabeli dodeljujem red i update-ujem tabelu u bazi
    '            tabela.Rows.Add(red)
    '            adapter.Update(tabela)
    '        Catch ex As Exception
    '            msgObavest("Greška prilikom upisa:" & vbCrLf & ex.ToString, "Greška prilikom upisa zaglavlja")
    '            Return False
    '            Exit Function
    '        End Try
    '        'Dim sqlTabelaZaUpis As String = "Select * FROM FaktZ"

    '    End While
    '    'lngID = modOleDb.vratiJedVrednostUpit($"SELECT Fkz_id FROM FaktZ WHERE Fkz_Dok_Sifra = '{strFkz_Dok_Sifra}'  AND Fkz_Broj = '{Broj}'  ")
    '    lngID = modOleDb.vratiJedVrednostUpit($"SELECT Fkz_id FROM FaktZ WHERE Fkz_Dok_Sifra = '{strFkz_Dok_Sifra}'  AND Fkz_Broj = '{fkzId}'  ")
    '    Thread.Sleep(1000)

    '    Dim strSqlStavke As String = $"Select * FROM EdiS, KontaR 
    '                     WHERE Eds_Edi_id = {ediId}                   
    '                     And Eds_Kon_1340 = Kor_Sifra 
    '                     And Eds_Kor_VrstaLookUp = Kor_VrstaLookUp 
    '                     ORDER BY Eds_id "
    '    Dim readerS As OleDbDataReader = Nothing
    '    readerS = modOleDb.vratiDataReader(strSqlStavke)
    '    While readerS.Read()

    '        Dim koloneStavke As String = "Fks_Fkz_id, Fks_Kon_1320, Fks_Kon_1320_Opis, Fks_Kon_1320_jm, Fks_Kolicina, Fks_Cena, 
    '                                Fks_Osnovica, Fks_Pr_462, Fks_Iz_462, Fks_Vr_462, Fks_VrednostP, Fks_Kon_462"

    '        Dim strSQLupdateStavke As String = $"SELECT  {koloneStavke}  FROM FaktS"
    '        Dim adapterS As New OleDbDataAdapter(strSQLupdateStavke, adoCN)
    '        Dim tabelaS As New DataTable
    '        Dim redS As DataRow
    '        Dim builderS As New OleDbCommandBuilder(adapterS)

    '        Try

    '            'iz tabele se ne uzimaju podaci jer nisu potrebni, preuzima se samo šema tabele
    '            adapterS.FillSchema(tabelaS, SchemaType.Source)
    '            'kreira se novi red sa istom šemom kao i tabela
    '            redS = tabelaS.NewRow
    '            redS("Fks_Fkz_id") = lngID
    '            redS("Fks_Kon_1320") = "13213" & readerS("Eds_Kon_1340")
    '            redS("Fks_Kon_1320_Opis") = readerS("Kor_Opis")
    '            redS("Fks_Kon_1320_jm") = readerS("Kor_JM")

    '            redS("Fks_Kolicina") = Round(readerS("Eds_Kolicina"), 3, MidpointRounding.AwayFromZero) 'treba Round - 3 decimale
    '            redS("Fks_Cena") = Round(readerS("Eds_CenaVP"), 7, MidpointRounding.AwayFromZero) 'treba Round - 4 decimale
    '            redS("Fks_Osnovica") = Round(readerS("Eds_Kolicina") * readerS("Eds_CenaVP"), 2, MidpointRounding.AwayFromZero) 'treba Round  3 decimale

    '            If readerS("Eds_PDVStopa") = 10 Then 'treba videti pdvM sta je
    '                redS("Fks_Kon_462") = "4710"
    '            Else
    '                redS("Fks_Kon_462") = "4700"
    '            End If
    '            redS("Fks_Pr_462") = readerS("Eds_PDVStopa")
    '            redS("Fks_Iz_462") = Round(readerS("Eds_CenaVP") * (readerS("Eds_PDVStopa") / 100), 2, MidpointRounding.AwayFromZero) 'treba Round - 2 decimale
    '            redS("Fks_Vr_462") = Round(redS("Fks_Iz_462") * readerS("Eds_Kolicina"), 2, MidpointRounding.AwayFromZero) 'treba Round - 2 decimale
    '            redS("Fks_VrednostP") = Round(redS("Fks_Osnovica") + redS("Fks_Vr_462"), 2, MidpointRounding.AwayFromZero)

    '            'tabeli dodeljujem red i update-ujem tabelu u bazi
    '            tabelaS.Rows.Add(redS)
    '            adapterS.Update(tabelaS)
    '        Catch ex As Exception
    '            msgObavest("Greška prilikom upisa stavki:" & vbCrLf & ex.ToString, "Greška prilikom upisa stavki")
    '            Return False
    '        End Try

    '    End While
    '    modFaktura.Set_IznosRacuna_U_Tab_FaktZ(Convert.ToInt32(lngID))

    '    Dim strSqlInsert As String = $"UPDATE Edi 
    '                                   SET Edi_BrojBlokRacuna = {lngID}, Edi_Por = 'V'
    '                                   WHERE Edi_id = {ediId}"
    '    modOleDb.izvrsiKomandu(strSqlInsert)
    '    msgObavest("Predračun je uspešno prebačen." & vbCrLf & "Pod dokumentom " & strFkz_Dok_Sifra & " broj: " & fkzId.ToString & vbCrLf & "u veleprodajni račun.", "Prebacivanje u veleprodaju")
    '    Return True
    '    '  PrintLaser_7 "41", Broj

    'End Function

    'Private Function setKontaR_M_u_K(ediId As Long)
    '    On Error Resume Next

    '    Dim strSql As String = $"Select * FROM KontaR, EdiS 
    '                    WHERE Eds_Edi_id = {ediId}
    '                    AND Eds_Kon_1340 = Kor_Sifra 
    '                    AND Kor_Vrsta = 'M' 
    '                    AND Kor_VrstaLookUp = 'M' "
    '    Dim reader As OleDbDataReader = Nothing
    '    reader = modOleDb.vratiDataReader(strSql)
    '    While reader.Read()
    '        Dim insKonto As New clsKonto
    '        If insKonto.ImaLiTajKonto("1321" & "3" & reader("Kor_Sifra")) Then
    '            'msgObavest("Već postoji konto " & "1321" & "3" & reader("Kor_Sifra") & "!", "Postoji konto")
    '        Else
    '            Dim strSqlInsert = $" INSERT INTO KontaR (Kor_Sifra, Kor_Opis, Kor_jm, Kor_Tar_Sifra, Kor_Drzava, 
    '                Kor_Vrsta, Kor_VrstaLookUp, Kor_Sint, Kor_Anl, Kor_CenaD, Kor_Ean, Kor_Kor ) 
    '                SELECT '3' + Kor_Sifra, Kor_Opis, Kor_jm, Kor_Tar_Sifra, '001', 
    '                'K', 'K', 1, 'A', Kor_CenaD, Kor_Ean, 'K' + '3' + Kor_Sifra
    '                From KontaR
    '                Where Kor_Sifra = '{reader("Eds_Kon_1340")}'
    '                And Kor_VrstaLookUp = 'M' "
    '            modOleDb.izvrsiKomandu(strSqlInsert)

    '            '          insArtikal.ImaLiTajKonto "3" & rs!Eds_Kon_1340, "K"

    '            If Not insKonto.ImaLiTajKonto("1321" & reader("Eds_Kon_1340")) Then
    '                '    If insArtikal.ImaLiTajKonto("3" & reader("Eds_Kon_1340"), "K") Then
    '                '        insKonto.DodajKolKonto "1321" & "3" & reader("Eds_Kon_1340"), insArtikal.NazivKonta, "K",
    '                '                  Len("1321") + insArtikal.sintetika, insArtikal.Kor_id,
    '                '                  insArtikal.JM
    '                'End If

    '                modKonta.upisiKolicinskiKontoUKonta("1321", "3" & reader("Kor_Sifra"), "K")
    '            End If
    '        End If
    '    End While

    'End Function

End Class