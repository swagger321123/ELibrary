using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ELibrary
{
    public partial class AddBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Restrict access to admin only
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string description = txtDescription.Text.Trim();
            decimal price = decimal.Parse(txtPrice.Text.Trim());
            bool isFree = chkIsFree.Checked;

            // Handle image upload
            string imagePath = "";
            if (fileImage.HasFile)
            {
                try
                {
                    // checkin Image folder existence if not creating Images folder
                    string imagesFolderPath = Server.MapPath("~/Images/");
                    if (!Directory.Exists(imagesFolderPath))
                    {
                        Directory.CreateDirectory(imagesFolderPath);
                    }

                    // Saving the uploaded image to the Images folder
                    string fileName = Path.GetFileName(fileImage.PostedFile.FileName);
                    string filePath = Path.Combine(imagesFolderPath, fileName);
                    fileImage.SaveAs(filePath);

                    // Setting the image path for the database
                    imagePath = "Images/" + fileName;
                }
                catch (Exception ex)
                {
                    // Handle file upload errors
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('Error uploading image: {ex.Message}');", true);
                    return;
                }
            }

            // Inserting book into database
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, Description, ImagePath, IsFree, Price, CreatedAt) " +
                               "VALUES (@Title, @Author, @Description, @ImagePath, @IsFree, @Price, @CreatedAt)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@ImagePath", imagePath);
                command.Parameters.AddWithValue("@IsFree", isFree);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Showing success popup and redirect
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", "showSuccessPopup('Book added successfully!'); setTimeout(redirectToAdminDashboard, 1000);", true);
                    }
                    else
                    {
                        // Show error message
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", "alert('Failed to add book.');", true);
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