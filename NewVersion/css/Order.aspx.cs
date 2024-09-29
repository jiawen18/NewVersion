using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;
using Razorpay.Api;

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
            List<OrderViewModel> orders = LoadOrdersItem();

           
            if (orders != null && orders.Count > 0)
            {
                var groupedOrders = orders
                    .GroupBy(order => order.OrderID)
                    .Select(group => new OrderViewModel
                    {
                        OrderID = group.Key,
                        Products = group.SelectMany(o => o.Products).ToList(),
                        InvoiceNumber = group.First().InvoiceNumber,
                        TotalPrice = group.Sum(o => o.TotalPrice),
                        DeliveryFee = group.First().DeliveryFee,
                        InvoiceDate = group.First().InvoiceDate
                    }).ToList();

                rptOrders.DataSource = groupedOrders;
                rptOrders.DataBind();
            }
            else
            {
                
                rptOrders.DataSource = null;
                rptOrders.DataBind();
            }
        }

        private List<OrderViewModel> LoadOrdersItem()
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = @"SELECT o.OrderID, o.TotalPrice, o.DeliveryFee, od.ProductName, t.InvoiceID, t.InvoiceDate, 
                                 od.ProductImage, od.Color, od.storage, od.Quantity, od.Price 
                         FROM [Order] o
                         JOIN [OrderDetails] od ON o.OrderID = od.OrderID
                         JOIN [Transaction] t ON o.OrderID = t.OrderID
                         WHERE o.DeliveryStatus = 'Shipping' AND t.TransactionStatus = 'Success'
                         ORDER BY o.OrderID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        Dictionary<string, OrderViewModel> orderDict = new Dictionary<string, OrderViewModel>();

                        while (reader.Read())
                        {
                            string orderID = reader["OrderID"].ToString();

                            
                            if (!orderDict.ContainsKey(orderID))
                            {
                                orderDict[orderID] = new OrderViewModel
                                {
                                    OrderID = orderID,
                                    Products = new List<Order>(), 
                                    InvoiceNumber = reader["InvoiceID"].ToString(),
                                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                                    DeliveryFee = Convert.ToDecimal(reader["DeliveryFee"]),
                                    InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]).ToString("yyyy-MM-dd") // 或者根据需要格式化
                                };
                            }

                           
                            Order order = new Order
                            {
                                ProductName = reader["ProductName"].ToString(),
                                ProductImage = reader["ProductImage"].ToString(),
                                Color = reader["Color"].ToString(),
                                Capacity = reader["storage"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            };

                            
                            orderDict[orderID].Products.Add(order);
                        }

                       
                        orderViewModels = orderDict.Values.ToList();
                    }
                }
            }

            return orderViewModels;
        }


        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var order = (OrderViewModel)e.Item.DataItem; 
                Repeater rptProducts = (Repeater)e.Item.FindControl("rptProducts");

                
                rptProducts.DataSource = order.Products; 
                rptProducts.DataBind();
            }
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

                    var refundRequest = new NewVersion.Models.Refund
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
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Color { get; set; }
        public string Capacity { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderViewModel
    {
        public string OrderID { get; set; }
        public List<Order> Products { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DeliveryFee { get; set; }
        public string InvoiceDate { get; set; }
    }

}


