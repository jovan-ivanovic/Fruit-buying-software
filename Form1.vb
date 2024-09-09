Imports System.Data.OleDb
Public Class Form1
    Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\korisnik\Documents\databaseVezba.accdb")
    Dim cmd As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim dt As DataTable

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            con.Open()
            da = New OleDbDataAdapter("SELECT * FROM Ljudi", con)
            dt = New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            con.Open()
            cmd = New OleDbCommand("INSERT INTO ljudi (sifra, ime, prezime, pozicija) VALUES (@sifra, @ime, @prezime, @pozicija)", con)
            cmd.Parameters.AddWithValue("@sifra", TextBox1.Text)
            cmd.Parameters.AddWithValue("@ime", TextBox2.Text)
            cmd.Parameters.AddWithValue("@prezime", TextBox3.Text)
            cmd.Parameters.AddWithValue("@pozicija", TextBox4.Text)
            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("Podatak uspešno dodat")
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            con.Open()
            cmd = New OleDbCommand("UPDATE ljudi SET ime=@ime, prezime=@prezime, pozicija=@pozicija WHERE sifra=@sifra", con)
            cmd.Parameters.AddWithValue("@ime", TextBox1.Text)
            cmd.Parameters.AddWithValue("@prezime", TextBox2.Text)
            cmd.Parameters.AddWithValue("@pozicija", TextBox3.Text)
            cmd.Parameters.AddWithValue("@sifra", TextBox4.Text)
            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("Podaci uspešno izmenjeni")
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            con.Open()
            cmd = New OleDbCommand("DELETE FROM ljudi WHERE sifra=@sifra", con)
            cmd.Parameters.AddWithValue("@sifra", TextBox1.Text)
            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("Podaci uspešno obrisani")
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer = DataGridView1.CurrentRow.Index
        TextBox1.Text = DataGridView1.Item(0, i).Value.ToString()
        TextBox2.Text = DataGridView1.Item(1, i).Value.ToString()
        TextBox3.Text = DataGridView1.Item(2, i).Value.ToString()
        TextBox4.Text = DataGridView1.Item(3, i).Value.ToString()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Isprazni sva polja
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

        ' Pronađi sledeću šifru
        TextBox1.Text = vratiSledecuSifru()
    End Sub
    Private Function VratiSledecuSifru() As Integer
        Dim sledecaSifra As Integer = 1
        Try
            Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\korisnik\Documents\databaseVezba.accdb")
                con.Open()
                Using cmd As New OleDbCommand("SELECT MAX(sifra) FROM ljudi", con)
                    Dim rezultat = cmd.ExecuteScalar()
                    If Not IsDBNull(rezultat) Then
                        sledecaSifra = Convert.ToInt32(rezultat) + 1
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Greška prilikom dodeljivanja šifre: " & ex.Message)
        End Try
        Return sledecaSifra
    End Function
End Class