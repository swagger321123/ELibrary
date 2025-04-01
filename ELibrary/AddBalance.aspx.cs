using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class AddBalance : System.Web.UI.Page
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
                LoadUsersDropdown();
            }
        }

        private void LoadUsersDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Username FROM Users WHERE UserType = 'User' AND IsActive = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    ddlUsers.DataSource = reader;
                    ddlUsers.DataTextField = "Username";
                    ddlUsers.DataValueField = "UserID";
                    ddlUsers.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error loading users: {ex.Message}";
                    lblMessage.CssClass = "text-danger";
                }
            }
        }

        protected void btnAddBalance_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            int userID = int.Parse(ddlUsers.SelectedValue);
            decimal amount = decimal.Parse(txtAmount.Text.Trim());

            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Update the Balance column (not Fine)
                    string query = "UPDATE Users SET Balance = Balance + @Amount WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@UserID", userID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Balance added successfully!";
                        lblMessage.CssClass = "text-success";
                        txtAmount.Text = ""; // Clear input
                    }
                    else
                    {
                        lblMessage.Text = "Failed to update balance.";
                        lblMessage.CssClass = "text-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"An error occurred: {ex.Message}";
                    lblMessage.CssClass = "text-danger";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect to Admin Dashboard
            Response.Redirect("AdminDashboard.aspx");
        }
    }
}