using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace NewVersion.css
{
    public partial class Home : System.Web.UI.Page
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
                                ReviewRating = Convert.ToInt32(reader["ReviewRating"]),
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
                                ReviewRating = Convert.ToInt32(reader["ReviewRating"]),
                            };
                            existingProduct.Reviews.Add(review);
                        }
                    }
                }
            }

            // Bind the products to the Repeater control
            ProductsRepeater.DataSource = products;
            ProductsRepeater.DataBind();
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductDetails.aspx");
        }
    }

    public partial class ProductProduct
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }
        public List<Review> Reviews { get; set; }
    }

    public partial class ReviewProduct
    {
        public string ReviewID { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewDescription { get; set; }
    }
}