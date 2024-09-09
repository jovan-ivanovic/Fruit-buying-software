'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports System.Data.OleDb
'Imports System
'Imports System.IO
'Imports System.Windows.Forms

'Public Module modStampaCR

'	Public Sub stampajGridCR(grid As DataGridView, naslov1 As String, landacape As Boolean, stampaExport As enmStampaExport, Optional naslov2 As String = "")
'		Dim polje As FieldObject
'		Dim zaglavlje As FieldHeadingObject
'		Dim line As LineObject
'		Dim ukSirina As Double
'		Dim deo As Double
'		Dim levo As Integer = 50
'		Dim sirina As Integer = 0
'		Dim tabela As DataTable
'        'izbor izveštaja u zavisnosti od poslate orijentacije
'        Dim insReport As ReportDocument
'		If landacape Then
'			insReport = New clrGridLand
'		Else
'			insReport = New clrGridPort
'		End If

'		'podesavam veličinu papira i margine
'		'insReport.PrintOptions.PaperSize = PaperSize.PaperA4
'		'insReport.PrintOptions.ApplyPageMargins(vratiMargine())

'		'provera da li je grid popunjen sa DataTable ili na klasičan način, ukoliko je popunjen sa DataTable
'		' tada vadim samo DataTable iz grida i u Excelu cu imati brojeve

'		tabela = napraviDataTable(grid)

'		insReport.SetDataSource(tabela)

'        'dodeljivanje parametara
'        insReport.SetParameterValue("firma", modFirma.naziv)
'		insReport.SetParameterValue("adresa", modFirma.adresa)
'		insReport.SetParameterValue("mesto", modFirma.mesto)
'		insReport.SetParameterValue("pib", modFirma.pib)

'		insReport.SetParameterValue("naslov1", naslov1)
'        insReport.SetParameterValue("naslov2", naslov2)

'        '11/2022 posto smo prosirili neke izvestaje vise od 16 kolona uvodimo promenljivu
'        'koja maximalno moze biti 16
'        Dim intBrojKolona As Integer
'        If grid.ColumnCount - 1 > 17 Then
'            intBrojKolona = 17
'        Else
'            intBrojKolona = grid.ColumnCount
'        End If

'        For i = 1 To intBrojKolona - 1 ' grid.ColumnCount - 1
'            'podešava se vrednost parametara
'            insReport.SetParameterValue("h" & i, grid.Columns(i).HeaderText)

'            'izračunava se ukupna širina kolona grida da bi se kasnije izračunala vrednost jedne te jedinice
'            ukSirina = ukSirina + grid.Columns(i).Width

'            polje = insReport.ReportDefinition.ReportObjects("col" & i & "1")

'            'podešava se poravnanje u izveštaju u zavisnosti od poravnanja u gridu
'            Select Case grid.Columns(i).DefaultCellStyle.Alignment.ToString()
'                Case "BottomCenter", "MiddleCenter", "TopCenter"
'                    polje.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign
'                Case "BottomLeft", "MiddleLeft", "TopLeft"
'                    polje.ObjectFormat.HorizontalAlignment = Alignment.LeftAlign
'                Case "BottomRight", "MiddleRight", "TopRight"
'                    polje.ObjectFormat.HorizontalAlignment = Alignment.RightAlign
'                Case "NotSet"
'                    polje.ObjectFormat.HorizontalAlignment = Alignment.LeftAlign
'            End Select
'        Next

'        'širina za štampanje(širina papira minus leva i desna margina) se deli sa ukupnom širinom grida i dobija se vrednost jedne jedinice u gridu
'        'jer se NE RADI sa istim merama u gridu i u izveštaju.
'        deo = (insReport.PrintOptions.PageContentWidth - insReport.PrintOptions.PageMargins.leftMargin - insReport.PrintOptions.PageMargins.rightMargin) / ukSirina

'        'podešavanje linija koje treba da bude nevidljive
'        'For i = grid.ColumnCount - 1 To 15
'        For i = intBrojKolona - 1 To 15
'            line = insReport.ReportDefinition.ReportObjects("Line" & i)

'            Dim top As Integer = line.Top
'            line.Top = line.Bottom
'            line.Right = 20
'            line.Left = 20
'            line.Top = top
'            line.LineStyle = LineStyle.SingleLine
'        Next

'        For i = 1 To intBrojKolona - 1 ' grid.ColumnCount - 1
'            polje = insReport.ReportDefinition.ReportObjects("col" & i & "1")
'            zaglavlje = insReport.ReportDefinition.ReportObjects("Text" & i)
'            zaglavlje.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign
'            'širina kolone u izveštaju je širina kolone u gridu puta vrednost jedne te jedinice
'            sirina = grid.Columns(i).Width * deo
'            'dodeljuje se početna pozicija i širina kolone
'            polje.Left = levo
'            zaglavlje.Left = levo

'            polje.Width = sirina
'            zaglavlje.Width = sirina
'            zaglavlje.Height = 550
'            polje.ObjectFormat.EnableCanGrow = True
'            zaglavlje.ObjectFormat.EnableCanGrow = True
'            polje.ObjectFormat.EnableKeepTogether = False
'            'određuje se početna pozicija sledeće kolone
'            levo = levo + sirina + 50

'            'podešavanje pozicije linija koje treba da budu vidljive
'            'If i < grid.ColumnCount - 1 Then
'            If i < intBrojKolona - 1 Then
'                line = insReport.ReportDefinition.ReportObjects("Line" & i)
'                line.EnableExtendToBottomOfSection = True

'                Dim top As Integer = line.Top
'                line.Top = line.Bottom
'                line.Right = levo - 30
'                line.Left = levo - 30
'                line.Top = top
'                line.LineStyle = LineStyle.SingleLine
'            End If
'        Next

'        'parametrima ostalih kolona dobijaju vrednost praznog stringa a širine kolona su 0. Ovo izgleda nije potrebno.
'        '        For i = grid.ColumnCount To 16
'        'For i = intBrojKolona + 1 To 16
'        '    polje = insReport.ReportDefinition.ReportObjects("col" & i & "1")
'        '    polje.Width = 0
'        '    insReport.SetParameterValue("h" & i, "")
'        'Next

'        'dodeljujem instancu komponenti za prikaz report i pokrecem izvestaj
'        Dim frmPrikaz As New frmAClrPrikaz
'		frmPrikaz.CrystalReportViewer1.ReportSource = insReport
'        frmPrikaz.CrystalReportViewer1.Refresh()

'        ' Determine whether the directory exists.
'        Dim exPath As String = Application.StartupPath & "\Export"
'        If Not Directory.Exists(exPath) Then
'            Directory.CreateDirectory(exPath)
'        End If


'        'prikazuje se ogvorarajući format u zavisnoti od poslatog načina štampe/exporta
'        Select Case stampaExport
'			Case enmStampaExport.Print
'				frmPrikaz.ShowDialog()
'			Case enmStampaExport.Pdf
'				insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\grid.pdf")
'				System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.pdf")
'			Case enmStampaExport.Word
'				insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\grid.doc")
'				System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'			Case enmStampaExport.Excel
'				insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\grid.xls")
'                System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'            Case enmStampaExport.EmailPdf
'                insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\prilog.pdf")

'            Case enmStampaExport.EmailWord
'                insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\prilog.doc")
'                'System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'            Case enmStampaExport.EmailExcel
'                insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\prilog.xls")
'                'System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'            Case Else
'                MsgBox("Novi način štampe/exporta nije programiran.")
'		End Select
'	End Sub

'	Enum enmStampaExport
'		Print
'		Pdf
'		Word
'        Excel
'        EmailPdf
'        EmailWord
'        EmailExcel
'    End Enum

