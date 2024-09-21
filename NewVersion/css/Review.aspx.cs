using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace NewVersion.css
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnApplyChanges_Click(object sender, EventArgs e)
        {
            // Logic for applying changes like cropping, flipping, or rotating can go here.
            // Example: Saving the final edited photo to a folder.

            // Hide the modal after applying changes
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "hideModal();", true);
        }

    }
    }
