Imports System.IO

Public Module modFirma

    Public iniFile As String

    Public naziv As String
    Public mesto As String
    Public adresa As String
    Public postanskiBroj As String
    Public pib As String
    Public baza As String
    Public put As String       'kad je u pitanju access to je folder gde je smeštena. to treba kod slanja mejlom da u taj folder upisem. to bi trebalo i kod eFaktura
    Public poslovnaGod As String
    Public maticniBroj As String
    Public jbkjs As String
    Public matBrHotel As String
    Public sifraDelatnosti As String
    Public tekuciRacun As String
    Public tekuciRacuniSvi As String        'ovo sam stavio da se svi racuni stave u niz jer valjda tako ide na eFaktura
    Public telefon As String
    Public opt_email As String
    Public opt_web As String
    Public u_PDV_sistemu As Boolean
    Public pdv_period As String
    Public broj_decimala As Integer = 2 ' 3     'može da se upiše u ini fajl
    Public GLN As String            'Global location number 1/23
    Public knjigovodja As String
    Public prodavac As String
    Public emailZaSlanjeKartica As String       '10/2023
    Public emailSifraZaSlanjeKartica As String
    '    opt_knjigovodja=Mira Cicvarić
    'opt_prodavac=Mile Prodavčević

    Public sql_server As String
    Public sql_server_user As String
    Public sql_server_pass As String

    ''' <summary>
    ''' putanja do centralne baze za replikaciju podataka
    ''' </summary>
    Public put_down As String

    ''' <summary>
    ''' ime centralne baze za replikaciju podataka
    ''' </summary>
    Public baza_down As String

    ''' <summary>
    ''' putanja do baze koja se uploaduje na FTP server
    ''' </summary>
    Public put_up As String

    ''' <summary>
    ''' putanja do baze koja se uploaduje na FTP server
    ''' </summary>
    Public baza_up As String

    Public brojMaloprodaja As String
    Public strBrojeviMaloprodaje As String
    Public brojeviMaloprodaja As String() = {}

    Public Enum enmBaza
        Access = 1
        Server
        ServerRemote
        AccessPass
    End Enum

    'baza na koju se program konektuje(MS Access ili SQL Server)
    Public koja_baza As enmBaza

    ''' <summary>
    ''' F-ja upisuje promeljive modFirma u ini file.
    ''' </summary>
    Public Sub upisiUIni()
        Try
            WriteIni(iniFile, "Options", "firma", naziv)
            WriteIni(iniFile, "Options", "mesto", mesto)
            WriteIni(iniFile, "Options", "adresa", adresa)
            WriteIni(iniFile, "Options", "posta", postanskiBroj)
            WriteIni(iniFile, "Options", "pib", pib)
            'WriteIni(iniFile, "Options", "baza", baza)
            WriteIni(iniFile, "Options", "poslovna_godina", poslovnaGod)
            WriteIni(iniFile, "Options", "maticnibroj", maticniBroj)
            WriteIni(iniFile, "Options", "jbkjs", jbkjs)
            WriteIni(iniFile, "Options", "sifradelatnosti", sifraDelatnosti)
            WriteIni(iniFile, "Options", "tekuciracun", tekuciRacun)
            WriteIni(iniFile, "Options", "telefon1", telefon)
            WriteIni(iniFile, "Options", "opt_email", opt_email)
            WriteIni(iniFile, "Options", "opt_web", opt_web)
            WriteIni(iniFile, "Options", "opt_br_fin_dec", broj_decimala)
            '1/23
            WriteIni(iniFile, "Options", "opt_GLN", GLN)
            WriteIni(iniFile, "Options", "opt_knjigovodja", knjigovodja)
            WriteIni(iniFile, "Options", "opt_prodavac", prodavac)
            '10/2023
            WriteIni(iniFile, "Options", "opt_mail_prilog", emailZaSlanjeKartica)
            WriteIni(iniFile, "Options", "opt_mail_prilog_sif", emailSifraZaSlanjeKartica)
            '11/23
            If u_PDV_sistemu = True Then
                WriteIni(iniFile, "Options", "van_sistema", "")
            Else
                WriteIni(iniFile, "Options", "van_sistema", "1")
            End If
            '01/2024
            WriteIni(iniFile, "Options", "pdv_period", pdv_period)
            '04/2024
            WriteIni(iniFile, "Options", "broj_maloprodaja", brojMaloprodaja)
            WriteIni(iniFile, "Options", "brojevi_maloprodaja", strBrojeviMaloprodaje)

        Catch ex As Exception
            MsgBox("Greška prilikom upisa u ini file: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' F-ja inicijalizuje promenljive modula Firma iz ini filea. Inicijalizuje i modMain.strBaza.
    ''' </summary>
    Public Sub citajIzIni()

        Try

            If ReadIni(iniFile, "Options", "sql_server", "") <> "" Then
                koja_baza = enmBaza.Server
            ElseIf ReadIni(iniFile, "Options", "rem_server", "") <> "" Then
                koja_baza = enmBaza.ServerRemote
            ElseIf ReadIni(iniFile, "Options", "acc_server", "") <> "" Then
                koja_baza = enmBaza.AccessPass
            Else
                koja_baza = enmBaza.Access
            End If

            sql_server = ReadIni(iniFile, "Options", "sql_server", "")
            sql_server_user = ReadIni(iniFile, "Options", "sql_server_user", "")
            sql_server_pass = ReadIni(iniFile, "Options", "sql_server_pass", "")
            'koja_baza = enmBaza.Server

            naziv = ReadIni(iniFile, "Options", "firma", "")
            mesto = ReadIni(iniFile, "Options", "mesto", "")
            adresa = ReadIni(iniFile, "Options", "adresa", "")
            postanskiBroj = ReadIni(iniFile, "Options", "posta", "")
            pib = ReadIni(iniFile, "Options", "pib", "")
            baza = ReadIni(iniFile, "Options", "baza", "")
            put = ReadIni(iniFile, "Options", "put", "")
            If put = "" And Not baza.Contains("\") Then
                Dim tmpLok As String = iniFile.Substring(iniFile.LastIndexOf("\") + 1)
                'put = Replace(iniFile, "\" & tmpLok, "")
                put = Replace(iniFile, tmpLok, "")
            ElseIf baza.Contains("\") Then
                Dim tmpLok As String = iniFile.Substring(iniFile.LastIndexOf("\") + 1)
                put = Replace(iniFile, tmpLok, "")
                baza = baza.Substring(baza.LastIndexOf("\") + 1)
            End If
            poslovnaGod = ReadIni(iniFile, "Options", "poslovna_godina", "")
            maticniBroj = ReadIni(iniFile, "Options", "maticnibroj", "")
            jbkjs = ReadIni(iniFile, "Options", "jbkjs", "")
            matBrHotel = ReadIni(iniFile, "Options", "mat_broj_hotel", "")
            sifraDelatnosti = ReadIni(iniFile, "Options", "sifradelatnosti", "")
            tekuciRacun = ReadIni(iniFile, "Options", "tekuciracun", "")
            tekuciRacuniSvi = ReadIni(iniFile, "Options", "tekuciracun", "") & " " & ReadIni(iniFile, "Options", "tekuciracu2", "") & " " & ReadIni(iniFile, "Options", "tekuciracun3", "") & " " & ReadIni(iniFile, "Options", "tekuciracun4", "") & " " & ReadIni(iniFile, "Options", "tekuciracun5", "")
            telefon = ReadIni(iniFile, "Options", "telefon1", "")
            GLN = ReadIni(iniFile, "Options", "opt_GLN", "")
            knjigovodja = ReadIni(iniFile, "Options", "opt_knjigovodja", "")
            prodavac = ReadIni(iniFile, "Options", "opt_prodavac", "")

            emailZaSlanjeKartica = ReadIni(iniFile, "Options", "opt_mail_prilog", "")
            emailSifraZaSlanjeKartica = ReadIni(iniFile, "Options", "opt_mail_prilog_sif", "") '10/23


            opt_email = ReadIni(iniFile, "Options", "opt_email", "")
            opt_web = ReadIni(iniFile, "Options", "opt_web", "")
            If ReadIni(iniFile, "Options", "van_sistema", "") = "1" Then
                u_PDV_sistemu = False
            Else
                u_PDV_sistemu = True
            End If
            '01/2024
            pdv_period = ReadIni(iniFile, "Options", "pdv_period", "1")
            '04/2024 brojevi maloprodaja
            brojMaloprodaja = ReadIni(iniFile, "Options", "broj_maloprodaja", "1")
            strBrojeviMaloprodaje = ReadIni(iniFile, "Options", "brojevi_maloprodaja", "1341")
            If strBrojeviMaloprodaje.Contains(",") Then
                brojeviMaloprodaja = New String(Convert.ToDouble(brojMaloprodaja) - 1) {}
                brojeviMaloprodaja = strBrojeviMaloprodaje.Split(",")
            Else
                brojeviMaloprodaja = New String(0) {}
                brojeviMaloprodaja(0) = strBrojeviMaloprodaje
            End If
            If ReadIni(iniFile, "Options", "opt_br_fin_dec", "") <> "" Then
                broj_decimala = ReadIni(iniFile, "Options", "opt_br_fin_dec", "")
            Else
                broj_decimala = 2
            End If

            put_down = ReadIni(iniFile, "Options", "put_down", "")
            baza_down = ReadIni(iniFile, "Options", "baza_down", "")

            put_up = ReadIni(iniFile, "Options", "put_up", "")
            baza_up = ReadIni(iniFile, "Options", "baza_up", "")

            'Ovde upisujemo konto prodavnice ili konto kafane za sada ne upisujemo konto defoltne prodaje i konto defoltnog magacina proizvoda
            '04/2024
            'strKontoProdavniceIliRestorana = ReadIni(iniFile, "POS", "POS_Prod", "1341")

            'Ovde upisujemo konto prodavnice ili konto kafane za sada ne upisujemo konto defoltne prodaje i konto defoltnog magacina proizvoda
            '04/2024
            'strKontoDefaultVeleprodajniMag = ReadIni(iniFile, "najcesce vrednosti", "default_magacin", "1321")

            'f-ja koja inicijalizuje modMain.strBaza
            formirajImeBaze()

        Catch ex As Exception
            MsgBox("Greška prilikom čitanja iz ini file: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' F-ja iz .tmp file-a čita ime .ini file-a i vraća putanju do njega.
    ''' </summary>
    Public Function citajIzTemp() As String
        'tmp.ini file se uvek nalazi na istom mestu gde i exe file
        Dim putanjaTmp As String = Application.StartupPath + "\tmp.ini"
        'putanja za vraćanje
        Dim putanjaIni As String = ""
        'provera da li tmp.ini postoji na putanji
        If File.Exists(putanjaTmp) Then
            Using sr As New StreamReader(putanjaTmp, System.Text.Encoding.Default, True) '(putanjaTmp, System.Text.Encoding.Default)
                putanjaIni = sr.ReadLine()
            End Using
            If File.Exists(putanjaIni) Then
                Return Application.StartupPath & "\" & putanjaIni
            Else
                MsgBox("Ne postoji inicijalni fajl: " & vbCrLf & putanjaIni, vbCritical, "Ne postoji fajl")
                End
            End If
        Else
            MsgBox("Ne postoji fajl tmp.ini!", vbCritical, "Ne postoji fajl")
            End
        End If
        'Return Application.StartupPath & "\" & putanjaIni
    End Function

    ''' <summary>
    ''' f-ja formira ime baze u modMain.strBaza u zavisnosti od opcionog parametra poslovna godina
    ''' </summary>
    ''' <param name="godina">ukoliko nije poslat uzima se modFirma.poslovnaGod</param>
    Public Sub formirajImeBaze(Optional godina As Integer = 0)
        Try
            If godina = 0 Then
                'nije poslatna poslovna godina, godinu uzimam modFirma.poslovnaGod
                If modFirma.put = "" Then
                    modMain.strBaza = modFirma.baza & modFirma.poslovnaGod & ".mdb"
                Else
                    modMain.strBaza = modFirma.put & modFirma.baza & modFirma.poslovnaGod & ".mdb"
                End If
            Else
                'uzimam poslatu poslovnu godinu
                If modFirma.put = "" Then
                    modMain.strBaza = modFirma.baza & godina & ".mdb"
                    ''ako je sql server nema ono .mdb
                    'If koja_baza = enmBaza.Server Then
                    '    modMain.strBaza = modFirma.baza & godina
                    'End If
                Else
                    modMain.strBaza = modFirma.put & modFirma.baza & godina & ".mdb"
                End If
            End If
            If koja_baza = enmBaza.Access Then
                If Not File.Exists(modMain.strBaza) Then
                    MsgBox("Ne postoji baza: " & vbCrLf & modMain.strBaza, vbCritical, "Ne postoji fajl")
                    End
                End If
            End If
        Catch ex As Exception
            Dim msg As New MojMsgBox(String.Format("Greška prilikom kreiranja imena baze: {0}", ex.Message), MojMsgBox.enmVidljivaDugmad.Potvrdi,
                                     "Greška", MojMsgBox.enmImgMsgBox.Critical)
            msg.ShowDialog()
            End
        End Try
    End Sub

End Module




