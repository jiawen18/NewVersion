using Aspose.Imaging.FileFormats.Cdr.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        string cs = 
            ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelMoreRatings.Visible = true;
                PanelMoreRatings.Visible = false;
                btnViewMore.Text = "View More Ratings";
            }

        }


        /*
        protected void Page_Load(object sender, EventArgs e)
        {
            string productID = Request.QueryString["ProductID"];
            System.Diagnostics.Debug.WriteLine($"ProductID: {productID}");
            if (!IsPostBack)
            {
                LoadProductDetails(productID);
                LoadProductReviews(productID);
            }

            else
            {
                lblProductName.Text = "Product not found.";
                lblPrice.Text = string.Empty;
                lblQuantity.Text = string.Empty;
                ProductImg.ImageUrl = string.Empty;
            }

        }*/

        /*
        private void LoadProductDetails(string productID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Product WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", productID);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lblProductName.Text = reader["ProductName"].ToString();
                            lblPrice.Text = $"${Convert.ToDecimal(reader["Price"]):F2}";
                            lblQuantity.Text = "Quantity: " + reader["Quantity"].ToString();
                            ProductImg.ImageUrl = reader["ProductImageURL"].ToString();
                        }
                    }
                    else
                    {
                        lblProductName.Text = "Product not found.";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (not shown)
                    lblProductName.Text = "Error loading product details.";
                }
            }
        }


        private void LoadProductReviews(string productID)
        {
            
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ReviewDate, ReviewRating, ReviewImage, ReviewDescription, ProductName FROM Review WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptCustomerRatings.DataSource = dt;
                    rptCustomerRatings.DataBind();
                }
            }
        }

        // Helper method to return the HTML for star ratings
        private string GetStarRatingHtml(int rating)
        {
            string starsHtml = "";
            for (int i = 0; i < rating; i++)
            {
                starsHtml += "<i class='fa fa-star'></i>";
            }
            for (int i = rating; i < 5; i++)
            {
                starsHtml += "<i class='fa fa-star-o'></i>";
            }
            return starsHtml;
        }

        */
        protected void btnViewMore_Click(object sender, EventArgs e)
        {
            PanelMoreRatings.Visible = !PanelMoreRatings.Visible;

            if (PanelMoreRatings.Visible)
            {
                btnViewMore.Text = "View Less Ratings";
            }
            else
            {
                btnViewMore.Text = "View More Ratings";
            }


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query1 = "INSERT INTO ShoppingCart(CreateAt) VALUES(@CreateAt)";

                SqlCommand cmd1 = new SqlCommand(query1, con);

                cmd1.Parameters.AddWithValue("@CreateAt", DateTime.Now);

                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                string query = "INSERT INTO CartItems (CartID,ProductID,ProductName,ProductImageURl,Price,Quantity,ProductStorage,ProductColor,TotalPrice) VALUES(@CartID,@ProductID,@ProductName,@ProductImageURL,@Price,@Quantity,@ProductStorage,@ProductColor,@TotalPrice)";

                SqlCommand cmd = new SqlCommand(query, con);

                int cartId = GetCurrentCartId();
                int productId = Convert.ToInt32(hiddenProductId.Value);

                int quantity = Convert.ToInt32(lblQuantity.Text);
                decimal price = Convert.ToDecimal(lblPrice.Text);
                decimal totalPrice = quantity * price;


                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@CartID", cartId);
                cmd.Parameters.AddWithValue("@ProductName", lblProductName.Text);
                cmd.Parameters.AddWithValue("@ProductImageURL", ProductImg.ImageUrl);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@ProductStorage", Button1.Text);
                cmd.Parameters.AddWithValue("@ProductColor", ColorButton1.Text);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private int GetCurrentCartId()
        {
            int cartId = -1;

            SqlConnection con = new SqlConnection(cs);

            string query = "SELECT CartID FROM ShoppingCart";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open(); 
                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read()) // if read
                {
                    cartId = reader.GetInt32(0); // get first cartId
                }
                reader.Close(); 
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
            finally
            {
                con.Close(); 
            }
        
            return cartId; 
        }
    }

}

 