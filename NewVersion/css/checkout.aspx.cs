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

                // Lists to store product details
                List<string> productNames = new List<string>();
                List<string> prices = new List<string>();
                List<int> quantities = new List<int>();
                List<string> storages = new List<string>();
                List<string> colors = new List<string>();

                while (reader.Read())
                {
                    TableRow row = new TableRow();

                    // Assuming you have the following fields
                    string productName = reader["ProductName"].ToString();
                    string price = reader["Price"].ToString();
                    int quantity = Convert.ToInt32(reader["Quantity"]);
                    string storage = reader["ProductStorage"].ToString(); // Fetch storage
                    string color = reader["ProductColor"].ToString();

                    // Add product details to lists
                    productNames.Add(productName);
                    prices.Add(price);
                    quantities.Add(quantity);
                    storages.Add(storage); // Store storage details
                    colors.Add(color);

                    TableCell cell1 = new TableCell();
                    cell1.Controls.Add(new Label { Text = productName });
                    cell1.Controls.Add(new Label { Text = "<strong class='mx-2'>x</strong>" });
                    cell1.Controls.Add(new Literal { Text = quantity.ToString() });
                    cell1.Controls.Add(new Literal { Text = "<br />" });

                    Label lblDetails = new Label();
                    lblDetails.Text = "Storage: " + storage + "</br>Color: " + color;
                    lblDetails.Attributes.Add("style", "color: #888;"); // Set lighter color
                    cell1.Controls.Add(lblDetails);
                    row.Cells.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.Controls.Add(new Label { Text = "RM " + price});
                    row.Cells.Add(cell2);


                    phCartItems.Controls.Add(row);

                    // Calculate subtotal
                    cartSubtotal += Convert.ToDecimal(price) * quantity;
                }

                lblCartSubTotal.Text = "RM " + cartSubtotal.ToString("F2");
                string subTotal = lblCartSubTotal.Text;

                decimal deliveryFeeInitial = 5.90m;
                lblDeliveryFee.Text = "RM " + deliveryFeeInitial.ToString("F2");
                string deliveryFee = lblDeliveryFee.Text;

                lblAmount.Text = "RM " + (cartSubtotal + deliveryFeeInitial).ToString("F2");
                string totalPrice = lblAmount.Text;

                // Store cart items including product names, prices, quantities, storages, and colors
                StoreCartItems(cartId, subTotal, deliveryFee, totalPrice, productNames, prices, quantities, storages, colors);
            }
        }

        private void StoreCartItems(int cartId, string subTotal, string deliveryFee, string totalPrice, List<string> productNames, List<string> prices, List<int> quantities,List<string> storages,List<string> colors)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                foreach (var productName in productNames.Select((value, index) => new { value, index }))
                {
                    string productDetails = "Storage: " + storages[productName.index] + "Color: " + colors[productName.index];

                    // Update the INSERT statement to include ProductName, Price, and Quantity
                    string query = "INSERT INTO CheckOut (CartID, SubTotal, DeliveryFee, TotalPrice, ProductName, Price, Quantity,ProductDetails) VALUES (@CartID, @SubTotal, @DeliveryFee, @TotalPrice, @ProductName, @Price, @Quantity,@ProductDetails)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CartID", cartId);
                        cmd.Parameters.AddWithValue("@SubTotal", subTotal);
                        cmd.Parameters.AddWithValue("@DeliveryFee", deliveryFee);
                        cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@ProductName", productNames[productName.index]); // Get product name
                        cmd.Parameters.AddWithValue("@Price", prices[productName.index]); // Get price
                        cmd.Parameters.AddWithValue("@Quantity", quantities[productName.index]); // Get quantity
                        cmd.Parameters.AddWithValue("@ProductDetails", productDetails);
                        

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private List<int> GetProductIDs(int cartId)
        {
            List<int> productIds = new List<int>(); // List to store multiple ProductIDs
            string query = "SELECT ProductID FROM CartItems WHERE CartID = @CartID";

            using (SqlConnection con = new SqlConnection(cs)) // 'cs' is your connection string
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CartID", cartId);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) // Loop through all rows
                    {
                        int productId = Convert.ToInt32(reader["ProductID"]); // Get ProductID
                        productIds.Add(productId); // Add to the list
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }

            return productIds; // Return the list of ProductIDs
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

            // Store the CartID and ProductIDs to session
            int cartId = GetCurrentCartID();
            Session["CartID"] = cartId;

            List<int> productIds = GetProductIDs(cartId); // Assuming GetProductIDs returns a list of ProductID based on CartID
            Session["ProductIDs"] = productIds; // Store the list of ProductIDs in session

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
    