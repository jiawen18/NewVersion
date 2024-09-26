using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class newSupplier : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        private List<Supplier> AddedSuppliers
        {
            get
            {
                if (Session["AddedSuppliers"] == null)
                {
                    Session["AddedSuppliers"] = new List<Supplier>();
                }
                return (List<Supplier>)Session["AddedSuppliers"];
            }
            set
            {
                Session["AddedSuppliers"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    // Create a new supplier
                    var supplier = new Supplier
                    {
                        supplierBranch = addBranch.Text,
                        supplierEmail = addEmail.Text,
                        supplierPhone = addPhone.Text,
                        supplierAddress = addAddress.Text
                    };

                    // Add the new supplier to the database
                    context.Suppliers.Add(supplier);
                    context.SaveChanges();

                    // Store the newly added supplier in the session list
                    AddedSuppliers.Add(supplier);

                    // Only show the newly added supplier in the GridView
                    GridView1.DataSource = new List<Supplier> { supplier };
                    GridView1.DataBind();

                    // Clear the input fields
                    addBranch.Text = string.Empty;
                    addEmail.Text = string.Empty;
                    addPhone.Text = string.Empty;
                    addAddress.Text = string.Empty;

                    FeedbackLabel.Text = "Supplier added successfully!";
                    FeedbackLabel.CssClass = "text-success";
                }
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
            Button btn = (Button)sender;
            int supplierId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {

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


    }
}
