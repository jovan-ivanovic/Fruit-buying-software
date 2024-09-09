Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Module modINI

    'Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
    '        Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
    '        ByVal lpKeyName As String, ByVal lpString As String,
    '        ByVal lpFileName As String) As Int32

    'Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
    '         Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String,
    '         ByVal lpKeyName As String, ByVal lpDefault As String,
    '        ByVal lpReturnedString As String, ByVal nSize As Int32,
    '        ByVal lpFileName As String) As Int32

    'Public Sub WriteIni(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamVal As String)
    '    Dim Result As Integer = WritePrivateProfileString(Section, ParamName, ParamVal, iniFileName)
    'End Sub

    'Public Function ReadIni(ByVal IniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamDefault As String) As String
    '    Dim ParamVal As String = Space$(1024)
    '    Dim LenParamVal As Long = GetPrivateProfileString(Section, ParamName, ParamDefault, ParamVal, Len(ParamVal), IniFileName)
    '    ReadIni = Left$(ParamVal, LenParamVal)

    '    If ReadIni = "" Then
    '        ReadIni = ParamDefault
    '    End If
    'End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Function GetPrivateProfileString(ByVal lpAppName As String,
                            ByVal lpKeyName As String,
                            ByVal lpDefault As String,
                            ByVal lpReturnedString As StringBuilder,
                            ByVal nSize As Integer,
                            ByVal lpFileName As String) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Function WritePrivateProfileString(ByVal lpAppName As String,
                        ByVal lpKeyName As String,
                        ByVal lpString As String,
                        ByVal lpFileName As String) As Boolean
    End Function

    Public Sub WriteIni(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamVal As String)
        Dim Result As Integer = WritePrivateProfileString(Section, ParamName, ParamVal, iniFileName)
    End Sub

    Public Function ReadIni(ByVal IniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamDefault As String) As String
        Dim res As Integer
        Dim sb As StringBuilder

        sb = New StringBuilder(500)
        res = GetPrivateProfileString(Section, ParamName, ParamDefault, sb, sb.Capacity, IniFileName)
        ReadIni = sb.ToString()

        If ReadIni = "" Then
            ReadIni = ParamDefault
        End If
    End Function



    ''' <summary>
    ''' sve sto se čita ili će se mozda nekad čitati iz inicijalnog file-a treba ubaciti u ovu f-ju
    ''' </summary>
    Public Sub citajSveIzIni()
        'modFirma.citajIzIni()
        'modRacunIni.citajIzIni()
        'modPrintIni.citajIzIni()
        ''modPDVIni.inicijalizujPDV()
        'modPosIni.CitajIzIni()
        'modABC.CitajIzIni()
        'modDesign.citajIzIni()
    End Sub


    'End Module

    'Public Module modPosIni

    '    ''' <summary>
    '    ''' iz ini fajla vrednost  POS_Prod a to je 1341 ili 1342 ili ...
    '    ''' </summary>
    '    Public POS_Prod As String

    '    Public Enum enmVrstaKase
    '        GalebGP550 = 1
    '        MetaData74 = 2
    '        EiInformatika = 3
    '        Euro500T = 4
    '        HCP = 5
    '        GENEKO_SUPER_cash_S = 6
    '        MetaData91 = 7
    '        IntRaster_DP25_DP35 = 8
    '        GalebGP100 = 9
    '        GENEKO_FP_200_PJ = 61
    '        IntRaster_FP_600 = 81
    '        MetaData10 = 100
    '        eFiskalizacija = 21
    '    End Enum

    '    ''' <summary>
    '    ''' promenljiva tipa enmVrstaKase koju koristim umesto promenljive Pos_Link, ali i dalje koristim Pos_Link u .ini file. 
    '    ''' </summary>
    '    Public POS_kasa As enmVrstaKase

    '    ''' <summary>
    '    '''trenutno je ne koristim jer koristim promenljivu POS_link koja je tipa enmVrstaKase
    '    ''' </summary>
    '    Public Pos_Link As String
    '    ''' <summary>
    '    ''' Link pisano: Galeb, Metadata, Ei, Euro500, HCP
    '    ''' </summary>
    '    Public Link_opis As String
    '    ''' <summary>
    '    ''' Pauza u mislisekndama posle provlačenja bar koda
    '    ''' </summary>
    '    Public POS_bar_pauza As String
    '    ''' <summary>
    '    ''' Potvrdi da li je fiskalizovano
    '    ''' </summary>
    '    Public POS_por_fisk As String
    '    ''' <summary>
    '    ''' Restoranski kasa. Ako je 1 onda posle fiskalizacije se vraca u glavni prozor
    '    ''' </summary>
    '    Public POS_res_kasa As String
    '    ''' <summary>
    '    ''' Broj karaktera koji se mogu stampati na fis. isecku
    '    ''' </summary>
    '    Public POS_br_kar As Integer
    '    ''' <summary>
    '    ''' Radio dugme u filtriranju artikala u prodaji
    '    ''' </summary>
    '    Public POS_filt_art As String
    '    ''' <summary>
    '    ''' Da li se vidi kartica u prodaji. 1- ne vidi se; ostalo vidi se
    '    ''' </summary>
    '    Public POS_Izvestaj As String
    '    ''' <summary>
    '    ''' Maksimalna količina na fiskalnoj kasi
    '    ''' </summary>
    '    Public POS_max_kol As Decimal
    '    ''' <summary>
    '    ''' Maksimalni iznos fiskalnog računa
    '    ''' </summary>
    '    Public POS_max_izn_fis As Decimal
    '    ''' <summary>
    '    ''' Ako je True onda se u pretrazi kod prodaje ne vide sakriveni artikli
    '    ''' </summary>
    '    Public POS_Bez_Pretrage_Artikala As String
    '    ''' <summary>
    '    ''' Sta se knjizi kod dnevnog izveštaja
    '    ''' </summary>
    '    Public POS_dnevni As String
    '    ''' <summary>
    '    ''' Nacin prodaje: "prazno" Sve rucno radi +, 1 Market cim nadje barkod doda 1
    '    ''' </summary>
    '    Public POS_mod As String
    '    ''' <summary>
    '    ''' Ako je 1 brisu se prazni isečci. To je ono kad idemo na listu. U mrezi ne sme biti aktivno
    '    ''' </summary>
    '    Public POS_brisi_praz_isecke As String
    '    ''' <summary>
    '    ''' Cuvaj u posebnom folderu fajlove fiskalnih isečaka
    '    ''' </summary>
    '    Public POS_cuvaj_fajl_isecke As String
    '    ''' <summary>
    '    ''' Cekaj odgovor kase  1 cekaj nista ili bilo šta drugo ne čekaj
    '    ''' </summary>
    '    Public POS_cekaj_odgovor As Boolean

    '    ''' <summary>
    '    ''' ako je true onda ispisuje msgbox sa porukom o grešci ili broj fiskalnog i fiskalizovani iznos
    '    ''' </summary>
    '    Public POS_msgbox_ok_err As Boolean

    '    ''' <summary>
    '    ''' Prefiks za vagu 270 ili 280
    '    ''' </summary>
    '    Public POS_vaga_prefiks As String
    '    ''' <summary>
    '    ''' dali se težina deli sa 100 ili 1000
    '    ''' </summary>
    '    Public POS_vaga_deljenje As String

    '    ''' <summary>
    '    ''' Broj karaktera u sifri
    '    ''' </summary>
    '    Public POS_sifra_duzina As Integer

    '    ''' <summary>
    '    ''' Prazno šalje se šifra artikala u kasu, 1 šalje se id
    '    ''' </summary>
    '    Public POS_plu As String
    '    ''' <summary>
    '    ''' prazno inicijalizuje se 0 i ne radi se sa stolovima, 1 radi se sa stolovima
    '    ''' </summary>
    '    Public POS_Desk As Integer
    '    ''' <summary>
    '    ''' širina papira za štampanje porudžbine i izveštaja
    '    ''' </summary>
    '    Public POS_sirina_por As Integer
    '    ''' <summary>
    '    ''' štampaj porudzbinu na POS štampaču
    '    ''' </summary>
    '    Public POS_print_por As Boolean

    '    ''' <summary>
    '    ''' štampaj porudzbinu i za šankera. ovo je stari sistem i znaci stampaj dve porudžbine za sto. od 12/2020 uvdeden su pravi promenljive za štampu POSEBNE porudžbine za kuhnju i POSEBNE porudžbine za šank
    '    ''' </summary>
    '    Public POS_print_por_sanker As Boolean
    '    ''' <summary>
    '    ''' 12/2020 štampa posebne porudžbine za kuhnju i štampa se u prostijoj formi zašta su ustvari i namenjeni pos štampači
    '    ''' </summary>
    '    Public POS_prin_posebne_porud_za_kuhnju As Boolean
    '    ''' <summary>
    '    ''' 12/2020 štampa posebne porudžbine za šank. samo što je piće
    '    ''' </summary>
    '    Public POS_prin_posebne_porud_za_sank As Boolean

    '    ''' <summary>
    '    ''' kod pordzbine pitaj broj stola
    '    ''' </summary>
    '    Public POS_por_sto As Boolean

    '    ''' <summary>
    '    ''' da li se prikazuje tastatura pri unosu artikala u prodaji restorana
    '    ''' </summary>
    '    Public POS_ne_prikazuj_tastaturu_unos_artikla As Boolean


    '    ''' <summary>
    '    ''' posle svake prodaje pokreni login konobara
    '    ''' </summary>
    '    Public POS_vise_konobara As Boolean

    '    ''' <summary>
    '    ''' ako je true stolovi se biraju grafički, a ukoliko je false biraju se iz liste
    '    ''' </summary>
    '    Public POS_izbor_stolova_graf As Boolean

    '    ''' <summary>
    '    ''' visina dugmadi u potprozoru artikala. Difoltno ili ako nije u normalnim granicama je 50
    '    ''' </summary>
    '    Public POS_dugme_art_visina As Integer

    '    ''' <summary>
    '    ''' verzija grafickih stolova koja se koristi, 2 ako se koristi V2
    '    ''' </summary>
    '    Public POS_StoloviGraf_Verzija As Integer

    '    ''' <summary>
    '    ''' Sifra aktivnog rasporeda koji se koristi u StoloviGraf V2, iz tabele KontaR lookup RR
    '    ''' </summary>
    '    Public POS_StoloviGraf_Raspored As String


    '    Public POS_PUT_TO As String
    '    Public POS_PUT_FROM As String
    '    Public POS_PUT_BON As String
    '    Public POS_PUT_ARHIVA As String

    '    Public strEsirBaza As String

    '    Private privremena As Object

    '    ''' <summary>
    '    ''' da li se knjiži nakon prodaje
    '    ''' </summary>
    '    Public pos_knjizenje As Boolean = True

    '    ''' <summary>
    '    ''' 11/2020 da li se prikazuje privju za POS štampače 
    '    ''' </summary>
    '    Public POS_privju_pos_printer As Boolean = True
    '    ''' <summary>
    '    ''' 12/2020 nazvi POS printera koji je u kuhnji. ako se koristi samo jedan za sve onda svugde staviti isti. valjda će raditi
    '    ''' </summary>
    '    Public POS_naziv_pos_print_kuhnja As String
    '    ''' <summary>
    '    ''' 12/2020 nazvi POS printera za sank. ta je najverovatnije i za konobara tj. za porudzbine po stolovima. zasad se to podrazumeva
    '    ''' </summary>
    '    Public POS_naziv_pos_print_sank As String


    '    ''' <summary>
    '    ''' 10/2020
    '    ''' Neki traže da se posle fiskalnog štampa i onaj slip poruđžbine sa pos štampača
    '    ''' tada mora da se otvori prikaz stolova da bi odabrao sto ako koristi stolovo 
    '    ''' </summary>
    '    Public boolPOS_Posle_Fis_Stampaj_i_Slip As Boolean

    '    ''' <summary>
    '    ''' 10/2020
    '    ''' Ako je true onda se štampa porudžbina na fiskalnom uređaju. Izbegavati
    '    ''' </summary>
    '    Public boolPOS_PorFis As Boolean
    '    ''' <summary>
    '    ''' U ovo promeljivu se upise odgovor kase. Broj fiskalnog i iznos ako ima
    '    ''' </summary>
    '    Public strOdgovorFiskKase As String

    '    ''' <summary>
    '    ''' 11/2020
    '    ''' Konacno je dodato broj kase ako ih u objektu ima vise ali je i dalje Dokument prodaje 681, 682 jer bi u nekom konacnom izveštaju šta je prodato
    '    ''' po objektu tražili 681 a ne bi nas zanimalo preko koje kase. Ovaj parametar se čak uzima i kod odredivanja novog broja prodaje tako da je to dobro
    '    ''' U ovom slučaju svaki exe fajl ima svoj ini i u njemu se to defineše za razliku kasira koji se čekiraju iz programa
    '    ''' </summary>
    '    Public strPOS_Redni_Broj_Kase As String
    '    ''' <summary>
    '    ''' ako je true nema Login fome pre prodaje 11/2020
    '    ''' </summary>
    '    Public boolPOS_BezPrijavljvanjaKonobara As Boolean
    '    ''' <summary>
    '    ''' ako je ture ne moze se prodavati pice. Kor_Adresa=1  7/2021
    '    ''' neki su trazili da se ne moze dodati artikal za prodaju ako ide u minus
    '    ''' mislim da je to nepotrebno ali je Dušica toliko danima insistirala da sam morao nešto uraditi
    '    ''' </summary>
    '    Public boolPOS_ne_idi_u_minus As Boolean

    '    ''' <summary>
    '    ''' Ako promenljiva ima vrednost true onda se kod klika na porudžbinu otvara
    '    ''' običan input box da se unese imae narucioca koje ce najverovatnije plaćati tu turu
    '    ''' 07/2021
    '    ''' </summary>
    '    Public boolPOS_turu_narucio As Boolean
    '    ''' <summary>
    '    ''' Posle štampe fiskalnog računa štama se porudžbina za kuhnju i šank
    '    ''' odnosno ako je smo piće samo za šank a ako je samo jelo za kuhnju
    '    ''' </summary>
    '    Public boolPOS_posle_fis_print_poru_za_kuh_i_sank As Boolean
    '    ''' <summary>
    '    ''' Promenljiva dobije ture koda iz forme stolova ode na frSharedPorudzbine i fiskalizuje
    '    ''' to je dalje glavnoj formi prodaje frmRestProdaja znak da izadje i otvori formu Login
    '    ''' </summary>
    '    Public boolFskalizovaoJeKadJeBioNaStolovima As Boolean

    '    ''' <summary>
    '    ''' Štampa se esir prilog kada se posle načina plaćanja Preko računa (virmanski) izabere kupac
    '    ''' </summary>
    '    ''' 06/2022
    '    Public boolPOS_stampaj_esir_prilog As Boolean

    '    '12/23 Veljko, dodajemo nacine placanja u ini fajl

    '    Public boolPOS_Placanja_Kartica As Boolean
    '    Public boolPOS_Placanja_InstantPlacanje As Boolean
    '    Public boolPOS_Placanja_Cek As Boolean
    '    Public boolPOS_Placanja_PrekoRacuna As Boolean
    '    Public boolPOS_Placanja_Vaucer As Boolean
    '    Public boolPOS_Placanja_Drugo As Boolean

    '    '04/24 Veljko, dodajemo period ucitavanja artikala 
    '    ''' <summary>
    '    ''' vreme u minutima posle kog ponovo ucitava artikle
    '    ''' </summary>
    '    Public intPosPeriodUcitavanjaArtikala As Integer  '04/24 dodajemo parametar za vreme ucitavanja artikala

    '    '05/24 Veljko, dodajemo popust u frmProdaja
    '    ''' <summary>
    '    ''' boolen koji podesava formu prodaje, tako da dozvoli unos popusta i doda u grid popust
    '    ''' </summary>
    '    Public boolPosDozvoliPopust As Integer  
    '    ''' <summary>
    '    ''' boolen koji omogucava da je stalno omogucen textBox popust u prodaji
    '    ''' </summary>
    '    Public boolPosDozvoliStalnoUnosPopusta As Integer  '
    '    ''' <summary>
    '    ''' boolen koji omogucava da je omogucen popust na ceo racun u prodaji
    '    ''' </summary>
    '    Public boolPosDozvoliPopustNaCeoRacun As Integer
    '    ''' <summary>
    '    ''' boolen koji omogucava stanje u pretrazi artikala
    '    ''' </summary>
    '    '''  '05/24 Veljko, dodajemo opcije šta se vidi u pretrazi artikala
    '    Public boolPosPrikaziStanje_uPretrazi As Integer
    '    ''' <summary>
    '    ''' boolen koji dodaje kataloski broj u pretragu artikala
    '    ''' </summary>
    '    Public boolPosPrikaziKataloskiBroj_uPretrazi As Integer
    '    ''' <summary>
    '    ''' boolen koji dodaje dodatni opis u pretragu artikala
    '    ''' </summary>
    '    Public boolPosPrikaziDodatniOpis_uPretrazi As Integer
    '    ''' <summary>
    '    ''' boolen koji omogucava pretragu prema početku reči
    '    ''' </summary>
    '    Public boolPosPrikaziPretraguPremaPocetkuReci As Integer
    '    ''' <summary>
    '    ''' boolen koji reguliše da li se knjiže otpremnice kupcu
    '    ''' </summary>
    '    Public boolPosPrivremenoKnjižiOtpremniceKupcu As Integer


    '#Region "PARAMETRI ZA STAMPAC ZA PORUDZBINE"
    '    ''' <summary>
    '    ''' ime termalnog štampača za porudzbine
    '    ''' štampač ne mora biti defaultni
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_Name As String

    '    ''' <summary>
    '    ''' termalni štampač za porudzbine
    '    ''' Sirina stampe na termalnom stampacu
    '    ''' 100 = 1 inch
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_Sirina As Integer

    '    ''' <summary>
    '    ''' termalni štampač za porudzbine
    '    ''' Kolika je leva margina, default 0
    '    ''' 100 = 1 inch
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_Margina_Levo As Integer

    '    ''' <summary>
    '    ''' termalni štampač za porudzbine
    '    ''' Broj karaktera u jednom redu 
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_BrKaraktera As Integer

    '    ''' <summary>
    '    ''' termalni štampač za porudzbine
    '    ''' naziv fonta za stampu na termalnom stampacu
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_Font_Name As String

    '    ''' <summary>
    '    ''' termalni štampač za porudzbine
    '    ''' Standardna velicina fonta
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_Font_Velicina As Double

    '    ''' <summary>
    '    ''' termalni štampač za porudzbine
    '    ''' Veci font za naslov
    '    ''' </summary>
    '    Public REST_Printer_Porudzbina_Font_VelicinaVeliki As Double

    '#End Region

    '#Region "PARAMETRI ZA STAMPAC ZA KUHINJU"

    '    ''' <summary>
    '    ''' ime termalnog štampača za kuhinju
    '    ''' štampač ne mora biti defaultni
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_Name As String

    '    ''' <summary>
    '    ''' termalni štampač za kuhinju
    '    ''' Sirina stampe na termalnom stampacu
    '    ''' 100 = 1 inch
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_Sirina As Integer

    '    ''' <summary>
    '    ''' termalni štampač za kuhinju
    '    ''' Kolika je leva margina, default 0
    '    ''' 100 = 1 inch
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_Margina_Levo As Integer

    '    ''' <summary>
    '    ''' termalni štampač za kuhinju
    '    ''' Broj karaktera u jednom redu 
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_BrKaraktera As Integer

    '    ''' <summary>
    '    ''' termalni štampač za kuhinju
    '    ''' naziv fonta za stampu na termalnom stampacu
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_Font_Name As String

    '    ''' <summary>
    '    ''' termalni štampač za kuhinju
    '    ''' Standardna velicina fonta
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_Font_Velicina As Double

    '    ''' <summary>
    '    ''' termalni štampač za kuhinju
    '    ''' Veci font za naslov
    '    ''' </summary>
    '    Public REST_Printer_Kuhinja_Font_VelicinaVeliki As Double

    '#End Region

    '    Public REST_Porudzbine_RadeDo_Sati As Integer

    '    ''' <summary>
    '    ''' f-ja čita promenljive modula POS iz ini file-a
    '    ''' </summary>
    '    Public Sub CitajIzIni()
    '        Try


    '            POS_Prod = ReadIni(iniFile, "POS", "POS_Prod", "1011")

    '            'Pos_Link mi vise nije neophodan jer koristim promenljivu POS_kasa koja je tipa enmVrstaKase
    '            'Pos_Link = ReadIni(iniFile, "POS", "Pos_Link", "")

    '            'neka default vrednost bude HCP ukoliko nista nije upisano u Pos_Link 
    '            POS_kasa = ReadIni(iniFile, "POS", "Pos_Link", 5)

    '            POS_bar_pauza = ReadIni(iniFile, "POS", "POS_bar_pauza", "")

    '            POS_por_fisk = ReadIni(iniFile, "POS", "POS_por_fisk", "")

    '            POS_res_kasa = ReadIni(iniFile, "POS", "POS_res_kasa", "")

    '            POS_filt_art = ReadIni(iniFile, "POS", "POS_filt_art", "")

    '            POS_Izvestaj = ReadIni(iniFile, "POS", "POS_Izvestaj", "")

    '            POS_Bez_Pretrage_Artikala = ReadIni(iniFile, "POS", "POS_Bez_Pretrage_Artikala", "")

    '            POS_dnevni = ReadIni(iniFile, "POS", "POS_dnevni", "")

    '            POS_mod = ReadIni(iniFile, "POS", "POS_mod", "") '---- Ako je 1 onda je market mod. Posle barkoda odmah doda količinu 1

    ''            POS_brisi_praz_isecke = ReadIni(iniFile, "POS", "POS_brisi_praz_isecke", "")

    '    '            If ReadIni(iniFile, "POS", "POS_cekaj_odgovor", "") = "1" Then
    '    '                POS_cekaj_odgovor = True
    '    '            Else
    '    '                POS_cekaj_odgovor = False
    '    '            End If

    '    '            If ReadIni(iniFile, "POS", "POS_msgbox_ok_err", "") = "1" Then
    '    '                POS_msgbox_ok_err = True
    '    '            Else
    '    '                POS_msgbox_ok_err = False
    '    '            End If


    '    '            POS_cuvaj_fajl_isecke = ReadIni(iniFile, "POS", "POS_cuvaj_fajl_isecke", "")

    '    '            'If POS_cuvaj_fajl_isecke = "1" Then
    '    '            '    If Dir("c:\Kopije_isecaka\") = "" Then
    '    '            '        MkDir("c:\Kopije_isecaka\")
    '    '            '    End If
    '    '            'End If

    '    '            If POS_cuvaj_fajl_isecke = "1" Then
    '    '                If Directory.Exists("c:\Kopije_isecaka\") = False Then
    '    '                    Directory.CreateDirectory("c:\Kopije_isecaka\")
    '    '                End If
    '    '            End If

    '    '            'If Get_INI("POS_ne_knjizi") = "1" Then
    '    '            If ReadIni(iniFile, "POS", "POS_ne_knjizi", "") = "1" Then
    '    '                pos_knjizenje = False
    '    '            Else
    '    '                pos_knjizenje = True
    '    '            End If

    '    '            If ReadIni(iniFile, "POS", "POS_privju_pos_printer", "") = "1" Then
    '    '                POS_privju_pos_printer = True
    '    '            Else
    '    '                POS_privju_pos_printer = False
    '    '            End If

    '    '            POS_vaga_prefiks = ReadIni(iniFile, "POS", "POS_vaga_prefiks", "")
    '    '            If POS_vaga_prefiks = "" Then
    '    '                POS_vaga_prefiks = "270"
    '    '            End If

    '    '            POS_vaga_deljenje = ReadIni(iniFile, "POS", "POS_vaga_deljenje", "")
    '    '            If POS_vaga_deljenje = "" Then
    '    '                POS_vaga_deljenje = "1000"
    '    '            End If

    '    '            privremena = ReadIni(iniFile, "POS", "POS_max_kol", "")
    '    '            If privremena = "" Then
    '    '                POS_max_kol = 1000
    '    '            Else
    '    '                POS_max_kol = Convert.ToDecimal(privremena)
    '    '            End If

    '    '            privremena = ReadIni(iniFile, "POS", "POS_max_izn_fis", "")
    '    '            If privremena = "" Then
    '    '                POS_max_izn_fis = 100000
    '    '            Else
    '    '                POS_max_izn_fis = Convert.ToDecimal(privremena)
    '    '            End If

    '    '            privremena = ReadIni(iniFile, "POS", "POS_br_kar", "")
    '    '            If privremena = "" Or POS_br_kar = 0 Then
    '    '                POS_br_kar = 22
    '    '            Else
    '    '                POS_br_kar = Convert.ToInt32(privremena)
    '    '            End If

    '    '            privremena = ReadIni(iniFile, "POS", "POS_sifra_duzina", "")
    '    '            If privremena = "" Then
    '    '                POS_sifra_duzina = 6
    '    '            Else
    '    '                POS_sifra_duzina = Convert.ToInt32(privremena)
    '    '            End If

    '    '            'Ako je prazno onda šifra, 1 id iz KontaR
    '    '            POS_plu = ReadIni(iniFile, "POS", "POS_plu", "")

    '    '            privremena = ReadIni(iniFile, "POS", "POS_Desk", "")
    '    '            If privremena = "" Then
    '    '                POS_Desk = 0
    '    '            Else
    '    '                POS_Desk = Convert.ToInt32(privremena)
    '    '            End If

    '    '            'ako uspe da konvertuje u integer onda konvertuje, a ako ne uspe onda uzima default širinu od 80 mm
    '    '            If Int32.TryParse(ReadIni(iniFile, "POS", "POS_sirina_por", ""), POS_sirina_por) <> True Then
    '    '                POS_sirina_por = 80
    '    '            End If

    '    '            'ako je 1 onda znaci stamap se na POS stampuacu porudzbina
    '    '            privremena = ReadIni(iniFile, "POS", "POS_print_por", "")
    '    '            If privremena = "1" Then
    '    '                POS_print_por = True
    '    '            Else
    '    '                POS_print_por = False
    '    '            End If

    '    '            'ako je 1 onda znaci da se na pos stampču štampa i porudžbina za šankera
    '    '            privremena = ReadIni(iniFile, "POS", "POS_print_por_sanker", "")
    '    '            If privremena = "1" Then
    '    '                POS_print_por_sanker = True
    '    '            Else
    '    '                POS_print_por_sanker = False
    '    '            End If

    '    '            '12/2020 ako je 1 znaci stampa se posebna porudžbina za kuhnuu posle porudžbine za sto. može biti štiklirano da se ne štama za sto
    '    '            privremena = ReadIni(iniFile, "POS", "POS_prin_posebne_porud_za_kuhnju", "")
    '    '            If privremena = "1" Then
    '    '                POS_prin_posebne_porud_za_kuhnju = True
    '    '            Else
    '    '                POS_prin_posebne_porud_za_kuhnju = False
    '    '            End If

    '    '            '12/2020 ako je 1 znaci stampa se posebna porudžbina za Šank posle porudžbine za sto. može biti štiklirano da se ne štama za sto
    '    '            privremena = ReadIni(iniFile, "POS", "POS_prin_posebne_porud_za_sank", "")
    '    '            If privremena = "1" Then
    '    '                POS_prin_posebne_porud_za_sank = True
    '    '            Else
    '    '                POS_prin_posebne_porud_za_sank = False
    '    '            End If


    '    '            'ako je 1 znači da se ne prikazuje tastatura pri unosu novog artikla
    '    '            privremena = ReadIni(iniFile, "POS", "POS_ne_prikazuj_tastaturu_unos_artikla", "")
    '    '            If privremena = "1" Then
    '    '                POS_ne_prikazuj_tastaturu_unos_artikla = True
    '    '            Else
    '    '                POS_ne_prikazuj_tastaturu_unos_artikla = False
    '    '            End If

    '    '            'ako je 1 pita nas za broj stola 
    '    '            privremena = ReadIni(iniFile, "POS", "POS_por_sto", "")
    '    '            If privremena = "1" Then
    '    '                POS_por_sto = True
    '    '            Else
    '    '                POS_por_sto = False
    '    '            End If

    '    '            'ako je 1 onda znaci pozovi login formu za prijavljivanje konobara/kasira
    '    '            privremena = ReadIni(iniFile, "POS", "POS_vise_konobara", "")
    '    '            If privremena = "1" Then
    '    '                POS_vise_konobara = True
    '    '            Else
    '    '                POS_vise_konobara = False
    '    '            End If

    '    '            'ako je 1 onda znaci pozovi login formu za prijavljivanje konobara/kasira
    '    '            privremena = ReadIni(iniFile, "POS", "POS_izbor_stolova_graf", "")
    '    '            If privremena = "1" Then
    '    '                POS_izbor_stolova_graf = True
    '    '            Else
    '    '                POS_izbor_stolova_graf = False
    '    '            End If

    '    '            'visina dugmadi u  potprozoru artikli
    '    '            POS_dugme_art_visina = ReadIni(iniFile, "POS", "POS_dugme_art_visina", "50")


    '    '            '10/2020
    '    '            If ReadIni(iniFile, "POS", "POS_Posle_Fis_Stampaj_i_Slip", "") = "1" Then
    '    '                boolPOS_Posle_Fis_Stampaj_i_Slip = True
    '    '            Else
    '    '                boolPOS_Posle_Fis_Stampaj_i_Slip = False
    '    '            End If

    '    '            '11/2020
    '    '            strPOS_Redni_Broj_Kase = ReadIni(iniFile, "POS", "POS_Redni_Broj_Kase", "1")

    '    '            '11/2020
    '    '            If ReadIni(iniFile, "POS", "POS_BezPrijavljvanjaKonobara", "") = "1" Then
    '    '                boolPOS_BezPrijavljvanjaKonobara = True
    '    '            Else
    '    '                boolPOS_BezPrijavljvanjaKonobara = False
    '    '            End If

    '    '            '12/2020
    '    '            If ReadIni(iniFile, "POS", "POS_StoloviGraf_Verzija", "") = "2" Then
    '    '                POS_StoloviGraf_Verzija = 2
    '    '            Else
    '    '                POS_StoloviGraf_Verzija = 1
    '    '            End If

    '    '            '12/2020
    '    '            POS_StoloviGraf_Raspored = ReadIni(iniFile, "POS", "POS_StoloviGraf_Raspored", "")


    '    '            '2/2021
    '    '            POS_naziv_pos_print_kuhnja = ReadIni(iniFile, "POS", "POS_naziv_pos_print_kuhnja", "")
    '    '            POS_naziv_pos_print_sank = ReadIni(iniFile, "POS", "POS_naziv_pos_print_sank", "")

    '    '            '7/2021
    '    '            If ReadIni(iniFile, "POS", "POS_ne_idi_u_minus", "") = "1" Then
    '    '                boolPOS_ne_idi_u_minus = True
    '    '            Else
    '    '                boolPOS_ne_idi_u_minus = False
    '    '            End If

    '    '            '07/2021 ako je true kod klika na Porudzbinu otvara se Input prozor za unos narucioca ture
    '    '            If ReadIni(iniFile, "POS", "POS_turu_narucio", "") = "1" Then
    '    '                boolPOS_turu_narucio = True
    '    '            Else
    '    '                boolPOS_turu_narucio = False
    '    '            End If

    '    '            '10/2021 posle fiskalnog štampaj i porudžbine za šank i kuhnju
    '    '            boolPOS_posle_fis_print_poru_za_kuh_i_sank = ReadIni(iniFile, "POS", "POS_posle_fis_print_poru_za_kuh_i_sank", "0")


    '    '                'If ReadIni(iniFile, "POS", "POS_posle_fis_print_poru_za_kuh_i_sank", "") = "1" Then
    '    '                '    boolPOS_posle_fis_print_poru_za_kuh_i_sank = True
    '    '                'Else
    '    '                '    boolPOS_posle_fis_print_poru_za_kuh_i_sank = False
    '    '                'End If

    '    '                '06/2022 ako je prazno to znaci stampa se prilog i onda je true ko nece rucno namestiti u ini fajlu kao sto je valjaonica
    '    '                If ReadIni(iniFile, "POS", "POS_stampaj_esir_prilog", "") = "" Then
    '    '                boolPOS_stampaj_esir_prilog = True
    '    '            Else
    '    '                boolPOS_stampaj_esir_prilog = False
    '    '            End If



    '    '            Select Case POS_kasa
    '    '                Case enmVrstaKase.GalebGP550
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\Users\Public\driverGP\TO_FP\"
    '    '                        POS_PUT_FROM = "C:\Users\Public\driverGP\FROM_FP\"
    '    '                    End If
    '    '                    Link_opis = "Galeb GP 550"

    '    '                Case enmVrstaKase.MetaData74
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then    'Pos_Input_Link, Pos_Otput_Link
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\METALINE\EXCH\LNK\TO_FP\"
    '    '                        POS_PUT_FROM = "C:\METALINE\EXCH\LNK\FROM_FP\"
    '    '                    End If
    '    '                    Link_opis = "MetaData 7.4"

    '    '                Case enmVrstaKase.EiInformatika
    '    '                    POS_PUT_TO = "C:\Ipos\UKasu\"
    '    '                    POS_PUT_BON = "C:\Ipos\Bon\"
    '    '                    POS_PUT_FROM = "C:\Ipos\IzKase\"
    '    '                    POS_PUT_ARHIVA = "C:\Ipos\Arhiva\" '& Day(Of Date)() & "-" & Month(Of Date)() & "-" & Year(Of Date)() & "\"
    '    '                    Link_opis = "EiInformatika"

    '    '                Case enmVrstaKase.Euro500T
    '    '                    POS_PUT_TO = "C:\Euro500\"
    '    '                    POS_PUT_FROM = "C:\Euro500\"
    '    '                    Link_opis = "Euro-500T"

    '    '                Case enmVrstaKase.HCP
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then    'Pos_Input_Link, Pos_Otput_Link
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\HCP\TO_FP\"
    '    '                        POS_PUT_FROM = "C:\HCP\FROM_FP\"
    '    '                    End If
    '    '                    Link_opis = "HCP"

    '    '                Case enmVrstaKase.GENEKO_SUPER_cash_S
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then    'Pos_Input_Link, Pos_Otput_Link
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\SuperCash\Input\"
    '    '                        POS_PUT_FROM = "C:\SuperCash\Output\"
    '    '                    End If
    '    '                    Link_opis = "GENEKO SUPER cash S"

    '    '                Case enmVrstaKase.GENEKO_FP_200_PJ
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then    'Pos_Input_Link, Pos_Otput_Link
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\SuperCash\Input\"
    '    '                        POS_PUT_FROM = "C:\SuperCash\Output\"
    '    '                    End If
    '    '                    Link_opis = "GENEKO_FP_200_PJ"

    '    '                Case enmVrstaKase.MetaData91
    '    '                    'If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then    'Pos_Input_Link, Pos_Otput_Link
    '    '                    '    POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                    '    POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    'Else
    '    '                    POS_PUT_TO = "C:\METALINE\EXCH\LNK\TO_FP\"
    '    '                    POS_PUT_FROM = "C:\METALINE\EXCH\LNK\FROM_FP\"
    '    '                    'End If
    '    '                    Link_opis = "MetaData 9.1"

    '    '                Case enmVrstaKase.MetaData10
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then    'Pos_Input_Link, Pos_Otput_Link
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\MetaLINK10\ExchData\FP_Inp\"
    '    '                        POS_PUT_FROM = "C:\MetaLINK10\ExchData\FP_Out\"
    '    '                    End If
    '    '                    Link_opis = "MetaData 10.1"

    '    '                Case enmVrstaKase.IntRaster_DP25_DP35, enmVrstaKase.IntRaster_FP_600
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\"
    '    '                        POS_PUT_FROM = "C:\"
    '    '                    End If
    '    '                    Link_opis = "INT RASTER DP25, DP35"

    '    '                Case enmVrstaKase.GalebGP100
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\Users\Public\driverGP\TO_FP\"
    '    '                        POS_PUT_FROM = "C:\Users\Public\driverGP\FROM_FP\"
    '    '                    End If
    '    '                    Link_opis = "Galeb GP 100"

    '    '                Case enmVrstaKase.eFiskalizacija
    '    '                    If ReadIni(iniFile, "POS", "Pos_Input_Link", "") <> "" Then
    '    '                        POS_PUT_TO = ReadIni(iniFile, "POS", "Pos_Input_Link", "")
    '    '                        POS_PUT_FROM = ReadIni(iniFile, "POS", "Pos_Otput_Link", "")
    '    '                    Else
    '    '                        POS_PUT_TO = "C:\POS\IN\"
    '    '                        POS_PUT_FROM = "C:\POS\OUT\"
    '    '                    End If
    '    '                    Link_opis = "eFiskalizacija 2022"
    '    '                    'ako nije nista onda je HCP mada je ranije pri citanju .ini filea ovo vec reseno
    '    '                Case Else
    '    '                    POS_PUT_TO = "C:\HCP\TO_FP\"
    '    '                    POS_PUT_FROM = "C:\HCP\FROM_FP\"
    '    '                    POS_kasa = enmVrstaKase.HCP
    '    '                    Link_opis = "HCP"
    '    '            End Select

    '    '            '12/23 Veljko, dodajemo nacine placanja u ini fajl

    '    '            boolPOS_Placanja_Kartica = ReadIni(iniFile, "POS", "POS_Placanja_Kartica", "1")
    '    '            boolPOS_Placanja_InstantPlacanje = ReadIni(iniFile, "POS", "POS_Placanja_InstantPlacanje", "1")
    '    '            boolPOS_Placanja_Cek = ReadIni(iniFile, "POS", "POS_Placanja_Cek", "1")
    '    '            boolPOS_Placanja_PrekoRacuna = ReadIni(iniFile, "POS", "POS_Placanja_PrekoRacuna", "1")
    '    '            boolPOS_Placanja_Vaucer = ReadIni(iniFile, "POS", "POS_Placanja_Vaucer", "1")
    '    '            boolPOS_Placanja_Drugo = ReadIni(iniFile, "POS", "POS_Placanja_Drugo", "1")

    '    '            '04/24 dodajemo period ucitavanja artikala
    '    '            intPosPeriodUcitavanjaArtikala = ReadIni(iniFile, "POS", "POS_Period_ucitavanja_artikala", "30")

    '    '            '05/24 dodajemo popust u prodaji
    '    '            boolPosDozvoliPopust = ReadIni(iniFile, "POS", "POS_Dozvoli_Popust", "0")
    '    '            boolPosDozvoliStalnoUnosPopusta = ReadIni(iniFile, "POS", "POS_Dozvoli_Stalno_Unos_popusta", "0")
    '    '            boolPosDozvoliPopustNaCeoRacun = ReadIni(iniFile, "POS", "POS_Dozvoli_Popust_Na_Ceo_Racun", "0")


    '    '            '05/24 dodajemo kataloski broj u pretrazi i dodatnu pretragu prema počektu reči
    '    '            boolPosPrikaziStanje_uPretrazi = ReadIni(iniFile, "POS", "POS_Stanje_uPretrazi", "0")
    '    '            boolPosPrikaziKataloskiBroj_uPretrazi = ReadIni(iniFile, "POS", "POS_KataloskiBroj_uPretrazi", "0")
    '    '            boolPosPrikaziDodatniOpis_uPretrazi = ReadIni(iniFile, "POS", "POS_DodatniOpis_uPretrazi", "0")
    '    '            boolPosPrikaziPretraguPremaPocetkuReci = ReadIni(iniFile, "POS", "POS_PretraguPremaPocetkuReci", "0")

    '    '            boolPosPrivremenoKnjižiOtpremniceKupcu = ReadIni(iniFile, "POS", "POS_privremeno_knjizi_otpremnice_kupcu", "0")


    '    '            '-----------------------------------------------------------
    '    '            ' PARAMETRI ZA STAMPAC ZA PORUDZBINE
    '    '            REST_Printer_Porudzbina_Name = ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_Name", "")
    '    '            REST_Printer_Porudzbina_Sirina = ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_Sirina", "288")
    '    '            REST_Printer_Porudzbina_Margina_Levo = ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_Margina_Levo", "0")
    '    '            REST_Printer_Porudzbina_BrKaraktera = ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_BrKaraktera", "40")
    '    '            REST_Printer_Porudzbina_Font_Name = ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_Font_Name", "Hack")
    '    '            REST_Printer_Porudzbina_Font_Velicina = GetDouble(ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_Font_Velicina", "8"))
    '    '            REST_Printer_Porudzbina_Font_VelicinaVeliki = GetDouble(ReadIni(iniFile, "REST", "REST_Printer_Porudzbina_Font_VelicinaVeliki", "10"))

    '    '            '-----------------------------------------------------------
    '    '            ' PARAMETRI ZA STAMPAC ZA KUHINJU
    '    '            REST_Printer_Kuhinja_Name = ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_Name", "")
    '    '            REST_Printer_Kuhinja_Sirina = ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_Sirina", "288")
    '    '            REST_Printer_Kuhinja_Margina_Levo = ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_Margina_Levo", "0")
    '    '            REST_Printer_Kuhinja_BrKaraktera = ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_BrKaraktera", "40")
    '    '            REST_Printer_Kuhinja_Font_Name = ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_Font_Name", "Hack")
    '    '            REST_Printer_Kuhinja_Font_Velicina = GetDouble(ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_Font_Velicina", "8"))
    '    '            REST_Printer_Kuhinja_Font_VelicinaVeliki = GetDouble(ReadIni(iniFile, "REST", "REST_Printer_Kuhinja_Font_VelicinaVeliki", "10"))

    '    '            '-----------------------------------------------------------
    '    '            ' PARAMETRI KOJI SU VEZANI SAMO ZA RESTORAN, POCINJU SA REST
    '    '            REST_Porudzbine_RadeDo_Sati = ReadIni(iniFile, "REST", "REST_Porudzbine_RadeDo_Sati", "5")

    '    '            ' PARAMETRI  POCINJU SA ESIR
    '    '            '01/24 dodavanje baze ESIR-a
    '    '            strEsirBaza = ReadIni(iniFile, "ESIR", "ESIR_baza", "")


    '    '        Catch ex As Exception
    '    '            MsgBox("Greška prilikom čitanja iz Ini file - POS: " & ex.Message)
    '    '        End Try
    '    '    End Sub

    '    '    Public Sub UpisiUIni()
    '    '        WriteIni(iniFile, "POS", "Pos_Otput_Link", POS_PUT_FROM)
    '    '        WriteIni(iniFile, "POS", "Pos_Input_Link", POS_PUT_TO)

    '    '        WriteIni(iniFile, "POS", "POS_sirina_por", POS_sirina_por)
    '    '        'POS_kasa je tipa enmVrstaKase i vraca broj
    '    '        WriteIni(iniFile, "POS", "Pos_Link", POS_kasa)

    '    '        WriteIni(iniFile, "POS", "POS_print_por", IIf(POS_print_por, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_print_por_sanker", IIf(POS_print_por_sanker, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_prin_posebne_porud_za_kuhnju", IIf(POS_prin_posebne_porud_za_kuhnju, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_prin_posebne_porud_za_sank", IIf(POS_prin_posebne_porud_za_sank, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_ne_prikazuj_tastaturu_unos_artikla", IIf(POS_ne_prikazuj_tastaturu_unos_artikla, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_por_sto", IIf(POS_por_sto, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_vise_konobara", IIf(POS_vise_konobara, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_izbor_stolova_graf", IIf(POS_izbor_stolova_graf, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_BezPrijavljvanjaKonobara", IIf(boolPOS_BezPrijavljvanjaKonobara, 1, 0))

    '    '        '------------------------ fiskalna poceto dodavanje 10/2020 POS_msgbox_ok_err
    '    '        WriteIni(iniFile, "POS", "POS_cekaj_odgovor", IIf(POS_cekaj_odgovor, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_msgbox_ok_err", IIf(POS_msgbox_ok_err, 1, 0))
    '    '        '12/2020------------------------ Group box POS ---------------------------------------
    '    '        WriteIni(iniFile, "POS", "POS_privju_pos_printer", IIf(POS_privju_pos_printer, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_naziv_pos_print_kuhnja", POS_naziv_pos_print_kuhnja)
    '    '        WriteIni(iniFile, "POS", "POS_naziv_pos_print_sank", POS_naziv_pos_print_sank)
    '    '        WriteIni(iniFile, "POS", "POS_turu_narucio", IIf(boolPOS_turu_narucio, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_ne_idi_u_minus", IIf(boolPOS_ne_idi_u_minus, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_privremeno_knjizi_otpremnice_kupcu", IIf(boolPosPrivremenoKnjižiOtpremniceKupcu, 1, 0))

    '    '        '-----------------------------------------------------------
    '    '        ' PARAMETRI ZA NAČINE ŠLAĆANJA  '12/23 Veljko, dodajemo nacine placanja u ini fajl
    '    '        WriteIni(iniFile, "POS", "POS_Placanja_Kartica", IIf(boolPOS_Placanja_Kartica, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_Placanja_InstantPlacanje", IIf(boolPOS_Placanja_InstantPlacanje, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_Placanja_Cek", IIf(boolPOS_Placanja_Cek, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_Placanja_PrekoRacuna", IIf(boolPOS_Placanja_PrekoRacuna, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_Placanja_Vaucer", IIf(boolPOS_Placanja_Vaucer, 1, 0))
    '    '        WriteIni(iniFile, "POS", "POS_Placanja_Drugo", IIf(boolPOS_Placanja_Drugo, 1, 0))

    '    '        ' 04/24 veljko, dodajemo period ucitavanja artikala
    '    '        WriteIni(iniFile, "POS", "POS_Period_ucitavanja_artikala", intPosPeriodUcitavanjaArtikala)

    '    '        ' 05/24 veljko, dodajemo popust u prodaji
    '    '        WriteIni(iniFile, "POS", "POS_Dozvoli_Popust", boolPosDozvoliPopust)
    '    '        WriteIni(iniFile, "POS", "POS_Dozvoli_Stalno_Unos_popusta", boolPosDozvoliStalnoUnosPopusta)
    '    '        WriteIni(iniFile, "POS", "POS_Dozvoli_Popust_Na_Ceo_Racun", boolPosDozvoliPopustNaCeoRacun)

    '    '        ' 05/24 veljko, dodajemo parametre sta se vidi u pretrazi artikala
    '    '        WriteIni(iniFile, "POS", "POS_Stanje_uPretrazi", boolPosPrikaziStanje_uPretrazi)
    '    '        WriteIni(iniFile, "POS", "POS_KataloskiBroj_uPretrazi", boolPosPrikaziKataloskiBroj_uPretrazi)
    '    '        WriteIni(iniFile, "POS", "POS_DodatniOpis_uPretrazi", boolPosPrikaziDodatniOpis_uPretrazi)
    '    '        WriteIni(iniFile, "POS", "POS_PretraguPremaPocetkuReci", boolPosPrikaziPretraguPremaPocetkuReci)

    '    '        '-----------------------------------------------------------
    '    '        ' PARAMETRI ZA GRAFIČKO OKRUŽENJE 12/23 Veljko, dodajemo da se može upsati novi grafički raspored
    '    '        WriteIni(iniFile, "POS", " POS_StoloviGraf_Raspored", POS_StoloviGraf_Raspored)
    '    '        WriteIni(iniFile, "POS", "POS_posle_fis_print_poru_za_kuh_i_sank", IIf(boolPOS_posle_fis_print_poru_za_kuh_i_sank, 1, 0))

    '    '        '10/2023 dodato je ovde i za REST neki parametri koje je Uros stavio zbog njegove univerzalne funkcije za stampu na termalnom printeru
    '    '        '-----------------------------------------------------------
    '    '        ' PARAMETRI ZA STAMPAC ZA PORUDZBINE
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_Name", REST_Printer_Porudzbina_Name)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_Sirina", REST_Printer_Porudzbina_Sirina)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_Margina_Levo", REST_Printer_Porudzbina_Margina_Levo)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_BrKaraktera", REST_Printer_Porudzbina_BrKaraktera)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_Font_Name", REST_Printer_Porudzbina_Font_Name)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_Font_Velicina", REST_Printer_Porudzbina_Font_Velicina)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Porudzbina_Font_VelicinaVeliki", REST_Printer_Porudzbina_Font_VelicinaVeliki)

    '    '        '-----------------------------------------------------------
    '    '        ' PARAMETRI ZA STAMPAC ZA KUHINJU
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_Name", REST_Printer_Kuhinja_Name)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_Sirina", REST_Printer_Kuhinja_Sirina)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_Margina_Levo", REST_Printer_Kuhinja_Margina_Levo)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_BrKaraktera", REST_Printer_Kuhinja_BrKaraktera)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_Font_Name", REST_Printer_Kuhinja_Font_Name)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_Font_Velicina", REST_Printer_Kuhinja_Font_Velicina)
    '    '        WriteIni(iniFile, "REST", "REST_Printer_Kuhinja_Font_VelicinaVeliki", REST_Printer_Kuhinja_Font_VelicinaVeliki)

    '    '        ' PARAMETRI POCINJU SA ESIR
    '    '        '01/24 dodavanje baze ESIR-a
    '    '        WriteIni(iniFile, "ESIR", "ESIR_baza", strEsirBaza)

    '    '    End Sub

    '    'End Module

    '    'Public Module modRacunIni

    '    '    Public fakt_senka_adresa As String
    '    '    Public fakt_def_usl As String
    '    '    Public fakt_br_cena_dec As String
    '    '    Public fakt_br_kol_dec As String
    '    '    Public fakt_potpislevo As String
    '    '    Public fakt_potpislev2 As String
    '    '    Public fakt_potpisdesno As String
    '    '    Public fakt_potpisdesn2 As String
    '    '    Public fakt_potpissredina As String
    '    '    Public fakt_potpissredin2 As String
    '    '    Public fakt_sifra As String
    '    '    Public fakt_vozac As String
    '    '    Public fakt_vozilo As String
    '    '    Public fakt_adr_isporuke As String
    '    '    Public fakt_futer4 As String
    '    '    Public fakt_futer3 As String
    '    '    Public fakt_futer2 As String
    '    '    Public fakt_futer1 As String
    '    '    Public fakt_i_broj_otpremnice As String
    '    '    Public fakt_BrojIDokument As String
    '    '    Public fakt_pecat As String
    '    '    Public fakt_slika As String
    '    '    'porez na razliku; preracunata stopa
    '    '    Public fr_porez_na_razl_prer As String
    '    '    Public fakt_memorandum As String
    '    '    Public fakt_leva_margina As String
    '    '    Public fakt_det_visina As String
    '    '    Public fatk_zaglavlje As String
    '    '    Public fakt_racun_otpremnica As String
    '    '    Public fakt_mesto As String
    '    '    Public fakt_firma As String
    '    '    Public fakt_adresa As String
    '    '    Public fakt_tekuci As String
    '    '    Public fakt_telefon As String
    '    '    Public fakt_pib As String
    '    '    Public fakt_obracunao As String
    '    '    Public fakt_odgovornolice As String
    '    '    Public fakt_robuprimio As String
    '    '    Public fakt_mr As String
    '    '    Public fakt_mm As String
    '    '    Public fakt_mp As String
    '    '    Public fakt_ml As String
    '    '    Public fakt_md As String
    '    '    Public fakt_mo As String
    '    '    Public fakt_levo_adresa As String
    '    '    Public fakt_gore_adresa As String
    '    '    Public fakt_ef_levo_adresa As String
    '    '    Public fakt_ef_gore_adresa As String
    '    '    Public fakt_levo_broj As String
    '    '    Public fakt_gore_broj As String
    '    '    Public fakt_koja_cena As String
    '    '    Public fakt_bruto As String

    '    '    'promenljiva je vec postojala, samo je Ivan dodao da je tipa enmPrintVerzija
    '    '    Public fakt_ef_print_verzija As enmPrintVerzija

    '    '    'dodao Ivan, ovo je novo za upis u ini file
    '    '    Public fakt_vr_zaglavlja As enmVrZaglavlja
    '    '    'visina memoranduma
    '    '    Public fakt_mem_visina As String
    '    '    'vrsta potpisa
    '    '    Public fakt_vr_potpis As enmPotpis
    '    '    'suma racuna
    '    '    Public fakt_suma_rac As enmSumaRacuna

    '    '    '09/2023
    '    '    Public faktkre_pregled_zadnjih As Integer
    '    '    Public faktkre_pregled_sortiranje As enmSortRacuna

    '    '    Public Enum enmVrZaglavlja
    '    '        Logo = 1
    '    '        Podaci
    '    '        LogoPodaci
    '    '        Memorandum
    '    '    End Enum

    '    '    Public Enum enmPrintVerzija
    '    '        Pemmy = 91
    '    '        Softek
    '    '    End Enum

    '    '    Public Enum enmPotpis
    '    '        Sa_Okvirom = 1
    '    '        Bez_Okvira
    '    '    End Enum

    '    '    Public Enum enmSumaRacuna
    '    '        Bez_Avansa = 1
    '    '        Sa_Avansom
    '    '    End Enum

    '    '    Public Enum enmSortRacuna
    '    '        Fkz_id = 1
    '    '        Fkz_Broj
    '    '        Fkz_Datum
    '    '        Fkz_DatumOtp
    '    '    End Enum

    '    '    Public Sub upisiUIni()
    '    '        Try
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_firma", fakt_firma)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_adresa", fakt_adresa)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_tekuci", fakt_tekuci)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_telefon", fakt_telefon)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_pib", fakt_pib)

    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_potpislevo", fakt_potpislevo)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_potpislev2", fakt_potpislev2)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_potpisdesno", fakt_potpisdesno)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_potpisdesn2", fakt_potpisdesn2)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_potpissredina", fakt_potpissredina)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_potpissredin2", fakt_potpissredin2)

    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_mesto", fakt_mesto)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_racun_otpremnica", fakt_racun_otpremnica)

    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_slika", fakt_slika)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_ef_print_verzija", fakt_ef_print_verzija)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_vr_zaglavlja", fakt_vr_zaglavlja)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_ef_gore_adresa", fakt_ef_gore_adresa)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_ef_levo_adresa", fakt_ef_levo_adresa)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_mem_visina", fakt_mem_visina)

    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_futer1", fakt_futer1)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_futer2", fakt_futer2)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_futer3", fakt_futer3)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_futer4", fakt_futer4)

    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_vr_potpis", fakt_vr_potpis)
    '    '            WriteIni(iniFile, "fakture nabavna", "fakt_suma_rac", fakt_suma_rac)

    '    '            '09/2023 nova sekcija u ini fajlu od Kreiranje [Fakture kreiranje]
    '    '            WriteIni(iniFile, "Fakture kreiranje", "faktkre_pregled_zadnjih", faktkre_pregled_zadnjih)
    '    '            WriteIni(iniFile, "Fakture kreiranje", "faktkre_pregled_sortiranje", faktkre_pregled_sortiranje)

    '    '        Catch ex As Exception
    '    '            MsgBox("Greška prilikom upisa u ini file: " & ex.Message)
    '    '        End Try
    '    '    End Sub

    '    '    Public Sub citajIzIni()
    '    '        Try

    '    '            '12/20222 poklapala se verzija stampe iz IksWina i eFakture pa je sad razdvojeno jer je to pravilo probleme ako se stampa malo tamo malo vamo


    '    '            fakt_firma = ReadIni(iniFile, "fakture nabavna", "fakt_firma", "")
    '    '            fakt_adresa = ReadIni(iniFile, "fakture nabavna", "fakt_adresa", "")
    '    '            fakt_tekuci = ReadIni(iniFile, "fakture nabavna", "fakt_tekuci", "")
    '    '            fakt_telefon = ReadIni(iniFile, "fakture nabavna", "fakt_telefon", "")
    '    '            fakt_pib = ReadIni(iniFile, "fakture nabavna", "fakt_pib", "")

    '    '            fakt_potpislevo = ReadIni(iniFile, "fakture nabavna", "fakt_potpislevo", "")
    '    '            fakt_potpislev2 = ReadIni(iniFile, "fakture nabavna", "fakt_potpislev2", "")
    '    '            fakt_potpisdesno = ReadIni(iniFile, "fakture nabavna", "fakt_potpisdesno", "")
    '    '            fakt_potpisdesn2 = ReadIni(iniFile, "fakture nabavna", "fakt_potpisdesn2", "")
    '    '            fakt_potpissredina = ReadIni(iniFile, "fakture nabavna", "fakt_potpissredina", "")
    '    '            fakt_potpissredin2 = ReadIni(iniFile, "fakture nabavna", "fakt_potpissredin2", "")

    '    '            fakt_mesto = ReadIni(iniFile, "fakture nabavna", "fakt_mesto", "")
    '    '            fakt_racun_otpremnica = ReadIni(iniFile, "fakture nabavna", "fakt_racun_otpremnica", "")

    '    '            fakt_slika = ReadIni(iniFile, "fakture nabavna", "fakt_slika", "")
    '    '            fakt_ef_print_verzija = ReadIni(iniFile, "fakture nabavna", "fakt_ef_print_verzija", "92")
    '    '            fakt_vr_zaglavlja = ReadIni(iniFile, "fakture nabavna", "fakt_vr_zaglavlja", 2)
    '    '            fakt_ef_gore_adresa = ReadIni(iniFile, "fakture nabavna", "fakt_ef_gore_adresa", "2800")
    '    '            fakt_ef_levo_adresa = ReadIni(iniFile, "fakture nabavna", "fakt_ef_levo_adresa", "6800")
    '    '            fakt_mem_visina = ReadIni(iniFile, "fakture nabavna", "fakt_mem_visina", "")

    '    '            fakt_futer1 = ReadIni(iniFile, "fakture nabavna", "fakt_futer1", "")
    '    '            fakt_futer2 = ReadIni(iniFile, "fakture nabavna", "fakt_futer2", "")
    '    '            fakt_futer3 = ReadIni(iniFile, "fakture nabavna", "fakt_futer3", "")
    '    '            fakt_futer4 = ReadIni(iniFile, "fakture nabavna", "fakt_futer4", "")

    '    '            fakt_vr_potpis = ReadIni(iniFile, "fakture nabavna", "fakt_vr_potpis", 1)
    '    '            fakt_suma_rac = ReadIni(iniFile, "fakture nabavna", "fakt_suma_rac", 1)

    '    '            faktkre_pregled_zadnjih = ReadIni(iniFile, "Fakture kreiranje", "faktkre_pregled_zadnjih", 100)
    '    '            faktkre_pregled_sortiranje = ReadIni(iniFile, "Fakture kreiranje", "faktkre_pregled_sortiranje", 2)


    '    '        Catch ex As Exception
    '    '            MsgBox("Greška prilikom čitanja iz ini file: " & ex.Message)
    '    '        End Try
    '    '    End Sub

    '    'End Module

    '    'Public Module modPrintIni

    '    '    Public Prn_MarDno As String
    '    '    Public Prn_MarVrh As String
    '    '    Public Prn_MarDesno As String
    '    '    Public Prn_MarLevo As String

    '    '    Public Sub upisiUIni()
    '    '        Try
    '    '            WriteIni(iniFile, "Printer", "Prn_MarDno", Prn_MarDno)
    '    '            WriteIni(iniFile, "Printer", "Prn_MarVrh", Prn_MarVrh)
    '    '            WriteIni(iniFile, "Printer", "Prn_MarDesno", Prn_MarDesno)
    '    '            WriteIni(iniFile, "Printer", "Prn_MarLevo", Prn_MarLevo)
    '    '        Catch ex As Exception
    '    '            MsgBox("Greška prilikom upisa u ini file: " & ex.Message)
    '    '        End Try
    '    '    End Sub

    '    '    Public Sub citajIzIni()
    '    '        Try
    '    '            Prn_MarDno = ReadIni(iniFile, "Printer", "Prn_MarDno", "")
    '    '            Prn_MarVrh = ReadIni(iniFile, "Printer", "Prn_MarVrh", "")
    '    '            Prn_MarDesno = ReadIni(iniFile, "Printer", "Prn_MarDesno", "")
    '    '            Prn_MarLevo = ReadIni(iniFile, "Printer", "Prn_MarLevo", "")
    '    '        Catch ex As Exception
    '    '            MsgBox("Greška prilikom čitanja iz ini file: " & ex.Message)
    '    '        End Try
    '    '    End Sub
    '    'End Module

    '    'Public Module modPDVIni

    '    '    Public pdv_opsti As Decimal
    '    '    Public pdv_posebni As Decimal
    '    '    Public pdv_poljo As Decimal

    '    '    ''' <summary>
    '    '    ''' f-ja inicijalizuje promenljive modula na osnovu vrednosti u bazi, za sada fiksno upisuje
    '    '    ''' </summary>
    '    '    Public Sub inicijalizujPDV()
    '    '        pdv_opsti = 20
    '    '        pdv_posebni = 10
    '    '        pdv_poljo = 8
    '    '    End Sub
    '    'End Module

    '    'Public Module modLOG

    '    '    Public fileErrorLog As String = "ErrorLog"

    '    '    ''' <summary>
    '    '    ''' Funkcija pravi u foldeur LOG fajl strLogFile za svaki mesec i upisuje prosledjenu poruku strPoruka
    '    '    ''' Ispred poruke štampa datum u formatu godina_mesec_dan sat_minut_sekund pa poruku tako da je laške vršiti pretragu po datumu
    '    '    ''' </summary>
    '    '    ''' <param name="strPoruka">Poruka koja se upisuje u novi red posle datuma i vremena</param>
    '    '    ''' <param name="strLogFile">Fajl u koji se upisuje. Npr. možemo greške slati u ErrorLog fajl a poruke obaveštanja u Log</param>
    '    '    Public Sub upisiLog(strPoruka As String, strLogFile As String, Optional posaljiNaMail As Boolean = False)

    '    '        On Error Resume Next    'ako postoji greska oko upisivanja u log fajl to nije razlog da program pukne Pa idemo dalje

    '    '        'Dim ThisPath As String = Application.StartupPath & "\LOG"
    '    '        Dim ThisPath As String = modFirma.put & "LOG"
    '    '        If Not Directory.Exists(ThisPath) Then
    '    '            Directory.CreateDirectory(ThisPath)
    '    '        End If

    '    '        'Dim strFile As String = ThisPath & "\" & strLogFile & "_" & DateTime.Today.ToString("yyyy-MM") & ".txt"  'ovo pravi fajl svakog meseca
    '    '        Dim strFile As String = ThisPath & "\" & strLogFile & "_" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"    'ovo pravi fajl svakog dana

    '    '        Dim Trenutno As Date = Date.Now         'daje format 20.11.2021 20:08:40
    '    '        'Trenutno.ToString("HH:mm:ss")           'a ovo daje format  HH:mm:ss
    '    '        'Trenutno.ToString("dd-MM-yyyy")

    '    '        Dim tekst As String = Trenutno.ToString("yyyy-MM-dd HH:mm:ss.fff") &
    '    '                                 "____________________________________________________________" &
    '    '                                 vbCrLf &
    '    '                                 strPoruka

    '    '        Using writer As New StreamWriter(strFile, True) 'ovo verovatno napravi fajl ako ga nema i dodaje jer je True. Ako je false ili ga nema, što je podrazumevano, prepisuje preko fajla ako vec postoji a ako ga nema napravi ga
    '    '            If File.Exists(strFile) Then
    '    '                writer.WriteLine(tekst)
    '    '            End If
    '    '        End Using

    '    '        If posaljiNaMail Then
    '    '            Dim mailBody As String = ""
    '    '            Dim listAdrese As New List(Of String)

    '    '            Select Case modUser.vrsta_projekta
    '    '                Case enmVrstaProjekta.Pos
    '    '                    listAdrese.Add("greske.esir@softek.rs")
    '    '                Case enmVrstaProjekta.Efaktura
    '    '                    listAdrese.Add("greske.efaktura@softek.rs")
    '    '                Case Else
    '    '                    listAdrese.Add("info@divac.rs")
    '    '            End Select

    '    '            Dim strEmailPosiljaoca As String = "vozniparksoftek@gmail.com"
    '    '            Dim strSifraPosiljaoca As String = "olxuncwsughqqios"

    '    '            mailBody = "Hej," & vbCrLf & vbCrLf

    '    '            mailBody = mailBody & "Pib: " & vbTab & modFirma.pib & vbCrLf
    '    '            mailBody = mailBody & "Naziv firme: " & vbTab & modFirma.naziv & vbCrLf
    '    '            mailBody = mailBody & "Mesto: " & vbTab & modFirma.mesto & vbCrLf
    '    '            mailBody = mailBody & "Telefon:" & vbTab & modFirma.telefon & vbCrLf

    '    '            mailBody = mailBody & vbCrLf

    '    '            mailBody = mailBody & tekst & vbCrLf & vbCrLf

    '    '            mailBody = mailBody & "Datum: " & vbTab & Today.Date & vbCrLf


    '    '            posaljiMail(strEmailPosiljaoca, strSifraPosiljaoca, listAdrese, strLogFile & " " & modFirma.naziv, mailBody, "", "", False)

    '    '        End If

    '    '    End Sub

End Module