'	Enum enmRacunOtpremnica
'		Racun
'		Otpremnica
'		PovratnicaKupca
'		KnjiznoOdobrenje
'	End Enum
'    ''' <summary>
'    ''' funkcija za prosledjeni DataGridView pravi DataTabel
'    ''' </summary>
'    ''' <param name="grid">Prosledjeni DataGridView</param>
'    ''' <returns></returns>
'    Private Function napraviDataTable(grid As DataGridView) As DataTable
'		Dim brojKolona As Integer = grid.ColumnCount

'        'DIREKTNO IZ GRIDA NAPRAVITI DATA TABLE
'        Dim str = New String(brojKolona - 2) {}

'        'posebne petlje jer se prvo moraju dodati sve kolone pa tek onda inicijalizovati podaci
'        'kolone imaju imena col1, col2, col3... kao sto je napravljeno sa report builderom
'        Dim tabela As New DataTable("Table")
'        For i = 1 To brojKolona - 1 '-1 jer se prva kolona(ID) ne stampa
'            'tabela.Columns.Add(New DataColumn("col" & i, GetType(String)))
'            tabela.Columns.Add("col" & i)
'        Next

'        'punim dataTable drugi nacin
'        Dim j As Integer
'		Dim k As Integer
'		Dim m As Integer

'		For j = 0 To grid.RowCount - 1
'			m = 0
'            For k = 1 To brojKolona - 1
'                str(m) = If(IsDBNull(grid(k, j).Value), "", grid(k, j).FormattedValue)
'                m = m + 1
'            Next
'            tabela.Rows.Add(str)
'		Next
'		Return tabela
'	End Function

'	Private Function napraviDataTableNova(grid As DataGridView) As DataTable
'        'ako grid nije popunjen sa Data Table onda se krije dugme za pretragu
'        If IsNothing(TryCast(grid.DataSource, DataTable)) Then
'			Dim brojKolona As Integer = grid.ColumnCount

'            'DIREKTNO IZ GRIDA NAPRAVITI DATA TABLE
'            Dim str = New String(brojKolona - 2) {}

'            'posebne petlje jer se prvo moraju dodati sve kolone pa tek onda inicijalizovati podaci
'            'kolone imaju imena col1, col2, col3... kao sto je napravljeno sa report builderom
'            Dim tabela As New DataTable("Table")
'			For i = 1 To brojKolona - 1 '-1 jer se prva kolona(ID) ne stampa
'                tabela.Columns.Add("col" & i)
'			Next

'            'punim dataTable drugi nacin
'            Dim j As Integer
'			Dim k As Integer
'			Dim m As Integer

'			For j = 0 To grid.RowCount - 1
'				m = 0
'				For k = 1 To brojKolona - 1
'					str(m) = If(IsDBNull(grid(k, j).Value), "", grid(k, j).Value)
'					m = m + 1
'				Next
'				tabela.Rows.Add(str)
'			Next
'			Return tabela
'		Else
'			Dim tabela As New DataTable("Table")
'            'ako je grid ispunjen sa DataTable prikazuje se dugme za pretragu i inicijalizuje se tabela, da bi kasnije grid
'            'lako mogao da vratim u pocetno stanje.
'            tabela = DirectCast(grid.DataSource, DataTable).Copy()
'			tabela.Columns.Remove(tabela.Columns.Item(0))
'			Dim i As Integer = 1
'			For Each col As DataColumn In tabela.Columns
'				col.ColumnName = "col" & i
'				i = i + 1
'			Next
'			Return tabela
'		End If

'	End Function

'	Private Sub dodeliParametar(report As ReportDocument, nazivParametra As String, vrednost As Object)
'		If IsDBNull(vrednost) Then
'			report.SetParameterValue(nazivParametra, "")
'		Else
'			report.SetParameterValue(nazivParametra, vrednost)
'		End If
'	End Sub

'#Region "Izveštaji grupisanja"

'    Public Sub grupisi(upit As String, naslov1 As String, naslovi() As String, kolone() As String, sirinaKolona() As Integer, stampaExport As enmStampaExport, Optional naslov2 As String = "", Optional tblTabela As DataTable = Nothing)
'        Dim polje As FieldObject
'        Dim zaglavlje As TextObject
'        Dim ukSirina As Double
'        Dim deo As Double
'        Dim levo As Integer = 50
'        Dim sirina As Integer = 0
'        Dim tabela As DataTable
'        'podesavam da sirina kolone rb bude 5mm
'        Dim sirRb As Decimal = mmToTwips(7)
'        'Dim visZag As Decimal = 300

'        'izbor izveštaja u zavisnosti od poslate orijentacije
'        Dim insReport As ReportDocument
'        insReport = New clrGroupCurrency

'        'prvo podesavam pocetnu poziciju i sirinu prve kolone rb
'        polje = insReport.ReportDefinition.ReportObjects("tRB")
'        zaglavlje = insReport.ReportDefinition.ReportObjects("HtRB")

'        polje.Left = levo
'        zaglavlje.Left = levo
'        polje.Width = sirRb
'        zaglavlje.Width = sirRb
'        'zaglavlje.Height = visZag
'        'ne moze da se podesi visina zaglavlja za prvo polje rb, podesio sam direktno u dizajneru
'        'zaglavlje.Height = visZag

'        'sledeca kolona krece od kraja kolone rb
'        levo = levo + sirRb + 50

'        'podesavanje papira i margina
'        insReport.PrintOptions.PaperSize = PaperSize.PaperA4
'        insReport.PrintOptions.PaperOrientation = PaperOrientation.Landscape
'        'insReport.PrintOptions.ApplyPageMargins(vratiMargine())

'        If tblTabela Is Nothing Then
'            tabela = modOleDb.napuniTabelu(upit)
'        Else
'            tabela = tblTabela
'        End If
'        'dodeljivanje tabele
'        insReport.SetDataSource(tabela)

'        'dodeljivanje parametara
'        insReport.SetParameterValue("firma", modFirma.naziv)
'        insReport.SetParameterValue("adresa", modFirma.adresa)
'        insReport.SetParameterValue("mesto", modFirma.mesto)
'        insReport.SetParameterValue("pib", modFirma.pib)

'        insReport.SetParameterValue("naslov1", naslov1)
'        insReport.SetParameterValue("naslov2", naslov2)

'        For i = 0 To kolone.Count - 1
'            insReport.SetParameterValue("P" & kolone(i), naslovi(i))
'        Next

'        'izračunava se ukupna širina kolona grida da bi se kasnije izračunala vrednost jedne te jedinice
'        ukSirina = sirinaKolona.Sum()

'        'širina za štampanje(širina papira minus leva i desna margina) se deli sa ukupnom širinom grida i dobija se vrednost jedne jedinice u gridu
'        'jer se NE RADI sa istim merama u gridu i u izveštaju.
'        deo = (insReport.PrintOptions.PageContentWidth - insReport.PrintOptions.PageMargins.leftMargin - insReport.PrintOptions.PageMargins.rightMargin) / ukSirina

'        For i = 0 To kolone.Count - 1
'            polje = insReport.ReportDefinition.ReportObjects(kolone(i))
'            zaglavlje = insReport.ReportDefinition.ReportObjects("H" & kolone(i))
'            zaglavlje.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign
'            'širina kolone u izveštaju je širina kolone u gridu puta vrednost jedne te jedinice
'            sirina = sirinaKolona(i) * deo
'            'dodeljuje se početna pozicija i širina kolone
'            polje.Left = levo
'            zaglavlje.Left = levo

