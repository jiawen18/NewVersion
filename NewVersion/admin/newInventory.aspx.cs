using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class newInventory : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSuppliers();
                BindProducts();
                BindGrid();
            }
        }

        private void BindSuppliers()
        {
            var suppliers = db.Suppliers.Select(s => new { s.supplierBranch, s.supplierID }).ToList();
            addInventorySupplier.DataSource = suppliers;
            addInventorySupplier.DataTextField = "supplierBranch";
            addInventorySupplier.DataValueField = "supplierID";
            addInventorySupplier.DataBind();
        }

        private void BindProducts()
        {
            var products = db.Products.Select(p => new { p.ProductName, p.ProductID }).ToList();
            addInventoryName.DataSource = products;
            addInventoryName.DataTextField = "ProductName";
            addInventoryName.DataValueField = "ProductID";
            addInventoryName.DataBind();
        }

        private void BindGrid()
        {
            var inventories = db.Inventories.ToList();
            GridView1.DataSource = inventories;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    string selectedProduct = addInventoryName.SelectedValue;
                    string selectedSupplier = addInventorySupplier.SelectedValue;
                    int quantity;

                    if (!int.TryParse(addInventoryQuantity.Text, out quantity) || quantity <= 0)
                    {
                        FeedbackLabel.Text = "Please enter a valid positive number for quantity.";
                        FeedbackLabel.CssClass = "text-danger";
                        return;
                    }

                    var existingInventory = context.Inventories
                        .FirstOrDefault(i => i.inventoryName == addInventoryName.SelectedItem.Text &&
                                             i.inventorySupplier == addInventorySupplier.SelectedItem.Text);

                    if (existingInventory != null)
                    {
                        FeedbackLabel.Text = "This combination of product and supplier already exists in the inventory.";
                        FeedbackLabel.CssClass = "text-danger";
                        return;
                    }

                    var inventory = new Inventory
                    {
                        inventoryName = addInventoryName.SelectedItem.Text,
                        inventorySupplier = addInventorySupplier.SelectedItem.Text,
                        inventoryQuantity = quantity
                    };

                    context.Inventories.Add(inventory);
                    context.SaveChanges();

                    BindGrid();

                    addInventoryName.SelectedIndex = -1;
                    addInventorySupplier.SelectedIndex = -1;
                    addInventoryQuantity.Text = string.Empty;

                    FeedbackLabel.Text = "Inventory item added successfully!";
                    FeedbackLabel.CssClass = "text-success";
                }
            }
        }

        protected void RemoveInventoryButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
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
    }
}
