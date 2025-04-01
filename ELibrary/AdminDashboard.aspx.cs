using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ELibrary
{
    public partial class AdminDashboard : System.Web.UI.Page
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
                // Bind data to the frontend
                Page.DataBind();
            }
        }

        // Get the count of pending user requests
        protected int GetRequestCount()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM BookRequests WHERE IsIssued = 0";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                    return 0;
                }
            }
        }

        // Redirect to User Registration Page
        protected void btnUserRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        // Redirect to Add Books Page
        protected void btnAddBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBooks.aspx");
        }

        // Redirect to Update Books Page
        protected void btnUpdateBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateBooks.aspx");
        }

        // Redirect to Update User Page
        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateUser.aspx");
        }

        // Redirect to Admin Book List Page
        protected void btnAdminBookList_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookList.aspx");
        }

        // Redirect to All Users Page
        protected void btnAllUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllUsers.aspx");
        }

        // Redirect to Admin Return Books Page
        protected void btnAdminReturnBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminReturnBooks.aspx");
        }

        // Redirect to Add Balance Page
        protected void btnAddBalance_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBalance.aspx");
        }

        // Redirect to User Requested Books Page
        protected void btnUserRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBooksUserRequested.aspx");
        }

        // Handle Logout Button Click
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session data
            Session.Clear();
            Session.Abandon();

            // Redirect to Login Page
            Response.Redirect("Login.aspx");
        }
    }
}