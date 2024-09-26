using NewVersion.admin;
using NewVersion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class SuccessPage : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string paymentStatus = "成功"; // 根据实际情况设置
                string deliveryStatus = "待发货"; // 根据实际情况设置
                string orderDetails = "订单详细信息";

                // 从查询字符串中获取数据
                string orderID = Request.QueryString["orderId"];
                lblOrderId.Text = orderID;

                string transactionID = Request.QueryString["TransactionId"];
                lblTransactionId.Text = transactionID;

                var (invoiceId, date) = GenerateRandomInvoiceId();
                lblInvoiceId.Text = invoiceId;
                lblInvoiceDate.Text = date;

                if (Session["CartID"] != null)
                {
                    int cartId = (int)Session["CartID"];
                    Response.Write($"当前 CartID: {cartId}<br/>");

                    // 获取产品 ID
                    List<int> productIds = GetProductIDsByCartId(cartId);
                    if (productIds.Count == 0)
                    {
                        Response.Write("未找到产品 ID，请检查购物车内容。<br/>");
                    }
                    else
                    {
                        Response.Write($"找到产品 ID: {string.Join(", ", productIds)}<br/>");
                        InsertOrder(orderID, paymentStatus, deliveryStatus, orderDetails, productIds, transactionID, invoiceId, date);
                    }
                }
                else
                {
                    Response.Write("购物车 ID 不存在。<br/>");
                }

                if (Session["Amount"] != null)
                {
                    decimal amount = (decimal)Session["Amount"]; // 从会话中读取金额
                    lblAmount.Text = "RM " + amount.ToString("F2"); // 显示金额
                }
            }
        }


        public static (string InvoiceId, string Date) GenerateRandomInvoiceId()
        {
            const int length = 10; // Length of the random ID (excluding the prefix)
            const string chars = "0123456789";
            StringBuilder result = new StringBuilder();
            Random random = new Random(); // Create a random number generator

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(chars.Length); // Generate a random index
                result.Append(chars[randomIndex]); // Randomly select a character and append it
            }

            // Get the current date in the specified format
            string date = DateTime.Now.ToString("dd MMMM yyyy"); // Format: DD Month YYYY

            return (result.ToString(), date); // Return the generated random invoice ID and date
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        private void InsertOrder(string orderID, string paymentStatus, string deliveryStatus, string orderDetails, List<int> productIds, string transactionID, string invoiceId, string date)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        Response.Write($"订单 ID: {orderID}<br/>");
                        Response.Write($"交易 ID: {transactionID}<br/>");
                        Response.Write($"发票号: {invoiceId}<br/>");
                        Response.Write($"发票日期: {date}<br/>");


                        // 插入订单
                        string orderQuery = "INSERT INTO [Order] (OrderID, PaymentStatus, DeliveryStatus, OrderDetails) VALUES (@OrderID, @PaymentStatus, @DeliveryStatus, @OrderDetails)";

                        using (SqlCommand cmd = new SqlCommand(orderQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderID);
                            cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                            cmd.Parameters.AddWithValue("@DeliveryStatus", deliveryStatus);
                            cmd.Parameters.AddWithValue("@OrderDetails", orderDetails);
                            cmd.ExecuteNonQuery();
                        }

                        // 确保 productIds 列表不为空
                        if (productIds != null && productIds.Count > 0)
                        {
                            // 保存交易细节
                            string transactionQuery = "INSERT INTO [Transaction] (ProductID, OrderID, TransactionID, InvoiceID, InvoiceDate) VALUES (@ProductID, @OrderID, @TransactionID, @InvoiceID, @InvoiceDate)";

                            foreach (int productId in productIds)
                            {
                                if (productId > 0) // 确保产品 ID 有效
                                {
                                    using (SqlCommand cmd = new SqlCommand(transactionQuery, con, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", productId);
                                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                                        cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                        cmd.Parameters.AddWithValue("@InvoiceDate", date);
                                        cmd.ExecuteNonQuery();
                                        Response.Write($"成功插入产品 ID: {productId}<br/>");
                                    }
                                }
                                else
                                {
                                    // 记录无效的产品 ID
                                    Response.Write($"无效的产品 ID: {productId}<br/>");
                                }
                            }
                        }
                        else
                        {
                            Response.Write("产品 ID 列表为空，无法保存交易细节。<br/>");
                        }

                        transaction.Commit();
                        Response.Write("所有交易细节已成功插入。<br/>");
                    }
                    catch (SqlException sqlEx)
                    {
                        transaction.Rollback();
                        Response.Write($"插入过程中发生 SQL 错误: {sqlEx.Message}<br/>");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.Write($"插入过程中发生错误: {ex.Message}<br/>");
                    }
                }

            }
         }


        private List<int> GetProductIDsByCartId(int cartId)
        {
            List<int> productIds = new List<int>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT ProductID FROM CartItems WHERE CartID = @CartID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CartID", cartId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int productId = Convert.ToInt32(reader["ProductID"]);
                        productIds.Add(productId);
                    }
                }
            }

            return productIds;
        }
    }


}

