using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class AllUsers : System.Web.UI.Page
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
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            string searchTerm = ViewState["SearchTerm"] as string ?? "";
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT UserID, Username, Fullname, Gender, Email, UserType, IsActive, CreatedAt 
                        FROM Users 
                        WHERE UserType = 'User'"; // Only show non-admin users

                    // Add search condition if searchTerm is provided
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += @"
                            AND (
                                Username LIKE @SearchTerm OR 
                                Fullname LIKE @SearchTerm
                            )";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    rptUsers.DataSource = reader;
                    rptUsers.DataBind();

                    // Show/hide no results message
                    lblNoResults.Visible = rptUsers.Items.Count == 0;
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Save search term to ViewState and reload users
            ViewState["SearchTerm"] = txtSearch.Text.Trim();
            LoadUsers();
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                DeleteUser(int.Parse(e.CommandArgument.ToString()));
            }
        }

        private void DeleteUser(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the user has active book issues
                    string checkQuery = "SELECT COUNT(*) FROM BookIssues WHERE UserID = @UserID AND IsReturned = 0";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@UserID", userID);
                    int activeIssues = (int)checkCommand.ExecuteScalar();

                    if (activeIssues > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Cannot delete user. They have active book issues.');", true);
                        return;
                    }

                    // Delete the user
                    string deleteQuery = "DELETE FROM Users WHERE UserID = @UserID";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@UserID", userID);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "alert('User deleted successfully!');", true);
                        LoadUsers(); // Reload with current search term
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to delete user.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }
    }
}