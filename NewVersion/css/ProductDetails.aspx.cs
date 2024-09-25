using Aspose.Imaging.FileFormats.Cdr.Types;
using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelFirstRating.Visible = true;
                PanelMoreRatings.Visible = false;
                btnViewMore.Text = "View More Ratings";
            }

        }


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

 