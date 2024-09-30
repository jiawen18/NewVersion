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
using NewVersion;


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
                 LoadProductRatings(productId);
                 ShowOnlyFirstRating();

                List<CartItem> cart = Cart; // Using Cart property to get cart items

                btnViewMore.Text = "View More Ratings";
            }

        }

        private void BindCustomerRatings(int productId)
        {
            rptCustomerRatings.DataSource = SqlDataSource1;
            rptCustomerRatings.DataBind();
        }


        protected string GetRatingStars(object ratingObj)
        {
            double rating = ratingObj != DBNull.Value ? Convert.ToDouble(ratingObj) : 0;
            int fullStars = (int)Math.Floor(rating);
            bool hasHalfStar = (rating - fullStars) >= 0.5;
            int emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

            // Define the star styles
            string fullStarStyle = "color: gold; font-size: 20px;"; // Customize color and size for full stars
            string halfStarStyle = "color: gold; font-size: 20px;"; // Customize color and size for half star
            string emptyStarStyle = "color: lightgray; font-size: 20px;"; // Customize color and size for empty stars

            // Build the HTML string for star ratings with inline styles
            string starsHtml = new string('★', fullStars); // Full stars
            if (hasHalfStar) starsHtml += "☆"; // Half star
            starsHtml += new string('☆', emptyStars); // Empty stars

            // Wrap each star with a span for styling
            // Replace full stars
            starsHtml = starsHtml.Replace("★", $"<span style='{fullStarStyle}'>★</span>");
            // Replace half star (only the first occurrence)
            if (hasHalfStar)
            {
                int halfStarIndex = starsHtml.IndexOf("☆");
                if (halfStarIndex != -1)
                {
                    starsHtml = starsHtml.Remove(halfStarIndex, 1)
                                         .Insert(halfStarIndex, $"<span style='{halfStarStyle}'>☆</span>");
                }
            }
            // Replace empty stars
            starsHtml = starsHtml.Replace("☆", $"<span style='{emptyStarStyle}'>☆</span>");

            return starsHtml;
        }

        private void LoadProductDetails(int productId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT p.ProductName,p.Price,p.ProductImageURL,s.Storage,c.Color FROM Product p JOIN Storage s ON s.StorageID = p.Productstorage JOIN Color c ON c.ColorID = p.ProductColor WHERE ProductID = @ProductID";

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
                            ColorButton1.Text =reader["Color"].ToString();
                            btnStorage1.Text = reader["Storage"].ToString();
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

        private void LoadProductRatings(int productId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"
            SELECT 
                R.ReviewRating,
                R.ReviewDescription,
                R.ReviewImage,
                R.ReviewDate,
                P.ProductName 
            FROM 
                Review R
            JOIN 
                Product P ON R.ProductID = P.ProductID 
            WHERE 
                R.ProductID = @ProductID"; // Adjust this query as necessary

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            rptCustomerRatings.DataSource = reader; // Bind the reader directly
                            rptCustomerRatings.DataBind();
                        }
                        else
                        {
                            ShowSuccessMessage("No ratings found for this product.");
                        }
                    }
                }
            }
        }

        private void ShowOnlyFirstRating()
        {
            if (rptCustomerRatings.Items.Count > 0)
            {
                rptCustomerRatings.Items[0].Visible = true; // Show the first rating

                if (rptCustomerRatings.Items.Count > 1)
                {
                    // Hide additional ratings initially
                    for (int i = 1; i < rptCustomerRatings.Items.Count; i++)
                    {
                        rptCustomerRatings.Items[i].Visible = false;
                    }
                }
            }
        }

        protected void btnViewMore_Click(object sender, EventArgs e)
        {
            // Show the next two ratings
            for (int i = 1; i < rptCustomerRatings.Items.Count && i <= 2; i++)
            {
                rptCustomerRatings.Items[i].Visible = true;
            }

            // Optionally, hide the button if all ratings are shown
            if (rptCustomerRatings.Items.Count <= 3)
            {
                btnViewMore.Visible = false;
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(Request.QueryString["productID"]);
            string productName = lblProductName.Text; 
            decimal price = Convert.ToDecimal(lblPrice.Text.Replace("RM ", ""));
            string productImage = ProductImg1.ImageUrl;

            string selectedStorage = btnStorage1.Text;
            string selectedColor = ColorButton1.Text;

            if (string.IsNullOrEmpty(selectedStorage) || string.IsNullOrEmpty(selectedColor))
            {
                ShowSuccessMessage("Please select both storage and color before adding to cart.");
                return;
            }

            int selectedQuantity = int.Parse(ddlQuantity.SelectedValue);

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>(); // Get the cart items

            var existingItem = cart.FirstOrDefault(item => item.ProductID == productId &&
                                                   item.StorageOption == selectedStorage &&
                                                   item.ColorOption == selectedColor);

            if (existingItem != null)
            {
                existingItem.Quantity += selectedQuantity;

            }
            else
            {
                CartItem newItem = new CartItem
                {
                    ProductID = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = selectedQuantity,
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

