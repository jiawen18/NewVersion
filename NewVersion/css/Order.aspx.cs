using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.css
{
    public partial class Order : System.Web.UI.Page
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
            List<Order> orders = LoadOrdersItem();

            rptOrders.DataSource = orders;
            rptOrders.DataBind();
        }

        private List<Order> LoadOrdersItem()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = @"SELECT o.OrderID, o.TotalPrice,o.DeliveryFee, od.ProductName,t.InvoiceID, t.InvoiceDate,od.ProductImage,od.Color, od.storage, od.Quantity, od.Price 
                                    FROM [Order] o
                                    JOIN [OrderDetails] od ON o.OrderID = od.OrderID
                                    JOIN [Transaction] t ON o.OrderID = t.OrderID
                                    WHERE o.DeliveryStatus = 'Shipping' AND t.TransactionStatus ='Success'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderID = reader["OrderID"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            InvoiceNumber = reader["InvoiceID"].ToString(),
                            InvoiceDate = reader["InvoiceDate"].ToString(),
                            ProductImage = reader["ProductImage"].ToString(),
                            DeliveryFee = reader["DeliveryFee"].ToString(),
                            Color = reader["Color"].ToString(),
                            Capacity = reader["storage"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Price = reader["Price"].ToString(),
                            TotalPrice = reader["TotalPrice"].ToString(),
                        };
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        protected void btnTrack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Delivery.aspx");
        }

        // yujing edited this for refund
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Button cancelButton = sender as Button;
            if (cancelButton != null)
            {

                string orderID = cancelButton.CommandArgument;

                using (var context = new userEntities())
                {

                    var refundRequest = new Refund
                    {
                        OrderID = orderID,
                        RefundRequestDate = DateTime.Now,
                        RefundStatus = "Pending",
                    };

                    context.Refunds.Add(refundRequest);
                    context.SaveChanges();
                }

                //FeedbackLabel.Text = "Refund request submitted successfully!";
                //FeedbackLabel.CssClass = "text-success";

                Response.Redirect("cancelled.aspx");
            }
        }

    }

    public partial class Order
    {
        public string OrderID { get; set; }
        public string ProductName { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string ProductImage { get; set; }
        public string Color { get; set; }
        public string Capacity { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string DeliveryFee { get; set; }
        public string TotalPrice { get; set; }
    }
}


