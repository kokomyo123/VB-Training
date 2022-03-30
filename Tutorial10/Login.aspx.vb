Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("userDB").ConnectionString)
    Private cmd As SqlCommand

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs)
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("userDB").ConnectionString)
        cmd = New SqlCommand("SELECT Password FROM tbl_User WHERE Name=@Name and Password=@Password", con)
        cmd.Parameters.AddWithValue("Name", txtUserName.Text)
        cmd.Parameters.AddWithValue("Password", txtPassword.Text)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As DataTable = New DataTable()
        sda.Fill(dt)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()

        If dt.Rows.Count > 0 Then
            Session("name") = txtUserName.Text
            Response.Redirect("Default.aspx")
        Else
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("userDB").ConnectionString)
            Dim name As String = txtUserName.Text
            Dim id As Integer
            cmd = New SqlCommand("SELECT * FROM tbl_User WHERE Name='" & name & "'", con)
            con.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            While dr.Read()
                id = Convert.ToInt32(dr("id"))
                name = dr("Name").ToString()
                ResetPassword(id, name)
            End While

            con.Close()
        End If
    End Sub

    Private Sub ResetPassword(ByVal id As Integer, ByVal name As String)
        Session("Email") = name
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("userDB").ConnectionString)
        cmd = New SqlCommand("Update tbl_User set Password=NULL,Status=1 WHERE id=@id", con)
        cmd.Parameters.AddWithValue("id", id)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Reset.aspx")
    End Sub

End Class