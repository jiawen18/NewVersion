
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load user details
                LoadUserDetails();

                productRepeater.DataSource = SqlDataSource1;
                productRepeater.DataBind();
                BindProductData();
            }
        }

        private void LoadUserDetails()
        {
            string username = HttpContext.Current.User.Identity.Name;

            // Connect to the database and retrieve the username and email from MemberUser
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Username, Email FROM MemberUser WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lbl_user_name.Text = reader["Username"].ToString();
                            lbl_user_email.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            // Handle case where user is not found, e.g., clear labels or display a message
                            lbl_user_name.Text = "User not found";
                            lbl_user_email.Text = "";
                        }
                    }
                }
            }
        }


        // Method to get the profile picture URL for the current user
        protected string GetProfilePictureUrl()
        {
            string username = HttpContext.Current.User.Identity.Name;
            string profilePictureUrl = "/admin/assets/img/default.jpg"; // Default image path

            // Connect to the database and retrieve the profile picture URL
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Check in both AdminUser and SuperAdminUser tables
                string query = @"
                    SELECT ProfilePicture 
                    FROM MemberUser
                    WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string profilePictureFilename = reader["ProfilePicture"].ToString();
                            if (!string.IsNullOrEmpty(profilePictureFilename))
                            {
                                profilePictureUrl = "/admin/assets/img/" + profilePictureFilename; // Append the profile picture filename }

                            }
                            else
                            {
                                profilePictureUrl = "/admin/assets/img/default.jpg"; // Default image path
                            }
                        }
                    }
                }
                return profilePictureUrl; // Return the URL
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