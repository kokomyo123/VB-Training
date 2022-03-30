Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class Reset
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private constring As String = ConfigurationManager.ConnectionStrings("userDB").ConnectionString

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not String.IsNullOrEmpty(txtNewPass.Text) AndAlso Not String.IsNullOrEmpty(txtConfirmPass.Text) Then

            If txtNewPass.Text = txtConfirmPass.Text Then
                SendEmail()
                Dim email As String = Session("Email").ToString()
                Dim con As SqlConnection = New SqlConnection(constring)
                Dim cmd As SqlCommand = New SqlCommand("Update tbl_User Set Password=@Password,Status=0 Where Name=@Name", con)
                cmd.Parameters.AddWithValue("Name", email)
                cmd.Parameters.AddWithValue("Password", txtNewPass.Text)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                Response.Redirect("Login.aspx")
            Else
                LtlMessage.Text = "confirm password must be same with new password"
            End If
        Else
            LtlMessage.Text = "please fill your password and confirm password"
        End If
    End Sub

    Private Sub SendEmail()
        Try
            Dim [to] As String = Session("Email").ToString()
            Dim from As String = "yehtetaung791998@gmail.com"
            Dim message As MailMessage = New MailMessage(from, [to])
            Dim mailbody As String = "<center><h1>Reset Password</h1></center>"
            mailbody += "<p>Dear Sir,</p><br>"
            mailbody += "<p style='padding-left:30px;'>Congratulation</p>"
            mailbody += "<p style='padding-left:30px;'>Resetting password successful</p>"
            mailbody += "<p style='padding-left:100px;'>From Company</p>"
            message.Subject = "Resetting password"
            message.Body = mailbody
            message.BodyEncoding = Encoding.UTF8
            message.IsBodyHtml = True
            Dim client As SmtpClient = New SmtpClient("smtp.gmail.com", 578)
            Dim basicCredential1 As System.Net.NetworkCredential = New System.Net.NetworkCredential("myominhtay01.mmh@gmail.com", "password")
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = basicCredential1
            client.Send(message)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class