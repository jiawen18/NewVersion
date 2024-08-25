using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class cart : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Step 3: sql statement
                string sql = "SELECT * FROM Product";

                //Step4: sqlconnection - establish connection
                //between app and db
                SqlConnection con = new SqlConnection(cs);

                //step 5: sql command
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                //step 6: handle the return records
                SqlDataReader dr = cmd.ExecuteReader();

                rptProduct.DataSource = dr;
                rptProduct.DataBind();

                //Step7: close connection & dr
                dr.Close();
                con.Close();
            }

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Smartphones.aspx");
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }
    }
}