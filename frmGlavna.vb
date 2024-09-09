Public Class frmGlavna


    Private Sub btn_Otkup_Click(sender As Object, e As EventArgs) Handles btn_Otkup.Click,
                                                                          btn_Rob.Click,
                                                                          btn_Polj_Frime.Click
        Select Case sender.name
            Case btn_Otkup.Name
                frmOtkupZ.Show()
            Case btn_Rob.Name
                frmSifArtikli.Show()
            Case btn_Polj_Frime.Name
                frmSifDob.Show()
        End Select

    End Sub

    Private Sub sif_Poljo_Proiz_Click(sender As Object, e As EventArgs) Handles sif_Poljo_Proiz.Click,
                                                                                sif_Otk_Mes.Click,
                                                                                sif_Poljo_Firme.Click,
                                                                                sif_Rok_Uplat.Click,
                                                                                sif_Ambalaza.Click
        Select Case sender.name
            Case sif_Poljo_Proiz.Name
                frmSifArtikli.Show()
            Case sif_Otk_Mes.Name
                frmSifOtkMes.Show()
            Case sif_Poljo_Firme.Name
                frmSifDob.Show()
            Case sif_Rok_Uplat.Name
                frmSifDok.Show()
            Case sif_Ambalaza.Name
                frmSifOtkAmb.Show()
        End Select


    End Sub
End Class
