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
                string paymentStatus = "Paid"; 
                string deliveryStatus = "Completed";
                string orderDetails = "Z-Flip 128GB | Blue";

                
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
                   

                    
                    List<int> productIds = GetProductIDsByCartId(cartId);
                    if (productIds.Count == 0)
                    {
                        ;
                    }
                    else
                    {
                        
                        InsertOrder(orderID, paymentStatus, deliveryStatus, orderDetails, productIds, transactionID, invoiceId, date);
                    }
                }
                else
                {
                    
                }

                if (Session["Amount"] != null)
                {
                    decimal amount = (decimal)Session["Amount"]; 
                    lblAmount.Text = "RM " + amount.ToString("F2");
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
            Response.Redirect("Home2.aspx");
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
                        
                        string orderQuery = "INSERT INTO [Order] (OrderID, PaymentStatus, DeliveryStatus, OrderDetails) VALUES (@OrderID, @PaymentStatus, @DeliveryStatus, @OrderDetails)";

                        using (SqlCommand cmd = new SqlCommand(orderQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderID);
                            cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                            cmd.Parameters.AddWithValue("@DeliveryStatus", deliveryStatus);
                            cmd.Parameters.AddWithValue("@OrderDetails", orderDetails);
                            cmd.ExecuteNonQuery();
                        }

                        
                        if (productIds != null && productIds.Count > 0)
                        {
                           
                            string transactionQuery = "INSERT INTO [Transaction] (ProductID, OrderID, TransactionID, InvoiceID, InvoiceDate) VALUES (@ProductID, @OrderID, @TransactionID, @InvoiceID, @InvoiceDate)";

                            foreach (int productId in productIds)
                            {
                                if (productId > 0) 
                                {
                                    using (SqlCommand cmd = new SqlCommand(transactionQuery, con, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", productId);
                                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                                        cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                        cmd.Parameters.AddWithValue("@InvoiceDate", date);
                                        cmd.ExecuteNonQuery();
                                       
                                    }
                                }
                                else
                                {
                                    
                                   
                                }
                            }
                        }
                        else
                        {
                           
                        }

                        transaction.Commit();
                        
                    }
                    catch (SqlException sqlEx)
                    {
                        transaction.Rollback();
                        
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        
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

