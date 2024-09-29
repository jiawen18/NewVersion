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
            // Retrieve the ProductID from the query string
            int productId;

            // Check if ProductID is present in the query string
            if (int.TryParse(Request.QueryString["ProductID"], out productId))
            {
                // If valid, load the product details
                LoadProductDetails(productId);
            }
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            Product product = new Product();

            int rating;
            int productId;

            if (!int.TryParse(HiddenFieldRating.Value, out rating) ||
                !int.TryParse(HiddenFieldProductID.Value, out productId))
            {
                Response.Write("Invalid rating or product ID. Please try again.");
                return;
            }

            string description = txtReviewDescription.Text;
            byte[] imageBytes = null; // Start with null byte array
            DateTime reviewDate = DateTime.Now;

            // Check if any file is uploaded
            if (FileUploadMedia.HasFile)
            {
                try
                {
                    imageBytes = SaveMediaFiles(); // Call the method to save the media files
                    if (imageBytes == null || imageBytes.Length == 0)
                    {
                        Response.Write("No valid files uploaded.<br/>");
                        return; // No valid image bytes to save
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error saving the file: " + ex.Message);
                    return; // Handle any errors in saving files
                }
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
                    command.Parameters.AddWithValue("@ReviewImage", (object)imageBytes ?? DBNull.Value); // Set DBNull if imageBytes is null
                    command.Parameters.AddWithValue("@ReviewDescription", description);
                    command.Parameters.AddWithValue("@ProductID", productId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException sqlEx)
                    {
                        Response.Write("Database error: " + sqlEx.Message);
                        return; // Handle database errors
                    }
                }
            }

            txtReviewDescription.Text = string.Empty; // Clear the text box
            Response.Redirect("Home2.aspx"); // Redirect after saving
        }

        private byte[] SaveMediaFiles()
        {
            byte[] imageBytes = null; // Initialize the byte array to hold the image data
            string photoPath = MapPath("~/Photos/"); // Path to save the uploaded photos

            // Check if files are uploaded
            if (FileUploadMedia.HasFiles)
            {
                foreach (HttpPostedFile file in FileUploadMedia.PostedFiles)
                {
                    // Check if the file is a valid image type
                    if (file.ContentType.StartsWith("image/"))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms); // Copy the input stream to the memory stream
                            imageBytes = ms.ToArray(); // Convert the memory stream to a byte array

                            // Save the file to the server if needed
                            string filename = Guid.NewGuid().ToString("N") + ".jpg"; // Unique filename for the photo
                            string fullPath = Path.Combine(photoPath, filename); // Full path to save the image
                            file.SaveAs(fullPath); // Save the original file to disk
                        }
                    }
                    else
                    {
                        Response.Write("Invalid file type. Please upload an image.<br/>");
                        return null; // Invalid file type, return null
                    }
                }
            }
            else
            {
                Response.Write("No files were uploaded.<br/>");
            }

            return imageBytes; // Return the byte array containing the image data
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



