using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class UserActivity : System.Web.UI.Page
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
                if (Request.QueryString["UserID"] != null)
                {
                    int userID = int.Parse(Request.QueryString["UserID"]);
                    LoadUserActivity(userID);
                }
                else
                {
                    Response.Redirect("AllUsers.aspx");
                }
            }
        }

        private void LoadUserActivity(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;

            // Load Borrowed Books
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        B.Title, 
                        B.Author, 
                        B.ImagePath, 
                        BI.IssueDate, 
                        BI.ReturnDate, 
                        BI.IsReturned 
                    FROM BookIssues BI 
                    INNER JOIN Books B ON BI.BookID = B.BookID 
                    WHERE BI.UserID = @UserID AND BI.IsReturned = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                rptBorrowedBooks.DataSource = reader;
                rptBorrowedBooks.DataBind();
                lblNoBorrowed.Visible = rptBorrowedBooks.Items.Count == 0; // FIXED: Use Items.Count
                reader.Close();
            }

            // Load Pending Requests
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        B.Title, 
                        B.Author, 
                        B.ImagePath, 
                        BR.RequestedAt 
                    FROM BookRequests BR 
                    INNER JOIN Books B ON BR.BookID = B.BookID 
                    WHERE BR.UserID = @UserID AND BR.IsIssued = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                rptPendingRequests.DataSource = reader;
                rptPendingRequests.DataBind();
                lblNoPending.Visible = rptPendingRequests.Items.Count == 0; // FIXED: Use Items.Count
                reader.Close();
            }
        }
    }
}