'            polje.Width = sirina
'            zaglavlje.Width = sirina
'            'zaglavlje.Height = visZag

'            'formatiraju se pozicije i currency simboli polja SUMA i GRAND SUMA, ukoliko je Currency polje.
'            If kolone(i).Substring(0, 1) = "c" Then

'                'pozicija i sirina suma
'                polje = insReport.ReportDefinition.ReportObjects("S" & kolone(i))
'                polje.Left = levo
'                polje.Width = sirina

'                'pozicija i sirina grand suma
'                polje = insReport.ReportDefinition.ReportObjects("GS" & kolone(i))
'                polje.Left = levo
'                polje.Width = sirina
'                'vidljivost simbola u zavisnosti od poslatog niza

'                'polje.FieldFormat.NumericFormat.CurrencySymbolFormat = CurrencySymbolFormat.NoSymbol
'            End If

'            'određuje se početna pozicija sledeće kolone
'            levo = levo + sirina + 50
'        Next

'        'dodeljujem instancu komponenti za prikaz report i pokrecem izvestaj
'        Dim frmPrikaz As New frmAClrPrikaz
'        frmPrikaz.CrystalReportViewer1.ReportSource = insReport
'        frmPrikaz.CrystalReportViewer1.Refresh()

'        'prikazuje se ogvorarajući format u zavisnoti od poslatog načina štampe/exporta
'        Select Case stampaExport
'            Case enmStampaExport.Print
'                frmPrikaz.ShowDialog()
'            Case enmStampaExport.Pdf
'                insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\grid.pdf")
'                System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.pdf")
'            Case enmStampaExport.Word
'                insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\grid.doc")
'                System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'            Case enmStampaExport.Excel
'                insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\grid.xls")
'                System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'            Case Else
'                MsgBox("Novi način štampe/exporta nije programiran.")
'        End Select
'    End Sub

'    Public Sub grupisiPromet(upit As String, naslov1 As String, naslovi() As String, kolone() As String, sirinaKolona() As Integer, stampaExport As enmStampaExport, Optional naslov2 As String = "")
'		Dim polje As FieldObject
'		Dim zaglavlje As TextObject
'		Dim ukSirina As Double
'		Dim deo As Double
'		Dim levo As Integer = 50
'		Dim sirina As Integer = 0
'		Dim tabela As DataTable

'        'izbor izveštaja u zavisnosti od poslate orijentacije
'        Dim insReport As ReportDocument
'		insReport = New clrGroupCurrencyPromet

'        'podesavanje papira i margina
'        insReport.PrintOptions.PaperSize = PaperSize.PaperA4
'		insReport.PrintOptions.ApplyPageMargins(vratiMargine())

'		tabela = modOleDb.napuniTabelu(upit)
'        'dodeljivanje tabele
'        insReport.SetDataSource(tabela)

'        'dodeljivanje parametara
'        insReport.SetParameterValue("firma", modFirma.naziv)
'		insReport.SetParameterValue("adresa", modFirma.adresa)
'		insReport.SetParameterValue("mesto", modFirma.mesto)
'		insReport.SetParameterValue("pib", modFirma.pib)

'		insReport.SetParameterValue("naslov1", naslov1)
'		insReport.SetParameterValue("naslov2", naslov2)

'		For i = 0 To kolone.Count - 1
'			insReport.SetParameterValue("P" & kolone(i), naslovi(i))
'		Next

'        'izračunava se ukupna širina kolona grida da bi se kasnije izračunala vrednost jedne te jedinice
'        ukSirina = sirinaKolona.Sum()

'        'širina za štampanje(širina papira minus leva i desna margina) se deli sa ukupnom širinom grida i dobija se vrednost jedne jedinice u gridu
'        'jer se NE RADI sa istim merama u gridu i u izveštaju.
'        deo = (insReport.PrintOptions.PageContentWidth - insReport.PrintOptions.PageMargins.leftMargin - insReport.PrintOptions.PageMargins.rightMargin) / ukSirina

'		For i = 0 To kolone.Count - 1
'			polje = insReport.ReportDefinition.ReportObjects(kolone(i))
'			zaglavlje = insReport.ReportDefinition.ReportObjects("H" & kolone(i))
'			zaglavlje.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign
'            'širina kolone u izveštaju je širina kolone u gridu puta vrednost jedne te jedinice
'            sirina = sirinaKolona(i) * deo
'            'dodeljuje se početna pozicija i širina kolone
'            polje.Left = levo
'			zaglavlje.Left = levo

'			polje.Width = sirina
'			zaglavlje.Width = sirina
'			zaglavlje.Height = 300

'            'formatiraju se pozicije i currency simboli polja SUMA i GRAND SUMA, ukoliko je Currency polje.
'            If kolone(i).Substring(0, 1) = "c" Then

'                'pozicija i sirina suma
'                polje = insReport.ReportDefinition.ReportObjects("S" & kolone(i))
'				polje.Left = levo
'				polje.Width = sirina

'                'pozicija i sirina grand suma
'                polje = insReport.ReportDefinition.ReportObjects("GS" & kolone(i))
'				polje.Left = levo
'				polje.Width = sirina
'                'vidljivost simbola u zavisnosti od poslatog niza

'                'polje.FieldFormat.NumericFormat.CurrencySymbolFormat = CurrencySymbolFormat.NoSymbol
'            End If

'            'određuje se početna pozicija sledeće kolone
'            levo = levo + sirina + 50
'		Next

'		'dodeljujem instancu komponenti za prikaz report i pokrecem izvestaj
'		Dim frmPrikaz As New frmAClrPrikaz
'		frmPrikaz.CrystalReportViewer1.ReportSource = insReport
'		frmPrikaz.CrystalReportViewer1.Refresh()

'        'prikazuje se ogvorarajući format u zavisnoti od poslatog načina štampe/exporta
'        Select Case stampaExport
'			Case enmStampaExport.Print
'				frmPrikaz.ShowDialog()
'			Case enmStampaExport.Pdf
'				insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\grid.pdf")
'				System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.pdf")
'			Case enmStampaExport.Word
'				insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\grid.doc")
'				System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'			Case enmStampaExport.Excel
'				insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\grid.xls")
'				System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'			Case Else
'				MsgBox("Novi način štampe/exporta nije programiran.")
'		End Select
'	End Sub

'#End Region

'#Region "Štampe računa"

'	''' <summary>
'	''' F-ja za štampu računa za firmu Pemmy, trebalo bi da je zameni nova štampa Softek kada se istestira dovoljno
'	''' </summary>
'	Public Sub stampajRacunPemmy(id As Long, stampaExport As enmStampaExport, Optional racunOtpremnica As enmRacunOtpremnica = enmRacunOtpremnica.Racun)
'		Dim tabela As DataTable = Nothing
'		Dim reader As OleDbDataReader = Nothing
'		Dim Fkz_Kon_201 As String = ""
'        'Kor_Sifra za kupca u tabeli KontaR se dobija kada se uzmu poslednja tri znaka iz Fkz_Kon_201 u tabeli FaktZ
'        'a Kor_VrstaLookUp za kupca u tabeli KontaR je uvek 'A'. Ta dva polja daju primarni kljuc u tabli KontaR.
'        Dim Kor_Sifra As String = ""
'		Dim brojRacuna As String = ""
'        'sifra iz tabele FaktZNapomene u tabeli FaktZ. Da bih nasao opis napomene.
'        Dim Fkz_FkzN_Sifra As String = ""
'        'sifra iz tabele FaktZUslovi u tabeli FaktZ. Da bih nasao opis uslova.
'        Dim Fkz_FkzU_Sifra As String = ""

