using System;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class WebForm2 : System.Web.UI.Page
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
        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    int supplierID;
                    if (int.TryParse(HiddenSupplierID.Value, out supplierID) && supplierID > 0)
                    {
                        // Editing existing supplier
                        var supplier = context.Suppliers.Find(supplierID);
                        if (supplier != null)
                        {
                            supplier.supplierBranch = addBranch.Text;
                            supplier.supplierEmail = addEmail.Text;
                            supplier.supplierPhone = addPhone.Text;
                            supplier.supplierAddress = addAddress.Text;
                        }
                    }
                    else
                    {
                        // Adding new supplier
                        var supplier = new Supplier
                        {
                            supplierBranch = addBranch.Text,
                            supplierEmail = addEmail.Text,
                            supplierPhone = addPhone.Text,
                            supplierAddress = addAddress.Text
                        };
                        context.Suppliers.Add(supplier);
                    }

                    context.SaveChanges();
                }

                // Clear input fields
                addBranch.Text = string.Empty;
                addEmail.Text = string.Empty;
                addPhone.Text = string.Empty;
                addAddress.Text = string.Empty;

                // Clear hidden supplier ID
                HiddenSupplierID.Value = string.Empty;

                BindGrid();
                FeedbackLabel.Text = "Supplier saved successfully!";
                FeedbackLabel.CssClass = "text-success";
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
                        // Populate fields with the supplier's data
                        addBranch.Text = supplier.supplierBranch;
                        addEmail.Text = supplier.supplierEmail;
                        addPhone.Text = supplier.supplierPhone;
                        addAddress.Text = supplier.supplierAddress;

                        // Store the supplierID in a hidden field to identify it during update
                        HiddenSupplierID.Value = supplierID.ToString(); // Ensure you have a hidden field to store supplierID

                        // Open the modal
                        ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#addRowModal').modal('show');", true);
                    }
                }
            }
        }


        protected void RemoveSupplierButton_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int supplierId = Convert.ToInt32(e.CommandArgument);

                using (var context = new userEntities())
                {
                    // Find the supplier by ID
                    var supplierToRemove = context.Suppliers.Find(supplierId);
                    if (supplierToRemove != null)
                    {
                        context.Suppliers.Remove(supplierToRemove);
                        context.SaveChanges();
                    }
                }

                // Rebind the GridView to reflect the changes
                BindGrid();
                FeedbackLabel.Text = "Supplier removed successfully!";
                FeedbackLabel.CssClass = "text-success";
            }
        }

    }
}
