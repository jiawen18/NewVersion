using Razorpay.Api;
using System;
using System.Collections.Generic;
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

        /* private void LogInUser(int memberId)
         {
             // 将 MemberID 存储在 Session 中
             HttpContext.Current.Session["MemberID"] = memberId;
         }*/

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
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

        private void StoreSessionForCheckOut()
        {

        }
        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            List<CartItem> cart = new List<CartItem>();

            foreach (RepeaterItem item in rptProduct.Items)
            {
                var productName = ((Label)item.FindControl("ProductName")).Text;
                var productPrice = ((Label)item.FindControl("lblProductPrice")).Text;
                var productColor = ((Label)item.FindControl("ProductColor")).Text;
                var productStorage = ((Label)item.FindControl("ProductStorage")).Text;
                var productQuantity = Convert.ToInt32((TextBox)item.FindControl("txtQuantity"));

                // slipt "RM"
                var Price = Convert.ToDecimal(productPrice.Replace("RM ", "").Trim());

                cart.Add(new CartItem
                {
                    ProductName = productName,
                    Price = Price,
                    ProductColor = productColor,
                    ProductStorage = productStorage,
                    Quantity = productQuantity
                });

                Session["CartProducts"] = cart;

                if (cart.Count > 0)
                {
                    CheckOutProducts(cart);
                    Response.Redirect("checkout.aspx");
                }
            }

        }

         private void CheckOutProducts(List<CartItem> cart)
         {
            using (SqlConnection con = new SqlConnection(cs))
            {
                 con.Open();
                 foreach(var CartProduct in cart) {

                    string productDetails = CartProduct.ProductColor + "</br>" + CartProduct.ProductStorage;

                    string query = "INSERT INTO CheckOut (ProductName,Price,Quantity,ProductDetails) VALUES (@ProductName,@Price,@Quantity,@ProductDetails)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", CartProduct.ProductName);
                        cmd.Parameters.AddWithValue("@Price", CartProduct.Price);
                        cmd.Parameters.AddWithValue("@Quantity", CartProduct.Quantity);
                        cmd.Parameters.AddWithValue("@ProductDetails", productDetails);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            Session.Remove("CartProducts");
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
                UpdateCarItemInDatabase(item);

            }
            HttpContext.Current.Session["Cart"] = cart; // update session
            UpdateCartTotals();
        }

        

        private void UpdateCarItemInDatabase(CartItem item)
        {
            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE ShoppingCart SET Quantity = @Quantity WHERE ProductID = @Id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
            //cmd.Parameters.AddWithValue("@MemberID", Session["MemberID"]);
            cmd.Parameters.AddWithValue("@ProductID", item.ProductId);

            con.Open();
            cmd.ExecuteNonQuery();
        }


        /*private void RemoveFromCart(int productId)
         {
             List<CartItem> cart = GetCartItems();
            
             CartItem itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);
            
             if (itemToRemove != null)
             {
                 cart.Remove(itemToRemove);
                DeleteCartItemFromDatabase(productId);
             }
            Session["Cart"] = cart; // update cart to Session
         }

        private void DeleteCartItemFromDatabase(int productId)
        {
            SqlConnection con = new SqlConnection(cs);

            string query = "DELETE FROM ShoppingCart WHERE MemberID = @MemberID AND ProductID = @Id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("MemberID", Session["MemberID"]);
            cmd.Parameters.AddWithValue("ProductID", productId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        private void LoadCartFromDatabase(int memberId)
        {
            List<CartItem> cart = new List<CartItem>();
            SqlConnection con = new SqlConnection(cs);

            string query = "SELECT ProductID,ProductImageURL,ProductStorage,ProductColor,Price,Quantity,TotalPrice FROM ShoppingCart WHERE MemberID = @MemberID";

            SqlCommand cmd = new SqlCommand(query, con);
            
            cmd.Parameters.AddWithValue("MemberID",memberId);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cart.Add(new CartItem
                {
                    ProductId = Convert.ToInt32(reader["ProductID"]),
                    ProductName = reader["ProductName"].ToString(),
                    ProductImageURL = reader["ProductImageURL"].ToString(),
                    ProductStorage = reader["ProductStorage"].ToString(),
                    ProductColor = reader["ProductColor"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Quantity = Convert.ToInt32(reader["Quantity"])
                });
            }
            Session["Cart"] = cart;
        }*/
    }

    public static class ShoppingCart
    {
        public static void AddProduct(int productId,string productImage, string productName, string storage, string color, decimal price,int quantity)
        {
            List<CartItem> cartItems = GetCartItemsFromSession();
            
            CartItem existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
            
            if (existingItem != null)
            {
                // item exist add quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // else add new item and quantity
                var newItem = new CartItem
                {
                    ProductId = productId,
                    ProductImageURL = productImage,
                    ProductName = productName,
                    ProductStorage = storage,
                    ProductColor = color,
                    Price = price,
                    Quantity = quantity,
                };

                cartItems.Add(newItem);
                InsertCartItemToDatabase(newItem);
            }

            HttpContext.Current.Session["Cart"] = cartItems;
        }

        public static List<CartItem> GetCartItemsFromSession()
        {
            return HttpContext.Current.Session["Cart"] as List<CartItem> ?? new List<CartItem>();
        }

        private static void InsertCartItemToDatabase(CartItem item)
        {
           /* int memberId = Convert.ToInt32(HttpContext.Current.Session["MemberID"]);

            if (memberId <= 0)
            {
                throw new Exception("无效的会员 ID。请确保会员 ID 在会话中有效。");
            }*/

            string cs = Global.CS;

            SqlConnection con = new SqlConnection(cs);

            string query = "INSERT INTO ShoppingCart(ProductID, ProductName, ProductImageURL, ProductStorage, ProductColor, Price, Quantity, TotalPrice) VALUES (@ProductID, @ProductName, @ProductImageURL, @ProductStorage, @ProductColor, @Price, @Quantity, @TotalPrice)";

            SqlCommand cmd = new SqlCommand(query, con);

            //cmd.Parameters.AddWithValue("@MemberID", memberId);
            cmd.Parameters.AddWithValue("@ProductID", item.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
            cmd.Parameters.AddWithValue("@ProductImageURL", item.ProductImageURL);
            cmd.Parameters.AddWithValue("@ProductStorage", item.ProductStorage);
            cmd.Parameters.AddWithValue("@ProductColor", item.ProductColor);
            cmd.Parameters.AddWithValue("@Price", item.Price);
            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
            con.Open();
            cmd.ExecuteNonQuery();
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