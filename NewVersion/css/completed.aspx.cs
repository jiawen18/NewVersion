using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class completed : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompletedOrderData();
            }

        }

        // Fetch completed orders from the database and bind them to a control
        private void BindCompletedOrderData()
        {
            List<OrderViewModel> orders = LoadCompletedOrders();

            
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
                // 处理没有订单的情况，例如显示一条消息
                rptOrders.DataSource = null;
                rptOrders.DataBind();
            }
        }

        private List<OrderViewModel> LoadCompletedOrders()
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = @"
                SELECT 
                    o.OrderID,
                    od.ProductName,
                    od.ProductID,
                    od.Quantity,
                    od.Storage,
                    od.Color,
                    od.Price,
                    od.ProductImage,
                    t.TransactionStatus
                FROM 
                    [dbo].[Transaction] t
                INNER JOIN 
                    [dbo].[Order] o ON t.OrderID = o.OrderID
                INNER JOIN 
                    [dbo].[OrderDetails] od ON o.OrderID = od.OrderID
                WHERE o.DeliveryStatus = 'Completed' AND
                    t.TransactionStatus = 'Success'
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
                var order = (OrderViewModel)e.Item.DataItem; // 使用 OrderViewModel 类型
                Repeater rptProducts = (Repeater)e.Item.FindControl("rptProducts");

                // 确保 order.Products 返回产品列表
                rptProducts.DataSource = order.Products; // 绑定产品列表
                rptProducts.DataBind();
            }
        }


        protected void btnTrack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Delivery.aspx");
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            Response.Redirect("Review.aspx?ProductID=" + productId);
        }


    }

    public partial class CompletedOrder
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Color { get; set; }
        public string Capacity { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CompletedOrderViewModel
    {
        public string OrderID { get; set; }
        public List<Order> Products { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DeliveryFee { get; set; }
        public string InvoiceDate { get; set; }
    }
}