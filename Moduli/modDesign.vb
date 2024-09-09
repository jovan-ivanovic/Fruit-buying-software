Imports System.Data.OleDb

Module modDesign


    'Public iniFile As String
    Private strTable As String = "Servis"

    Public bojaTb As String
    Public bojaAktivnogTb As String
    Public bojaPozadineProzora As String
    Public bojaNaslovneLinije As String
    Public bojaSelReda As String
    Public bojaFontaSelReda As String
    Public fontMonitorVrsta As String
    Public fontMonitorVelicina As String
    Public fontPrinterVrsta As String
    Public fontPrinterVelicina As String
    'Dodao sam jos neke promenljive za dizajn grida. Ali ih sad ne čuvam u bazi već u ini fajlu u sekcije Design
    Public gridBojaFontaNaslovneLinije As String    'boja fonta u naslovu
    Public gridBojaBackground As String             'boja koja se vidi ispod nepopunjenog dela grida desno ili dole u zavisnosti koliko ima redova
    Public gridBojaRowsBack As String               'boja svakog neparnog reda u gridu
    Public gridBojaRowsBackAlternting As String     'boja svakog parnog reda
    Public gridForeColor As String                  'boja teksta u gridu


    ''' <summary>
    ''' F-ja upisuje promeljive modDesign u bazu.
    ''' </summary>
    Public Sub upisiUBazu()
        Dim strSQL As String = "SELECT TOP 1 * FROM " & strTable
        Dim adapter As New OleDbDataAdapter(strSQL, adoCN)
        Dim tabela As New DataTable
        Dim red As DataRow
        Dim builder As New OleDbCommandBuilder(adapter)
        Try
            'adapter puni tabelu
            adapter.Fill(tabela)
            'izima se prvi red iz tabele
            red = tabela.Rows(0)
            'update odgovarajuca polja u redu
            red.Item("Srv_BojaTexBoxaNeaktiv") = bojaTb
            red.Item("Srv_BojaTexBoxaAktiv") = bojaAktivnogTb
            red.Item("Srv_BojaPozadine") = bojaPozadineProzora
            red.Item("Srv_GridNaslov") = bojaNaslovneLinije
            red.Item("Srv_GridSel") = bojaSelReda
            red.Item("Srv_GridFontSel") = bojaFontaSelReda
            red.Item("Srv_FontMonitor") = fontMonitorVrsta
            red.Item("Srv_FontMonitorSize") = fontMonitorVelicina
            red.Item("Srv_FontPrinter") = fontPrinterVrsta
            red.Item("Srv_FontPrinterSize") = fontPrinterVelicina
            'update-ujem tabelu u bazi
            adapter.Update(tabela)
        Catch ex As Exception
            MsgBox("Greška prilikom upisa dizajna u bazu: " + ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' F-ja inicijalizuje promenljive modDesign iz baze.
    ''' </summary>
    Public Sub citajIzBaze()
        Dim strSQL As String = "SELECT TOP 1 * FROM " & strTable
        Dim reader As OleDbDataReader = Nothing
        Try
            reader = modOleDb.vratiDataReader(strSQL)

            If (reader.Read()) Then
                'inicijalizujem promenljive modDesign iz tabele
                bojaTb = reader("Srv_BojaTexBoxaNeaktiv")
                bojaAktivnogTb = reader("Srv_BojaTexBoxaAktiv")
                bojaPozadineProzora = reader("Srv_BojaPozadine")
                bojaNaslovneLinije = reader("Srv_GridNaslov")
                bojaSelReda = reader("Srv_GridSel")
                bojaFontaSelReda = reader("Srv_GridFontSel")
                fontMonitorVrsta = reader("Srv_FontMonitor")
                fontMonitorVelicina = reader("Srv_FontMonitorSize")
                fontPrinterVrsta = reader("Srv_FontPrinter")
                fontPrinterVelicina = reader("Srv_FontPrinterSize")
            Else
                MsgBox("Nema dizajna u bazi!")
            End If

        Catch ex As Exception
            MsgBox("Greška prilikom čitanja dizajna iz baze: " + ex.Message)
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Sub


    Public Sub upisiUIni()
        Try
            WriteIni(iniFile, "Design", "gridBojaFontaNaslovneLinije", gridBojaFontaNaslovneLinije)
            WriteIni(iniFile, "Design", "gridBojaBackground", gridBojaBackground)
            WriteIni(iniFile, "Design", "gridBojaRowsBack", gridBojaRowsBack)
            WriteIni(iniFile, "Design", "gridBojaRowsBackAlternting", gridBojaRowsBackAlternting)
            WriteIni(iniFile, "Design", "gridForeColor", gridForeColor)
        Catch ex As Exception
            MsgBox("Greška prilikom upisa u ini file: " & ex.Message)
        End Try
    End Sub

    Public Sub citajIzIni()
        Try
            gridBojaFontaNaslovneLinije = ReadIni(iniFile, "Design", "gridBojaFontaNaslovneLinije", "&HFFFFFF")
            gridBojaBackground = ReadIni(iniFile, "Design", "gridBojaBackground", "&HFFFFFF")
            gridBojaRowsBack = ReadIni(iniFile, "Design", "gridBojaRowsBack", "&HFFFFFF")
            gridBojaRowsBackAlternting = ReadIni(iniFile, "Design", "gridBojaRowsBackAlternting", "&HFFFFFA")
            gridForeColor = ReadIni(iniFile, "Design", "gridForeColor", "&H000000")
        Catch ex As Exception
            MsgBox("Greška prilikom čitanja iz ini file: " & ex.Message)
        End Try
    End Sub

End Module
