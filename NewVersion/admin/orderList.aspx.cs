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
            string query = "SELECT OrderID, OrderDetails, PaymentStatus, DeliveryStatus,OrderDate FROM [Order]";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();

                    // Fill the DataTable with the result of the query
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    OrderRepeater.DataSource = dt;
                    OrderRepeater.DataBind();
                }
            }
        }

}
}