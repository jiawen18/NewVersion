using Aspose.Imaging.FileFormats.Cdr.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class ProductDetails : System.Web.UI.Page
    {
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
            int productId = 1;
            string productImage = ProductImg.ImageUrl;
            string productName = lblProductName.Text;
            string storage = Button1.Text;
            string color = ColorButton1.Text;
            decimal price = Decimal.Parse(lblPrice.Text);
            List<CartItem> cartItems = ShoppingCart.GetCartItemsFromSession();
            int quantity = cartItems.FirstOrDefault(item => item.ProductId == productId)?.Quantity ?? 1;


            ShoppingCart.AddProduct(productId,productImage,productName, storage, color, price,quantity);
        }
    }
    }

 