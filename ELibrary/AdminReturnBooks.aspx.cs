using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class AdminReturnBooks : System.Web.UI.Page
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
                LoadIssuedBooks();
            }
        }

        private void LoadIssuedBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            BI.IssueID, 
                            U.Username, 
                            U.Fullname, 
                            B.Title, 
                            BI.IssueDate, 
                            BI.ReturnDate, 
                            BI.IsReturned, 
                            BI.Fine 
                        FROM BookIssues BI
                        INNER JOIN Users U ON BI.UserID = U.UserID
                        INNER JOIN Books B ON BI.BookID = B.BookID
                        WHERE BI.IsReturned = 0"; // Only show unreturned books

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    rptIssuedBooks.DataSource = reader;
                    rptIssuedBooks.DataBind();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void rptIssuedBooks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ReturnBook")
            {
                int issueID = int.Parse(e.CommandArgument.ToString());

                string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Check if the book is already returned
                        string selectQuery = @"
                    SELECT 
                        UserID, 
                        ReturnDate, 
                        Fine, 
                        IsReturned 
                    FROM BookIssues 
                    WHERE IssueID = @IssueID";
                        SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                        selectCommand.Parameters.AddWithValue("@IssueID", issueID);
                        SqlDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            DateTime dueDate = (DateTime)reader["ReturnDate"];
                            bool isReturned = (bool)reader["IsReturned"];
                            decimal fine = (decimal)reader["Fine"];
                            int userID = (int)reader["UserID"];

                            if (isReturned)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Book already returned.');", true);
                                return;
                            }

                            // Calculate fine if overdue
                            if (DateTime.Today > dueDate)
                            {
                                TimeSpan overdue = DateTime.Today - dueDate;
                                fine = (decimal)overdue.TotalDays * 1.00m; // $1 per day overdue
                            }

                            // Update BookIssues and deduct fine from user's balance
                            string updateQuery = @"
                        UPDATE BookIssues 
                        SET IsReturned = 1, Fine = @Fine 
                        WHERE IssueID = @IssueID;

                        UPDATE Users 
                        SET Balance = Balance - @Fine 
                        WHERE UserID = @UserID;";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                            updateCommand.Parameters.AddWithValue("@Fine", fine);
                            updateCommand.Parameters.AddWithValue("@IssueID", issueID);
                            updateCommand.Parameters.AddWithValue("@UserID", userID);

                            reader.Close();
                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", $"alert('Book returned successfully! Fine deducted: ${fine:F2}');", true);
                                LoadIssuedBooks(); // Refresh the list
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to return book.');", true);
                            }
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
}