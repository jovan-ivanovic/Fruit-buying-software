Imports System.Data.OleDb
Module modGrid

    Public Sub napuniGrid(ByRef dgvPrikaz As DataGridView, ByVal strUpit As String, ByVal strFildGrid As String(), ByVal intBrKol As Integer,
                          Optional ByVal intSuma As Integer() = Nothing, Optional ByVal PrvoBrisi As Boolean = True)
        'If PrvoBrisi Then dgvPrikaz.Rows.Clear()
        Dim reader As OleDbDataReader = Nothing
        Dim noviRed = New String(intBrKol - 1) {}
        Dim rb As Long = 0
        Dim i As Integer
        'decimal niz za izracunavanje sume u onom polju u kome je intSuma = 1
        Dim rezSuma = New Decimal(intBrKol - 1) {}

        Try
            If PrvoBrisi Then dgvPrikaz.Rows.Clear()
            reader = modOleDb.vratiDataReader(strUpit)
            While reader.Read()
                rb = rb + 1
                '23/11
                If IsDBNull(reader(strFildGrid(0))) Then
                    noviRed(0) = ""
                    noviRed(1) = ""
                Else
                    noviRed(0) = reader(strFildGrid(0))
                    noviRed(1) = rb
                End If


                'ako je vrednost procitanog DBNull onda gridu dodeljujem prazan string, a ako vrednost postoji
                'onda gledam koji je tip podataka i u zavisnosti od toga formatiram podatke i dodeljujem gridu, link sa tabelom za 
                'konverziju podataka: https://support.microsoft.com/en-us/kb/320435

                For i = 2 To intBrKol - 1
                    '04/20 ove sume sam stavio ovde, a ne dole gde su kejsovi. ovde moze provera da li je isnumber strFildGrid(i) 
                    'If Not IsNothing(intSuma) Then
                    '    If IsNumeric(reader(strFildGrid(i))) And Not IsDBNull(reader(strFildGrid(i))) Then
                    '        If (intSuma(i) = 1) Then
                    '            rezSuma(i) = rezSuma(i) + reader(strFildGrid(i))
                    '        End If
                    '    End If
                    'End If
                    If IsNothing(strFildGrid(i)) Then
                        'može se desiti da strFildGrid(i) bude nothing onda program krahira pa ga zajebemo
                    ElseIf IsDBNull(reader(strFildGrid(i))) Then
                        noviRed(i) = ""
                    Else
                        Select Case reader(strFildGrid(i)).GetType()
                            'Case GetType(Int16) 'Number: Integer in MS Access
                            '    noviRed(i) = reader(strFildGrid(i))
                            '    dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                'If Not IsNothing(intSuma) Then
                                '    If (intSuma(i) = 1) Then
                                '        rezSuma(i) = rezSuma(i) + reader(strFildGrid(i))
                                '    End If
                                'End If
                            'Case GetType(Int32) 'Autonumber (Long Integer), Number: (Long Integer) in MS Access
                            '    noviRed(i) = reader(strFildGrid(i))
                            Case GetType(String) 'Text, Memo, Hyperlink in MS Access
                                noviRed(i) = reader(strFildGrid(i))
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                            Case GetType(Decimal) 'Currency, Number: Decimal in MS Access
                                noviRed(i) = Format(reader(strFildGrid(i)), "#,##0.00")
                                'noviRed(i) = modABC.vratiDecimalVrednostZaPrikaz(reader(strFildGrid(i)))
                                'noviRed(i) = modABC.vratiDecimalPrikazKolikoImaDecimala(reader(strFildGrid(i)))
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                'izracunavam sumu u onim poljima gde u nizu intSuma stoji 1, al prvo proveravam dal je poslat
                                'opcioni parametar
                                If Not IsNothing(intSuma) Then
                                    If (intSuma(i) = 1) Then
                                        rezSuma(i) = rezSuma(i) + reader(strFildGrid(i))
                                    End If
                                End If
                            Case GetType(Int64), GetType(Int16), GetType(Int32) 'Currency, Number: Decimal in MS Access
                                'noviRed(i) = Format(reader(strFildGrid(i)), "#,##0.00")
                                'noviRed(i) = modABC.vratiIntegerVrednostZaPrikaz(reader(strFildGrid(i)))
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            Case GetType(DateTime) 'Date/Time in MS Access
                                noviRed(i) = Format(reader(strFildGrid(i)), "dd.MM.yyyy")
                                dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            Case Else
                                noviRed(i) = reader(strFildGrid(i))
                        End Select
                    End If
                Next
                dgvPrikaz.Rows.Add(noviRed)
            End While


            'dodeljujem poslednjem redu u gridu izracunate sume ukoliko postoje
            If dgvPrikaz.RowCount > 0 Then
                If Not IsNothing(intSuma) Then
                    For i = 0 To intBrKol - 1
                        If rezSuma(i) <> 0 Then
                            'noviRed(i) = modABC.vratiDecimalVrednostZaPrikaz(rezSuma(i))
                        Else
                            noviRed(i) = ""
                        End If
                    Next
                    dgvPrikaz.Rows.Add(noviRed)
                    'ako postoji suma, vrednosti podesavam da bude crvene
                    dgvPrikaz.Rows(dgvPrikaz.RowCount - 1).DefaultCellStyle.ForeColor = Color.Red
                    dgvPrikaz.Rows(dgvPrikaz.RowCount - 1).DefaultCellStyle.Font = New Font(Control.DefaultFont, FontStyle.Bold)
                End If
            End If
        Catch ex As Exception
            MsgBox("Greška prilikom učitavanja grida: " & ex.Message)
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try

        dgvPrikaz.Columns(0).Visible = False ' kolona ID je sakrivena

        'prikazuje poslednji red dataGrid-a ako u gridu uopšte postoje redovi
        Dim brojRedova As Integer
        brojRedova = dgvPrikaz.Rows.Count
        If brojRedova > 0 Then
            If dgvPrikaz.Rows(brojRedova - 1).Displayed = False Then 'ako posledni red nije prikazan onda ga prikazuje
                dgvPrikaz.FirstDisplayedScrollingRowIndex = brojRedova - 1
            End If
        End If
        'dgvPrikaz.ForeColor = Color.Blue
    End Sub

    Public Sub gridPrviRed(grid As DataGridView, brKolona As Integer, nazKolona() As String, sirKolona() As Integer, strRowWidth As String)
        'Dim sirina_grida As Long = grid.Width
        'Dim poslata_sirina As Long = sirKolona.Sum()
        'Dim odnos = sirina_grida / poslata_sirina
        'odnos = 1

        Dim odnos As Integer = 1

        '02/2020 ovde cu onu promenljivu koju je Ivan uveo  sirKolona() koja se u princiju koristi samo prvi put kad
        'u tabeli Width nije upisanan ta forma odnosno njene sirein sam kreirati
        sirKolona(0) = 55
        For i = 1 To brKolona - 1
            'sirKolona(i) = (sirina_grida - 60) / brKolona - 1
            sirKolona(i) = 100
        Next

        'poziva se f-ja koja čita širine kolona iz baze i ukoliko postoje menja širine kolona poslate f-ji
        'citajSirineKolona(strRowWidth, brKolona, sirKolona)

        ''02/2020 ovde pokusavam da sve kolone stavim u grid prema odnosu
        'odnos = sirina_grida / sirKolona.Sum()

        grid.ColumnCount = brKolona

        With grid.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font(grid.Font, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        End With



        For i = 0 To brKolona - 1
            grid.Columns(i).Name = nazKolona(i)
            If sirKolona.Length > i Then
                grid.Columns(i).Width = sirKolona(i) * odnos
            Else
                grid.Columns(i).Width = 100 * odnos
            End If
            grid.Columns(i).HeaderCell().Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        'poslata_sirina = 0
        'For i = 1 To brKolona - 1
        '    poslata_sirina = poslata_sirina + grid.Columns(i).Width
        'Next
        'odnos = (sirina_grida - 60) / poslata_sirina
        'For i = 1 To brKolona - 1
        '    grid.Columns(i).Width = sirKolona(i) * odnos
        'Next



        'dizajnGrida_Ver1(grid)

        '      grid.ForeColor = ColorTranslator.FromWin32(modDesign.gridForeColor)  ' Color.Brown

        '      grid.GridColor = Color.Black
        '      grid.BackgroundColor = ColorTranslator.FromWin32(modDesign.gridBojaBackground)  'Color.AliceBlue ' Color.LightBlue

        '      grid.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'grid.MultiSelect = False
        'grid.AllowUserToResizeColumns = True

        '      grid.RowsDefaultCellStyle.BackColor = ColorTranslator.FromWin32(modDesign.gridBojaRowsBack)  ' Color.LightGray
        '      grid.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromWin32(modDesign.gridBojaRowsBackAlternting)  ' Color.FloralWhite

        '      'visina header reda, mora prvo da se podesi na disable or enable resizing pa onda da se dodeli visina
        '      grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        'grid.ColumnHeadersHeight = 35

        '      'onemoguceno je da se sa TAB-om ide kroz grid
        '      grid.TabStop = False
        '      'da se ne prikazuje poslednji prazan red
        '      grid.AllowUserToAddRows = False
        'grid.AllowDrop = False
        '      'ne prikazuje prvu kolonu koja se automatski generise
        '      grid.RowHeadersVisible = False
        '      'linije unutar grida
        '      grid.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        '      'sledi bivsa funkcija implementiraj dizajn
        '      grid.EnableHeadersVisualStyles = False 'omoguciti da se promeni boja naslovne linije
        '      grid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromWin32(modDesign.bojaNaslovneLinije)
        '      grid.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromWin32(modDesign.gridBojaFontaNaslovneLinije)
        '      grid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromWin32(modDesign.bojaSelReda)
        'grid.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromWin32(modDesign.bojaFontaSelReda)
        '      'korisnik ne može da menja sadržaj ćelija grida
        '      grid.ReadOnly = True
        ''omogućava multiline ćelije grida i znacajno usporava punjenje grida
        ''grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        'grid.AllowUserToResizeRows = False

    End Sub

    Public Sub dodajRedUGrid(grid As DataGridView, strFildGrid() As String, strUpit As String, strID As String)
        Dim reader As OleDbDataReader = Nothing
        Dim novi_red As DataGridViewRow = Nothing
        Dim strSql As String = ""
        Dim ima_sumu As Boolean = False

        strSql = strUpit & " WHERE " & strID & " = " & modOleDb.vratiJedVrednostUpit("SELECT @@IDENTITY")

        'strSql = strUpit & " AND " & strID & " = " & modOleDb.vratiJedVrednostUpit("SELECT @@IDENTITY")
        reader = modOleDb.vratiDataReader(strSql)
        novi_red = grid.Rows(0).Clone()

        Try
            If reader.Read() Then
                For i As Integer = 0 To strFildGrid.Count - 1
                    If IsNothing(strFildGrid(i)) Then Exit For
                    If i = 1 Then
                        'generišem redni broj u zavisnosti da li grid ima sumu
                        If grid.Rows(grid.Rows.Count - 1).Cells(1).Value.ToString() = "" Then
                            ima_sumu = True
                            novi_red.Cells(i).Value = grid.Rows(grid.Rows.Count - 2).Cells(1).Value + 1
                        Else
                            novi_red.Cells(i).Value = grid.Rows(grid.Rows.Count - 1).Cells(1).Value + 1
                        End If

                        Continue For
                    End If

                    If IsDBNull(reader(strFildGrid(i))) Then
                        novi_red.Cells(i).Value = ""
                    Else
                        Select Case reader(strFildGrid(i)).GetType()
                            Case GetType(Int16) 'Number: Integer in MS Access
                                novi_red.Cells(i).Value = reader(strFildGrid(i))
                            Case GetType(Int32) 'Autonumber (Long Integer), Number: (Long Integer) in MS Access
                                novi_red.Cells(i).Value = reader(strFildGrid(i))
                            Case GetType(String) 'Text, Memo, Hyperlink in MS Access
                                novi_red.Cells(i).Value = reader(strFildGrid(i))
                                grid.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                            Case GetType(Decimal) 'Currency, Number: Decimal in MS Access
                                'novi_red.Cells(i).Value = Format(reader(strFildGrid(i)), "#,##0.00")
                                'novi_red.Cells(i).Value = modABC.vratiDecimalVrednostZaPrikaz(reader(strFildGrid(i)))
                                grid.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            Case GetType(DateTime) 'Date/Time in MS Access
                                novi_red.Cells(i).Value = Format(reader(strFildGrid(i)), "dd.MM.yyyy")
                                grid.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            Case Else
                                novi_red.Cells(i).Value = reader(strFildGrid(i))
                        End Select
                    End If
                Next

                'ubacujem na odogovarajuću poziciju u zavisnosti da li ima sumu
                If ima_sumu Then
                    grid.Rows.Insert(grid.Rows.Count - 1, novi_red)
                Else
                    grid.Rows.Add(novi_red)
                End If

                prikaziPoslednjiRedUGridu(grid)
                obojiRed(novi_red)
            Else
                MsgBox("Došlo je do greške prilikom dodavanja novog reda u tabelu, podaci nisu pronađeni", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox("Greška prilikom dodavanja reda u grid: " & ex.Message)
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Sub

    Public Sub prikaziPoslednjiRedUGridu(dgv As DataGridView)

        Try
            Dim brojRedova = dgv.Rows.Count
            If brojRedova > 0 Then
                dgv.FirstDisplayedScrollingRowIndex = brojRedova - 1
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub obojiRed(red As DataGridViewRow)
        red.DefaultCellStyle.ForeColor = Color.Red
    End Sub


    ''' <summary>
    ''' formatiranje grida koji se puni sa Data Tabelom
    ''' </summary>
    Public Sub formatirajGridDataTableIzbor(grid As DataGridView, sirKolona() As Integer, Optional bool_suma As Boolean = False)
        Try
            Dim dblOdnos As Double
            For i As Integer = 0 To grid.ColumnCount - 1
                dblOdnos = dblOdnos + sirKolona(i)
            Next
            dblOdnos = (grid.Width - 20) / (dblOdnos * 10)
            For i As Integer = 0 To grid.ColumnCount - 1
                Dim kol As DataGridViewColumn
                'dodeljujem trenutnu kolonu
                kol = grid.Columns(i)

                kol.Width = sirKolona(i) * 10 * dblOdnos
                kol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                Select Case kol.ValueType
                    Case GetType(String) 'Text, Memo, Hyperlink in MS Access
                        kol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    Case GetType(Decimal) 'Currency, Number: Decimal in MS Access
                        'kol.DefaultCellStyle.Format = modABC.vratiDecimalFormat()
                        kol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    Case GetType(DateTime) 'Date/Time in MS Access
                        kol.DefaultCellStyle.Format = "dd.MM.yyyy"
                        kol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End Select
            Next

            With grid.ColumnHeadersDefaultCellStyle
                '.BackColor = Color.Navy
                '.ForeColor = Color.White
                .Font = New Font(grid.Font, FontStyle.Bold)
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            grid.GridColor = Color.Black
            'grid.BackgroundColor = Color.LightBlue
            grid.BackgroundColor = Color.WhiteSmoke

            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grid.MultiSelect = False
            grid.AllowUserToResizeColumns = True

            'grid.RowsDefaultCellStyle.BackColor = Color.LightGray
            'grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite

            'visina header reda, mora prvo da se podesi na disable or enable resizing pa onda da se dodeli visina
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            grid.ColumnHeadersHeight = 30

            'onemoguceno je da se sa TAB-om ide kroz grid
            grid.TabStop = False
            'da se ne prikazuje poslednji prazan red
            grid.AllowUserToAddRows = False
            grid.AllowDrop = False
            'ne prikazuje prvu kolonu koja se automatski generise
            grid.RowHeadersVisible = False
            'linije unutar grida
            grid.CellBorderStyle = DataGridViewCellBorderStyle.Raised
            'sledi bivsa funkcija implementiraj dizajn
            grid.EnableHeadersVisualStyles = False 'omoguciti da se promeni boja naslovne linije
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
            grid.DefaultCellStyle.SelectionBackColor = Color.LightGray
            grid.DefaultCellStyle.SelectionForeColor = Color.Red
            'korisnik ne može da menja sadržaj ćelija grida
            grid.ReadOnly = True
            'omogućava multiline ćelije grida, znacajno usporava ucitavanje grida
            'grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            grid.AllowUserToResizeRows = False
            grid.ClearSelection()

            prikaziPoslednjiRedUGridu(grid)

            'ukoliko postoji suma bojim taj redu crveno
            If bool_suma Then
                Dim brojRedova = grid.Rows.Count
                If brojRedova > 0 Then
                    grid.Rows(brojRedova - 1).DefaultCellStyle.ForeColor = Color.Red
                End If
            End If
        Catch ex As Exception
            Dim msg As New MojMsgBox(String.Format("Greška prilikom formatiranja grida data table: {0}",
                                                   ex.Message), MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
            msg.ShowDialog()
        End Try
    End Sub

    Public Sub Set_Grid_Width(Grid As DataGridView, strRowWidth As String)

        If Grid.ColumnCount = 0 Then
            Exit Sub
        End If

        Try
            Dim i As Integer = 0
            Dim provera_SQL As String = ""
            Dim insert_SQL As String = ""
            Dim update_SQL As String = ""

            'pamtim širinu kolona samo ukoliko grid ima redova, inače će program da iskoči
            'If Grid.Rows.Count = 0 Then
            '	Exit Sub
            'End If

            'upit za PROVERU da li red postoji 
            'Ukoliko postoje korisnici, proveravam za trenutno ulogovanog korisnika
            If modUser.bool_user Then
                provera_SQL = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", strRowWidth, login_info.sifra_korisnika)
            Else
                provera_SQL = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}'", strRowWidth)
            End If


            'ukoliko f-ja vrati Nothing znači da red ne postoji pa ga upisujem, u suprotnom ga menjam
            If IsNothing(modOleDb.vratiJedVrednostUpit(provera_SQL)) Then
                'formiranje upita za UPIS podataka
                insert_SQL = "INSERT INTO Width("
                For i = 0 To Grid.ColumnCount - 1
                    insert_SQL = insert_SQL & String.Format("wid_{0}, ", If(i + 1 >= 10, i + 1, "0" & i + 1))
                Next

                'Ukoliko postoje korisnici, doda naziv kolone gde se upisuje User
                If modUser.bool_user Then
                    insert_SQL = insert_SQL & "wid_tab_naziv, wid_Usr_Sifra) VALUES("
                Else
                    insert_SQL = insert_SQL & "wid_tab_naziv) VALUES("
                End If

                For i = 0 To Grid.ColumnCount - 1
                    insert_SQL = insert_SQL & Grid.Columns(i).Width & ", "
                Next

                'Ukoliko postoje korisnici, doda sifru Usera u VALUES
                If modUser.bool_user Then
                    insert_SQL = insert_SQL & String.Format("'{0}', '{1}')", strRowWidth, login_info.sifra_korisnika)
                Else
                    insert_SQL = insert_SQL & String.Format("'{0}')", strRowWidth)
                End If

                modOleDb.izvrsiKomandu(insert_SQL)
            Else
                'formiranje upita za IZMENU podataka
                update_SQL = "UPDATE Width SET "

                For i = 0 To Grid.ColumnCount - 1
                    update_SQL = update_SQL & String.Format("wid_{0} = {1} , ", If(i + 1 >= 10, i + 1, "0" & i + 1), Grid.Columns(i).Width)
                Next

                update_SQL = Left(update_SQL, update_SQL.Length - 2)
                update_SQL = update_SQL & String.Format("WHERE wid_tab_naziv = '{0}'", strRowWidth)

                'Ukoliko postoje korisnici, doda i sifru Usera
                If modUser.bool_user Then
                    update_SQL = update_SQL & String.Format(" AND wid_Usr_Sifra = '{0}'", login_info.sifra_korisnika)
                End If

                modOleDb.izvrsiKomandu(update_SQL)
            End If
        Catch ex As Exception
            Dim msg As New MojMsgBox(String.Format("Greška prilikom upisa širina kolona grida:", ex.Message),
                                     MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
            msg.ShowDialog()
        End Try
    End Sub
    Public Sub azurirajSumeGrida(ByRef dgvPrikaz As DataGridView, ByVal strUpit As String, ByVal strFildGrid As String(), ByVal intBrKol As Integer,
                          Optional ByVal intSuma As Integer() = Nothing)

        'samo da uzima kolone koje se sumiraju 
        Dim reader As OleDbDataReader = Nothing
        Dim noviRed = New String(intBrKol - 1) {}
        Dim rb As Long = 0
        Dim i As Integer
        'a
        Dim rezSuma = New Decimal(intBrKol - 1) {}

        Try
            reader = modOleDb.vratiDataReader(strUpit)
            While reader.Read() '01/24 samo se uzmaju kolone koje se sumiraju 

                'ako je vrednost procitanog DBNull onda gridu dodeljujem prazan string, a ako vrednost postoji
                'onda gledam koji je tip podataka i u zavisnosti od toga formatiram podatke i dodeljujem gridu, link sa tabelom za 
                'konverziju podataka: https://support.microsoft.com/en-us/kb/320435

                For i = 2 To intBrKol - 1
                    If intSuma(i) = 1 Then
                        If IsNothing(strFildGrid(i)) Then
                            'može se desiti da strFildGrid(i) bude nothing onda program krahira pa ga zajebemo
                        ElseIf IsDBNull(reader(strFildGrid(i))) Then
                            noviRed(i) = ""
                        Else
                            Select Case reader(strFildGrid(i)).GetType()

                                'Case GetType(String) 'Text, Memo, Hyperlink in MS Access
                                    'noviRed(i) = reader(strFildGrid(i))
                                    'dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                                Case GetType(Decimal) 'Currency, Number: Decimal in MS Access
                                    'noviRed(i) = Format(reader(strFildGrid(i)), "#,##0.00")
                                    'noviRed(i) = modABC.vratiDecimalVrednostZaPrikaz(reader(strFildGrid(i)))
                                    'noviRed(i) = modABC.vratiDecimalPrikazKolikoImaDecimala(reader(strFildGrid(i)))
                                    'dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                    'izracunavam sumu u onim poljima gde u nizu intSuma stoji 1, al prvo proveravam dal je poslat
                                    'opcioni parametar
                                    If Not IsNothing(intSuma) Then
                                        If (intSuma(i) = 1) Then
                                            rezSuma(i) = rezSuma(i) + reader(strFildGrid(i))
                                        End If
                                    End If
                                    'Case GetType(Int64), GetType(Int16), GetType(Int32) 'Currency, Number: Decimal in MS Access
                                    'noviRed(i) = Format(reader(strFildGrid(i)), "#,##0.00")
                                    'noviRed(i) = modABC.vratiIntegerVrednostZaPrikaz(reader(strFildGrid(i)))
                                    'dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                    'Case GetType(DateTime) 'Date/Time in MS Access
                                    '    noviRed(i) = Format(reader(strFildGrid(i)), "dd.MM.yyyy")
                                    '    dgvPrikaz.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                Case Else
                                    noviRed(i) = reader(strFildGrid(i))
                            End Select
                        End If

                    End If

                Next

            End While

            '-------------- STARO --------------------
            'dodeljujem poslednjem redu u gridu izracunate sume ukoliko postoje
            dgvPrikaz.Rows.RemoveAt(dgvPrikaz.RowCount - 1)
            If dgvPrikaz.RowCount > 0 Then
                If Not IsNothing(intSuma) Then
                    For i = 0 To intBrKol - 1
                        If rezSuma(i) <> 0 Then
                            noviRed(i) = rezSuma(i)
                            'Nemam još razvijen OBAJ ABC ZATO SAMO IDE rezSuma(i) 30.6.2024
                            'noviRed(i) = modABC.vratiDecimalVrednostZaPrikaz(rezSuma(i))
                        Else
                            noviRed(i) = ""
                        End If
                    Next
                    dgvPrikaz.Rows.Add(noviRed)
                    'ako postoji suma, vrednosti podesavam da bude crvene
                    dgvPrikaz.Rows(dgvPrikaz.RowCount - 1).DefaultCellStyle.ForeColor = Color.Red
                    dgvPrikaz.Rows(dgvPrikaz.RowCount - 1).DefaultCellStyle.Font = New Font(Control.DefaultFont, FontStyle.Bold)
                End If
            End If
        Catch ex As Exception
            MsgBox("Greška prilikom učitavanja grida: " & ex.Message)
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try

        'dgvPrikaz.Columns(0).Visible = False ' kolona ID je sakrivena

        ''prikazuje poslednji red dataGrid-a ako u gridu uopšte postoje redovi
        'Dim brojRedova As Integer
        'brojRedova = dgvPrikaz.Rows.Count
        'If brojRedova > 0 Then
        '    If dgvPrikaz.Rows(brojRedova - 1).Displayed = False Then 'ako posledni red nije prikazan onda ga prikazuje
        '        dgvPrikaz.FirstDisplayedScrollingRowIndex = brojRedova - 1
        '    End If
        'End If
        'dgvPrikaz.ForeColor = Color.Blue
    End Sub

    Public Sub izmeniSirineKolonaGrida(grid As DataGridView, strRowWidth As String)
        Dim upit As String = ""
        'Ukoliko postoje korisnici, proveravam za trenutno ulogovanog korisnika
        If modUser.bool_user Then
            upit = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", strRowWidth, login_info.sifra_korisnika)
        Else
            upit = "SELECT * FROM Width WHERE wid_tab_naziv = '" & strRowWidth & "'"
        End If

        Dim reader As OleDbDataReader = Nothing
        Dim i As Integer
        Try
            reader = modOleDb.vratiDataReader(upit)
            If (reader.Read()) Then
                For i = 0 To grid.Columns.Count - 1
                    'grid.Columns(i).Width = reader.GetInt32(i)
                    Try
                        If reader(i) > 999 Then grid.Columns(i).Width = 100 Else grid.Columns(i).Width = reader(i)
                        'If reader.GetInt32(i) > 500 Then grid.Columns(i).Width = 100 Else grid.Columns(i).Width = reader.GetInt32(i)
                    Catch ex As Exception
                        'If reader.GetInt32(i) > 500 Then grid.Columns(i).Width = 100 Else grid.Columns(i).Width = reader.GetDouble(i)
                        grid.Columns(i).Width = 100
                    End Try
                Next
            End If
        Catch ex As Exception
            MsgBox("Greška prilikom čitanja širina kolona grida: " & vbCrLf & vbCrLf & ex.Message)
        Finally
            modOleDb.zatvoriDataReader(reader)
        End Try
    End Sub

    ''' <summary>
    ''' f-ju koristim samo kada grid punim sa DataTable objektom i pozivam je nakon sto je punjenje
    ''' zavrseno, jer drugacije red nece da primi format, rizicno je sa eventom AfterBindingComplete
    ''' </summary>
    ''' <param name="dgv"></param>
    Public Sub podesiPoslednjiRed(dgv As DataGridView)
        Dim tabela As DataTable = Nothing
        Dim brojRedova As Integer = dgv.Rows.Count

        Try

            tabela = DirectCast(dgv.DataSource, DataTable)
            'formatiram red suma samo ukoliko je u tabelu kojom je popunjem grid dodata suma
            If Not IsNothing(tabela.GetChanges(DataRowState.Added)) Then
                If tabela.GetChanges(DataRowState.Added).Rows.Count = 1 Then
                    Dim red As DataGridViewRow = Nothing
                    red = dgv.Rows(brojRedova - 1)
                    red.DefaultCellStyle.ForeColor = Color.Red
                End If
            End If

            'ukoliko postoji kolona rb i ukoliko je na prvoj poziciji tada se id koji je na nultoj nece videti
            If tabela.Columns.Contains("Rb.") Then
                If tabela.Columns("Rb.").Ordinal = 1 Then
                    dgv.Columns(0).Visible = False
                End If
            End If

            prikaziPoslednjiRedUGridu(dgv)

        Catch ex As Exception
            Dim msg As New MojMsgBox(String.Format("Greška prilikom formatiranja poslednjeg reda u 
									tabeli izveštaja: {0}", ex.Message), MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
            msg.ShowDialog()
        End Try
    End Sub

End Module
