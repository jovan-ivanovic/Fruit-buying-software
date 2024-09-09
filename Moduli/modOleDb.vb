Imports System.Data.OleDb
Imports System.IO

Module modOleDb

    Dim databaseName As String = DatabaseConfig.GetDatabaseName()

    Public Class DatabaseConfig
        Public Shared Function GetDatabaseName() As String
            Dim exeDirectory As String = AppDomain.CurrentDomain.BaseDirectory
            Dim knjFiles As String() = Directory.GetFiles(exeDirectory, "knj_*")

            ' Pronalaženje prve datoteke koja odgovara šablonu
            If knjFiles.Length > 0 Then
                Dim knjFilePath As String = knjFiles(0)
                ' Čitanje imena baze iz ini datoteke
                Dim databaseName As String = ReadDatabaseNameFromIni(knjFilePath)
                Return databaseName
            Else
                Throw New FileNotFoundException("Nije pronađena nijedna datoteka koja počinje sa 'knj_'.")
            End If
        End Function

        Private Shared Function ReadDatabaseNameFromIni(iniFilePath As String) As String
            Dim databaseName As String = ""

            If File.Exists(iniFilePath) Then
                Dim lines As String() = File.ReadAllLines(iniFilePath)
                For Each line As String In lines
                    If line.StartsWith("baza=") Then
                        databaseName = line.Substring("baza=".Length).Trim()
                        Exit For
                    End If
                Next
            Else
                Throw New FileNotFoundException("INI datoteka nije pronađena.")
            End If

            If String.IsNullOrEmpty(databaseName) Then
                Throw New Exception("Naziv baze nije pronađen u INI datoteci.")
            End If

            Return databaseName
        End Function
    End Class

    Public Sub konektujSe(databaseName As String, Optional password As String = Nothing)

        zatvoriKonekciju(adoCN)

        adoCN.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseName};"


        adoCN.Open()




    End Sub


    Public Function vratiJedVrednostUpit(upit As String) As Object
        Dim vrednost As Object = Nothing
        Dim komanda As New OleDbCommand(upit, adoCN)
        Try
            vrednost = komanda.ExecuteScalar()
        Catch ex As Exception
            MsgBox("Greška prilikom vraćanja jedinične vrednosti iz baze na osnovu upita: " & ex.Message)

            Throw
        End Try
        'vraća se jedinična vrednost iz baze , a ukoliko dodje do greške onda će biti vraćeno Nothing.
        If IsDBNull(vrednost) Then vrednost = ""
        Return vrednost
    End Function

    Public Function vratiDataReader(upit As String) As OleDbDataReader
        'Dim databaseName As String = DatabaseConfig.GetDatabaseName()
        konektujSe(databaseName)

        Dim reader As OleDbDataReader = Nothing
        Dim komanda As New OleDbCommand(upit, adoCN)
        Try
            reader = komanda.ExecuteReader()
        Catch ex As Exception
            MsgBox("Greška prilikom vraćanja data readera: " & ex.Message)

            Throw
        End Try
        'vraća se DataReader ukoliko je inicijalizovan, a ukoliko dodje do greške onda će biti vraćeno Nothing.
        Return reader
    End Function

    Public Function izvrsiKomandu(upit As String) As Integer
        Dim broj As Integer = 0
        Dim komanda As New OleDbCommand(upit, adoCN)
        Try
            broj = komanda.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Greška prilikom izvršavanja funkcije izvrsiKomandu: " & vbCrLf & "sql upit: " & upit & vbCrLf & vbCrLf & ex.Message, MsgBoxResult.Ok, "SQL komonda nije izvršena")

            Throw
        End Try

        'vraća se broj redova koji je obuhvaćen komandom, a ukoliko dodje do greške onda će biti vraćeno 0.
        Return broj
    End Function

    ''' <summary>
    ''' f-ja zatvara poslatu konekciju 
    ''' </summary>
    Public Sub zatvoriKonekciju(konekcija As OleDbConnection)
        If Not IsNothing(konekcija) And konekcija.State = ConnectionState.Open Then
            konekcija.Close()
        End If
    End Sub

    'Public Sub PrikaziPodatkeUTabeli(tableName As String, dgv As DataGridView)
    '    Dim query As String = "SELECT * FROM " & tableName

    '    Using adapter As New OleDbDataAdapter(query, connection)
    '        Dim dataSet As New DataSet()

    '        Try
    '            adapter.Fill(dataSet, tableName)
    '            dgv.DataSource = dataSet.Tables(tableName)
    '        Catch ex As Exception
    '            MessageBox.Show("Greška prilikom prikazivanja podataka: " & ex.Message)
    '        End Try
    '    End Using
    'End Sub

    Public Sub zatvoriDataReader(reader As OleDbDataReader)
        If Not IsNothing(reader) Then
            If Not (reader.IsClosed) Then
                reader.Close()
            End If
        End If
    End Sub

    Public Function napuniTabelu(upit As String) As DataTable
        Dim tabela As New DataTable
        Dim adapter As New OleDbDataAdapter(upit, adoCN)
        Try
            adapter.Fill(tabela)
        Catch ex As Exception
            MsgBox("Greška prilikom punjenja tabele: " & vbCrLf & vbCrLf & ex.Message)

        End Try
        'vraća se tabela ukoliko je inicijalizovana, a ukoliko dodje do greške onda će biti vraćeno Nothing.
        Return tabela
    End Function

    ''' <summary>
    ''' Funkcija prosledjuje broj slogova u public konekciji adoCN za poslati strSql
    ''' </summary>
    ''' <param name="strSql">Sql uput</param>
    ''' <returns></returns>
    Public Function RecordCountB(strSql As String) As Long
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        Try
            oledbAdapter = New OleDbDataAdapter(strSql, adoCN)
            oledbAdapter.Fill(ds, "OLEDB Temp Table")
            oledbAdapter.Dispose()

            Return ds.Tables(0).Rows.Count

        Catch ex As Exception
            'modLOG.upisiLog("Greška prilikom izvršavanja " & MethodBase.GetCurrentMethod().Name & ": " & vbCrLf & ex.ToString, fileErrorLog)
            Return 0
        End Try
        ds.Dispose()
    End Function

    ''' <summary>
    ''' f-ja formira insert upit na osnovu poslatih parametra i izvršava ga na bazi
    ''' </summary>
    ''' <param name="strTabela">tabela</param>
    ''' <param name="kolone">kolone za insert</param>
    ''' <param name="vrednosti">vrednosti za insert</param>
    ''' <param name="strWhere">Naziv id kolone</param>
    ''' <returns></returns>
    Public Function IzmeniRedUTabl_2_where(strTabela As String, kolone() As String, vrednosti() As Object, strWhere As String) As Boolean
        Dim strSQL As String = "SELECT * FROM " & strTabela & " WHERE " & strWhere
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
            For i = 0 To kolone.Length - 1
                red(kolone(i)) = vrednosti(i)
            Next
            'update-ujem tabelu u bazi
            adapter.Update(tabela)
            Return True
        Catch ex As Exception
            MsgBox("Greška prilikom izmene podataka: " + ex.Message, 0, "Izmena podataka tabela " & strTabela)
            'modLOG.upisiLog("Greška prilikom izvršavanja " & MethodBase.GetCurrentMethod().Name & ": " & vbCrLf & ex.ToString, fileErrorLog)
        End Try
    End Function

    ''' <summary>
    ''' f-ja formira insert upit na osnovu poslatih parametra i izvršava ga na bazi
    ''' </summary>
    ''' <param name="strTabela">tabela</param>
    ''' <param name="kolone">kolone za insert</param>
    ''' <param name="vrednosti">vrednosti za insert</param>
    ''' <returns></returns>
    Public Function DodajRedUTabl_2(strTabela As String, kolone() As String, vrednosti() As Object) As Boolean
        'VAZNO!!! pri upisu novog sloga uzimaju se samo kolone u koje hoćemo da dodamo vrednost.
        'Samo tako će kolone u koje ne dodajemo vrednost dobiti DEFAULT vrednosti definisanu u bazi,
        'a i brže se izvršava kada se povlače samo potrebni podaci iz baze.
        Dim j As Integer
        Dim strKolone As String = ""

        For j = 0 To kolone.Length - 1
            strKolone = strKolone & kolone(j) & " , "
        Next
        strKolone = strKolone.Remove(strKolone.LastIndexOf(","))
        'kolone = kolone & " , Ars_JedTezina, Ars_Tezina, Ars_KomadaIzSipke, Ars_BrSipki, Ars_OstatakSipke, Ars_TezinaOstatka"

        Dim strSQL As String = "SELECT " & strKolone & " FROM " + strTabela
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
            For i = 0 To kolone.Length - 1
                red(kolone(i)) = vrednosti(i)
            Next
            'tabeli dodeljujem red i update-ujem tabelu u bazi
            tabela.Rows.Add(red)
            adapter.Update(tabela)

            Return True

        Catch ex As Exception
            MsgBox("Greška prilikom upisa podataka: " + ex.Message, 0, "Tabela " & strTabela)
            'modLOG.upisiLog("Greška prilikom izvršavanja " & MethodBase.GetCurrentMethod().Name & ": " & vbCrLf & ex.ToString, fileErrorLog)
            Return False
        End Try
    End Function

    Public Function sortUpit(strKolona As String) As String
        Select Case modFirma.koja_baza
            Case enmBaza.Access, enmBaza.AccessPass
                Return " VAL (" & strKolona & ") "
            Case enmBaza.Server, enmBaza.ServerRemote
                'Return " TRY_CONVERT(int, " & strKolona & ") "
                'sortira sifre i slovne i brojne
                Return " TRY_CONVERT(int, " & strKolona & "), " & strKolona & " "
            Case Else
                Return ""
        End Select
    End Function

End Module
