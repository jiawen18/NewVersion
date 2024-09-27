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
                var inventoryItems = (from product in context.Products
                                      join supplier in context.Suppliers on product.SupplierID equals supplier.supplierID into supplierGroup
                                      from supplier in supplierGroup.DefaultIfEmpty() // Left join
                                      select new
                                      {
                                          product.ProductID,
                                          product.ProductName,
                                          SupplierName = supplier != null ? supplier.supplierBranch : string.Empty,
                                          product.Quantity
                                      }).ToList();

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
                addInventorySupplier.DataValueField = "supplierID";
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
                var productItems = (from p in context.Products
                                    join s in context.Suppliers on p.ProductID equals s.supplierID
                                    select new
                                    {
                                        ProductID = p.ProductID,
                                        ProductName = p.ProductName,
                                        SupplierBranch = s.supplierBranch,
                                        Quantity = p.Quantity
                                    }).ToList();

                switch (sortColumn)
                {
                    case "ProductName":
                        productItems = productItems.OrderBy(p => p.ProductName).ToList();
                        break;
                    case "SupplierBranch":
                        productItems = productItems.OrderBy(p => p.SupplierBranch).ToList();
                        break;
                    case "Quantity":
                        productItems = productItems.OrderBy(p => p.Quantity).ToList();
                        break;
                }

                GridView1.DataSource = productItems;
                GridView1.DataBind();
            }
        }

        protected void RemoveProductButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {
                var productToRemove = context.Products.Find(productId);
                if (productToRemove != null)
                {
                    context.Products.Remove(productToRemove);
                    context.SaveChanges();
                }
            }

            BindGrid();
            FeedbackLabel.Text = "Product removed successfully!";
            FeedbackLabel.CssClass = "text-success";
        }

        protected void UpdateRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    int productId;
                    if (int.TryParse(HiddenProductID.Value, out productId) && productId > 0)
                    {
                        var productItem = context.Products.Find(productId);
                        if (productItem != null)
                        {
                            int initialQuantity = productItem.Quantity;

                            if (!string.IsNullOrEmpty(addInventorySupplier.SelectedValue) && int.TryParse(addInventorySupplier.SelectedValue, out int selectedSupplierId))
                            {
                                productItem.SupplierID = selectedSupplierId;
                            }
                            else
                            {
                                productItem.SupplierID = null;
                            }

                            int adjustment;
                            if (int.TryParse(adjustInventoryQuantity.Text, out adjustment))
                            {
                                productItem.Quantity = initialQuantity + adjustment < 0 ? 0 : initialQuantity + adjustment;

                                context.SaveChanges();

                                FeedbackLabel.Text = $"Product updated successfully!<br />" +
                                                     $"Initial quantity: {initialQuantity}, " +
                                                     $"{(adjustment >= 0 ? "added" : "subtracted")}: {Math.Abs(adjustment)}, " +
                                                     $"final quantity: {productItem.Quantity}";
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
                        FeedbackLabel.Text = "Invalid Product ID.";
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
            HiddenProductID.Value = string.Empty;

            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#addRowModal').modal('hide');", true);
            BindGrid();
        }
    }
}
