using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace NewVersion.admin
{
    public partial class reviews : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReviewRepeater(); 
            }

        }

        protected void ReviewRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteReview")
            {
                string reviewId = e.CommandArgument.ToString(); // Get the review ID from the command argument

                string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
                string sql = "DELETE FROM Review WHERE ReviewID = @ReviewID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Rebind the repeater to reflect changes
                BindReviewRepeater();
            }
        }

        private void BindReviewRepeater()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            string sql = "SELECT * FROM [Review]"; 

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt); 
                        ReviewRepeater.DataSource = dt; 
                        ReviewRepeater.DataBind(); 
                    }
                }
            }
        }
    }
}