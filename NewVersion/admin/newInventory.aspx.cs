using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    // Create a new inventory item
                    var inventory = new Inventory
                    {
                        inventoryName = addInventoryName.SelectedItem.Text, // Get product name
                        inventorySupplier = addInventorySupplier.SelectedItem.Text, // Get supplier branch
                        inventoryQuantity = int.Parse(addInventoryQuantity.Text)
                    };

                    // Add the new inventory item to the database
                    context.Inventories.Add(inventory);
                    context.SaveChanges();

                    // Only show the newly added inventory item in the GridView
                    GridView1.DataSource = new List<Inventory> { inventory };
                    GridView1.DataBind();

                    // Clear the input fields
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
