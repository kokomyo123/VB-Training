Imports System.Data.SqlClient

Public Class Edit
    Inherits System.Web.UI.Page

    Dim constring As String = ConfigurationManager.ConnectionStrings("userDB").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id As Integer = Convert.ToInt32(Request.QueryString("id"))
            Dim con As SqlConnection = New SqlConnection(constring)
            Dim cmd As SqlCommand = New SqlCommand("SELECT * from tbl_Cat WHERE id=@id", con)
            cmd.Parameters.AddWithValue("id", id)
            con.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            While dr.Read()
                Dim petid As String = dr("id").ToString()
                Dim name As String = dr("name").ToString()
                txtName.Text = name
                Lblpetid.Text = petid
            End While

            con.Close()
        End If
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtName.Text <> "" Then
            Dim id As Integer = Convert.ToInt32(Lblpetid.Text)
            Dim name As String = txtName.Text
            Dim con As SqlConnection = New SqlConnection(constring)
            Dim cmd As SqlCommand = New SqlCommand("UPDATE tbl_Cat SET name=@name WHERE id=@id", con)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.Parameters.AddWithValue("@name", name)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Response.Redirect("Add.aspx")
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMesage", "alert('Please fill name')", True)
        End If
    End Sub

End Class