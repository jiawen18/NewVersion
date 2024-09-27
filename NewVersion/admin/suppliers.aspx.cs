using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class supplier : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            using (var context = new userEntities())
            {
                var suppliers = context.Suppliers.ToList();
                GridView1.DataSource = suppliers;
                GridView1.DataBind();
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
                var suppliers = context.Suppliers.ToList();

                switch (sortColumn)
                {
                    case "supplierBranch":
                        suppliers = suppliers.OrderBy(s => s.supplierBranch).ToList();
                        break;
                    case "supplierEmail":
                        suppliers = suppliers.OrderBy(s => s.supplierEmail).ToList();
                        break;
                    case "supplierPhone":
                        suppliers = suppliers.OrderBy(s => s.supplierPhone).ToList();
                        break;
                    case "supplierAddress":
                        suppliers = suppliers.OrderBy(s => s.supplierAddress).ToList();
                        break;
                }

                GridView1.DataSource = suppliers;
                GridView1.DataBind();
            }
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null)
            {
                int supplierID = Convert.ToInt32(editButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var supplier = context.Suppliers.Find(supplierID);
                    if (supplier != null)
                    {

                        addBranch.Text = supplier.supplierBranch;
                        addEmail.Text = supplier.supplierEmail;
                        addPhone.Text = supplier.supplierPhone;
                        addAddress.Text = supplier.supplierAddress;

                        HiddenSupplierID.Value = supplierID.ToString();

                        ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#addRowModal').modal('show');", true);
                    }
                }
            }
        }


        protected void RemoveSupplierButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; // Cast sender to Button
            int supplierId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {
                var productsWithSupplier = context.Products.Where(p => p.SupplierID == supplierId).ToList();
                foreach (var product in productsWithSupplier)
                {
                    product.SupplierID = null;
                }
                context.SaveChanges();

                var supplierToRemove = context.Suppliers.Find(supplierId);
                if (supplierToRemove != null)
                {
                    context.Suppliers.Remove(supplierToRemove);
                    context.SaveChanges();
                }
            }

            BindGrid();
            FeedbackLabel.Text = "Supplier removed successfully!";
            FeedbackLabel.CssClass = "text-success";
        }


        protected void UpdateRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    int supplierID;
                    if (int.TryParse(HiddenSupplierID.Value, out supplierID) && supplierID > 0)
                    {
                        var supplier = context.Suppliers.Find(supplierID);
                        if (supplier != null)
                        {
                            supplier.supplierBranch = addBranch.Text;
                            supplier.supplierEmail = addEmail.Text;
                            supplier.supplierPhone = addPhone.Text;
                            supplier.supplierAddress = addAddress.Text;

                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        FeedbackLabel.Text = "Invalid Supplier ID.";
                        FeedbackLabel.CssClass = "text-danger";
                        return;
                    }
                }

                ResetModalState();

                FeedbackLabel.Text = "Supplier updated successfully!";
                FeedbackLabel.CssClass = "text-success";
            }
        }

        private void ResetModalState()
        {
            addBranch.Text = string.Empty;
            addEmail.Text = string.Empty;
            addPhone.Text = string.Empty;
            addAddress.Text = string.Empty;
            HiddenSupplierID.Value = string.Empty;

            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#addRowModal').modal('hide');", true);
            BindGrid();
        }
    }
}