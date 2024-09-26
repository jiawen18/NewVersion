using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.admin
{
    public partial class update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAdminData();
            }
        }


        // Method to load admin data and bind it to the Repeater control
        private void LoadAdminData()
        {
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Username, Position, Office FROM AdminUser";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    RepeaterAdminList.DataSource = reader;
                    RepeaterAdminList.DataBind();
                }
            }
        }

        // Event handler for the "Edit" button to enable editing
        protected void EditAdmin_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

            // Toggle visibility of the TextBox and Label controls
            Label lblPosition = item.FindControl("lblPosition") as Label;
            TextBox txtPosition = item.FindControl("txtPosition") as TextBox;

            Label lblOffice = item.FindControl("lblOffice") as Label;
            TextBox txtOffice = item.FindControl("txtOffice") as TextBox;

            // Hide labels and show TextBoxes for editing
            lblPosition.Visible = false;
            txtPosition.Visible = true;

            lblOffice.Visible = false;
            txtOffice.Visible = true;

            // Show the Save button and hide the Edit button
            Button btnEdit = item.FindControl("btnEdit") as Button;
            Button btnSave = item.FindControl("btnSave") as Button;

            btnEdit.Visible = false;
            btnSave.Visible = true;
        }

        // Event handler for saving updated admin details
        protected void SaveAdmin_Click(object sender, EventArgs e)
        {
            string username = (sender as Button).CommandArgument;
            RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

            // Get updated values from TextBoxes
            TextBox txtPosition = item.FindControl("txtPosition") as TextBox;
            TextBox txtOffice = item.FindControl("txtOffice") as TextBox;

            string newPosition = txtPosition.Text;
            string newOffice = txtOffice.Text;

            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string updateQuery = "UPDATE AdminUser SET Position = @Position, Office = @Office WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Position", newPosition);
                    cmd.Parameters.AddWithValue("@Office", newOffice);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.ExecuteNonQuery();
                }
            }

            // Reload the admin data to reflect the changes
            LoadAdminData();
        }

        // Event handler for deleting an admin account
        protected void DeleteAdmin_Click(object sender, EventArgs e)
        {
            string username = (sender as Button).CommandArgument;

            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.ExecuteNonQuery();
                }
            }

            // Reload the admin data to reflect the changes
            LoadAdminData();
        }
    }
}