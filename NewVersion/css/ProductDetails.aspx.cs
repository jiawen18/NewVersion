using Aspose.Imaging.FileFormats.Cdr.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection con= new SqlConnection(cs);

            string query = "INSERT Product(ProductID,ProductName,ProductImageURl,Price,Quantity,ProductStorage,ProductColor) VALUES(@ProductID,@ProductName,@ProductImageURL,@Price,@Quantity,@ProductStorage,@ProductColor";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ProductName", lblProductName.Text);
            cmd.Parameters.AddWithValue("@ProductImageURL", ProductImg.ImageUrl);
            cmd.Parameters.AddWithValue("@Price", lblPrice.Text);
            cmd.Parameters.AddWithValue("@Quantity",lblQuantity);
            cmd.Parameters.AddWithValue("@ProductStorage", Button1.Text);
            cmd.Parameters.AddWithValue("@ProductColor", ColorButton1.Text);

        }
    }

}

 