Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim constring As String = ConfigurationManager.ConnectionStrings("userDB").ConnectionString
        Dim con As SqlConnection = New SqlConnection(constring)
        Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM tbl_User WHERE Name=@Name and Password=@Password", con)
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
            Session.RemoveAll()
        Else
            LtlMessage.Text = "Username or Password is Wroung. Try again!!!"
        End If
    End Sub

End Class