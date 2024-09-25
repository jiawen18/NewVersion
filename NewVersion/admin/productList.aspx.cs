using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Step 1: 
//using System.Data.SqlClient;
using System.Xml.Linq;
using Razorpay.Api;
using System.Configuration;
using System.Data;


namespace NewVersion.admin
{
    public partial class productList : System.Web.UI.Page
    {
        //step 2: retrieve CS from Global.asax
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductTable();
            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text;
            string productImageURL = txtProductImageURL.Text;
            decimal price;
            int quantity;

            // Validate inputs
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                lblMessage.Text = "Please enter a valid price.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                lblMessage.Text = "Please enter a valid quantity.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            bool isVisible = chkIsVisible.Checked;
            decimal totalPrice = price * quantity;

            // Connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            string query = @"INSERT INTO Product (ProductImageURL, ProductName, Price, Quantity, IsVisible, TotalPrice) 
                     VALUES (@ProductImageURL, @ProductName, @Price, @Quantity, @IsVisible, @TotalPrice)";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Supply input/data into parameters
                    cmd.Parameters.AddWithValue("@ProductImageURL", productImageURL);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@IsVisible", isVisible);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Product added successfully.";
                    lblMessage.CssClass = "text-success";

                }
            }
        }

        private void BindProductTable()
        {
            // Define connection string (assumed stored in Web.config)
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            // SQL query to fetch product data
            string query = "SELECT ProductID, ProductName, ProductImageURL, Price, Quantity FROM Product WHERE IsVisible = 1";

            // Connect to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();

                    // Fill the DataTable with the result of the query
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    // Bind the DataTable to the Repeater control
                    ProductRepeater.DataSource = dt;
                    ProductRepeater.DataBind();
                }
            }
        }

        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditProduct")
            {
                string productId = e.CommandArgument.ToString();
                LoadProductDetails(productId);
                ShowEditModal(); // Call a method to show the modal
            }

            if (e.CommandName == "DeleteProduct")
            {
                string productId = e.CommandArgument.ToString();
                DeleteProduct(productId);
                BindProductTable(); // Rebind the products after deletion
            }
        }

        private void LoadProductDetails(string productId)
        {
            string sql = "SELECT ProductName, ProductImageURL, Price, Quantity, IsVisible FROM Product WHERE ProductID = @ProductID";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtProductID.Value = productId; // Assuming you have a hidden field for ProductID
                            txtProductName.Text = reader["ProductName"].ToString();
                            txtProductImageURL.Text = reader["ProductImageURL"].ToString();
                            txtPrice.Text = reader["Price"].ToString();
                            txtQuantity.Text = reader["Quantity"].ToString();
                            chkIsVisible.Checked = (bool)reader["IsVisible"];
                        }
                    }
                }
            }
        }

        private void ShowEditModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModal", "$('#addRowModal').modal('show');", true);
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                // Retrieve user input
                string productId = txtProductID.Value;
                string productName = txtProductName.Text;
                string productImageUrl = txtProductImageURL.Text;
                decimal price;
                int quantity;

                // Validate inputs
                if (!decimal.TryParse(txtPrice.Text, out price))
                {
                    lblMessage.Text = "Please enter a valid price.";
                    lblMessage.CssClass = "text-danger";
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out quantity))
                {
                    lblMessage.Text = "Please enter a valid quantity.";
                    lblMessage.CssClass = "text-danger";
                    return;
                }

                bool isVisible = chkIsVisible.Checked;

                // Update the product in the database
                string sql = @"UPDATE Product SET ProductName = @ProductName, ProductImageURL = @ProductImageURL, 
                       Price = @Price, Quantity = @Quantity, IsVisible = @IsVisible WHERE ProductID = @ProductID";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Supply input/data into parameters
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@ProductImageURL", productImageUrl);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@IsVisible", isVisible);
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        try
                        {
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                lblMessage.Text = "Product updated successfully.";
                                lblMessage.CssClass = "text-success";
                            }
                            else
                            {
                                lblMessage.Text = "Product update failed. Product ID not found.";
                                lblMessage.CssClass = "text-danger";
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error: " + ex.Message;
                            lblMessage.CssClass = "text-danger";
                        }
                    }
                }

                // Refresh the product list after updating
                BindProductTable();

                // Optionally, reset fields and close the modal
                ClearFields();
                CloseEditModal();
            }
        }

        private void ClearFields()
        {
            txtProductID.Value = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductImageURL.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            chkIsVisible.Checked = false;
        }

        private void CloseEditModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeEditModal", "$('#addRowModal').modal('hide');", true);
        }

        private void LoadProducts()
        {
            string sql = "SELECT ProductID, ProductName, ProductImageURL, Price, Quantity, IsVisible FROM Product";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ProductRepeater.DataSource = reader;
                ProductRepeater.DataBind();
                con.Close();
            }
        }

        private void DeleteProduct(string productId)
        {
            string sql = "DELETE FROM Product WHERE ProductID = @ProductID";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class ProductRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

        public void AddProduct(string productImageURL, string productName, decimal price, int quantity, bool isVisible)
        {
            decimal totalPrice = price * quantity;
            string query = @"INSERT INTO Product (ProductImageURL, ProductName, Price, Quantity, IsVisible, TotalPrice) 
                             VALUES (@ProductImageURL, @ProductName, @Price, @Quantity, @IsVisible, @TotalPrice)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductImageURL", productImageURL);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@IsVisible", isVisible);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetVisibleProducts()
        {
            string query = "SELECT ProductID, ProductName, ProductImageURL, Price, Quantity FROM Product WHERE IsVisible = 1";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void UpdateProduct(string productId, string productName, string productImageURL, decimal price, int quantity, bool isVisible)
        {
            string sql = @"UPDATE Product SET ProductName = @ProductName, ProductImageURL = @ProductImageURL, 
                           Price = @Price, Quantity = @Quantity, IsVisible = @IsVisible WHERE ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@ProductImageURL", productImageURL);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@IsVisible", isVisible);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(string productId)
        {
            string sql = "DELETE FROM Product WHERE ProductID = @ProductID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataRow GetProductById(string productId)
        {
            string sql = "SELECT ProductName, ProductImageURL, Price, Quantity, IsVisible FROM Product WHERE ProductID = @ProductID";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
}