'		Try
'			Dim insReport As ReportDocument
'			insReport = New clrRacunPemy

'            'dodeljivanje tabele kao DataSource
'            tabela = modOleDb.napuniTabelu("SELECT * FROM FaktS WHERE Fks_Fkz_id = " & id & " ORDER BY Fks_id")
'			insReport.SetDataSource(tabela)

'            'dodeljivanje parametara iz modRacun
'            insReport.SetParameterValue("logo", fakt_slika)
'            insReport.SetParameterValue("firma", fakt_firma & " ")
'            insReport.SetParameterValue("adresa", fakt_adresa & " ")

'            insReport.SetParameterValue("tel", fakt_telefon & " ")
'            insReport.SetParameterValue("ziroRacun", fakt_tekuci & " ")
'            insReport.SetParameterValue("email", opt_email & " ")
'            insReport.SetParameterValue("web", opt_web & " ")

'            'insReport.SetParameterValue("pib", fakt_pib & " ")
'            insReport.SetParameterValue("pib", modFirma.pib & " ")
'            insReport.SetParameterValue("mb", modFirma.maticniBroj & " ")

'            Select Case racunOtpremnica
'                'ako je izabran račun, tada parametar uzimam iz modRacun.fakt_racun_otpremnica
'                Case enmRacunOtpremnica.Racun
'                    insReport.SetParameterValue("racun/otpremnica", fakt_racun_otpremnica & ":  ")
'                'ako je izbrana otpremnica tada parametru dodeljujem rec Otpremnica
'                Case enmRacunOtpremnica.Otpremnica
'					insReport.SetParameterValue("racun/otpremnica", "Otpremnica: ")
'			End Select

'            'dodeljivanje parametara iz modRacun
'            insReport.SetParameterValue("fakt_mesto", fakt_mesto & " ")
'            insReport.SetParameterValue("fakt_potpislevo", fakt_potpislevo & " ")
'            insReport.SetParameterValue("fakt_potpislev2", fakt_potpislev2 & " ")

'            insReport.SetParameterValue("fakt_potpissredina", fakt_potpissredina & " ")
'            insReport.SetParameterValue("fakt_potpissredin2", fakt_potpissredin2 & " ")

'            insReport.SetParameterValue("fakt_potpisdesno", fakt_potpisdesno & " ")
'            insReport.SetParameterValue("fakt_potpisdesn2", fakt_potpisdesn2 & " ")

'            'dodeljivanje parametara iz tabele FaktZ
'            reader = modOleDb.vratiDataReader("SELECT * FROM FaktZ WHERE Fkz_id = " & id)
'			If reader.Read() Then
'				insReport.SetParameterValue("Fkz_Broj", reader("Fkz_Broj") & " ")
'				brojRacuna = reader("Fkz_Broj")
'				insReport.SetParameterValue("Fkz_Datum", reader("Fkz_Datum") & " ")
'				insReport.SetParameterValue("Fkz_Valuta", reader("Fkz_Valuta") & " ")
'				insReport.SetParameterValue("Fkz_DatumOtp", reader("Fkz_DatumOtp") & " ")
'				insReport.SetParameterValue("Fkz_BrOtp", reader("Fkz_BrOtp") & " ")
'				insReport.SetParameterValue("Fkz_MestoIsporuke", reader("Fkz_MestoIsporuke") & " ")
'				insReport.SetParameterValue("Fkz_AdresaIsporuke", reader("Fkz_AdresaIsporuke") & " ")
'				Fkz_Kon_201 = reader("Fkz_Kon_201")
'				Fkz_FkzN_Sifra = reader("Fkz_FkzN_Sifra")
'				Fkz_FkzU_Sifra = reader("Fkz_FkzU_Sifra")
'			End If
'			modOleDb.zatvoriDataReader(reader)

'            'dodeljivanje parametara o firmi primaocu iz tabele KontaR
'            'secem prva 4(0,1,2,3) sto je "2040" da bi dobio samo sifru u KontaR
'            Kor_Sifra = Fkz_Kon_201.Substring(4)
'			reader = modOleDb.vratiDataReader("SELECT * FROM KontaR WHERE Kor_Sifra = '" & Kor_Sifra & "' AND Kor_VrstaLookUp = 'A'")
'			If reader.Read() Then
'				insReport.SetParameterValue("firmaPrimalac", reader("Kor_Opis") & " ")
'				insReport.SetParameterValue("adresaFirmaPrimalac", reader("Kor_Adresa") & " ")
'                insReport.SetParameterValue("mestoFirmaPrimalac", reader("Kor_PBroj") & " " & reader("Kor_Mesto") & " ")
'                'insReport.SetParameterValue("pibFirmaPrimalac", reader("Kor_Pib") & " ")
'                insReport.SetParameterValue("fPrim_pib", reader("Kor_Pib") & " ")
'                insReport.SetParameterValue("fPrim_mb", reader("Kor_MatBr") & " ")
'            End If
'			modOleDb.zatvoriDataReader(reader)

'            'opis napomene
'            insReport.SetParameterValue("FkzN_Sifra", vratiJedVrednost("FaktZNapomene", "FkzN_Opis", "FkzN_Sifra", Fkz_FkzN_Sifra) & " ")

'            'opis uslova prodaje
'            insReport.SetParameterValue("FkzU_Sifra", vratiJedVrednost("FaktZUslovi", "FkzU_Opis", "FkzU_Sifra", Fkz_FkzU_Sifra) & " ")

'            'kontrolni broj
'            'insReport.SetParameterValue("kontrolniBroj", KontrolniBroj(97, brojRacuna))
'            insReport.SetParameterValue("kontrolniBroj", "")

'			'dodeljujem instancu komponenti za prikaz report i pokrecem izvestaj
'			Dim frmPrikaz As New frmAClrPrikaz
'			frmPrikaz.CrystalReportViewer1.ReportSource = insReport
'			frmPrikaz.CrystalReportViewer1.Refresh()

'            'prikazuje se ogvorarajući format u zavisnoti od poslatog načina štampe/exporta
'            Select Case stampaExport
'				Case enmStampaExport.Print
'					frmPrikaz.ShowDialog()
'				Case enmStampaExport.Pdf
'					insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\grid.pdf")
'					System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.pdf")
'				Case enmStampaExport.Word
'					insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\grid.doc")
'					System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'				Case enmStampaExport.Excel
'					insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\grid.xls")
'					System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'				Case Else
'					MsgBox("Novi način štampe/exporta nije programiran.")
'			End Select
'		Catch ex As Exception
'			MsgBox("Štampanje računa nije uspelo: " & ex.Message)
'		End Try

'	End Sub

'    ''' <summary>
'    ''' Ovo je f-ja za štampu novog računa za Pemmy gde je moguće štampati račun, otpremnicu, povratnicu kupca ili knjižno odobrenje
'    ''' </summary>
'    Public Sub stampajRacunPemmyKnjOdb(id As Long, stampaExport As enmStampaExport, Optional racunOtpremnica As enmRacunOtpremnica = enmRacunOtpremnica.Racun)
'		Dim tabela As DataTable = Nothing
'		Dim reader As OleDbDataReader = Nothing
'		Dim Fkz_Kon_201 As String = ""
'        'Kor_Sifra za kupca u tabeli KontaR se dobija kada se uzmu poslednja tri znaka iz Fkz_Kon_201 u tabeli FaktZ
'        'a Kor_VrstaLookUp za kupca u tabeli KontaR je uvek 'A'. Ta dva polja daju primarni kljuc u tabli KontaR.
'        Dim Kor_Sifra As String = ""
'		Dim brojRacuna As String = ""
'        'sifra iz tabele FaktZNapomene u tabeli FaktZ. Da bih nasao opis napomene.
'        Dim Fkz_FkzN_Sifra As String = ""
'        'sifra iz tabele FaktZUslovi u tabeli FaktZ. Da bih nasao opis uslova.
'        Dim Fkz_FkzU_Sifra As String = ""
'        'pomoćna promenljiva u kojoj formiram String
'        Dim strTemp As String = ""
'        'pomocna promenljiva u kojoj je sačuvan datum izdavanja računa, da bih mogao da ga ubacim u txtTemp kasnije
'        Dim strDatIzdRac As String = ""

