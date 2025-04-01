using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class UsersViewBook : System.Web.UI.Page
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
                ViewState["SearchTerm"] = "";
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            int userID = (int)Session["UserID"];
            string searchTerm = ViewState["SearchTerm"] as string ?? "";

            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            B.BookID, 
                            B.Title, 
                            B.Author, 
                            B.Description, 
                            B.ImagePath, 
                            B.Price, 
                            BR.RequestID, 
                            BR.IsIssued, 
                            BI.IssueID, 
                            BI.IsReturned 
                        FROM Books B 
                        LEFT JOIN BookRequests BR ON B.BookID = BR.BookID AND BR.UserID = @UserID AND BR.IsIssued = 0 
                        LEFT JOIN BookIssues BI ON B.BookID = BI.BookID AND BI.UserID = @UserID AND BI.IsReturned = 0";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " WHERE (B.Title LIKE @SearchTerm OR B.Author LIKE @SearchTerm)";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    rptBooks.DataSource = reader;
                    rptBooks.DataBind();

                    lblNoResults.Visible = rptBooks.Items.Count == 0;
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void rptBooks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnBorrow = (Button)e.Item.FindControl("btnBorrow");
                int? requestID = DataBinder.Eval(e.Item.DataItem, "RequestID") as int?;
                int? issueID = DataBinder.Eval(e.Item.DataItem, "IssueID") as int?;

                // Check if the book is currently borrowed (IssueID exists)
                if (issueID.HasValue)
                {
                    btnBorrow.Text = "Borrowed";
                    btnBorrow.Enabled = false;
                    btnBorrow.CssClass = "btn btn-success disabled";
                }
                // Check if there is a pending request (RequestID exists)
                else if (requestID.HasValue)
                {
                    btnBorrow.Text = "Cancel Borrow";
                    btnBorrow.CssClass = "btn btn-warning";
                }
                else
                {
                    btnBorrow.Text = "Borrow Book";
                    btnBorrow.CssClass = "btn btn-primary";
                }
            }
        }

        protected void rptBooks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToggleRequest")
            {
                int bookID = int.Parse(e.CommandArgument.ToString());
                int userID = (int)Session["UserID"];

                bool hasActiveBorrow = CheckActiveBorrow(userID, bookID);
                bool hasPendingRequest = CheckPendingRequest(userID, bookID);

                if (hasActiveBorrow)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Book is already borrowed. Contact admin to return.');", true);
                    return;
                }

                if (hasPendingRequest)
                {
                    DeleteRequest(userID, bookID);
                }
                else
                {
                    InsertRequest(userID, bookID);
                }

                LoadBooks(); // Refresh the list
            }
        }

        private bool CheckActiveBorrow(int userID, int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IssueID FROM BookIssues WHERE UserID = @UserID AND BookID = @BookID AND IsReturned = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@BookID", bookID);

                connection.Open();
                return command.ExecuteScalar() != null;
            }
        }

        private bool CheckPendingRequest(int userID, int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestID FROM BookRequests WHERE UserID = @UserID AND BookID = @BookID AND IsIssued = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@BookID", bookID);

                connection.Open();
                return command.ExecuteScalar() != null;
            }
        }

        private void InsertRequest(int userID, int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO BookRequests (UserID, BookID, RequestedAt) VALUES (@UserID, @BookID, @RequestedAt)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@BookID", bookID);
                command.Parameters.AddWithValue("@RequestedAt", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["SearchTerm"] = txtSearch.Text.Trim();
            LoadBooks(); // Refresh the book 
        }

        private void DeleteRequest(int userID, int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM BookRequests WHERE UserID = @UserID AND BookID = @BookID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@BookID", bookID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}