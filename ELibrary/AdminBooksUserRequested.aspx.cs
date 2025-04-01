using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class AdminBooksUserRequested : System.Web.UI.Page
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
                LoadRequests();
            }
        }

        private void LoadRequests()
        {
            string searchTerm = ViewState["SearchTerm"] as string ?? "";
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            BR.RequestID, 
                            U.Username, 
                            U.Fullname, 
                            U.Email, 
                            B.Title, 
                            B.Author, 
                            BR.RequestedAt, 
                            BR.IsIssued 
                        FROM BookRequests BR
                        INNER JOIN Users U ON BR.UserID = U.UserID
                        INNER JOIN Books B ON BR.BookID = B.BookID
                        WHERE BR.IsIssued = 0"; // Only pending requests

                    // Add search condition if searchTerm is provided
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += @"
                            AND (
                                U.Username LIKE @SearchTerm OR 
                                U.Fullname LIKE @SearchTerm OR 
                                B.Title LIKE @SearchTerm
                            )";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    rptRequests.DataSource = reader;
                    rptRequests.DataBind();

                    // Show/hide no results message
                    lblNoResults.Visible = rptRequests.Items.Count == 0;
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Save search term to ViewState and reload requests
            ViewState["SearchTerm"] = txtSearch.Text.Trim();
            LoadRequests();
        }

        protected void rptRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "IssueBook")
            {
                int requestID = int.Parse(e.CommandArgument.ToString());
                IssueBook(requestID);
            }
        }

        private void IssueBook(int requestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Get request details
                    string getRequestQuery = @"
                        SELECT 
                            BR.UserID, 
                            BR.BookID, 
                            B.Price, 
                            B.IsFree, 
                            U.Balance 
                        FROM BookRequests BR
                        INNER JOIN Books B ON BR.BookID = B.BookID
                        INNER JOIN Users U ON BR.UserID = U.UserID
                        WHERE BR.RequestID = @RequestID";
                    SqlCommand getRequestCommand = new SqlCommand(getRequestQuery, connection);
                    getRequestCommand.Parameters.AddWithValue("@RequestID", requestID);
                    SqlDataReader reader = getRequestCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        int userID = (int)reader["UserID"];
                        int bookID = (int)reader["BookID"];
                        decimal bookPrice = (decimal)reader["Price"];
                        bool isFree = (bool)reader["IsFree"];
                        decimal userBalance = (decimal)reader["Balance"];

                        reader.Close();

                        // Check if the book is free or user has sufficient balance
                        if (!isFree && userBalance < bookPrice)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('User does not have sufficient balance to issue this book.');", true);
                            return;
                        }

                        // Deduct balance if the book is not free
                        if (!isFree)
                        {
                            string deductBalanceQuery = @"
                                UPDATE Users 
                                SET Balance = Balance - @Price 
                                WHERE UserID = @UserID";
                            SqlCommand deductCommand = new SqlCommand(deductBalanceQuery, connection);
                            deductCommand.Parameters.AddWithValue("@Price", bookPrice);
                            deductCommand.Parameters.AddWithValue("@UserID", userID);
                            deductCommand.ExecuteNonQuery();
                        }

                        // Mark request as issued and add to BookIssues
                        string issueQuery = @"
                            UPDATE BookRequests 
                            SET IsIssued = 1 
                            WHERE RequestID = @RequestID;

                            INSERT INTO BookIssues (UserID, BookID, IssueDate, ReturnDate, Fine, IsReturned)
                            VALUES (@UserID, @BookID, @IssueDate, DATEADD(DAY, 7, @IssueDate), 0.00, 0)";
                        SqlCommand issueCommand = new SqlCommand(issueQuery, connection);
                        issueCommand.Parameters.AddWithValue("@RequestID", requestID);
                        issueCommand.Parameters.AddWithValue("@UserID", userID);
                        issueCommand.Parameters.AddWithValue("@BookID", bookID);
                        issueCommand.Parameters.AddWithValue("@IssueDate", DateTime.Today);

                        int rowsAffected = issueCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "alert('Book issued successfully!');", true);
                            LoadRequests(); // Reload with current search term
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to issue book.');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Request not found.');", true);
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