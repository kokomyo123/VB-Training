Imports System.Data.SqlClient

Public Class Add
    Inherits System.Web.UI.Page

    Dim constring As String = ConfigurationManager.ConnectionStrings("userDB").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetData()
        End If
    End Sub

    Private Sub GetData()
        Dim con As SqlConnection = New SqlConnection(constring)
        Dim cmd As SqlCommand = New SqlCommand("SELECT * from tbl_Cat", con)
        con.Open()
        Dim ds As DataSet = New DataSet()
        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        da.Fill(ds)
        gvPet.DataSource = ds
        gvPet.DataBind()
        con.Close()
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Not String.IsNullOrWhiteSpace(txtName.Text) Then

            If Not DataExist() Then
                Dim con As SqlConnection = New SqlConnection(constring)
                Dim cmd As SqlCommand = New SqlCommand("INSERT INTO tbl_Cat VALUES(@name)", con)
                cmd.Parameters.AddWithValue("name", txtName.Text)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMesage", "alert('Added successfully')", True)
                txtName.Text = ""
                GetData()
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMesage", "alert('Data already exists')", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMesage", "alert('Please fill name')", True)
        End If
    End Sub

    Private Function DataExist() As Boolean
        Dim con As SqlConnection = New SqlConnection(constring)
        Dim cmd As SqlCommand = New SqlCommand("SELECT * from tbl_Cat WHERE name=@name ", con)
        cmd.Parameters.AddWithValue("name", txtName.Text)
        con.Open()
        Dim dr As SqlDataReader = cmd.ExecuteReader()

        If dr.HasRows Then
            Return True
        End If

        Return False
    End Function

    Protected Sub gvPet_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPet.RowDeleting
        Dim id As Integer = Convert.ToInt32(gvPet.DataKeys(e.RowIndex).Value)
        Dim con As SqlConnection = New SqlConnection(constring)
        Dim cmd As SqlCommand = New SqlCommand("DELETE FROM tbl_Cat WHERE id=@id", con)
        cmd.Parameters.AddWithValue("id", id)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMesage", "alert('Deleted successfully')", True)
        GetData()
    End Sub

    Protected Sub gvPet_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPet.PageIndexChanging
        gvPet.PageIndex = e.NewPageIndex
        Me.GetData()
    End Sub

End Class