'		Try
'			Dim insReport As ReportDocument
'			insReport = New clrRacunPemyKnjOdb

'            'dodeljivanje tabele kao DataSource
'            tabela = modOleDb.napuniTabelu("SELECT * FROM FaktS WHERE Fks_Fkz_id = " & id & " ORDER BY Fks_id")
'			insReport.SetDataSource(tabela)

'            'dodeljivanje parametara iz modFirma
'            insReport.SetParameterValue("logo", fakt_slika)
'            insReport.SetParameterValue("firma", modFirma.naziv & " ")
'			insReport.SetParameterValue("adresa", modFirma.mesto & ", " & modFirma.adresa & " ")

'			insReport.SetParameterValue("tel", modFirma.telefon & " ")
'			insReport.SetParameterValue("ziroRacun", modFirma.tekuciRacun & " ")
'			insReport.SetParameterValue("email", modFirma.opt_email & " ")
'			insReport.SetParameterValue("web", modFirma.opt_web & " ")

'			insReport.SetParameterValue("pib", modFirma.pib & " ")
'			insReport.SetParameterValue("mb", modFirma.maticniBroj & " ")

'			Select Case racunOtpremnica
'                'ako je izabran račun, tada parametar uzimam iz modRacun.fakt_racun_otpremnica
'                Case 0    'a to je isto kao kada bi stavili enmRacunOtpremnica.Racun
'                    insReport.SetParameterValue("racun/otpremnica", fakt_racun_otpremnica & ": ")
'                    insReport.SetParameterValue("vratitiOverenPr", "računa/otpremnice")
'                    'sakrivam rFooterKnjizno
'                    insReport.ReportDefinition.Sections().Item("rFooterKnjizno").SectionFormat.EnableSuppress = True

'				'ako je izbrana otpremnica tada parametru dodeljujem rec Otpremnica
'				Case enmRacunOtpremnica.Otpremnica
'					insReport.SetParameterValue("racun/otpremnica", "Otpremnica: ")
'					insReport.SetParameterValue("vratitiOverenPr", "računa/otpremnice")
'                    'sakrivam rFooterKnjizno
'                    insReport.ReportDefinition.Sections().Item("rFooterKnjizno").SectionFormat.EnableSuppress = True

'				'dodate NOVE dve opcije: Povratnica kupca i Knjižno odobrenje
'				Case enmRacunOtpremnica.PovratnicaKupca
'					insReport.SetParameterValue("racun/otpremnica", "Povratnica kupca-odobrenje br. ")
'					insReport.SetParameterValue("vratitiOverenPr", "Knjižnog odobrenja")
'                    'sakrivam rFooterUslovi, rFooterPlacanje
'                    insReport.ReportDefinition.Sections().Item("rFooterUslovi").SectionFormat.EnableSuppress = True
'					insReport.ReportDefinition.Sections().Item("rFooterPlacanje").SectionFormat.EnableSuppress = True

'				Case enmRacunOtpremnica.KnjiznoOdobrenje
'					insReport.SetParameterValue("racun/otpremnica", "Knjižno odobrenje br. ")
'					insReport.SetParameterValue("vratitiOverenPr", "Knjižnog odobrenja")
'                    'sakrivam rFooterUslovi, rFooterPlacanje
'                    insReport.ReportDefinition.Sections().Item("rFooterUslovi").SectionFormat.EnableSuppress = True
'					insReport.ReportDefinition.Sections().Item("rFooterPlacanje").SectionFormat.EnableSuppress = True
'			End Select

'            'dodeljivanje parametara iz modRacun
'            insReport.SetParameterValue("fakt_mesto", fakt_mesto & " ")
'            insReport.SetParameterValue("fakt_potpislevo", fakt_potpislevo & " ")
'            insReport.SetParameterValue("fakt_potpislev2", fakt_potpislev2 & " ")

'            insReport.SetParameterValue("fakt_potpissredina", fakt_potpissredina & " ")
'            insReport.SetParameterValue("fakt_potpissredin2", fakt_potpissredin2 & " ")

'            insReport.SetParameterValue("fakt_potpisdesno", fakt_potpisdesno & " ")
'            insReport.SetParameterValue("fakt_potpisdesn2", fakt_potpisdesn2 & " ")

'            'dodeljivanje parametara iz tabele FaktZ
'            reader = modOleDb.vratiDataReader("SELECT * FROM FaktZ WHERE Fkz_id = " & id)
'			If reader.Read() Then
'				insReport.SetParameterValue("Fkz_Broj", reader("Fkz_Broj") & " ")
'				brojRacuna = reader("Fkz_Broj")
'                'čuvam datum izdavanja računa u promenljivoj da bih kasnije mogao da pošaljem u tmpText
'                strDatIzdRac = reader("Fkz_Datum") & " "
'				insReport.SetParameterValue("Fkz_Datum", strDatIzdRac)
'				insReport.SetParameterValue("Fkz_Valuta", reader("Fkz_Valuta") & " ")
'				insReport.SetParameterValue("Fkz_DatumOtp", reader("Fkz_DatumOtp") & " ")
'				insReport.SetParameterValue("Fkz_BrOtp", reader("Fkz_BrOtp") & " ")
'				insReport.SetParameterValue("Fkz_MestoIsporuke", reader("Fkz_MestoIsporuke") & " ")
'				insReport.SetParameterValue("Fkz_AdresaIsporuke", reader("Fkz_AdresaIsporuke") & " ")
'				Fkz_Kon_201 = reader("Fkz_Kon_201")
'				Fkz_FkzN_Sifra = reader("Fkz_FkzN_Sifra")
'				Fkz_FkzU_Sifra = reader("Fkz_FkzU_Sifra")
'			End If
'			modOleDb.zatvoriDataReader(reader)

'            'dodeljivanje parametara o firmi primaocu iz tabele KontaR
'            'secem prva 4(0,1,2,3) sto je "2040" da bi dobio samo sifru u KontaR
'            Kor_Sifra = Fkz_Kon_201.Substring(4)
'            reader = modOleDb.vratiDataReader("SELECT * FROM KontaR WHERE Kor_Sifra = '" & Kor_Sifra & "' AND Kor_VrstaLookUp = 'A'")
'			If reader.Read() Then
'				insReport.SetParameterValue("firmaPrimalac", reader("Kor_Opis") & " ")
'				insReport.SetParameterValue("adresaFirmaPrimalac", reader("Kor_Adresa") & " ")
'				insReport.SetParameterValue("mestoFirmaPrimalac", reader("Kor_Mesto") & " ")
'				insReport.SetParameterValue("pibFirmaPrimalac", reader("Kor_Pib") & " ")
'			End If
'                modOleDb.zatvoriDataReader(reader)

'                'opis napomene
'                insReport.SetParameterValue("FkzN_Sifra", vratiJedVrednost("FaktZNapomene", "FkzN_Opis", "FkzN_Sifra", Fkz_FkzN_Sifra) & " ")

