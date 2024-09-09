Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.OleDb

Module modForma
	''' <summary>
	''' F-ja upisuje lokaciju i dimenzije poslate forme u bazu.
	''' </summary>
	''' <param name="forma"> Forma čije podatke treba upisati. </param>
	Public Sub upisiPozicijuUBazu(forma As Form)
		Try
			Dim provera_SQL As String = ""
			Dim upis_SQL As String = ""
			Dim izmena_SQL As String = ""

			'ukoliko postoje korisnici, proveravam dimenzije forme za trenutno ulogovanog korisnika
			If modUser.bool_user Then
				provera_SQL = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", forma.Name, login_info.sifra_korisnika)
			Else
				provera_SQL = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}'", forma.Name)
			End If

			'ukoliko f-ja vrati Nothing znači da red ne postoji pa ga upisujem, u suprotnom ga menjam
			If IsNothing(modOleDb.vratiJedVrednostUpit(provera_SQL)) Then
				'ukoliko postoje korisnici tada upisujem šifru korisnika za kojeg se čuvaju podešavanja  
				If modUser.bool_user Then
					upis_SQL = String.Format("INSERT INTO Width(wid_01, wid_02, wid_03, wid_04, wid_05, wid_tab_naziv, wid_Usr_Sifra) " &
														" VALUES({0}, {1}, {2}, {3}, {4}, '{5}', '{6}')", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y,
														forma.Size.Width, forma.Size.Height, forma.Name, login_info.sifra_korisnika)
				Else
					upis_SQL = String.Format("INSERT INTO Width(wid_01, wid_02, wid_03, wid_04, wid_05, wid_tab_naziv) " &
														" VALUES({0}, {1}, {2}, {3}, {4}, '{5}')", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y,
														forma.Size.Width, forma.Size.Height, forma.Name)
				End If

				modOleDb.izvrsiKomandu(upis_SQL)
			Else
				'ukoliko korisnici postoje izmena se vrši u podešavanjima za trenutno ulogovanog korisnika
				If modUser.bool_user Then
					izmena_SQL = String.Format(String.Format("UPDATE Width SET wid_01 = {0},  wid_02 = {1}, wid_03 = {2}, wid_04 = {3}, wid_05 = {4} " &
													 " WHERE wid_tab_naziv = '{5}' AND wid_Usr_Sifra = '{6}' ", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y, forma.Size.Width,
													  forma.Size.Height, forma.Name, login_info.sifra_korisnika))
				Else
					izmena_SQL = String.Format(String.Format("UPDATE Width SET wid_01 = {0},  wid_02 = {1}, wid_03 = {2}, wid_04 = {3}, wid_05 = {4} " &
													 " WHERE wid_tab_naziv = '{5}'", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y, forma.Size.Width,
													  forma.Size.Height, forma.Name))
				End If

				modOleDb.izvrsiKomandu(izmena_SQL)
			End If
		Catch ex As Exception
			Dim msg As New MojMsgBox(String.Format("Greška prilikom upisa veličine i lokacije forme:", ex.Message),
									 MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
			msg.ShowDialog()
		End Try
	End Sub

	''' <summary>
	''' F-ja čita lokaciju, dimenzije i orijantaciju za štampanje za poslatu formu.
	''' Za razliku od funkcije citajPozicijuIzBaze ovde se radi sa imenom a ne sa forma.name
	''' </summary>
	''' <param name="forma"> Forma čije podatke treba pročitati. </param>
	''' <param name="landscape"> Landscape štampanje(true, false) </param>
	Public Sub citajPozicIzBazePoslatoIme(forma As Form, strImeForme As String, ByRef landscape As Boolean)
		Dim upit As String = ""
		Dim reader As OleDbDataReader = Nothing
		'ukoliko se radi sa korisnicima tada čitam podešavanja trenutno ulogovanog korisnika

		If modUser.bool_user Then
			upit = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", strImeForme, login_info.sifra_korisnika)
		Else
			upit = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}'", strImeForme)
		End If

		Try
			reader = modOleDb.vratiDataReader(upit)
			If (reader.Read()) Then
				forma.WindowState = reader.GetInt32(0)
				'ukoliko je forma minimizirana tada je otvaram u normalnom stanju
				If forma.WindowState = FormWindowState.Minimized Then
					forma.WindowState = FormWindowState.Normal
				End If

				forma.Location = New Point(reader.GetInt32(1), reader.GetInt32(2))
				forma.Size = New System.Drawing.Size(reader.GetInt32(3), reader.GetInt32(4))
				forma.MinimumSize = New System.Drawing.Size(100, 100) 'HERE IS MY FIX
				landscape = If(reader.GetInt32(5) = 1, True, False)
			End If
		Catch ex As Exception
			MsgBox("Greška prilikom čitanja veličine i lokacije forme: " + ex.Message)
		Finally
			modOleDb.zatvoriDataReader(reader)
		End Try
	End Sub

	''' <summary>
	''' F-ja čita lokaciju i dimenzije poslate forme iz bazu.
	''' </summary>
	''' <param name="forma"> Forma čije podatke treba pročitati. </param>
	Public Sub citajPozicijuIzBaze(forma As Form)
		Dim upit As String = ""
		Dim reader As OleDbDataReader = Nothing

		'ukoliko se radi sa korisnicima tada čitam podešavanja trenutno ulogovanog korisnika
		If modUser.bool_user Then
			'upit = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", forma.Name, login_info.sifra_korisnika)
			upit = String.Format("SELECT wid_01, wid_02, wid_03, wid_04, wid_05, wid_06 FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", forma.Name, login_info.sifra_korisnika)
		Else
			'upit = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}'", forma.Name)
			upit = String.Format("SELECT  wid_01, wid_02, wid_03, wid_04, wid_05, wid_06 FROM Width WHERE wid_tab_naziv = '{0}'", forma.Name)
		End If

		Try
			reader = modOleDb.vratiDataReader(upit)
			If (reader.Read()) Then
				forma.WindowState = reader.GetInt32(0)
				'ukoliko je forma minimizirana tada je otvaram u normalnom stanju
				If forma.WindowState = FormWindowState.Minimized Then
					forma.WindowState = FormWindowState.Normal
				End If

				forma.Location = New Point(reader.GetInt32(1), reader.GetInt32(2))
				forma.Size = New System.Drawing.Size(reader.GetInt32(3), reader.GetInt32(4))
				forma.MinimumSize = New System.Drawing.Size(100, 100) 'HERE IS MY FIX
			End If
		Catch ex As Exception
			forma.WindowState = FormWindowState.Normal
			'MsgBox("Greska prilikom citanja velicine i lokacije forme: " + ex.Message)
		Finally
			modOleDb.zatvoriDataReader(reader)
		End Try
	End Sub

	''' <summary>
	''' F-ja upisuje lokaciju, dimenzije i orijantaciju za štampanje za poslatu formu.
	''' Za razliku od funkcije upisiPozicijuUBazu ovde se prosledi ime koje forme koje se cuva u tabeli Width
	''' </summary>
	''' <param name="forma"> Forma čije podatke treba upisati. </param>
	''' <param name="landscape"> Landscape štampanje(true, false) </param>
	Public Sub upisiPozicUBazuPrekoImena(forma As Form, strImeForme As String, landscape As Boolean)
		Try
			Dim provera_SQL As String = ""
			Dim upis_SQL As String = ""
			Dim izmena_SQL As String = ""

			'ukoliko postoje korisnici, proveravam dimenzije forme za trenutno ulogovanog korisnika
			If modUser.bool_user Then
				provera_SQL = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}' AND wid_Usr_Sifra = '{1}'", strImeForme, login_info.sifra_korisnika)
			Else
				provera_SQL = String.Format("SELECT * FROM Width WHERE wid_tab_naziv = '{0}'", strImeForme)
			End If

			'ukoliko f-ja vrati Nothing znači da red ne postoji pa ga upisujem, u suprotnom ga menjam
			If IsNothing(modOleDb.vratiJedVrednostUpit(provera_SQL)) Then
				'ukoliko postoje korisnici tada upisujem šifru korisnika za kojeg se čuvaju podešavanja  
				If modUser.bool_user Then
					upis_SQL = String.Format("INSERT INTO Width(wid_01, wid_02, wid_03, wid_04, wid_05, wid_06, wid_tab_naziv, wid_Usr_Sifra) " &
														" VALUES({0}, {1}, {2}, {3}, {4}, {5}, '{6}', '{7}')", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y,
														forma.Size.Width, forma.Size.Height, If(landscape = True, 1, 0), strImeForme, login_info.sifra_korisnika)
				Else
					upis_SQL = String.Format("INSERT INTO Width(wid_01, wid_02, wid_03, wid_04, wid_05, wid_06, wid_tab_naziv) " &
														" VALUES({0}, {1}, {2}, {3}, {4}, {5}, '{6}')", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y,
														forma.Size.Width, forma.Size.Height, If(landscape = True, 1, 0), strImeForme)
				End If

				modOleDb.izvrsiKomandu(upis_SQL)
			Else
				'ukoliko korisnici postoje izmena se vrši u podešavanjima za trenutno ulogovanog korisnika
				If modUser.bool_user Then
					izmena_SQL = String.Format(String.Format("UPDATE Width SET wid_01 = {0},  wid_02 = {1}, wid_03 = {2}, wid_04 = {3}, wid_05 = {4}, wid_06 = {5} " &
													 " WHERE wid_tab_naziv = '{6}' AND wid_Usr_Sifra = '{7}'", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y, forma.Size.Width,
													  forma.Size.Height, If(landscape = True, 1, 0), strImeForme, login_info.sifra_korisnika))
				Else
					izmena_SQL = String.Format(String.Format("UPDATE Width SET wid_01 = {0},  wid_02 = {1}, wid_03 = {2}, wid_04 = {3}, wid_05 = {4}, wid_06 = {5} " &
													 " WHERE wid_tab_naziv = '{6}'", Convert.ToInt32(forma.WindowState), forma.Location.X, forma.Location.Y, forma.Size.Width,
													  forma.Size.Height, If(landscape = True, 1, 0), strImeForme))
				End If

				modOleDb.izvrsiKomandu(izmena_SQL)
			End If
		Catch ex As Exception
			Dim msg As New MojMsgBox(String.Format("Greška prilikom upisa veličine, lokacije i orijentacije forme:", ex.Message),
									 MojMsgBox.enmVidljivaDugmad.Potvrdi, "Greška")
			msg.ShowDialog()
		End Try
	End Sub

End Module
