using Aspose.Imaging.FileFormats.Cdr.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

 