using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ELibrary
{
    public partial class UpdateBooks : System.Web.UI.Page
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
                // Load book details if BookID is provided in the query string
                if (Request.QueryString["BookID"] != null)
                {
                    int bookID = int.Parse(Request.QueryString["BookID"]);
                    LoadBookDetails(bookID);
                }
            }
        }

        private void LoadBookDetails(int bookID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Author, Description, ImagePath, IsFree, Price FROM Books WHERE BookID = @BookID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", bookID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtBookID.Text = bookID.ToString();
                        txtTitle.Text = reader["Title"].ToString();
                        txtAuthor.Text = reader["Author"].ToString();
                        txtDescription.Text = reader["Description"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        chkIsFree.Checked = (bool)reader["IsFree"];
                        lblCurrentImage.Text = "Current Image: " + reader["ImagePath"].ToString();
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
            int bookID = int.Parse(txtBookID.Text);
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string description = txtDescription.Text.Trim();
            decimal price = decimal.Parse(txtPrice.Text.Trim());
            bool isFree = chkIsFree.Checked;

            // Handle image upload
            string imagePath = lblCurrentImage.Text.Replace("Current Image: ", "");
            if (fileImage.HasFile)
            {
                try
                {
                    // Ensure the Images folder exists
                    string imagesFolderPath = Server.MapPath("~/Images/");
                    if (!Directory.Exists(imagesFolderPath))
                    {
                        Directory.CreateDirectory(imagesFolderPath);
                    }

                    // Save the uploaded file to the Images folder
                    string fileName = Path.GetFileName(fileImage.PostedFile.FileName);
                    string filePath = Path.Combine(imagesFolderPath, fileName);
                    fileImage.SaveAs(filePath);

                    // Set the image path for the database
                    imagePath = "Images/" + fileName;
                }
                catch (Exception ex)
                {
                    // Handle file upload errors
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('Error uploading image: {ex.Message}');", true);
                    return;
                }
            }

            // Update book in the database
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Title = @Title, Author = @Author, Description = @Description, ImagePath = @ImagePath, IsFree = @IsFree, Price = @Price WHERE BookID = @BookID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@ImagePath", imagePath);
                command.Parameters.AddWithValue("@IsFree", isFree);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@BookID", bookID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show success popup and redirect
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "showSuccessPopup('Book updated successfully!'); setTimeout(redirectToAdminDashboard, 1000);", true);
                    }
                    else
                    {
                        // Show error message
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to update book.');", true);
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