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

        /* private void LogInUser(int memberId)
         {
             
             HttpContext.Current.Session["MemberID"] = memberId;
         }*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new List<CartItem>(); // Initialize the cart
                }

                LoadCartItems();
                CalculateTotals();
            }

            RegisterPostBackControls();

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

        private void RegisterPostBackControls()
        {
            foreach (RepeaterItem item in rptProduct.Items)
            {
                Button btnDecrease = (Button)item.FindControl("btnDecrease");
                Button btnIncrease = (Button)item.FindControl("btnIncrease");

                // Get the cart item from the data item
                CartItem cartItem = (CartItem)item.DataItem; 

                // Check if cartItem is not null
                if (cartItem != null)
                {
                    // Assuming ProductID is a property of CartItem
                    int productId = cartItem.ProductID;
                    string storageOption = cartItem.StorageOption;
                    string colorOption = cartItem.ColorOption;

                    // Set CommandArgument with product details
                    string commandArgument = $"{productId.ToString()}|{storageOption.ToString()}|{colorOption.ToString()}";

                    if (btnDecrease != null)
                    {
                        btnDecrease.CommandArgument = commandArgument; // Set CommandArgument
                        ClientScript.RegisterForEventValidation(btnDecrease.UniqueID, commandArgument);
                    }

                    if (btnIncrease != null)
                    {
                        btnIncrease.CommandArgument = commandArgument;// Set CommandArgument
                        ClientScript.RegisterForEventValidation(btnIncrease.UniqueID, commandArgument);
                    }
                }
            }
        }


        private void LoadCartItems()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart != null && cart.Count > 0)
            {
                rptProduct.DataSource = cart;
                rptProduct.DataBind();
            }
            else
            {
                rptProduct.DataSource = null;
                rptProduct.DataBind();
            }
        }

        

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;

            int productId = Convert.ToInt32(((Button)item.FindControl("btnDecrease")).CommandArgument.Split('|')[0]);
            string storageOption = ((Button)item.FindControl("btnDecrease")).CommandArgument.Split('|')[1];
            string colorOption = ((Button)item.FindControl("btnDecrease")).CommandArgument.Split('|')[2];

            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId && i.StorageOption == storageOption && i.ColorOption == colorOption);

            if (existingItem != null)
            {
                int quantity;
                if (int.TryParse(txtQuantity.Text, out quantity))
                {
                    if (quantity <= 0)
                    {
                        
                            cart.Remove(existingItem);
                        
                    }
                    else
                    {
                        existingItem.Quantity = quantity;
                    }
                }
            }

            Session["Cart"] = cart;
            LoadCartItems();
            CalculateTotals();
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>; // 假设你有一个方法获取购物车中的所有项目


            for (int i = cart.Count - 1; i >= 0; i--)
            {
                RepeaterItem item = rptProduct.Items[i];
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");

                if (chkSelect.Checked)
                {
                    cart.RemoveAt(i);
                }
            }



            LoadCartItems();
            CalculateTotals(); // 更新购物车小计和总计

            // 隐藏垃圾桶图标
            ScriptManager.RegisterStartupScript(this, GetType(), "HideTrashIcon", "document.getElementById('trash-icon').style.display = 'none';", true);
        }

        private void CalculateTotals()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            decimal subtotal = cart?.Sum(item => item.Price * item.Quantity) ?? 0;
            lblSubtotal.Text = subtotal.ToString("N2");
            lblTotal.Text = subtotal.ToString("N2");
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            CheckBox selectAll = (CheckBox)sender;
            foreach (RepeaterItem item in rptProduct.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                chkSelect.Checked = selectAll.Checked;
            }
        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            Button btnIncrease = (Button)sender;
            string[] args = btnIncrease.CommandArgument.Split('|');

            if (args.Length != 3) return;

            int productId = Convert.ToInt32(args[0]); // Get the ProductID from CommandArgument
            string storageOption = args[1];
            string colorOption = args[2];

            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId && i.StorageOption == storageOption && i.ColorOption == colorOption);

            if (existingItem != null)
            {
                existingItem.Quantity++; // Increment the quantity
            }

            Session["Cart"] = cart;
            LoadCartItems(); // Refresh the cart display
            CalculateTotals(); // Update totals
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            Button btnDecrease = (Button)sender;
            string[] args = btnDecrease.CommandArgument.Split('|');

            if (args.Length != 3) return;

            int productId = Convert.ToInt32(args[0]); // Get the ProductID from CommandArgument
            string storageOption = args[1];
            string colorOption = args[2];

            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId && i.StorageOption == storageOption && i.ColorOption == colorOption);

            if (existingItem != null)
            {
                if (existingItem.Quantity > 1) // Prevent quantity from going below 1
                {
                    existingItem.Quantity--; // Decrement the quantity
                }
                else
                {
                        cart.Remove(existingItem);
                }
            }

            Session["Cart"] = cart;
            LoadCartItems(); // Refresh the cart display
            CalculateTotals(); // Update totals
        }

        

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Smartphones.aspx");
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            if (cart != null && cart.Count > 0)
            {
                
                Session["CartItems"] = cart; 
                Session["TotalPrice"] = "RM " + cart.Sum(item => item.Price * item.Quantity); // 计算并保存总价格

                
                Response.Redirect("checkout.aspx");
            }
        }
    }
}