using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Data.SqlClient;
using Razorpay.Api;


namespace NewVersion.css
{
    public partial class Smartphones : System.Web.UI.Page
    {
        private string connectionString 
            = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }

        }

        private void LoadProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT P.ProductID, P.ProductName, P.ProductImageURL, P.Price, R.ReviewID, R.ReviewDate, R.ReviewRating, R.ReviewDescription " +
                           "FROM Product P LEFT JOIN Review R ON P.ProductID = R.ProductID WHERE P.IsVisible = 1";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Check if the product is already added
                    var existingProduct = products.Find(p => p.ProductID == reader["ProductID"].ToString());

                    if (existingProduct == null)
                    {
                        // Create a new product if it's not already added
                        var product = new Product
                        {
                            ProductID = reader["ProductID"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            ProductImageURL = reader["ProductImageURL"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Reviews = new List<Review>() // Initialize reviews list
                        };

                        // If there's a review, add it to the list of reviews
                        if (!DBNull.Value.Equals(reader["ReviewID"]))
                        {
                            var review = new Review
                            {
                                ReviewID = reader["ReviewID"].ToString(),
                                ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                ReviewRating = Convert.ToInt32(reader["ReviewRating"]),
                                ReviewDescription = reader["ReviewDescription"].ToString()
                            };
                            product.Reviews.Add(review);
                        }

                        products.Add(product);
                    }
                    else
                    {
                        // If product exists, add the review to the existing product
                        if (!DBNull.Value.Equals(reader["ReviewID"]))
                        {
                            var review = new Review
                            {
                                ReviewID = reader["ReviewID"].ToString(),
                                ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                ReviewRating = Convert.ToInt32(reader["ReviewRating"]),
                                ReviewDescription = reader["ReviewDescription"].ToString()
                            };
                            existingProduct.Reviews.Add(review);
                        }
                    }
                }
            }

            // Bind the products to the Repeater control
            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var product = (Product)e.Item.DataItem;

                // Find the nested repeater
                Repeater rptReviews = (Repeater)e.Item.FindControl("rptReviews");
                if (rptReviews != null && product.Reviews.Count > 0)
                {
                    rptReviews.DataSource = product.Reviews;
                    rptReviews.DataBind();
                }
            }
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            // Redirect to the product detail page or add to cart
            Response.Redirect($"ProductDetails.aspx?ProductID={productId}");

        }
    }

    // Product class to hold the product information
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }

         public List<Review> Reviews { get; set; }

    }

    public partial class Review
    {
        public string ReviewID { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewDescription { get; set; }
    }

}