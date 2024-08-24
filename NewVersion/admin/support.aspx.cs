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
    public partial class support : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
            }
        }

        private void BindRepeater()
        {
            // Define your connection string (make sure to include your own database details)
            //string connStr = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;

            // SQL query to fetch data
            string query = "SELECT Name, Price, Quantity FROM Product";

            // Initialize the connection
            using (SqlConnection conn = new SqlConnection(cs))
            {
                // Create a command
                SqlCommand cmd = new SqlCommand(query, conn);

                // Open the connection
                conn.Open();

                // Execute the command and read data
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                // Bind the reader to the Repeater
                Repeater1.DataSource = ds;
                Repeater1.DataBind();

                // Close the connection
                conn.Close();
            }
        }

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Add button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }
        protected void CopyItemButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }

    }
}