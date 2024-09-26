using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.admin
{
    
    public partial class orderList : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindOrderData();
        }

        private void BindOrderData()
        {
            
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT OrderID, OrderDetails, PaymentStatus, DeliveryStatus FROM [Order]";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                OrderRepeater.DataSource = dt;
                OrderRepeater.DataBind();
            }
        }
    }
}