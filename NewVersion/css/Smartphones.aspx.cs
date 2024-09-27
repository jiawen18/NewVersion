using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Data.SqlClient;
using Razorpay.Api;
using NewVersion.Models;


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
            string query = "SELECT ProductID, ProductName, ProductImageURL, Price FROM Product WHERE IsVisible = 1";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Create a product object and populate it
                    var product = new Product
                    {
                        ProductID = reader["ProductID"].ToString(),
                        ProductName = reader["ProductName"].ToString(),
                        ProductImageURL = reader["ProductImageURL"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),

                    };
                    products.Add(product);
                }
            }

            // Bind the products to the Repeater control
            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            Response.Redirect("ProductDetails.aspx?ProductID=" + productId);

        }
    }

}

// Product class to hold the product information
public class Product
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public string ProductImageURL { get; set; }
    public decimal Price { get; set; }

}


