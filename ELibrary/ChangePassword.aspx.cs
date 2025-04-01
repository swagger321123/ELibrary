using System;
using System.Configuration;
using System.Data.SqlClient;
using BCrypt.Net;

namespace ELibrary
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if not logged in as a user
            if (Session["UserID"] == null || Session["UserType"].ToString() != "User")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Pre-fill username field with session data
                txtUsername.Text = Session["Username"].ToString();
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblErrorMessage.Text = "Please fill in all fields.";
                lblErrorMessage.Visible = true;
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblErrorMessage.Text = "New password and confirm password do not match.";
                lblErrorMessage.Visible = true;
                return;
            }

            // Retrieve connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Password FROM Users WHERE Username = @Username AND IsActive = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedHash = reader["Password"].ToString();

                        // Verify old password
                        if (BCrypt.Net.BCrypt.Verify(oldPassword, storedHash))
                        {
                            // Hash the new password
                            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                            // Update password in the database
                            UpdatePassword(username, hashedNewPassword);
                            lblErrorMessage.Text = "Password changed successfully!";
                            lblErrorMessage.CssClass = "text-success";
                            lblErrorMessage.Visible = true;
                        }
                        else
                        {
                            lblErrorMessage.Text = "Old password is incorrect.";
                            lblErrorMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "User not found.";
                        lblErrorMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = "An error occurred. Please try again later.";
                    lblErrorMessage.Visible = true;
                }
            }
        }

        private void UpdatePassword(string username, string hashedNewPassword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Password = @Password WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", hashedNewPassword);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect to user dashboard
            Response.Redirect("UserDashboard.aspx");
        }
    }
}