'            'opis uslova prodaje
'            insReport.SetParameterValue("FkzU_Sifra", vratiJedVrednost("FaktZUslovi", "FkzU_Opis", "FkzU_Sifra", Fkz_FkzU_Sifra) & " ")

'            'kontrolni broj
'            'insReport.SetParameterValue("kontrolniBroj", KontrolniBroj(97, brojRacuna))
'            insReport.SetParameterValue("kontrolniBroj", "")

'            'parametar za text koji se ispusiju u rFooterKnjizno - ta sekcija je vidljiva za Knjizno odobrenje i Povratnicu kupca
'            strTemp = strTemp & modFirma.naziv & vbCrLf &
'				modFirma.adresa & vbCrLf &
'				modFirma.mesto & vbCrLf &
'				modFirma.pib & vbCrLf & vbCrLf & vbCrLf &
'				"OBAVEŠTENJE O UMENJENJU PRETHODNOG PDV-A" & vbCrLf &
'				"u skladu sa čl.21 i čl.31 Zakona o PDV(Sl.glasnik RS br.84/2004 i 86/2004)" & vbCrLf & vbCrLf &
'				"Obaveštavamo Vas da smo u svojim knjigama sproveli knjiženja vezana za navedeno knjižno odobrenje broj: " & brojRacuna & " od " & strDatIzdRac & vbCrLf &
'				" i da smo po tom osnovu u skladu sa čl.21 i čl.31 Zakona o PDV(Sl.glasnik RS br.84/2004 i 86/2004) " & vbCrLf &
'				" umanjili odbitak prethodnog PDV-a u iznosu od: "

'			insReport.SetParameterValue("textKnjiznoOdobrenje", strTemp)

'			'dodeljujem instancu komponenti za prikaz report i pokrecem izvestaj
'			Dim frmPrikaz As New frmAClrPrikaz
'			frmPrikaz.CrystalReportViewer1.ReportSource = insReport
'			frmPrikaz.CrystalReportViewer1.Refresh()

'            'prikazuje se ogvorarajući format u zavisnoti od poslatog načina štampe/exporta
'            Select Case stampaExport
'				Case enmStampaExport.Print
'					frmPrikaz.ShowDialog()
'				Case enmStampaExport.Pdf
'					insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\grid.pdf")
'					System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.pdf")
'				Case enmStampaExport.Word
'					insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\grid.doc")
'					System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'				Case enmStampaExport.Excel
'					insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\grid.xls")
'					System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'				Case Else
'					MsgBox("Novi način štampe/exporta nije programiran.")
'			End Select
'		Catch ex As Exception
'			MsgBox("Štampanje računa nije uspelo: " & ex.Message)
'		End Try

'	End Sub

'    ''' <summary>
'    ''' Ovo je f-ja za štampu računa Softek, ovo je nova f-ja koja bC:\Softek vb15\FiskalnaKasa\SharedProject\Moduli\modStampaCR.vbi trebala da obuhvati sve u vezi štampanja
'    ''' </summary>
'    ''' 
'    ''' <param name="racunOtpremnica">Za sada može samo račun i otpremnica</param>
'    Public Sub stampajRacunSoftek(id As Long, stampaExport As enmStampaExport,
'                                  Optional racunOtpremnica As enmRacunOtpremnica = enmRacunOtpremnica.Racun,
'                                  Optional boolPrivju As Boolean = True, Optional pdfNaziv As String = "")
'        Dim tabela As DataTable = Nothing
'        Dim reader As OleDbDataReader = Nothing
'        Dim Fkz_Kon_201 As String = ""
'        'Kor_Sifra za kupca u tabeli KontaR se dobija kada se uzmu poslednja tri znaka iz Fkz_Kon_201 u tabeli FaktZ
'        'a Kor_VrstaLookUp za kupca u tabeli KontaR je uvek 'A'. Ta dva polja daju primarni kljuc u tabli KontaR.
'        Dim Kor_Sifra As String = ""
'        Dim brojRacuna As String = ""
'        'sifra iz tabele FaktZNapomene u tabeli FaktZ. Da bih nasao opis napomene.
'        Dim Fkz_FkzN_Sifra As String = ""
'        'sifra iz tabele FaktZUslovi u tabeli FaktZ. Da bih nasao opis uslova.
'        Dim Fkz_FkzU_Sifra As String = ""
'        'trenutni vazeci header, potrebno je da ga sačuvam da bih kasnije mogao da znam njegovu visinu
'        Dim header As Section = Nothing
'        'standardna visina headera u milimetrima(Ovo je definisano i u dizajnu reporta ali dobro da ima i ovde)
'        Dim headerVisina As Decimal = 40

'        Try
'            Dim insReport As ReportDocument
'            insReport = New clrRacunSoftek

'            'dodeljivanje tabele kao DataSource
'            tabela = modOleDb.napuniTabelu("SELECT * FROM FaktS WHERE Fks_Fkz_id = " & id & " ORDER BY Fks_id")
'            insReport.SetDataSource(tabela)

'            'Dim box As BoxObject
'            'box = insReport.ReportDefinition.ReportObjects.Item("Box5")

'            'sakrivam sve verzije zaglavlja, kasnije cu prikazati samo izabranu verziju
'            insReport.ReportDefinition.Sections().Item("rHeaderLogo").SectionFormat.EnableSuppress = True
'            insReport.ReportDefinition.Sections().Item("rHeaderInfo").SectionFormat.EnableSuppress = True
'            insReport.ReportDefinition.Sections().Item("rHeaderLogoInfo").SectionFormat.EnableSuppress = True
'            insReport.ReportDefinition.Sections().Item("rHeaderMemorandum").SectionFormat.EnableSuppress = True

'            'sakrivam sve verzije potpisa pa cu kasnije prikazati samo odabranu
'            insReport.ReportDefinition.Sections().Item("rFooterPotpis").SectionFormat.EnableSuppress = True
'            insReport.ReportDefinition.Sections().Item("rFooterPotpisOkvir").SectionFormat.EnableSuppress = True

'            'sakrivam sve verzija sume racuna pa cu kasnije prikazati samo izabranu
'            insReport.ReportDefinition.Sections().Item("rFooterBezAvansa").SectionFormat.EnableSuppress = True
'            insReport.ReportDefinition.Sections().Item("rFooterSaAvansom").SectionFormat.EnableSuppress = True

'            'sakrivam stalne podatke u footeru
'            insReport.ReportDefinition.Sections().Item("rFooterStalniPodaci").SectionFormat.EnableSuppress = True

'            'prikazujem izabranu verziju zaglavlja
'            Select Case fakt_vr_zaglavlja
'                Case enmVrZaglavlja.Logo
'                    header = insReport.ReportDefinition.Sections().Item("rHeaderLogo")
'                    header.SectionFormat.EnableSuppress = False
'                Case enmVrZaglavlja.Podaci
'                    header = insReport.ReportDefinition.Sections().Item("rHeaderInfo")
'                    header.SectionFormat.EnableSuppress = False
'                Case enmVrZaglavlja.LogoPodaci
'                    header = insReport.ReportDefinition.Sections().Item("rHeaderLogoInfo")
'                    header.SectionFormat.EnableSuppress = False
'                Case enmVrZaglavlja.Memorandum
'                    header = insReport.ReportDefinition.Sections().Item("rHeaderMemorandum")
'                    header.SectionFormat.EnableSuppress = False
'                    insReport.ReportDefinition.Sections().Item("rHeaderMemorandum").Height = fakt_mem_visina
'            End Select

'            'podesavanje standardne visine headera
'            header.Height = mmToTwips(headerVisina)

'            'samo trenuno podesavam margine
'            insReport.PrintOptions.PaperSize = PaperSize.PaperA4
'            insReport.PrintOptions.ApplyPageMargins(vratiMargine())

