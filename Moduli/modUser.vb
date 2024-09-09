Imports System.Data.OleDb

Public Structure loginStruktura
    Public uspesno_prijavljivanje As Boolean

    Public sifra_korisnika As String
    Public ime_korisnika As String
    Public prezime_korisnika As String
    Public nivo_korisnika As String 'enmNivoKorisnika
    Public password_korisnika As String

    ''' <summary>
    ''' resetuje vrednosti strukture
    ''' </summary>
    Public Sub resetujVrednosti()
        uspesno_prijavljivanje = False

        sifra_korisnika = ""
        ime_korisnika = ""
        prezime_korisnika = ""
        nivo_korisnika = "" 'False
        password_korisnika = ""
    End Sub
End Structure

Public Enum enmVrstaProjekta
    FiskalnaKasa = 1
    Restoran
    Vozila
    Delovodnik
    Pos
    Efaktura
End Enum
Module modUser

    ''' <summary>
    ''' struktura u kojoj se čuvaju informacije o trenutno ulogovanom korisniku(konobaru, kasiru)
    ''' </summary>
    Public login_info As loginStruktura

    ''' <summary>
    ''' promenljiva koja definiše da li je pos projekat restoran ili fiskalna kasa jer će projekti koristiti iste f-je i logiku
    ''' </summary>
    Public vrsta_projekta As enmVrstaProjekta

    'da li korisnici postoje
    Public bool_user As Boolean = False

    'promenjiva u koju ucitavam pozadinu za login
    Public imgLogin As Image

    '''' <summary>
    '''' programska šifra
    '''' </summary>
    'Public sifra As String = ""
    '''' <summary>
    '''' korisnička šifra
    '''' </summary>
    'Public password As String = ""
    'Public ime As String = ""
    'Public prezime As String = ""
    'Public nivo As String = ""


    ''' <summary>
    ''' f-ja proverava da li u bazi postoji tabela Users i inicijalizuje promenljivu bool_user
    ''' </summary>
    Public Sub postojeKorisnici()
        Dim schema_table As DataTable = Nothing
        schema_table = adoCN.GetSchema("Tables")
        Dim table_view As DataView = schema_table.DefaultView
        table_view.RowFilter = String.Format("TABLE_NAME='{0}'", "Users")
        '0 je false, sve veće od 0 je true
        bool_user = table_view.Count
        If bool_user = False Then Exit Sub

        'ukoliko ima tabela Users, proveravam ima li Usera u njoj
        Dim strBrojUsera As String = "SELECT * FROM Users WHERE Usr_Sifra <> '/' "
        bool_user = modOleDb.RecordCountB(strBrojUsera)

    End Sub

    Public Sub resetujVrednosti()
        'sifra = ""
        'password = ""
        'ime = ""
        'prezime = ""
        'nivo = ""

        login_info.resetujVrednosti()

    End Sub

    ''' <summary>
    ''' f-ja neće raditi dok se ne uklopi sa tabelom Users koju treba napraviti
    ''' </summary>
    ''' <param name="password"></param>
    ''' <returns></returns>
    Public Function inicijalizujPodatkeOKorisniku(password As String) As Boolean
        Dim upit As String = String.Format("SELECT * FROM Users WHERE Usr_Password = '{0}'", password)
        Dim reader As OleDbDataReader = Nothing
        Try
            reader = modOleDb.vratiDataReader(upit)
            If (reader.Read()) Then
                'sifra = reader("Usr_Sifra")
                'password = reader("Usr_Password")
                'ime = reader("Usr_Ime")
                'prezime = reader("Usr_Prezime")
                'nivo = reader("Usr_Nivo")

                login_info.sifra_korisnika = reader("Usr_Sifra")
                login_info.ime_korisnika = reader("Usr_Ime")
                login_info.prezime_korisnika = reader("Usr_Prezime")
                login_info.nivo_korisnika = reader("Usr_Nivo")
                login_info.password_korisnika = reader("Usr_Password")
                login_info.uspesno_prijavljivanje = True
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Dim msg As New MojMsgBox(String.Format("Greška prilikom inicijalizacija podataka o korisniku: {0}", ex.Message), MojMsgBox.enmVidljivaDugmad.Potvrdi,
                                     "Informacije o korisnicima", MojMsgBox.enmImgMsgBox.Critical)
            msg.ShowDialog()
            Return False
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Function

    Public Sub skloniDugmeIzmeniBrisi(forma As Form)

        If login_info.nivo_korisnika = "ZabIzmeniBrisi" Then
            Dim izmeni As Button = forma.Controls.Find("btnIzmeni", True).FirstOrDefault()
            izmeni.Visible = False
            Dim brisi As Button = forma.Controls.Find("btnBrisi", True).FirstOrDefault()
            brisi.Visible = False
        End If

    End Sub


    Public Sub skloniZabranjenuDugmad(forma As Form)
        Dim upisi As Button = forma.Controls.Find("btnUpisi", True).FirstOrDefault()
        Dim izmeni As Button = forma.Controls.Find("btnIzmeni", True).FirstOrDefault()
        Dim brisi As Button = forma.Controls.Find("btnBrisi", True).FirstOrDefault()
        Dim listaDugmadi As New List(Of Button)({upisi, izmeni, brisi})

        For Each button In listaDugmadi
            If vratiJedVrednostUpit("SELECT Dzv_Dozvoljeno FROM Dozvoljeno 
                                WHERE Dzv_Nivo = '" & login_info.nivo_korisnika & "' 
                                AND Dzv_Objekat = '" & forma.Name & button.Name & "'") = 1 Then
                button.Visible = False
            End If
        Next

    End Sub


    Public Sub skloniZabranjeneMenuItem(ms As MenuStrip)
        Dim myItems As List(Of ToolStripMenuItem) = GetItems(ms)

        For Each item In myItems
            If vratiJedVrednostUpit("SELECT Dzv_Dozvoljeno FROM Dozvoljeno 
                                WHERE Dzv_Nivo = '" & login_info.nivo_korisnika & "' 
                                AND Dzv_Objekat = '" & item.Name & "'") = 1 Then
                item.Visible = False
            End If
        Next

    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="menuStrip"></param>
    ''' <returns></returns>
    Public Function GetItems(ByVal menuStrip As MenuStrip) As List(Of ToolStripMenuItem)
        Dim myItems As List(Of ToolStripMenuItem) = New List(Of ToolStripMenuItem)()

        For Each i As ToolStripMenuItem In menuStrip.Items
            GetMenuItems(i, myItems)
        Next

        Return myItems
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="items"></param>
    Private Sub GetMenuItems(ByVal item As ToolStripMenuItem, ByVal items As List(Of ToolStripMenuItem))
        items.Add(item)

        For Each i As ToolStripItem In item.DropDownItems

            If TypeOf i Is ToolStripMenuItem Then
                GetMenuItems(CType(i, ToolStripMenuItem), items)
            End If
        Next
    End Sub

End Module
