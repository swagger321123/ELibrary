using System;
using System.Configuration;
using System.Data.SqlClient;
using BCrypt.Net;

namespace ELibrary
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblErrorMessage.Text = "Please enter both username and password.";
                lblErrorMessage.Visible = true;
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            UserID, 
                            Username, 
                            Password, 
                            UserType, 
                            IsActive, 
                            Balance 
                        FROM Users 
                        WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        bool isActive = (bool)reader["IsActive"];
                        if (!isActive)
                        {
                            lblErrorMessage.Text = "Your account is inactive. Contact admin.";
                            lblErrorMessage.Visible = true;
                            return;
                        }

                        string storedHash = reader["Password"].ToString();
                        if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                        {
                            // Set session variables
                            Session["UserID"] = (int)reader["UserID"];
                            Session["Username"] = username;
                            Session["UserType"] = reader["UserType"].ToString();
                            Session["Balance"] = (decimal)reader["Balance"];

                            // Redirect based on user type
                            if (Session["UserType"].ToString() == "Admin")
                            {
                                Response.Redirect("AdminDashboard.aspx");
                            }
                            else
                            {
                                Response.Redirect("UserDashboard.aspx");
                            }
                        }
                        else
                        {
                            lblErrorMessage.Text = "Invalid username or password.";
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
                    lblErrorMessage.Text = $"An error occurred: {ex.Message}";
                    lblErrorMessage.Visible = true;
                }
            }
        }
    }
}