using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ELibrary
{
    public partial class UserDashboard : System.Web.UI.Page
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
                // Bind user stats to the frontend
                Page.DataBind();
            }
        }

        // Get username from session
        protected string GetUsername()
        {
            return Session["Username"]?.ToString() ?? "User";
        }

        // Get user's balance from session
        protected decimal GetBalance()
        {
            return Session["Balance"] != null ? (decimal)Session["Balance"] : 0.00m;
        }

        // Get active request count
        protected int GetActiveRequestsCount()
        {
            int userID = (int)Session["UserID"];
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM BookRequests WHERE UserID = @UserID AND IsIssued = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        // Get borrowed books count
        protected int GetBorrowedBooksCount()
        {
            int userID = (int)Session["UserID"];
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM BookIssues WHERE UserID = @UserID AND IsReturned = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        // Redirect to UsersViewBook.aspx when "Browse Books" button is clicked
        protected void btnBrowseBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersViewBook.aspx");
        }

        // Redirect to BorrowedBooks.aspx when "Borrowed Books" button is clicked
        protected void btnBorrowedBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersBorrowedBooks.aspx");
        }

        // Redirect to ChangePassword.aspx when "Change Password" button is clicked
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
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