using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ELibrary
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load books added to the default page
                LoadDefaultBooks();
            }
        }

        private void LoadDefaultBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT BookID, Title, Author, Description, ImagePath, Price FROM Books WHERE IsOnDefaultPage = 1";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Dynamically create book cards
                foreach (DataRow row in dt.Rows)
                {
                    string bookID = row["BookID"].ToString();
                    string title = row["Title"].ToString();
                    string author = row["Author"].ToString();
                    string description = row["Description"].ToString();
                    string imagePath = row["ImagePath"].ToString();
                    string price = row["Price"].ToString();

                    // Create card HTML
                    string cardHtml = $@"
                    <div class='col-md-4'>
                        <div class='card'>
                            <img src='{imagePath}' class='card-img-top' alt='{title}'>
                            <div class='card-body'>
                                <h5 class='card-title'>{title}</h5>
                                <p class='card-text'><strong>Author:</strong> {author}</p>
                                <p class='card-text'><strong>Price:</strong> ${price}</p>
                                <p class='card-description'>{description}</p>
                            </div>
                        </div>
                    </div>";

                    // Add card to the default book list
                    defaultBookList.Controls.Add(new Literal { Text = cardHtml });
                }
            }
        }
    }
}