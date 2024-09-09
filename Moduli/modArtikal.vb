Module modArtikal
    Public Function vratiCenuRobe(kor_sifra As String, kor_vrstalookup As String) As Decimal
        Dim rezultat As Object
        Dim planska_cena As Decimal = 0
        Dim upit_cena_robe As String = "SELECT Kor_CenaN FROM KontaR WHERE Kor_Sifra ='{0}' AND Kor_VrstaLookUp='{1}'"

        Try

            rezultat = modOleDb.vratiJedVrednostUpit(String.Format(upit_cena_robe, kor_sifra, kor_vrstalookup))

            'IsNothing(rezultat) - artikal uopšte ne postoji
            If IsNothing(rezultat) Then
                planska_cena = 0
            Else
                planska_cena = Convert.ToDecimal(rezultat)
            End If

            Return planska_cena
        Catch ex As Exception
            'Dim msg As New MojMsgBox(String.Format("Greška prilikom provere naloga: ", ex.Message),
            '                         MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
            'msg.ShowDialog()
            Return 0

        End Try

    End Function
    Public Function vratiTezGajbe(kor_sifra As String, kor_vrstalookup As String) As Decimal
        Dim rezultat As Object
        Dim tezina As Decimal = 0
        Dim upit_tez_gajbe As String = "SELECT Kor_Tezina FROM KontaR WHERE Kor_Sifra ='{0}' AND Kor_VrstaLookUp='{1}'"

        Try

            rezultat = modOleDb.vratiJedVrednostUpit(String.Format(upit_tez_gajbe, kor_sifra, kor_vrstalookup))

            'IsNothing(rezultat) - artikal uopšte ne postoji
            If IsNothing(rezultat) Then
                tezina = 0
            Else
                tezina = Convert.ToDecimal(rezultat)
            End If

            Return tezina
        Catch ex As Exception
            'Dim msg As New MojMsgBox(String.Format("Greška prilikom provere naloga: ", ex.Message),
            '                         MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
            'msg.ShowDialog()
            Return 0

        End Try

    End Function
End Module
