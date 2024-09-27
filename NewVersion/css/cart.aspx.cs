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
                LoadCartItems();
                CalculateTotals();
            }

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

            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            int productId = Convert.ToInt32(((Button)item.FindControl("btnRemove")).CommandArgument);

            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId);

            if (existingItem != null)
            {
                existingItem.Quantity = Convert.ToInt32(txtQuantity.Text);
            }

            Session["Cart"] = cart;
            CalculateTotals();
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            if (cart != null)
            {
                for (int i = cart.Count - 1; i >= 0; i--)
                {
                    RepeaterItem item = rptProduct.Items[i];
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {
                        // Confirm deletion if needed.
                        cart.RemoveAt(i); // Remove the selected item.
                    }
                }

                Session["Cart"] = cart;
                LoadCartItems(); // Refresh the cart items display.
                CalculateTotals(); // Update totals after deletion.
            }
        }

        private void CalculateTotals()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            decimal subtotal = cart?.Sum(item => item.Price * item.Quantity) ?? 0;
            lblSubtotal.Text = subtotal.ToString("0.00");
            lblTotal.Text = subtotal.ToString("0.00");
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
            int productId = Convert.ToInt32(btnIncrease.CommandArgument); // Get the ProductID from CommandArgument

            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId);

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
            int productId = Convert.ToInt32(btnDecrease.CommandArgument); // Get the ProductID from CommandArgument

            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId);

            if (existingItem != null)
            {
                if (existingItem.Quantity > 1) // Prevent quantity from going below 1
                {
                    existingItem.Quantity--; // Decrement the quantity
                }
                else
                {
                    // Optionally handle removing the item if quantity is 1 and the user decrements
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

        }
    }
}