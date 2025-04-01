using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class UsersBorrowedBooks : System.Web.UI.Page
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
                LoadBorrowedBooks();
            }
        }

        private void LoadBorrowedBooks()
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
                            BI.IssueID, 
                            B.BookID, 
                            B.Title, 
                            B.Author, 
                            B.Description, 
                            B.ImagePath, 
                            B.Price, 
                            BI.ReturnDate 
                        FROM BookIssues BI 
                        INNER JOIN Books B ON BI.BookID = B.BookID 
                        WHERE BI.UserID = @UserID AND BI.IsReturned = 0";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " AND (B.Title LIKE @SearchTerm OR B.Author LIKE @SearchTerm)";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    rptBorrowedBooks.DataSource = reader;
                    rptBorrowedBooks.DataBind();

                    lblNoResults.Visible = rptBorrowedBooks.Items.Count == 0;
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["SearchTerm"] = txtSearch.Text.Trim();
            LoadBorrowedBooks();
        }

        protected void rptBorrowedBooks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ReturnBook")
            {
                int issueID = int.Parse(e.CommandArgument.ToString());
                ReturnBook(issueID);
                LoadBorrowedBooks(); // Refresh the list
            }
        }

        private void ReturnBook(int issueID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE BookIssues 
                    SET IsReturned = 1 
                    WHERE IssueID = @IssueID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IssueID", issueID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "alert('Book returned successfully!');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to return book.');", true);
                }
            }
        }
    }
}