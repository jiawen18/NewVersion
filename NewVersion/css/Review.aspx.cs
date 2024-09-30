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
            string orderId = Request.QueryString["OrderID"];

            // Check if OrderID is present
            if (!string.IsNullOrEmpty(orderId))
            {
                // Load the product details using the string OrderID
                LoadProductDetails(orderId);
            }
            else
            {
                Response.Write("Invalid or missing OrderID.");
            }

        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            Product product = new Product();


            int rating;
            int productId;

            HiddenField hfProductID = (HiddenField)rptReview.FindControl("HiddenFieldProductID");

            if (!int.TryParse(HiddenFieldRating.Value, out rating) ||
                !int.TryParse(hfProductID.Value, out productId))
            {
                Response.Write("Invalid rating or product ID. Please try again.");
                return;
            }

            string description = txtReviewDescription.Text;

            DateTime reviewDate = DateTime.Now;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string insertQuery = "INSERT INTO Review (ReviewDate, ReviewRating, ReviewDescription, ProductID) " +
                    "VALUES (@reviewDate, @rating, @description, @productId); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmdInsert = new SqlCommand(insertQuery, con))
                {
                    // Add parameters for the values you want to insert
                    cmdInsert.Parameters.AddWithValue("@reviewDate", reviewDate);
                    cmdInsert.Parameters.AddWithValue("@rating", rating);
                    cmdInsert.Parameters.AddWithValue("@productId", productId);
                    cmdInsert.Parameters.AddWithValue("@description", description);

                    // Execute the command and retrieve the new ReviewID
                    var newReviewID = cmdInsert.ExecuteScalar();

                    // Convert to the appropriate type (e.g., int)
                    int reviewID = Convert.ToInt32(newReviewID);

                    string fileName2 = HiddenFieldImagePath.Value;
                    if (fileName2 != null)
                    {


                        // Set the directory to store the image on your server
                        string savePath = Server.MapPath("ReviewImg/");

                        // Create the directory if it doesn't exist
                        if (!Directory.Exists(savePath))
                        {
                            Directory.CreateDirectory(savePath);
                        }

                        // Combine the directory and file name to get the complete path
                        string fullFilePath = Path.Combine(savePath, fileName2);

                        // Update the productPic in the existing product row
                        string updateQuery = "UPDATE Review SET ReviewImage = @ReviewImage WHERE ReviewID = @ReviewID";
                        string fileName = Path.GetFileName(fileName2);
                        using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                        {
                            cmdUpdate.Parameters.AddWithValue("@ReviewImage", "ReviewImg/" + fileName);
                            cmdUpdate.Parameters.AddWithValue("@ReviewID", reviewID);

                            cmdUpdate.ExecuteNonQuery();
                        }


                    }
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

        private void LoadProductDetails(string orderId)
        {
            // Define the connection string (assuming it's in the web.config file)
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            List<Product> productList = new List<Product>();

            // SQL query to join Order and Product tables and retrieve product details for the given OrderID
            string query = @"
                        SELECT p.ProductID, p.ProductImageURL, p.ProductName, p.Price 
                        FROM OrderDetails od
                        INNER JOIN Product p ON od.ProductID = p.ProductID
                        WHERE od.OrderID = @OrderID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)  // Check if the reader has rows
                    {
                        while (reader.Read())
                        {
                            productList.Add(new Product
                            {
                                // Get the product details
                                ProductImageURL = reader["ProductImageURL"].ToString(),
                                ProductName = reader["ProductName"].ToString(),
                                Price = (decimal)reader["Price"],
                                ProductID = (int)reader["ProductID"]


                            });
                        }
                    }
                    else
                    {
                        Response.Write("No products found for the given OrderID.");
                    }
                }
            }
            rptReview.DataSource = productList;
            rptReview.DataBind();
        }

        protected void rptReview_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 查找 HiddenField 控件
                HiddenField hfProductID = (HiddenField)e.Item.FindControl("hfProductID");

                // 获取 HiddenField 的值
                int productId = Convert.ToInt32(hfProductID.Value);


            }
        }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }
    }

}


