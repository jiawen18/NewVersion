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
                if (Session["ProductId"] != null)
                {
                    int productId = Convert.ToInt32(Session["ProductId"]);
                    int quantity = Convert.ToInt32(Session["Quantity"]);


                    //Step 3: sql statement
                    string sql = "SELECT * FROM Product WHERE Id = @Id";

                    //Step4: sqlconnection - establish connection
                    //between app and db
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();

                    //step 5: sql command
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Id", productId);

                    decimal price = 0;
                    int currentQuantity = 0;

                    SqlDataReader dr = cmd.ExecuteReader();



                    rptProduct.DataSource = dr;
                    rptProduct.DataBind();

                    con.Close();

                }
            }

        }

        decimal totalPrice = 0;

        protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取产品价格
                decimal price = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Price"));
                int quantity = Convert.ToInt32(Session["Quantity"]);

                // 计算总价
                decimal totalPrice = price * quantity;

                // 更新 Repeater 中的 Label 控件
                Label lblTotalPrice = (Label)e.Item.FindControl("lblTotalPrice");
                if (lblTotalPrice != null)
                {
                    lblTotalPrice.Text = "Total: " + totalPrice.ToString("C");
                }
            }
            else if (e.Item.ItemType == ListItemType.Header || e.Item.ItemType == ListItemType.Footer)
            {
                // 在 Repeater 的 Footer 部分显示总价
                Label lblFooterTotalPrice = (Label)e.Item.FindControl("lblFooterTotalPrice");
                if (lblFooterTotalPrice != null)
                {
                    lblFooterTotalPrice.Text = "Total Price: " + totalPrice.ToString("C");
                }
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Shop.aspx");
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }
    }
}