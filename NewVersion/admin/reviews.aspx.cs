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
                string reviewId = e.CommandArgument.ToString(); 
                DeleteReview(reviewId);

                BindReviewRepeater(); 
            }
        }

        private void BindReviewRepeater()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            string sql = "SELECT ReviewID, ProductID, ReviewDate, ReviewRating, ReviewImage, ReviewDescription FROM Review";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        da.Fill(dt); 
                        ReviewRepeater.DataSource = dt; 
                        ReviewRepeater.DataBind();
                    }
                }
            }
        }

        private void DeleteReview(string reviewId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            string sql = "DELETE FROM Review WHERE ReviewID = @ReviewID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                    cmd.ExecuteNonQuery();
                }
            }
            BindReviewRepeater();
        }
    }
}