using System;
using System.Configuration;
using System.Data.SqlClient;
using BCrypt.Net;

namespace ELibrary
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Page load logic (if needed)
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullname = txtFullname.Text.Trim();
            string gender = ddlGender.SelectedValue; // M or F
            string phoneNo = txtPhoneNo.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(phoneNo) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblErrorMessage.Text = "Please fill in all fields.";
                lblErrorMessage.Visible = true;
                return;
            }

            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Fullname, Gender, PhoneNo, Email, Password, UserType, Balance, IsActive, CreatedAt) " +
                                   "VALUES (@Username, @Fullname, @Gender, @PhoneNo, @Email, @Password, @UserType, @Balance, @IsActive, @CreatedAt)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Fullname", fullname);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@UserType", "User"); // Default user type
                    command.Parameters.AddWithValue("@Balance", 0.00); // Default balance
                    command.Parameters.AddWithValue("@IsActive", 1); // Default active status
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Registration successful
                        Response.Redirect("AdminDashboard.aspx");
                    }
                    else
                    {
                        // Registration failed
                        lblErrorMessage.Text = "Registration failed. Please try again.";
                        lblErrorMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    lblErrorMessage.Text = "An error occurred. Please try again later.";
                    lblErrorMessage.Visible = true;
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
        }
    }
}