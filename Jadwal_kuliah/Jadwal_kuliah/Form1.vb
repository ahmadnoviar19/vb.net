Public Class Form1
    Private akses As New kendaliDB
    Public pubhari As String
    Public pubjammulai As String
    Public pubjamselesai As String

    Private Function tdkKosong(ByVal teks As String) As Boolean
        Return Not String.IsNullOrEmpty(teks)
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub btn_aksi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_aksi.Click
        'jalankan kueri
        jalankan()
    End Sub

    Private Sub ambilhari()
        Dim noew As String
        noew = Date.Now.ToString("dddd")
        Select Case noew
            Case "Monday"
                pubhari = "Senin"
            Case "Tuesday"
                pubhari = "Selasa"
            Case "Wednesday"
                pubhari = "Rabu"
            Case "Thursday"
                pubhari = "Kamis"
            Case "Friday"
                pubhari = "Jumat"
            Case "Saturday"
                pubhari = "Sabtu"
        End Select
        Label1.Text = pubhari
    End Sub

    Private Sub cari()
        akses.tambahparam("@nip", txt_nip.Text)
        akses.jalankan("SELECT Jam_masuk, Jam_Keluar From " & pubhari & " WHERE NIP = @nip")
        For Each waktu As DataRow In akses.DBDT.Rows
            pubjammulai = waktu("Jam_masuk")
            pubjamselesai = waktu("Jam_keluar")
        Next
        Label2.Text = pubjammulai
        Label3.Text = pubjamselesai
    End Sub


    Public Sub jalankan()
        ambilhari()
        cari()

        Dim jam As Integer
        Dim menit As Integer
        Dim rentangwaktu(3) As Integer
        Dim mulai(2) As String
        Dim selesai(2) As String

        jam = Val(Date.Now.ToString("HH"))
        menit = Val(Date.Now.ToString("mm"))

        rentangwaktu(0) = (jam * 60) + menit

        Try
            mulai = pubjammulai.Split(":")
            selesai = pubjamselesai.Split(":")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pengecualian : ")
        End Try

        rentangwaktu(1) = (Val(mulai(0)) * 60) + Val(mulai(1))
        rentangwaktu(2) = (Val(selesai(0)) * 60) + Val(selesai(1))

        If rentangwaktu(1) <= rentangwaktu(0) And rentangwaktu(2) > rentangwaktu(0) Then
            MessageBox.Show("OK", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lbl_ket.Text = "Kelas dibuka"
        Else
            MessageBox.Show("NO", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lbl_ket.Text = "Tidak ada jadwal"
        End If
        txt_nip.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Timer1.Enabled = True Then lbl_jam.Text = Date.Now.ToString("dddd, HH:mm:ss") Else Timer1.Start()
    End Sub
End Class
