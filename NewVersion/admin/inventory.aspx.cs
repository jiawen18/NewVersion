using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class inventory : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindSuppliers();
            }
        }

        private void BindGrid()
        {
            using (var context = new userEntities())
            {
                var inventoryItems = context.Inventories.ToList();
                GridView1.DataSource = inventoryItems;
                GridView1.DataBind();
            }
        }

        private void BindSuppliers()
        {
            using (var context = new userEntities())
            {
                var suppliers = context.Suppliers.ToList();
                addInventorySupplier.DataSource = suppliers;
                addInventorySupplier.DataTextField = "supplierBranch";
                addInventorySupplier.DataValueField = "supplierBranch";
                addInventorySupplier.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            var sortColumn = e.SortExpression;
            using (var context = new userEntities())
            {
                var inventoryItems = context.Inventories.ToList();

                switch (sortColumn)
                {
                    case "inventoryName":
                        inventoryItems = inventoryItems.OrderBy(i => i.inventoryName).ToList();
                        break;
                    case "inventorySupplier":
                        inventoryItems = inventoryItems.OrderBy(i => i.inventorySupplier).ToList();
                        break;
                    case "inventoryQuantity":
                        inventoryItems = inventoryItems.OrderBy(i => i.inventoryQuantity).ToList();
                        break;
                }

                GridView1.DataSource = inventoryItems;
                GridView1.DataBind();
            }
        }

        protected void RemoveInventoryButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; // Cast sender to Button
            int inventoryId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {
                var inventoryToRemove = context.Inventories.Find(inventoryId);
                if (inventoryToRemove != null)
                {
                    context.Inventories.Remove(inventoryToRemove);
                    context.SaveChanges();
                }
            }

            BindGrid();
            FeedbackLabel.Text = "Inventory item removed successfully!";
            FeedbackLabel.CssClass = "text-success";
        }

        protected void UpdateRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    int inventoryID;
                    if (int.TryParse(HiddenInventoryID.Value, out inventoryID) && inventoryID > 0)
                    {
                        var inventoryItem = context.Inventories.Find(inventoryID);
                        if (inventoryItem != null)
                        {

                            int initialQuantity = inventoryItem.inventoryQuantity;

                            inventoryItem.inventorySupplier = addInventorySupplier.SelectedValue;


                            int adjustment;
                            if (int.TryParse(adjustInventoryQuantity.Text, out adjustment))
                            {
                                inventoryItem.inventoryQuantity += adjustment;
                                context.SaveChanges();

                                FeedbackLabel.Text = $"Inventory item updated successfully!<br />" +
                                                     $"Initial Quantity: {initialQuantity} | " +
                                                     $"Adjustment: {adjustment} ({(adjustment >= 0 ? "Added" : "Subtracted")}) | " +
                                                     $"Final Quantity: {inventoryItem.inventoryQuantity}";
                                FeedbackLabel.CssClass = "text-success";
                            }
                            else
                            {
                                FeedbackLabel.Text = "Invalid adjustment quantity.";
                                FeedbackLabel.CssClass = "text-danger";
                            }
                        }
                    }
                    else
                    {
                        FeedbackLabel.Text = "Invalid Inventory ID.";
                        FeedbackLabel.CssClass = "text-danger";
                        return;
                    }
                }

                ResetModalState();
            }
        }


        private void ResetModalState()
        {
            addInventoryName.Text = string.Empty;
            addInventorySupplier.SelectedIndex = -1;
            currentInventoryQuantity.Text = string.Empty;
            adjustInventoryQuantity.Text = string.Empty;
            HiddenInventoryID.Value = string.Empty;

            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#addRowModal').modal('hide');", true);
            BindGrid();
        }
    }
}