'            'postavljam poziciju informacija o firmi primaocu na izabranu lokaciju(podrazumeva se da je korinik za TOP uneo razmak od vrha strane)
'            Dim firmaPrimalac As TextObject = insReport.ReportDefinition.Sections().Item("rHeaderKupac").ReportObjects.Item("txtFirmaPrimalac")
'            'CR podrazumeva da su ove dimenzije od početka sektora u kome se TextBox nalazi a meni je potrebno od početka strane
'            'pa zato oduzimam visinu headera i gornju marginu
'            If fakt_ef_gore_adresa > header.Height + insReport.PrintOptions.PageMargins.topMargin Then
'                firmaPrimalac.Top = fakt_ef_gore_adresa - header.Height - insReport.PrintOptions.PageMargins.topMargin
'            Else
'                MsgBox("Lokacija adrese primaoca ne sme da bude unutar zaglavlja računa. Podesite ponovo lokaciju adrese primaoca.", MsgBoxStyle.OkOnly, "Štampanje računa")
'                'Exit Sub
'            End If
'            'CR porazumeva da je ovo udaljenost od leve margine a meni treba od početka strane tako da oduzimam marginu
'            If fakt_ef_levo_adresa > insReport.PrintOptions.PageMargins.leftMargin Then
'                firmaPrimalac.Left = fakt_ef_levo_adresa - insReport.PrintOptions.PageMargins.leftMargin
'            Else
'                MsgBox("Lokacija adrese primaoca sa leve strane mora biti veća od leve margine. Podesite ponovo lokaciju adrese primaoca.", MsgBoxStyle.OkOnly, "Štampanje računa")
'                'Exit Sub
'            End If

'            'prikaz izabranog načina potpisa
'            Select Case fakt_vr_potpis
'                Case enmPotpis.Sa_Okvirom
'                    insReport.ReportDefinition.Sections().Item("rFooterPotpisOkvir").SectionFormat.EnableSuppress = False
'                    'sakrivam nepotrebne potpise
'                    If fakt_potpislevo = "" And fakt_potpislev2 = "" Then
'                        insReport.ReportDefinition.Sections().Item("rFooterPotpisOkvir").ReportObjects.Item("txtPotpisLevoOkvir").ObjectFormat.EnableSuppress = True
'                    End If

'                    If fakt_potpissredina = "" And fakt_potpissredin2 = "" Then
'                        insReport.ReportDefinition.Sections().Item("rFooterPotpisOkvir").ReportObjects.Item("txtPotpisSredinaOkvir").ObjectFormat.EnableSuppress = True
'                    End If

'                    If fakt_potpisdesno = "" And fakt_potpisdesn2 = "" Then
'                        insReport.ReportDefinition.Sections().Item("rFooterPotpisOkvir").ReportObjects.Item("txtPotpisDesnoOkvir").ObjectFormat.EnableSuppress = True
'                    End If
'                Case enmPotpis.Bez_Okvira
'                    insReport.ReportDefinition.Sections().Item("rFooterPotpis").SectionFormat.EnableSuppress = False
'                    'sakrivam nepotrebne potpise
'                    If fakt_potpislevo = "" And fakt_potpislev2 = "" Then
'                        insReport.ReportDefinition.Sections().Item("rFooterPotpis").ReportObjects.Item("txtPotpisLevo").ObjectFormat.EnableSuppress = True
'                    End If

'                    If fakt_potpissredina = "" And fakt_potpissredin2 = "" Then
'                        insReport.ReportDefinition.Sections().Item("rFooterPotpis").ReportObjects.Item("txtPotpisSredina").ObjectFormat.EnableSuppress = True
'                    End If

'                    If fakt_potpisdesno = "" And fakt_potpisdesn2 = "" Then
'                        insReport.ReportDefinition.Sections().Item("rFooterPotpis").ReportObjects.Item("txtPotpisDesno").ObjectFormat.EnableSuppress = True
'                    End If
'            End Select

'            Select Case fakt_suma_rac
'                Case enmSumaRacuna.Bez_Avansa
'                    insReport.ReportDefinition.Sections().Item("rFooterBezAvansa").SectionFormat.EnableSuppress = False
'                Case enmSumaRacuna.Sa_Avansom
'                    insReport.ReportDefinition.Sections().Item("rFooterSaAvansom").SectionFormat.EnableSuppress = False
'            End Select

'            'stalni podaci na kraju računa će biti prikazani samo ukoliko je nešto uneto u prvi red
'            If fakt_futer1 <> "" Then
'                insReport.ReportDefinition.Sections().Item("rFooterStalniPodaci").SectionFormat.EnableSuppress = False
'            End If

'            'dodeljivanje parametara iz modFirma
'            insReport.SetParameterValue("logo", fakt_slika)
'            insReport.SetParameterValue("firma", fakt_firma)
'            insReport.SetParameterValue("adresa", fakt_adresa)

'            insReport.SetParameterValue("tel", fakt_telefon)
'            insReport.SetParameterValue("ziroRacun", "Tekući račun: " & modFirma.tekuciRacun)
'            insReport.SetParameterValue("email", modFirma.opt_email)
'            insReport.SetParameterValue("web", modFirma.opt_web)

'            'insReport.SetParameterValue("txtNapomena1", "Broj fiskalnog isečka: " & reader("Fkz_AdresaIsporuke"))
'            insReport.SetParameterValue("pib", modFirma.pib)
'            insReport.SetParameterValue("mb", modFirma.maticniBroj)

'            Dim strBrojRacuna As String     'treba mi da upisem pozovite se na broj

'            'dodeljivanje parametara iz tabele FaktZ
'            reader = modOleDb.vratiDataReader("SELECT * FROM FaktZ WHERE Fkz_id = " & id)
'            If reader.Read() Then
'                'više ne dodeljujem ovaj parametar već samo inicijalizujem promenljivu broj računa, pa je dodeljujem zajedno u parametru
'                ' "racun/otpremnica"
'                'insReport.SetParameterValue("Fkz_Broj", reader("Fkz_Broj") & " ")
'                strBrojRacuna = reader("Fkz_Broj")
'                brojRacuna = reader("Fkz_Broj")
'                'insReport.SetParameterValue("txtNapomena1", "") '7/2021
'                insReport.SetParameterValue("txtNapomena1", "") '7/2021
'                insReport.SetParameterValue("Fkz_Datum", reader("Fkz_Datum") & " ")
'                insReport.SetParameterValue("Fkz_Valuta", reader("Fkz_Valuta") & " ")
'                insReport.SetParameterValue("Fkz_DatumOtp", reader("Fkz_DatumOtp") & " ")
'                insReport.SetParameterValue("Fkz_BrOtp", reader("Fkz_BrOtp") & " ")
'                insReport.SetParameterValue("Fkz_MestoIsporuke", reader("Fkz_MestoIsporuke") & " ")
'                'insReport.SetParameterValue("Fkz_AdresaIsporuke", "Broj fiskalnog isečka: " & reader("Fkz_AdresaIsporuke") & " ")
'                insReport.SetParameterValue("Fkz_AdresaIsporuke", reader("Fkz_AdresaIsporuke") & " ")
'                Fkz_Kon_201 = reader("Fkz_Kon_201")
'                Fkz_FkzN_Sifra = reader("Fkz_FkzN_Sifra")
'                Fkz_FkzU_Sifra = reader("Fkz_FkzU_Sifra")
'                insReport.SetParameterValue("Fkz_OsnovicaAO", reader("Fkz_OsnovicaAO") & " ")
'                insReport.SetParameterValue("Fkz_PdvAO", reader("Fkz_PdvAO") & " ")
'                insReport.SetParameterValue("Fkz_OsnovicaAP", reader("Fkz_OsnovicaAP") & " ")
'                insReport.SetParameterValue("Fkz_PdvAP", reader("Fkz_PdvAP") & " ")
'                insReport.SetParameterValue("Fkz_Avans", reader("Fkz_Avans") & " ")
'                insReport.SetParameterValue("Fkz_Iznos", reader("Fkz_Iznos") & " ")
'                insReport.SetParameterValue("Fkz_Rabat", reader("Fkz_Rabat") & " ")
'            End If
'            modOleDb.zatvoriDataReader(reader)

