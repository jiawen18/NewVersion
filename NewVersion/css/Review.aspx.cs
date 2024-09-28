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
using NewVersion;

namespace NewVersion.css
{
    public partial class Review : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productId = Convert.ToInt32(Request.QueryString["ProductID"] ?? "1");
                LoadProductDetails(productId);
            }
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {

            int rating = Convert.ToInt32(HiddenFieldRating.Value); ;
            int productId = Convert.ToInt32(HiddenFieldProductID.Value);

            
            if (!int.TryParse(HiddenFieldRating.Value, out rating) ||
                !int.TryParse(HiddenFieldProductID.Value, out productId))
            {
                
                Response.Write("Invalid rating or product ID. Please try again.");
                return; 
            }
            string description = txtReviewDescription.Text;
            string imagePath = "";

            DateTime reviewDate = DateTime.Now;

            if (FileUploadMedia.HasFile)
            {
                string fileName = Path.GetFileName(FileUploadMedia.PostedFile.FileName);
                string uploadPath = Server.MapPath("~/Uploads/");
                string fullPath = Path.Combine(uploadPath, fileName);

                // Save the file to the server
                FileUploadMedia.SaveAs(fullPath);

                Response.Write($"File saved to: {fullPath}<br/>");

                // Set the image path to save in the database
                imagePath = "~/Uploads/" + fileName; 
                HiddenFieldImagePath.Value = imagePath; 
            }

            else
            {
                Response.Write("No file uploaded.<br/>");
            }

            // Save the review to the database
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Review (ReviewDate, ReviewRating, ReviewImage, ReviewDescription, ProductID) " +
                               "VALUES (@ReviewDate, @ReviewRating, @ReviewImage, @ReviewDescription, @ProductID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReviewDate", reviewDate);
                    command.Parameters.AddWithValue("@ReviewRating", rating);
                    command.Parameters.AddWithValue("@ReviewImage", imagePath);
                    command.Parameters.AddWithValue("@ReviewDescription", description);
                    command.Parameters.AddWithValue("@ProductID", productId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            txtReviewDescription.Text = string.Empty;

            Response.Redirect("Home2.aspx");

        }

        private string SaveMediaFiles()
        {
            string photoPath = MapPath("~/Photos/");
            string filename = Guid.NewGuid().ToString("N") + ".jpg"; // Unique filename for the photo
            string imagePath = "";

            // Check if files are uploaded
            if (FileUploadMedia.HasFile)
            {
                // Process each uploaded file
                foreach (HttpPostedFile file in FileUploadMedia.PostedFiles)
                {
                    // Save image processing logic here
                    SimpleImage img = new SimpleImage(file.InputStream);
                    img.Square();
                    img.Resize(150);
                    img.SaveAs(photoPath + filename);
                    imagePath = "~/Photos/" + filename; // Set the image path to save in the database
                }
            }

            return imagePath;
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



