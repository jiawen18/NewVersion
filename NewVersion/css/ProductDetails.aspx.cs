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

        }


protected void btnViewMore_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the panel
            PanelMoreRatings.Visible = !PanelMoreRatings.Visible;

            // Optionally change the button text based on the panel visibility
            if (PanelMoreRatings.Visible)
            {
                btnViewMore.Text = "View Less Ratings";
            }
            else
            {
                btnViewMore.Text = "View More Ratings";
            }

        }
    }
    }

 