using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace NewVersion.css
{
    public partial class checkout : System.Web.UI.Page
    {
        String cs = Global.CS;

        private const string _key = "rzp_test_7sBM0c2utoTQ59";
        private const string _secret = "OKDPvhfckfnU2BnhPs7dKERM";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSessionValues();
                LoadCartItems();
                //LoadCartItems();
            }
        }

        private int GetCurrentCartID()
        {
            int cartId = -1;

            SqlConnection con = new SqlConnection(cs);

            string query = "SELECT CartID FROM ShoppingCart";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // if read
                {
                    cartId = reader.GetInt32(0); // get first cartId
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
            finally
            {
                con.Close();
            }

            return cartId;
        }

        private void LoadSessionValues()
        {
            
            if (Session["FirstName"] != null)
            {
                c_diff_fname.Text = Session["FirstName"].ToString();
            }

            if (Session["LastName"] != null)
            {
                c_diff_lname.Text = Session["LastName"].ToString();
            }

            if (Session["Phone"] != null)
            {
                c_diff_phone.Text = Session["Phone"].ToString();
            }

            if (Session["Address"] != null)
            {
                c_diff_address.Text = Session["Address"].ToString();
            }
        }


        protected void btnCloseDialog_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) { 
                PlaceHolder1.Controls.Clear();

                Label lbl1 = new Label();
                lbl1.ID = "lbl1";
                lbl1.Text = c_diff_fname.Text.Trim();  // First Name

                Label lbl2 = new Label();
                lbl2.ID = "lbl2";
                lbl2.Text = c_diff_lname.Text.Trim();  // Last Name

                Label lbl3 = new Label();
                lbl3.ID = "lbl3";
                lbl3.Text = c_diff_phone.Text;  // Phone Number

                Label lbl4 = new Label();
                lbl4.ID = "lbl4";
                lbl4.Text = c_diff_address.Text;  // Address

                PlaceHolder1.Controls.Add(lbl1);
                PlaceHolder1.Controls.Add(new LiteralControl(" "));
                PlaceHolder1.Controls.Add(lbl2);
                PlaceHolder1.Controls.Add(new LiteralControl(" | "));
                PlaceHolder1.Controls.Add(lbl3);
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                PlaceHolder1.Controls.Add(lbl4);

                Session["FirstName"] = c_diff_fname.Text.Trim();
                Session["LastName"] = c_diff_lname.Text.Trim();
                Session["Phone"] = c_diff_phone.Text;
                Session["Address"] = c_diff_address.Text;
            }
        }

        private void LoadCartItems()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                int cartId = GetCurrentCartID();

                string query = "SELECT * FROM CartItems WHERE CartID = @CartID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CartID", cartId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                decimal cartSubtotal = 0m;

                while (reader.Read())
                {
                    TableRow row = new TableRow();

                    // 假设你有以下字段
                    string productName = reader["ProductName"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    int quantity = Convert.ToInt32(reader["Quantity"]);

                    TableCell cell1 = new TableCell();
                    cell1.Controls.Add(new Label { Text = productName });
                    cell1.Controls.Add(new Label { Text = "<strong class='mx-2'>x</strong>" });
                    cell1.Controls.Add(new Literal { Text = quantity.ToString() });
                    row.Cells.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.Controls.Add(new Label { Text = "RM " + price.ToString("F2") });
                    row.Cells.Add(cell2);

                    phCartItems.Controls.Add(row);

                    // 计算小计
                    cartSubtotal += price * quantity;
                }

                lblCartSubTotal.Text = "RM " + cartSubtotal.ToString("F2");
                string subTotal = lblCartSubTotal.Text;

                decimal deliveryFeeInitial = 5.90m;
                lblDeliveryFee.Text = "RM " + deliveryFeeInitial.ToString("F2");
                string deliveryFee = lblDeliveryFee.Text;

                lblAmount.Text = "RM " + (cartSubtotal + deliveryFeeInitial).ToString("F2");
                string totalPrice = lblAmount.Text;

                // 将数据存储到 Checkout 表中
                StoreCartItems(cartId,subTotal, deliveryFee, totalPrice,productName);
            }
        }

        private void StoreCartItems(int cartId,string subTotal, string deliveryFee, string totalPrice,string productName)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // 这里的 INSERT 语句需要与你的 Checkout 表的字段匹配
                string query = "INSERT INTO CheckOut (CartID,SubTotal, DeliveryFee, TotalPrice,ProductName) VALUES (@CartID,@SubTotal, @DeliveryFee, @TotalPrice,@ProductName)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CartID", cartId);
                    cmd.Parameters.AddWithValue("@SubTotal", subTotal);
                    cmd.Parameters.AddWithValue("@DeliveryFee", deliveryFee);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        protected void btnPay_Click1(object sender, EventArgs e)
        {
            string currency = "MYR";
            decimal amount = GetAmountFromLabel(lblAmount.Text);

            if (amount <= 0)
            {
                throw new Exception("Parsed amount is less than or equal to 0: " + amount);
            }

            decimal amountInSubunits = amount * 100;

            if (amountInSubunits <= 0)
            {
                throw new Exception("Amount in subunits is less than or equal to 0: " + amountInSubunits);
            }

            decimal Amount = GetAmountFromLabel(lblAmount.Text);

            Session["Amount"] = Amount; // store to session

            string description = "Razorpay Payment Gateway";
            string imageLogo = "";

            Dictionary<string, string> notes = new Dictionary<string, string>()
            {
                {"note 1", "This is a Payment Note"},
                {"note 2", "Here another note, you can add max 15 notes"}
            };

            string orderId = CreateOrder(currency, amountInSubunits, notes);
            string jsFunction = "OpenPaymentWindow('" + _key + "','" + currency + "','" + amountInSubunits + "','" + description + "', '" + imageLogo + "', '" + orderId + "','" + JsonConvert.SerializeObject(notes) + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "OpenPaymentWindow", jsFunction, true);
        }

        private decimal GetAmountFromLabel(string amountText)
        {
            // slipt "RM"
            string cleanedAmountText = amountText.Replace("RM ", "").Trim();

            decimal amount;
            if (decimal.TryParse(cleanedAmountText, out amount))
            {
                return amount; //successful,return amount
            }
            else
            {
                return -1; // failed
            }
        }

        private string CreateOrder(string currency, decimal amountInSubunits, Dictionary<string, string> notes)
        {
            try
            {
                int paymentCapture = 1;

                RazorpayClient client = new RazorpayClient(_key, _secret);

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("currency", currency);
                options.Add("amount", amountInSubunits);
                options.Add("payment_capture", paymentCapture);
                options.Add("notes", notes);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                System.Net.ServicePointManager.Expect100Continue = false;

                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                var orderId = orderResponse.Attributes["id"].ToString();
                return orderId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when create Order: " + ex.Message);
            }
        }

    }
}
    