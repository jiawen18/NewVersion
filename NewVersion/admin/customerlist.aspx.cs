using NewVersion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion
{
    public partial class customerlist : System.Web.UI.Page
    {
        // Create an instance of your entity framework data context
        userEntities ue = new userEntities();
        protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadCustomerData();
                }
            }


            // Method to load admin data and bind it to the Repeater control
            private void LoadCustomerData()
            {
                string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT MemberID, Email, Username FROM MemberUser";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        RepeaterCustomerList.DataSource = reader;
                        RepeaterCustomerList.DataBind();
                    }
                }
            }

            // Event handler for the "Edit" button to enable editing
            protected void EditCustomer_Click(object sender, EventArgs e)
            {
                RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

                // Toggle visibility of the TextBox and Label controls
                Label lbl_username = item.FindControl("lbl_username") as Label;
                TextBox txt_username = item.FindControl("txt_username") as TextBox;

                Label lbl_email = item.FindControl("lbl_email") as Label;
                TextBox txt_email = item.FindControl("txt_email") as TextBox;

                // Hide labels and show TextBoxes for editing
                lbl_username.Visible = false;
                txt_username.Visible = true;

                lbl_email.Visible = false;
                txt_email.Visible = true;

                // Show the Save button and hide the Edit button
                Button btnEdit = item.FindControl("btnEdit") as Button;
                Button btnSave = item.FindControl("btnSave") as Button;

                btnEdit.Visible = false;
                btnSave.Visible = true;
            }

            // Event handler for saving updated admin details
            protected void SaveCustomer_Click(object sender, EventArgs e)
            {
                string username = (sender as Button).CommandArgument;
                RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

                // Get updated values from TextBoxes
                TextBox txt_username = item.FindControl("txt_username") as TextBox;
                TextBox txt_email = item.FindControl("txt_email") as TextBox;

                string newUsername = txt_username.Text;
                string newEmail = txt_email.Text;

                string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE MemberUser SET Username = @Username, Email = @Email WHERE Username = @OriginalUsername";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", newUsername);
                        cmd.Parameters.AddWithValue("@Email",newEmail);
                        cmd.Parameters.AddWithValue("@OriginalUsername", username);
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Write("<script>alert('Infomation Edited successfully.');</script>");
                // Reload the customer data to reflect the changes
                LoadCustomerData();
            }

            // Event handler for deleting an admin account
            protected void DeleteCustomer_Click(object sender, EventArgs e)
            {
                string username = (sender as Button).CommandArgument;

                string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM MemberUser WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Write("<script>alert('Account Deleted successfully.');</script>");
                // Reload the admin data to reflect the changes
                LoadCustomerData();
            }

       
    }
    }