'            Select Case racunOtpremnica
'                'ako je izabran račun, tada parametar uzimam iz modRacun.fakt_racun_otpremnica
'                Case enmRacunOtpremnica.Racun
'                    insReport.SetParameterValue("racun/otpremnica", fakt_racun_otpremnica & ": " & brojRacuna)
'                'ako je izbrana otpremnica tada parametru dodeljujem rec Otpremnica
'                Case enmRacunOtpremnica.Otpremnica
'                    insReport.SetParameterValue("racun/otpremnica", "Otpremnica: " & brojRacuna)
'            End Select

'            'dodeljivanje parametara iz modRacun
'            insReport.SetParameterValue("fakt_mesto", fakt_mesto & "")
'            insReport.SetParameterValue("fakt_potpislevo", fakt_potpislevo & "")
'            insReport.SetParameterValue("fakt_potpislev2", fakt_potpislev2 & "")

'            insReport.SetParameterValue("fakt_potpissredina", fakt_potpissredina & "")
'            insReport.SetParameterValue("fakt_potpissredin2", fakt_potpissredin2 & "")

'            insReport.SetParameterValue("fakt_potpisdesno", fakt_potpisdesno & "")
'            insReport.SetParameterValue("fakt_potpisdesn2", fakt_potpisdesn2 & "")

'            'stalni podaci na dnu računa
'            insReport.SetParameterValue("fakt_futer1", fakt_futer1 & "")
'            insReport.SetParameterValue("fakt_futer2", fakt_futer2 & "")
'            insReport.SetParameterValue("fakt_futer3", fakt_futer3 & "")
'            insReport.SetParameterValue("fakt_futer4", fakt_futer4 & "")

'            'dodeljivanje parametara o firmi primaocu iz tabele KontaR
'            'secem prva 4(0,1,2,3) sto je "2040" da bi dobio samo sifru u KontaR
'            If Fkz_Kon_201.Length > 4 Then
'                Kor_Sifra = Fkz_Kon_201.Substring(4)
'                Dim strKontaR As String
'                strKontaR = "SELECT * 
'                                FROM KontaR 
'                                LEFT JOIN Konta ON Konta.Kon_Link_id = KontaR.Kor_id
'                                WHERE Kon_Sifra = '" & Fkz_Kon_201 & "' "
'                reader = modOleDb.vratiDataReader(strKontaR)
'                'reader = modOleDb.vratiDataReader("SELECT * FROM KontaR WHERE Kor_Sifra = '" & Kor_Sifra & "' AND Kor_VrstaLookUp = 'A'")
'                If reader.Read() Then
'                    insReport.SetParameterValue("pibFirmaPrimalac", reader("Kor_Pib") & " ")

'                    insReport.SetParameterValue("MatBrFirmaPrimalac", reader("Kor_MatBr") & " ")
'                    insReport.SetParameterValue("firmaPrimalac", reader("Kor_Opis") & " ")
'                    insReport.SetParameterValue("adresaFirmaPrimalac", reader("Kor_Adresa") & " ")
'                    insReport.SetParameterValue("mestoFirmaPrimalac", reader("Kor_PBroj") & " " & reader("Kor_Mesto") & " ")
'                End If
'                modOleDb.zatvoriDataReader(reader)
'            Else
'            End If

'            'opis napomene
'            insReport.SetParameterValue("FkzN_Sifra", vratiJedVrednost("FaktZNapomene", "FkzN_Opis", "FkzN_Sifra", Fkz_FkzN_Sifra) & " ")

'            'opis uslova prodaje
'            insReport.SetParameterValue("FkzU_Sifra", vratiJedVrednost("FaktZUslovi", "FkzU_Opis", "FkzU_Sifra", Fkz_FkzU_Sifra) & " ")
'            'insReport.SetParameterValue("FkzU_Sifra", " ")

'            'kontrolni broj
'            'insReport.SetParameterValue("kontrolniBroj", KontrolniBroj(97, brojRacuna))
'            insReport.SetParameterValue("kontrolniBroj", strBrojRacuna)

'            'dodeljujem instancu komponenti za prikaz report i pokrecem izvestaj
'            Dim frmPrikaz As New frmAClrPrikaz
'            frmPrikaz.CrystalReportViewer1.ReportSource = insReport
'            frmPrikaz.CrystalReportViewer1.Refresh()

'            'prikazuje se ogvorarajući format u zavisnoti od poslatog načina štampe/exporta
'            Select Case stampaExport
'                Case enmStampaExport.Print
'                    frmPrikaz.ShowDialog()
'                Case enmStampaExport.Pdf
'                    If pdfNaziv = "" Then
'                        insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\grid.pdf")
'                        If boolPrivju Then
'                            System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.pdf")
'                        End If
'                    Else
'                        insReport.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + "\Export\" & pdfNaziv)
'                        If boolPrivju Then
'                            System.Diagnostics.Process.Start(Application.StartupPath & "\Export\" & pdfNaziv)
'                        End If

'                    End If
'                Case enmStampaExport.Word
'                    insReport.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\Export\grid.doc")
'                    System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.doc")
'                Case enmStampaExport.Excel
'                    insReport.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\Export\grid.xls")
'                    System.Diagnostics.Process.Start(Application.StartupPath & "\Export\grid.xls")
'                Case Else
'                    MsgBox("Novi način štampe/exporta nije programiran.")
'            End Select
'        Catch ex As Exception
'            MsgBox("Štampanje računa nije uspelo: " & ex.Message)
'        End Try

'    End Sub

'#End Region

'    ''' <summary>
'    ''' F-ja konvertuje mm u Twips. Twips je jedinica mere koju koristi Crystal Report.
'    ''' </summary>
'    Public Function mmToTwips(d As Decimal) As Decimal
'		Return d * 56.7
'	End Function

'    ''' <summary>
'    ''' F-ja konvertuje Twips u mm. Twips je jedinica mere koju koristi Crystal Report.
'    ''' </summary>
'    Public Function twipsToMm(d As Decimal) As Decimal
'		Return d / 56.7
'	End Function

'    ''' <summary>
'    ''' F-ja vraca PageMargins objekat koji se dodeljuje CR. Vrednost margina uzima iz modula modPrint.
'    ''' </summary>
'    Public Function vratiMargine() As PageMargins
'		Dim margine = New PageMargins()
'		Try
'            margine.leftMargin = If(Prn_MarLevo <> "", Prn_MarLevo, mmToTwips(5))
'            margine.rightMargin = If(Prn_MarDesno <> "", Prn_MarDesno, mmToTwips(5))
'            margine.topMargin = If(Prn_MarVrh <> "", Prn_MarVrh, mmToTwips(5))
'            margine.bottomMargin = If(Prn_MarDno <> "", Prn_MarDno, mmToTwips(5))
'            Return margine
'		Catch ex As Exception
'			MsgBox("Margine nisu dobre. Proverite ih u prozoru Podešavanja -> Printer")
'		End Try
'	End Function


'End Module