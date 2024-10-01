using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using NewVersion;

namespace NewVersion.css
{
    public partial class Home2 : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                productRepeater.DataSource = SqlDataSource1;
                productRepeater.DataBind();
                BindProductData();
            }
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            Response.Redirect("ProductDetails.aspx?ProductID=" + productId);

        }

        private void BindProductData()
        {
           
            using (SqlConnection connection = new SqlConnection(cs))
            {
                string query = @"
                    SELECT 
                        p.ProductID, 
                        p.ProductImageURL, 
                        p.ProductName, 
                        p.Price, 
                        AVG(r.ReviewRating) AS AverageRating 
                    FROM 
                        Product p 
                    LEFT JOIN 
                        Review r ON p.ProductID = r.ProductID 
                    GROUP BY 
                        p.ProductID, 
                        p.ProductImageURL, 
                        p.ProductName, 
                        p.Price";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    productRepeater.DataSource = reader;
                    productRepeater.DataBind();
                }
            }
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
    }
}