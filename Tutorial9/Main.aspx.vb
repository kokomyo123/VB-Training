Imports System.Data.OleDb
Imports System.IO

Public Class Main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim excelfilepath As String = Server.MapPath("~/Upload/User.xlsx")

        If File.Exists(excelfilepath) Then
            ImoprtExceltoGridView(excelfilepath, ".xlsx", "Yes")
        Else
            Response.Write("There is no file to read")
        End If
    End Sub

    Public Sub ImoprtExceltoGridView(ByVal File As String, ByVal extension As String, ByVal ishdr As String)
        Dim connectionStr As String = ConfigurationManager.ConnectionStrings("Excelconnection").ConnectionString
        connectionStr = String.Format(connectionStr, File, ishdr)
        Dim con As OleDbConnection = New OleDbConnection(connectionStr)
        Dim cmd As OleDbCommand = New OleDbCommand()
        Dim oda As OleDbDataAdapter = New OleDbDataAdapter()
        Dim dt As DataTable = New DataTable()
        cmd.Connection = con
        con.Open()
        Dim dtexcel As DataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim sheetname As String = dtexcel.Rows(0)("TABLE_NAME").ToString()
        cmd.CommandText = "Select * from [" & sheetname & "]"
        oda.SelectCommand = cmd
        oda.Fill(dt)
        con.Close()
        gvFile.DataSource = dt
        gvFile.DataBind()
    End Sub

    Protected Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click
        Dim csvPath As String = Server.MapPath("~/Upload/User.csv")

        If File.Exists(csvPath) Then
            Dim dt As DataTable = New DataTable()
            dt.Columns.AddRange(New DataColumn(3) {New DataColumn("Id"), New DataColumn("Name"), New DataColumn("Address"), New DataColumn("Email")})
            Dim csvData As String = File.ReadAllText(csvPath)

            For Each row As String In csvData.Split(vbLf)

                If Not String.IsNullOrEmpty(row) Then
                    dt.Rows.Add()
                    Dim i As Integer = 0

                    For Each cell As String In row.Split(","c)
                        dt.Rows(dt.Rows.Count - 1)(i) = cell
                        i += 1
                    Next
                End If
            Next

            gvFile.DataSource = dt
            gvFile.DataBind()
        Else
            Response.Write("There is no file to read")
        End If
    End Sub

    Protected Sub btnText_Click(sender As Object, e As EventArgs) Handles btnText.Click
        Dim csvPath As String = Server.MapPath("~/Upload/User.txt")

        If File.Exists(csvPath) Then
            Dim dt As DataTable = New DataTable()
            dt.Columns.AddRange(New DataColumn(3) {New DataColumn("Id"), New DataColumn("Name"), New DataColumn("Address"), New DataColumn("Email")})
            Dim csvData As String = File.ReadAllText(csvPath)

            For Each row As String In csvData.Split(vbLf)

                If Not String.IsNullOrEmpty(row) Then
                    dt.Rows.Add()
                    Dim i As Integer = 0

                    For Each cell As String In row.Split(","c)
                        dt.Rows(dt.Rows.Count - 1)(i) = cell
                        i += 1
                    Next
                End If
            Next

            gvFile.DataSource = dt
            gvFile.DataBind()
        Else
            Response.Write("There is no file to read")
        End If
    End Sub

    Protected Sub btnDoc_Click(sender As Object, e As EventArgs) Handles btnDoc.Click
        Dim MyWord As Microsoft.Office.Interop.Word.Application = New Microsoft.Office.Interop.Word.Application()
        Dim sFile As String = Server.MapPath("~/Upload/User.docx")

        If File.Exists(sFile) Then
            Dim rstData As DataTable = New DataTable()
            MyWord.Documents.Open(sFile)
            Dim MyDoc As Microsoft.Office.Interop.Word.Document = MyWord.ActiveDocument
            Dim MyTable As Microsoft.Office.Interop.Word.Table = MyDoc.Tables(1)
            Dim iCol As Integer = 0

            For iCol = 1 To MyTable.Columns.Count
                rstData.Columns.Add(CleanCellText(MyTable.Rows(1).Cells(iCol).Range.Text), GetType(String))
            Next

            Dim iRows As Integer = 0

            For iRows = 2 To MyTable.Rows.Count
                Dim OneRow As DataRow = rstData.NewRow()

                For iCol = 1 To MyTable.Columns.Count
                    OneRow(iCol - 1) = CleanCellText(MyTable.Rows(iRows).Cells(iCol).Range.Text)
                Next

                rstData.Rows.Add(OneRow)
            Next

            MyDoc.Close()
            MyWord.Quit()
            gvFile.DataSource = rstData
            gvFile.DataBind()
        Else
            Response.Write("There is no file to read")
        End If
    End Sub

    Private Function CleanCellText(ByVal s As String) As String
        Dim MyClean As String = (ChrW(13)).ToString() & (ChrW(7)).ToString()
        Return s.Replace(MyClean, "")
    End Function

End Class