using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Step 1: 

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
            decimal price = decimal.Parse(txtPrice.Text);
            int quantity = int.Parse(txtQuantity.Text);
            int storage = Convert.ToInt32(DropDownList2.SelectedValue);
            int color = Convert.ToInt32(DropDownList1.SelectedValue);

            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            string query = @"INSERT INTO Product (ProductImageURL, ProductName, Price, Quantity,ProductStorage, ProductColor) 
                     VALUES (@ProductImageURL, @ProductName, @Price, @Quantity,@ProductStorage, @ProductColor)";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Supply input/data into parameters
                    cmd.Parameters.AddWithValue("@ProductImageURL", productImageURL);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@ProductStorage", storage);
                    cmd.Parameters.AddWithValue("@ProductColor", color);

                    con.Open(); 
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Product added successfully.";
                        lblMessage.CssClass = "text-success";

                        // Rebind the product data to the UI to show the newly added product
                        BindProductTable();

                        Response.Redirect("productList.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Product addition failed.";
                        lblMessage.CssClass = "text-danger";
                    }

                }
            }
    }

        private void BindProductTable()
        {
            // Define connection string (assumed stored in Web.config)
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            // SQL query to fetch product data
            string query = @"
    SELECT p.ProductID, p.ProductName, p.ProductImageURL, p.Price, p.Quantity, c.Color, s.Storage 
    FROM Product p 
    JOIN Storage s ON p.ProductStorage = s.StorageID 
    JOIN Color c ON c.ColorID = p.ProductColor";
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
                int productId = Convert.ToInt32 (e.CommandArgument);
                LoadProductDetails(productId);
                ShowEditModal(); 
            }

            if (e.CommandName == "DeleteProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                DeleteProduct(productId);
                BindProductTable(); 
            }
        }

        private void LoadProductDetails(int productId)
        {

            string sql = "SELECT p.ProductName, p.ProductImageURL, p.Price, p.Quantity,c.Color,s.Storage FROM Product p JOIN s.Storage ON p.ProductStorage = s.StorageID JOIN c.ColorID = p.ProductColor WHERE ProductID = @ProductID";
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
                            txtProductID.Value = (Convert.ToInt32(productId)).ToString();
                            txtProductName.Text = reader["ProductName"].ToString();
                            txtProductImageURL.Text = reader["ProductImageURL"].ToString();
                            txtPrice.Text = reader["Price"].ToString();
                            txtQuantity.Text = reader["Quantity"].ToString();
                            DropDownList2.SelectedValue = reader["Storage"].ToString();
                            DropDownList1.SelectedValue = reader["Color"].ToString();
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
            // Retrieve user input
            string productId = (txtProductID.Value).ToString();
            string productName = txtProductName1.Text;
            string productImageUrl = txtProductImageURL1.Text;
            decimal price = Convert.ToDecimal (txtPrice1.Text);
            int quantity = int.Parse(txtQuantity1.Text);
            int storage = Convert. ToInt32(DropDownList2.SelectedValue);
            int color = Convert.ToInt32(DropDownList1.SelectedValue);

            // Update the product in the database
            string sql = @"UPDATE Product SET ProductName = @ProductName, ProductImageURL = @ProductImageURL, 
                       Price = @Price, Quantity = @Quantity ,ProductColor = @ProductColor, 
                       ProductStorage = @ProductStorage
                        WHERE ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Supply input/data into parameters
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@ProductImageURL", productImageUrl);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ProductStorage", storage);
                    cmd.Parameters.AddWithValue("@ProductColor", color);

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

            BindProductTable();
        }


        private void ClearFields()
        {
            txtProductID.Value = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductImageURL.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }


        private void LoadProducts()
        {
            string sql = "SELECT p.ProductID,p.ProductName, p.ProductImageURL, p.Price, p.Quantity,c.Color,s.Storage FROM Product p JOIN s.Storage ON p.ProductStorage = s.StorageID JOIN c.ColorID = p.ProductColor";
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

        private void DeleteProduct(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            string query = "DELETE FROM Product WHERE ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
