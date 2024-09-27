using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.admin
{
    public partial class transactions : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTransactionData();
        }

        private void BindTransactionData()
        {
            string query = "SELECT TransactionID, OrderID, TransactionStatus,InvoiceID, InvoiceDate FROM [Transaction]";

            using (SqlConnection conn = new SqlConnection(cs))
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

                    TransactionRepeater.DataSource = dt;
                    TransactionRepeater.DataBind();
                }
            }
        }

        protected void TransactionRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditTransaction")
            {
                string transactionId = e.CommandArgument.ToString();
                LoadTransactionDetails(transactionId);
                ShowEditModal();
            }

            if (e.CommandName == "DeleteTransaction")
            {
                string transactionId = e.CommandArgument.ToString();
                DeleteTransaction(transactionId);
                BindTransactionData();
            }
        }

        protected void RemoveTransactionButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string transactionId = btn.CommandArgument;

            DeleteTransaction(transactionId);

            BindTransactionData();

            lblMessage.Text = "Transaction Deleted.";
            lblMessage.CssClass = "alert alert-success";
        }

        private void ShowEditModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModal", "$('#addRowModal').modal('show');", true);
        }

        private void LoadTransactionDetails(string transactionId)
        {
            string sql = "SELECT OrderID,TransactionStatus, InvoiceID, InvoiceDate FROM [Transaction] WHERE TransactionID = @TransactionID";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTransactionID.Value = transactionId;
                            txtOrderID.Text = reader["OrderID"].ToString();
                            ddlTransactionStatus.SelectedValue = reader["TransactionStatus"].ToString();
                            txtInvoiceID.Text = reader["InvoiceID"].ToString();
                            txtInvoiceDate.Text = Convert.ToDateTime(reader["InvoiceDate"]).ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
        }

        protected void btnUpdateTransaction_Click(object sender, EventArgs e)
        {
            string transactionID = txtTransactionID.Value;
            string transactionStatus = ddlTransactionStatus.SelectedValue;

            // Update the product in the database
            string sql = "UPDATE [Transaction] SET TransactionStatus = @TransactionStatus WHERE TransactionID = @TransactionID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Supply input/data into parameters
                    cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                    cmd.Parameters.AddWithValue("@TransactionStatus", transactionStatus);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Transaction updated successfully.";
                            lblMessage.CssClass = "text-success";
                        }
                        else
                        {
                            lblMessage.Text = "Transaction update failed. Order ID not found.";
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

            BindTransactionData();
        }

        private void DeleteTransaction(string transactionID)
        {
            string query = "DELETE FROM [Transaction] WHERE TransactionID = @TransactionID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
