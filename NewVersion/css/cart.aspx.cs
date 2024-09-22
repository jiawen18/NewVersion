using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        private List<CartItem> Cart
        {
            get
            {
                // get cart from Session，if empty => initialize
                return Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            }
            set
            {
                // store cart to Session
                Session["Cart"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindCart(); //initialize data bind
            }
        }

       private void AddToCart(int productId)
        {
            //Step 1: sql statement
            string sql = "SELECT * FROM Product WHERE ProductID = @Id";

            //Step 2: sqlconnection - establish connection
            //between app and db
            SqlConnection con = new SqlConnection(cs);

            //step 3: sql command
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", productId);

            con.Open();

            //step 4: handle the return records
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                CartItem item = new CartItem
                {
                    ProductId = productId,
                    ProductName = dr["Name"].ToString(),
                    ProductImageURL = dr["ProductImageURL"].ToString(),
                    Price = Convert.ToDecimal(dr["Price"]),
                    Quantity = 1
                };

                List<CartItem> cart = (List<CartItem>) Session["Cart"] ?? new List<CartItem>();
                cart.Add(item);
                Session["Cart"] = cart;
            }
            
            //rptProduct.DataSource = dr;
            //rptProduct.DataBind();

            //Step5: close connection & dr
            dr.Close();
            con.Close();
        }

        private void BindCart()
        {
            List<CartItem> cartItems = ShoppingCart.GetCartItemsFromSession();

            //bind the cart data to repeater
            rptProduct.DataSource = cartItems; //get cart item from session
            rptProduct.DataBind();
            UpdateCartTotals();  
        }

        private List<CartItem> GetCartItems()
        {
            return (List<CartItem>)Session["Cart"] ?? new List<CartItem>();
        }

        private void UpdateCartTotals()
        {
            var cartItems = ShoppingCart.GetCartItemsFromSession();
            
            decimal subtotal = cartItems.Sum(item => item.Price * item.Quantity);
            
            lblSubtotal.Text = $"RM {subtotal:F2}";
            lblTotal.Text = $"RM {subtotal:F2}";
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Smartphones.aspx");
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }

        protected void rptProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateQuantity")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
               
                int quantity;
                
                if (int.TryParse(txtQuantity.Text, out quantity) && quantity > 0)
                {
                    
                    UpdateQuantity(productId, quantity);
                    BindCart();
                }
            }
        }

        private void UpdateQuantity(int productId, int quantity)
        {
            List<CartItem> cart = ShoppingCart.GetCartItemsFromSession();
            
            CartItem item = cart.FirstOrDefault(i => i.ProductId == productId);
           
            if (item != null)
            {
                item.Quantity = quantity; // update quantity
                
            }
            HttpContext.Current.Session["Cart"] = cart; // update session
            UpdateCartTotals();
        }


        /* private void RemoveFromCart(int productId)
         {
             List<CartItem> cart = GetCartItems();
             var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);
             if (itemToRemove != null)
             {
                 cart.Remove(itemToRemove);
                 Session["Cart"] = cart; // update cart to Session
             }
         }
        */
    }

    public static class ShoppingCart
    {
        public static void AddProduct(int productId,string productImage, string productName, string storage, string color, decimal price,int quantity)
        {
            List<CartItem> cartItems = GetCartItemsFromSession();
            
            CartItem existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
            
            if (existingItem != null)
            {
                // 如果商品存在，则增加数量
                existingItem.Quantity += quantity;
            }
            else
            {
                // 否则，添加一个新商品，指定数量
                cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductImageURL = productImage,
                    ProductName = productName,
                    ProductStorage = storage,
                    ProductColor = color,
                    Price = price,
                    Quantity = quantity
                });
            }

            HttpContext.Current.Session["Cart"] = cartItems;
        }

        public static List<CartItem> GetCartItemsFromSession()
        {
            return HttpContext.Current.Session["Cart"] as List<CartItem> ?? new List<CartItem>();
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageURL { get; set; }
        public string ProductStorage { get; set; }
        public string ProductColor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}