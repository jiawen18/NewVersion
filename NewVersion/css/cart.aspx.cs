using NewVersion.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class cart : System.Web.UI.Page
    {
        string cs = Global.CS;

        /* private void LogInUser(int memberId)
         {
             // 将 MemberID 存储在 Session 中
             HttpContext.Current.Session["MemberID"] = memberId;
         }*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCartItems();
                UpdateCartTotals();
            }

            /*if (Session["MemberID"] != null)
            {
                int memberId = Convert.ToInt32(Session["MemberID"]);
                LoadCartFromDatabase(memberId);
                BindCart(); // initialize data bind
            }
            else
            {
                // remind user to Login
                Response.Redirect("Login.aspx");
            }*/

        }

        private void LoadCartItems()
        {
            using(SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM CartItems  WHERE CartID = @CartID";

                SqlCommand cmd = new SqlCommand(query, con);

                int cartId = GetCurrentCartID();

                cmd.Parameters.AddWithValue("@CartID", cartId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptProduct.DataSource = dt;
                    rptProduct.DataBind();
                }
                else
                {
                    // 可能显示一个消息或做其他处理
                    rptProduct.DataSource = null;
                    rptProduct.DataBind(); // 绑定空数据以清除旧数据
                }
            }
        }

        private int GetCurrentCartID()
        {
            int cartId = -1;

            SqlConnection con = new SqlConnection(cs);

            string query = "SELECT CartID FROM ShoppingCart";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // if read
                {
                    cartId = reader.GetInt32(0); // get first cartId
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
            finally
            {
                con.Close();
            }

            return cartId;
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] arguments = btn.CommandArgument.Split(','); // 拆分字符串
            string ciId = arguments[0]; // CartItemID
            string pId = arguments[1];

            int cartItemId = Convert.ToInt32(arguments[0]);
            int productID = Convert.ToInt32(arguments[1]);

            // 获取当前购物车的数量
            int currentQuantity = GetCurrentQuantity(cartItemId,productID);

            if (currentQuantity > 1)
            {
                // 如果数量大于 1，正常减少数量
                UpdateCartQuantity(cartItemId, -1, productID);
                LoadCartItems();
            }
            else
            {
                DeleteCartItem(cartItemId, productID);
                LoadCartItems();
            }

        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] arguments = btn.CommandArgument.Split(','); // 拆分字符串
            string ciId = arguments[0]; // CartItemID
            string pId = arguments[1];

            int cartItemId = Convert.ToInt32(arguments[0]);
            int productID = Convert.ToInt32(arguments[1]);

            // 增加产品数量的逻辑
            UpdateCartQuantity(cartItemId, 1,productID); // 假设你有一个方法来处理数量更新
        }

        private int GetCurrentQuantity(int cartItemId, int productID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT Quantity FROM CartItems WHERE CartItemID = @CartItemID AND ProductID=@ProductID AND CartID=@CartID" ;
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CartID", GetCurrentCartID());
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void DeleteCartItem(int cartItemId,int productID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM CartItems WHERE CartItemID = @CartItemID AND ProductID=@ProductID AND CartID=@CartID";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CartID", GetCurrentCartID());
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // 返回受影响的行数

                if (rowsAffected == 0)
                {
                    // 如果没有行被删除，可能是因为条件不匹配
                    Response.Write("<script>alert('没有找到匹配的项，删除失败。');</script>");
                }

                con.Close();
            }
        }

        private void UpdateCartQuantity(int cartItemId, int change, int productID)
        {
            int currentQuantity = 0; // Declare these variables at the start
            decimal price = 0;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT Quantity, Price FROM CartItems WHERE CartItemID = @CartItemID AND ProductID=@ProductID AND CartID=@CartID";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CartID", GetCurrentCartID());
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        currentQuantity = reader.GetInt32(0); // Get quantity
                        price = reader.GetDecimal(1); // Get price
                    }
                } // The reader is closed here automatically
            } // The connection is closed here automatically

            // Calculate the new quantity
            int newQuantity = currentQuantity + change;

            // If the new quantity is valid
            if (newQuantity > 0)
            {
                // Calculate the new total price for this item
                decimal newTotalPrice = price * newQuantity;

                // Now update the quantity and total price
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string updateQuery = "UPDATE CartItems SET Quantity = @Quantity, TotalPrice = @TotalPrice WHERE CartItemID = @CartItemID AND ProductID=@ProductID AND CartID=@CartID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);

                    updateCmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    updateCmd.Parameters.AddWithValue("@TotalPrice", newTotalPrice);
                    updateCmd.Parameters.AddWithValue("@CartID", GetCurrentCartID());
                    updateCmd.Parameters.AddWithValue("@CartItemID", cartItemId);
                    updateCmd.Parameters.AddWithValue("@ProductID", productID);

                    con.Open();
                    updateCmd.ExecuteNonQuery();
                }
            }
            else
            {
                // If quantity goes below 1, delete the item
                DeleteCartItem(cartItemId, productID);
            }

            LoadCartItems(); // Refresh cart items
            UpdateCartTotals(); // Update the overall totals
        }


        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT Quantity, Price FROM CartItems WHERE CartID = @CartID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CartID", GetCurrentCartID());

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int quantity = reader.GetInt32(0); // Quantity
                    decimal price = reader.GetDecimal(1); // Price
                    subtotal += quantity * price; // Calculate subtotal
                }
                reader.Close();
            }

            return subtotal;
        }

        private void UpdateCartTotals()
        {
            decimal subtotal = CalculateSubtotal();
            decimal total = subtotal; // Assuming no additional fees for simplicity

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE ShoppingCart SET Subtotal = @SubTotal, CartTotal = @CartTotal WHERE CartID = @CartID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@SubTotal", subtotal);
                cmd.Parameters.AddWithValue("@CartTotal", total);
                cmd.Parameters.AddWithValue("@CartID", GetCurrentCartID());

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // Update UI Labels
            lblSubtotal.Text = $"RM {subtotal:F2}";
            lblTotal.Text = $"RM {total:F2}";
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Smartphones.aspx");
        }

        
        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            int cartId = GetCurrentCartID(); // 获取当前购物车 ID

            // 将 cartId 存储在查询字符串中
            Response.Redirect($"Checkout.aspx?cartId={cartId}");
        }

    }
}