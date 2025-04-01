using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ELibrary
{
    public partial class IssueBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Restrict access to admin only
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Load users and books into dropdowns
                LoadUsers();
                LoadBooks();
            }
        }

        private void LoadUsers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Username FROM Users WHERE IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ddlUser.DataSource = dt;
                ddlUser.DataBind();
            }
        }

        private void LoadBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT BookID, Title FROM Books";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ddlBook.DataSource = dt;
                ddlBook.DataBind();
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(ddlUser.SelectedValue);
            int bookID = int.Parse(ddlBook.SelectedValue);
            DateTime issueDate = DateTime.Parse(txtIssueDate.Text);
            DateTime returnDate = DateTime.Parse(txtReturnDate.Text);

            // Insert issue record into database
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO BookIssues (UserID, BookID, IssueDate, ReturnDate, Fine, IsReturned) " +
                               "VALUES (@UserID, @BookID, @IssueDate, @ReturnDate, @Fine, @IsReturned)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@BookID", bookID);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ReturnDate", returnDate);
                command.Parameters.AddWithValue("@Fine", 0.00); // Default fine
                command.Parameters.AddWithValue("@IsReturned", false); // Default not returned

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show success popup and redirect
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "showSuccessPopup('Book issued successfully!'); setTimeout(redirectToAdminDashboard, 1000);", true);
                    }
                    else
                    {
                        // Show error message
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to issue book.');", true);
                    }
                }
                catch (Exception ex)
                {
                    // Handle database errors
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect to admin dashboard
            Response.Redirect("AdminDashboard.aspx");
        }
    }
}