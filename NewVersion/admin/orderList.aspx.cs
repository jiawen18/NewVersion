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
            if (!IsPostBack)
            {
                BindOrderData();
            }
        }

        private void BindOrderData()
        {
            string query = "SELECT OrderID,UserDetails,OrderDate, TransactionStatus, DeliveryStatus,TotalPrice,DeliveryFee FROM [Order]";

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

        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditOrder")
            {
                string orderId = e.CommandArgument.ToString();
                LoadOrderDetails(orderId);
                ShowEditModal();
            }

            if (e.CommandName == "DeleteOrder")
            {
                string orderId = e.CommandArgument.ToString();
                DeleteOrder(orderId);
                BindOrderData();
            }
        }

        protected void RemoveOrderButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string orderId = btn.CommandArgument;

            DeleteOrder(orderId);

            BindOrderData();

            lblMessage.Text = "order deleted";
            lblMessage.CssClass = "alert alert-success";
        }

        private void ShowEditModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModal", "$('#addRowModal').modal('show');", true);
        }

        private void LoadOrderDetails(string orderId)
        {
            string sql = "SELECT TransactionStatus,UserDetails,DeliveryStatus, OrderDate,TotalPrice,DeliveryFee FROM [Order] WHERE OrderID = @OrderID";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtOrderID.Value = orderId;
                            txtUserDetails.Text = reader["UserDetails"].ToString();
                            txtDeliveryFee.Text = reader["DeliveryFee"].ToString();
                            txtTotalPrice.Text = reader["TotalPrice"].ToString();
                            txtTransactionStatus.Text = reader["TransactionStatus"].ToString();
                            ddlDeliveryStatus.SelectedValue = reader["DeliveryStatus"].ToString();
                            txtOrderDate.Text = Convert.ToDateTime(reader["OrderDate"]).ToString("M/d/yyyy hh:mm:ss tt");
                        }
                    }
                }
            }
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            string orderID = txtOrderID.Value;
            string deliveryStatus = ddlDeliveryStatus.SelectedValue;

            // Update the product in the database
            string sql = "UPDATE [Order] SET DeliveryStatus = @DeliveryStatus WHERE OrderID = @OrderID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Supply input/data into parameters
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    cmd.Parameters.AddWithValue("@DeliveryStatus", deliveryStatus);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Order updated successfully.";
                            lblMessage.CssClass = "text-success";
                        }
                        else
                        {
                            lblMessage.Text = "Order update failed. Order ID not found.";
                            lblMessage.CssClass = "text-danger";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.CssClass = "text-danger";
                    }
                }
            }

            BindOrderData();
        }

        private void DeleteOrder(string orderID)
        {
           
            using (SqlConnection con = new SqlConnection(cs))
            {
                string deleteDetailsQuery = "DELETE FROM [OrderDetails] WHERE OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(deleteDetailsQuery, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            string query = "DELETE FROM [Order] WHERE OrderID = @OrderID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
