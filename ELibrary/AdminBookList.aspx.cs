using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class AdminBookList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Restrict access to admin only
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                ViewState["SearchTerm"] = ""; // Initialize ViewState for search term
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            string searchTerm = ViewState["SearchTerm"] as string ?? "";
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT BookID, Title, Author, Description, ImagePath, Price, IsOnDefaultPage 
                        FROM Books 
                        WHERE 1=1";

                    // Apply search term if available
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += @"
                            AND (
                                Title LIKE @SearchTerm OR 
                                Author LIKE @SearchTerm
                            )";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    rptBooks.DataSource = reader;
                    rptBooks.DataBind();

                    // Show/hide no results message
                    lblNoResults.Visible = rptBooks.Items.Count == 0;
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Save search term to ViewState and reload books
            ViewState["SearchTerm"] = txtSearch.Text.Trim();
            LoadBooks();
        }

        protected void rptBooks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToggleDisplay")
            {
                ToggleDisplay(int.Parse(e.CommandArgument.ToString()));
            }
            else if (e.CommandName == "DeleteBook")
            {
                DeleteBook(int.Parse(e.CommandArgument.ToString()));
            }
        }

        private void ToggleDisplay(int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Books SET IsOnDefaultPage = ~IsOnDefaultPage WHERE BookID = @BookID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookID", bookID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "alert('Display status updated successfully!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to update display status.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
            LoadBooks(); // Reload with current search term from ViewState
        }

        private void DeleteBook(int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the book is currently issued
                    string checkQuery = "SELECT COUNT(*) FROM BookIssues WHERE BookID = @BookID AND IsReturned = 0";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@BookID", bookID);
                    int activeIssues = (int)checkCommand.ExecuteScalar();

                    if (activeIssues > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Cannot delete book. It is currently issued to a user.');", true);
                        return;
                    }

                    // Delete the book
                    string deleteQuery = "DELETE FROM Books WHERE BookID = @BookID";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@BookID", bookID);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "alert('Book deleted successfully!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to delete book.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
            LoadBooks(); // Reload with current search term from ViewState
        }
    }
}