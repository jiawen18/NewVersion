using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class Order : System.Web.UI.Page
    {
        String cs1 = Global.CS;
        String cs2 = Global.CS;
        String cs3 = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve order data from the database or Session
            List<Order> orders = GetOrdersFromSessionOrDatabase();

            // If there are orders, bind them to the Repeater control
            if (orders != null && orders.Count > 0)
            {
                rptOrders.DataSource = orders;
                rptOrders.DataBind();
            }
        }

        // Simulate retrieving order data from the database or Session
        private List<Order> GetOrdersFromSessionOrDatabase()
        {
            // Assume you have stored the order information in Session
            List<Order> orders = new List<Order>();

            string orderId = Request.QueryString["orderId"];

            SqlConnection con1 = new SqlConnection(cs1);

            SqlConnection con2 = new SqlConnection(cs2);

            SqlConnection con3 = new SqlConnection(cs3);

            try
            {
                string query1 = "SELECT InvoiceNumber,InvoiceDate FROM [Transaction] WHERE OrderID = @OrderID";

                SqlCommand cmd1 = new SqlCommand(query1, con1);

                cmd1.Parameters.AddWithValue("@OrderID", orderId);

                con1.Open();

                SqlDataReader dr1 = cmd1.ExecuteReader();

                while (dr1.Read())
                {
                    orders.Add(new Order
                    {
                        InvoiceNumber = dr1["InvoiceNumber"].ToString(),
                        InvoiceDate = Convert.ToDateTime(dr1["InvoiceDate"])
                    });
                }

                dr1.Close();

                con2.Open();

                string query2 = "SELECT ProductName,ProductImageURL,ProductColor,ProductStorage,Quantity,ProductPrice FROM ShoppingCart WHERE OrderID = @OrderID";

                SqlCommand cmd2 = new SqlCommand(query2, con2);

                cmd2.Parameters.AddWithValue("@OrderID", orderId);

                con2.Open();

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    orders.Add(new Order
                    {
                        ProductName  = dr2["ProductNumber"].ToString(),
                        ProductImageUrl  = dr2["ProductImageURL"].ToString(),
                        ProductColor = dr2["ProductColor"].ToString(),
                        ProductCapacity = dr2["ProductStorage"].ToString(),
                        ProductPrice = Convert.ToDecimal(dr2["ProductPrice"]),
                        ProductQuantity = Convert.ToInt32(dr2["Quantity"])
                    });
                }

                dr2.Close();

                con3.Open();

                string query3 = "SELECT TotalPrice,SubTotal,DeliveryFee FROM CheckOut WHERE OrderID = @OrderID";

                SqlCommand cmd3 = new SqlCommand(query3, con3);

                cmd3.Parameters.AddWithValue("@OrderID", orderId);

                con3.Open();

                SqlDataReader dr3 = cmd3.ExecuteReader();

                while (dr3.Read())
                {
                    orders.Add(new Order
                    {
                        TotalPaid  = dr3["TotalPrice"].ToString(),
                        TotalPrice = dr3["SubTotal"].ToString(),
                        DeliveryFee = dr3["DeliveryFee"].ToString()
                    });
                }

                dr3.Close();
            }
            catch(Exception ex)
            {
                throw new Exception("Error fetching order data", ex);
            }
            finally
            {
                if(con1.State == System.Data.ConnectionState.Open)
                {
                    con1.Close();
                }

                if (con2.State == System.Data.ConnectionState.Open)
                {
                    con2.Close();
                }
                if (con3.State == System.Data.ConnectionState.Open)
                {
                    con3.Close();
                }
            }
            return orders;
        }

        // Store order data in Session after successful order placement
        private void StoreOrderInSession(Order order)
        {
            List<Order> orders = Session["Orders"] as List<Order> ?? new List<Order>();
            orders.Add(order);
            Session["Orders"] = orders;
        }


        protected void btnTrack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Delivery.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("cancelled.aspx");
        }
    }

    public partial class Order
    {
        public int OrderID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public string ProductCapacity { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string TotalPrice { get; set; }
        public string DeliveryFee { get; set; }
        public string TotalPaid { get; set; }
    }
}


