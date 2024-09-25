using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace NewVersion.css
{
    public partial class Review : System.Web.UI.Page
    {
        //step 2: retrieve CS from Global.asax
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the ProductID from a query string or any other source
                int productId = Convert.ToInt32(Request.QueryString["ProductID"] ?? "1");
                LoadProductDetails(productId);
            }
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {

            //get user input value
            int rating = int.Parse(HiddenFieldRating.Value);
            string description = txtReviewDescription.Text;
            int productId = int.Parse(HiddenFieldProductID.Value);
            string imagePath = "";

            DateTime reviewDate = DateTime.Now;

            // Save the review to the database
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Review (ReviewDate, ReviewRating, ReviewImage, ReviewDescription, ProductID) " +
                               "VALUES (@ReviewDate, @ReviewRating, @ReviewImage, @ReviewDescription, @ProductID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReviewDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ReviewRating", rating);
                    command.Parameters.AddWithValue("@ReviewImage", imagePath);
                    command.Parameters.AddWithValue("@ReviewDescription", description);
                    command.Parameters.AddWithValue("@ProductID", productId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            txtReviewDescription.Text = string.Empty;

        }

        private void LoadProductDetails(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            string query = "SELECT ProductImageURL, ProductName, Price FROM Product WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Get the product details
                        string productImageURL = reader["ProductImageURL"].ToString();
                        string productName = reader["ProductName"].ToString();
                        decimal price = (decimal)reader["Price"];

                        // Bind the retrieved values to the controls
                        Image1.ImageUrl = productImageURL;
                        lblProdName.Text = productName;
                        lblProdDetails.Text = $"Price: {price:C}";
                        HiddenFieldProductID.Value = productId.ToString();
                    }
                    else
                    {
                        // Handle case where product is not found
                        lblProdName.Text = "Product not found";
                        lblProdDetails.Text = "";
                    }
                }
            }
        }
    }
}



