Imports System.Data.OleDb

Public Class kendaliDB
    'buat koneksi database
    Private DBCon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" & "Data Source=Jadwal.accdb;")

    'siapkan perintah DB
    Private perintah As OleDbCommand

    'data DB
    Public DBDA As OleDbDataAdapter
    Public DBDT As DataTable

    'parameter kueri
    Public Params As New List(Of OleDbParameter)

    'statistik kueri
    Public jumlahdata As Integer
    Public pengecualian As String

    Public Sub jalankan(ByVal kueri As String)
        'ulang statistik kueri
        jumlahdata = 0
        pengecualian = ""

        Try
            'buka koneksi
            DBCon.Open()

            'buat perintah DB
            perintah = New OleDbCommand(kueri, DBCon)

            'muat parameter ke perntah DB
            Params.ForEach(Sub(p) perintah.Parameters.Add(p))

            'bersihkan parameter
            Params.Clear()

            'jalankan perintah dan masukan ke tabel data
            DBDT = New DataTable
            DBDA = New OleDbDataAdapter(perintah)
            jumlahdata = DBDA.Fill(DBDT)
        Catch ex As Exception
            pengecualian = ex.Message
        End Try

        'tutup koneksi
        If DBCon.State = ConnectionState.Open Then DBCon.Close()
    End Sub

    'masukan kueri dan parameter perintah
    Public Sub tambahparam(ByVal Nama As String, ByVal Nilai As Object)
        Dim parambaru As New OleDbParameter(Nama, Nilai)
        Params.Add(parambaru)
    End Sub
End Class
