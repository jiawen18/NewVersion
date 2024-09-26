using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.admin
{
    public partial class transactions : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTransactionData();
        }

        private void BindTransactionData()
        {
            string query = "SELECT TransactionID, OrderID, InvoiceID, InvoiceDate FROM [Transaction]";

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

                    TransactionRepeater.DataSource = dt;
                    TransactionRepeater.DataBind();
                }
            }
        }
    }
}