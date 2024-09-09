Imports System.Data.OleDb
Module modMain
    Public adoCN As New OleDbConnection
    Public strBaza As String
    Public tackice As String = ". . . . . . . . . . . . . . . . ."
    'globalna promenljiva za formu IZBOR
    Public glbPrenesi As String = ""
    'globalna promeljiva u kojoj se cuva sifra pri prelasku sa forme zaglavlje na formu stavke
    Public strLinkPub As String = ""
    'globalna promeljiva tj. niz koji se dobija iz input prozora
    Public inputProzorNiz() As String = Nothing

    Public Function vratiSledecuSifru(strTabela As String, strKolona As String) As String
        Dim maxBr As Integer = 0
        Dim maxDuz As Integer = 0
        Dim slBr As String = ""
        Dim pocString As String = ""
        Dim reader As OleDbDataReader = Nothing
        Dim strSQL As String = "SELECT " & strKolona & " FROM " & strTabela
        Dim command As New OleDbCommand(strSQL, adoCN)
        Dim trBroj As Integer
        Try
            reader = command.ExecuteReader()
            While reader.Read()
                'trBroj je out parametar i on se inicijalizuje ako konverzija uspe i tada f-ja tryParse vraća true i ulazi se u if blok
                'a ako konverzija ne uspe onda se ne inicijalizuje trBroj, f-ja tryParse vraća false i ne ulazi se u if blok
                If Int32.TryParse(reader(0), trBroj) Then
                    If trBroj > maxBr Then
                        maxBr = trBroj
                        maxDuz = Convert.ToString(reader(0)).Length
                    End If
                End If
            End While
            'ako ništa nije našao vraćam 001, a ako jeste izračunavam sledeći broj
            If maxBr <> 0 Then
                slBr = Convert.ToString(maxBr + 1)
                If (slBr.Length >= maxDuz) Then
                    Return slBr
                Else
                    pocString = "000000".Substring(0, maxDuz - slBr.Length)
                    Return pocString & slBr
                End If
            Else
                Return "001"
            End If
        Catch ex As Exception
            'MsgBox("Greska prilikom vracanja nove sifre dokumenta: " & ex.Message)
            Return ""
        Finally
            If Not IsNothing(reader) Then
                If Not (reader.IsClosed) Then
                    reader.Close()
                End If
            End If
        End Try
    End Function

    ''' <summary>
    ''' poziva formu za izbor sa novim načinom pretrage podataka, trebalo bi da zameni staru formu 
    ''' </summary>
    Public Sub kreirajFormuZaIzbor(strUpit As String, nazKolona As String(), sirKolona As Integer(), naslov As String, Optional text As String = "")
        Dim izbor = New frmAIzborDataTable(strUpit, nazKolona, sirKolona, naslov, text)
        izbor.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        izbor.ShowDialog()
    End Sub

    Public Function vratiSledecuSifruUslov(strTabela As String, strKolona As String, strKolonaUslov As String, strVrednostUslov As String, Optional saVodecimNulama As Integer = 2) As String
        Dim maxBr As Integer = 0
        Dim maxDuz As Integer = 0
        Dim slBr As String = ""
        Dim pocString As String = ""
        Dim reader As OleDbDataReader = Nothing
        Dim strSQL As String = "SELECT " & strKolona & " FROM " & strTabela &
              " WHERE " & strKolonaUslov & " = '" & strVrednostUslov & "'"
        Dim command As New OleDbCommand(strSQL, adoCN)
        Dim trBroj As Integer
        Try
            reader = command.ExecuteReader()
            While reader.Read()
                'trBroj je out parametar i on se inicijalizuje ako konverzija uspe i tada f-ja tryParse vraća true i ulazi se u if blok
                'a ako konverzija ne uspe onda se ne inicijalizuje trBroj, f-ja tryParse vraća false i ne ulazi se u if blok
                If Int32.TryParse(reader(0), trBroj) Then
                    If trBroj > maxBr Then
                        maxBr = trBroj
                        maxDuz = Convert.ToString(reader(0)).Length
                    End If
                End If
            End While
            'ako ništa nije našao vraćam 001, a ako jeste izračunavam sledeći broj
            If maxBr <> 0 Then
                slBr = Convert.ToString(maxBr + 1)
                If (slBr.Length >= maxDuz) Then
                    Return slBr
                Else
                    pocString = "000000".Substring(0, maxDuz - slBr.Length)
                    Return pocString & slBr
                End If
            Else
                If saVodecimNulama = 0 Then
                    Return "1"
                ElseIf saVodecimNulama = 1 Then
                    Return "01"
                ElseIf saVodecimNulama = 2 Then
                    Return "001"
                Else
                    Return "0001"
                End If
            End If
        Catch ex As Exception
            MsgBox("Greska prilikom vracanja nove sifre dokumenta: " & ex.Message)
            Return ""
        Finally
            If Not IsNothing(reader) Then
                If Not (reader.IsClosed) Then
                    reader.Close()
                End If
            End If
        End Try
    End Function

    Public Function vratiSledecuSifruIntUslov(strTabela As String, strKolona As String, strKolonaUslov As String, strVrednostUslov As String) As String
        Dim rez As String = ""
        Dim slBr As String = ""
        Dim pocString As String = ""
        Dim strSql As String

        'Select Case modFirma.koja_baza
        'Case enmBaza.Access
        strSql = $"SELECT TOP 1 {strKolona} FROM {strTabela}
                         WHERE {strKolonaUslov} = '{strVrednostUslov}'
                         ORDER BY VAL({strKolona}) DESC"
        'Case enmBaza.Server
        '    strSql = $"Select TOP 1 {strKolona}  FROM  {strTabela} 
        '             WHERE  {strKolonaUslov} = '{strVrednostUslov}'
        '             ORDER BY CAST(LEFT({strKolona}, Patindex('%[^-.0-9]%', {strKolona} + 'x') - 1) AS INT) DESC"
        '    'ORDER BY CAST({strKolona} AS INT) DESC"
        '             ' ORDER BY CAST({strKolona} AS INT) DESC 
        'Case enmBaza.ServerRemote
        '    strSql = $"Select TOP 1 {strKolona}  FROM  {strTabela} 
        '             WHERE {strKolonaUslov} = '{strVrednostUslov}'
        '             ORDER BY CAST(LEFT({strKolona}, Patindex('%[^-.0-9]%', {strKolona} + 'x') - 1) AS INT) DESC"
        'End Select


        Dim command As New OleDbCommand(strSql, adoCN)

        Try
            'ExecuteScalar vraca samo jednu vrednost iz baze
            rez = command.ExecuteScalar
            If rez <> "" Then
                slBr = Convert.ToString(Convert.ToInt32(rez) + 1)
                Return slBr
            Else
                Return "1"
            End If
        Catch ex As Exception
            MsgBox("Greska prilikom vracanja nove sifre dokumenta: " & ex.Message)
            Return ""
        End Try
    End Function
    'Public Sub kreirajFormuZaPretragu(ByRef forma As Form, ByVal strNazKol As String(),
    '                                 ByVal intBrKol As Integer, ByVal strUpit As String,
    '                                 ByVal strOrder As String, ByVal strFildGrid As String(),
    '                                 strNaslov As String, ByVal intSuma As Integer(),
    '                                 Optional ByRef lblNaslov As ToolStripStatusLabel = Nothing,
    '                                 Optional strGrid As String = "dgvPrikaz",
    '                                 Optional strFildGridOriginal As String() = Nothing)
    '    Dim pretraga As New frmAPretraga
    '    'popunjavam combo boxove, treba ucitati samo datumska polja u izdvoj po datumu
    '    pretraga.cmbPretrazi.Items.Add("")
    '    pretraga.cmbSortiraj.Items.Add("")
    '    pretraga.cmbIzdvojPoDatumu.Items.Add("")
    '    For i As Integer = 2 To intBrKol - 1
    '        pretraga.cmbPretrazi.Items.Add(strNazKol(i))
    '        pretraga.cmbSortiraj.Items.Add(strNazKol(i))
    '        pretraga.cmbIzdvojPoDatumu.Items.Add(strNazKol(i))
    '    Next
    '    pretraga.trenutnaForma = forma
    '    pretraga.strUpitPretraga = strUpit
    '    pretraga.strOrderPretraga = strOrder
    '    pretraga.strFildGridPretraga = strFildGrid
    '    pretraga.strNazKolPretraga = strNazKol
    '    pretraga.strNaslovPretraga = strNaslov
    '    pretraga.intSumaPretraga = intSuma
    '    pretraga.lblNaslovPretraga = lblNaslov
    '    pretraga.strGrid = strGrid

    '    pretraga.strFildGridOriginalPretraga = strFildGridOriginal

    '    'otvara se u centru perent forme i ovo mora da se definise pre poziva ShowDialog() f-je, tj ne moze u Load dogadjaju forme
    '    pretraga.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    '    pretraga.ShowDialog()

    'End Sub

    ''' <summary>
    ''' 10/2021
    ''' poziva formu kao prethodna samo se mora zadati sirina i visina forme. ona iz dizajna je 400 x 400 
    ''' </summary>
    Public Sub kreirajFormuZaIzbor_SirinaVisina(strUpit As String, nazKolona As String(), sirKolona As Integer(),
                                           naslov As String, Optional text As String = "",
                                                Optional sirinaa As Integer = 0, Optional visinaa As Integer = 0)
        Dim izbor = New frmAIzborDataTable(strUpit, nazKolona, sirKolona, naslov, text, sirinaa, visinaa)
        izbor.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        izbor.ShowDialog()
    End Sub

    Public Sub kreirajFormuZaPretraguDataTable(grid As DataGridView, Optional labela As ToolStripStatusLabel = Nothing)
        'Dim pretragaDT As New frmAPretragaDataTable
        'pretragaDT.pretGrid = grid
        'pretragaDT.pretLabel = labela

        'pretragaDT.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        'pretragaDT.ShowDialog()
    End Sub

    Public Sub kreirajFormuZaIzbor_DataTableV2(poslataTabela As DataTable, sirKolona As Integer(),
                                        naslov As String, pocText As String)
        Dim izbor = New frmAIzborDataTable(poslataTabela, sirKolona, naslov, pocText)
        izbor.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        izbor.ShowDialog()
    End Sub

End Module
