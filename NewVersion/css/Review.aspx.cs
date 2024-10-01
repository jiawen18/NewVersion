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
using System.Data;

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
    // 获取评分和产品ID
    int rating;
    int productId;

    if (!int.TryParse(HiddenFieldRating.Value, out rating) ||
        !int.TryParse(HiddenFieldProductID.Value, out productId))
    {
        Response.Write("Invalid rating or product ID. Please try again.");
        return;
    }

    string description = txtReviewDescription.Text;
    DateTime reviewDate = DateTime.Now;

    using (SqlConnection con = new SqlConnection(cs))
    {
        con.Open();

        // 插入评论
        string insertQuery = "INSERT INTO Review (ReviewDate, ReviewRating, ReviewDescription, ProductID) VALUES (@reviewDate, @rating, @description, @productId); SELECT SCOPE_IDENTITY();";
        using (SqlCommand cmdInsert = new SqlCommand(insertQuery, con))
        {
            cmdInsert.Parameters.AddWithValue("@reviewDate", reviewDate);
            cmdInsert.Parameters.AddWithValue("@rating", rating);
            cmdInsert.Parameters.AddWithValue("@productId", productId);
            cmdInsert.Parameters.AddWithValue("@description", description);

            // 执行命令并获取新插入的 ReviewID
            var newReviewID = cmdInsert.ExecuteScalar();
            int reviewID = Convert.ToInt32(newReviewID);

            // 处理图像文件上传
            string imagePath = SaveMediaFiles();
            if (!string.IsNullOrEmpty(imagePath))
            {
                // 更新评论记录中的图像路径
                string updateQuery = "UPDATE Review SET ReviewImage = @ReviewImage WHERE ReviewID = @ReviewID";
                using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                {
                    cmdUpdate.Parameters.AddWithValue("@ReviewImage", imagePath);
                    cmdUpdate.Parameters.AddWithValue("@ReviewID", reviewID);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
        }
    }

    // 清空描述文本框
    txtReviewDescription.Text = string.Empty;

    // 重定向到主页面
    Response.Redirect("Home2.aspx");
}

private string SaveMediaFiles()
{
    string photoPath = Server.MapPath("../css/ReviewImg/");
    string imagePath = "";

    // 检查是否有文件被上传
    if (FileUploadMedia.HasFile)
    {
        foreach (HttpPostedFile file in FileUploadMedia.PostedFiles)
        {
            // 创建唯一的文件名
            string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName); 
            string fullPath = Path.Combine(photoPath, fileName);

            // 确保目录存在
            if (!Directory.Exists(photoPath))
            {
                Directory.CreateDirectory(photoPath);
            }

            // 保存文件
            file.SaveAs(fullPath);
            imagePath = "../css/ReviewImg/" + fileName; // 设置数据库中存储的图像路径
        }
    }

    return imagePath;
}

        private void LoadProductDetails(string orderId)
        {
            // Define the connection string (assuming it's in the web.config file)
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

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
                            // Get the product details
                            string productImageURL = reader["ProductImageURL"].ToString();
                            string productName = reader["ProductName"].ToString();
                            decimal price = (decimal)reader["Price"];
                            int productId = (int)reader["ProductID"];


                            // Bind the retrieved values to the controls
                            Image1.ImageUrl = productImageURL;
                            lblProdName.Text = productName;
                            lblProdDetails.Text = $"Price: {price:C}";
                            HiddenFieldProductID.Value = productId.ToString();
                        }
                    }
                    else
                    {
                        Response.Write("No products found for the given OrderID.");
                    }
                }
            }

        }

    }
}






