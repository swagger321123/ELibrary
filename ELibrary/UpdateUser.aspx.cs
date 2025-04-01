using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ELibrary
{
    public partial class UpdateUser : System.Web.UI.Page
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
                // Load user details if UserID is provided in the query string
                if (Request.QueryString["UserID"] != null)
                {
                    int userID = int.Parse(Request.QueryString["UserID"]);
                    LoadUserDetails(userID);
                }
            }
        }

        private void LoadUserDetails(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, Fullname, Gender, PhoneNo, Email, UserType, IsActive FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtUserID.Text = userID.ToString();
                        txtUsername.Text = reader["Username"].ToString();
                        txtFullname.Text = reader["Fullname"].ToString();
                        ddlGender.SelectedValue = reader["Gender"].ToString();
                        txtPhoneNo.Text = reader["PhoneNo"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        ddlUserType.SelectedValue = reader["UserType"].ToString();
                        chkIsActive.Checked = (bool)reader["IsActive"];
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(txtUserID.Text);
            string username = txtUsername.Text.Trim();
            string fullname = txtFullname.Text.Trim();
            string gender = ddlGender.SelectedValue;
            string phoneNo = txtPhoneNo.Text.Trim();
            string email = txtEmail.Text.Trim();
            string userType = ddlUserType.SelectedValue;
            bool isActive = chkIsActive.Checked;

            // Update user in the database
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Username = @Username, Fullname = @Fullname, Gender = @Gender, PhoneNo = @PhoneNo, Email = @Email, UserType = @UserType, IsActive = @IsActive WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Fullname", fullname);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@PhoneNo", phoneNo);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@UserType", userType);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show success popup and redirect
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "showSuccessPopup('User updated successfully!'); setTimeout(redirectToAdminDashboard, 1000);", true);
                    }
                    else
                    {
                        // Show error message
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to update user.');", true);
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