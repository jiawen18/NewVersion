using Aspose.Imaging.FileFormats.Cdr.Types;
using NewVersion.Models;
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
        string cs = Global.CS;

        private List<CartItem> Cart
        {
            get
            {
                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new List<CartItem>();
                }
                return (List<CartItem>)Session["Cart"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productId = Convert.ToInt32(Request.QueryString["productID"]);
               
                 LoadProductDetails(productId);


                List<CartItem> cart = Cart; // Using Cart property to get cart items

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

        private void LoadProductDetails(int productId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ProductName,Price,ProductImageURL FROM [Product] WHERE ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            lblProductName.Text = reader["ProductName"].ToString();
                            ProductImg1.ImageUrl = reader["ProductImageURL"].ToString();
                            ProductImg2.ImageUrl = reader["ProductImageURL"].ToString();
                            ProductImg3.ImageUrl = reader["ProductImageURL"].ToString();
                            lblPrice.Text = "RM " + Convert.ToDecimal(reader["Price"]).ToString("F2"); 
                                                                                                       
                        }
                        else
                        {
                            
                            ShowSuccessMessage("Product not found.");
                        }
                    }
                }
            }
        }

                /*
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

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(Request.QueryString["productID"]);
            string productName = lblProductName.Text; 
            decimal price = Convert.ToDecimal(lblPrice.Text.Replace("RM ", ""));
            string productImage = ProductImg1.ImageUrl;

            string selectedStorage = Session["SelectedStorage"]?.ToString();
            string selectedColor = Session["SelectedColor"]?.ToString(); ;

            if (string.IsNullOrEmpty(selectedStorage) || string.IsNullOrEmpty(selectedColor))
            {
                ShowSuccessMessage("Please select both storage and color before adding to cart.");
                return;
            }


            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>(); // Get the cart items

            var existingItem = cart.FirstOrDefault(item => item.ProductID == productId &&
                                                   item.StorageOption == selectedStorage &&
                                                   item.ColorOption == selectedColor);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                
            }
            else
            {
                CartItem newItem = new CartItem
                {
                    ProductID = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = 1, 
                    StorageOption = selectedStorage,
                    ColorOption = selectedColor,
                    ProductImage = productImage
                };

                cart.Add(newItem);
                
            }

            Session["Cart"] = cart;
            Session.Remove("SelectedStorage");
            Session.Remove("SelectedColor");

            // Show success message
            lblSuccessMessage.Text = "Product added to cart successfully!";
            divSuccessMessage.Style["display"] = "block";
        }


        private void ShowSuccessMessage(string message)
        {
            // Set the message text
            lblSuccessMessage.Text = message;

            // Use JavaScript to show the div and hide it after a few seconds
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage",
             $"document.getElementById('{lblSuccessMessage.ClientID}').innerText = '{HttpUtility.JavaScriptStringEncode(message)}';" +
             $"var div = document.getElementById('{divSuccessMessage.ClientID}');" + 
             "div.style.display = 'block';" +
             "setTimeout(function() { div.style.display = 'none'; }, 10000);", true);
        }



        protected void btnStorage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string selectedStorage = btn.Text;
            Session["SelectedStorage"] = selectedStorage;

            colorContainer.Style["display"] = "block"; // Assuming 'colorContainer' is the ID of the color selection div

        }

        protected void ColorButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string selectedColor = btn.Attributes["value"];
            Session["SelectedColor"] = selectedColor;

        }

    }

}

public partial class CartItem
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string StorageOption { get; set; }
    public string ColorOption { get; set; }
    public string ProductImage { get; set; }
}

