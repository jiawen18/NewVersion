using System;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class suppliers : System.Web.UI.Page
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
            var suppliers = db.Suppliers.ToList();
            gvSuppliers.DataSource = suppliers;
            gvSuppliers.DataBind();
        }

        protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int supplierId = Convert.ToInt32(e.CommandArgument);
                // Handle edit action (implement your logic for editing if necessary)
            }
            else if (e.CommandName == "Delete")
            {
                int supplierId = Convert.ToInt32(e.CommandArgument);
                DeleteSupplier(supplierId);
                BindGrid(); // Refresh the grid after deletion
            }
        }

        protected void AddSupplier()
        {
            var newSupplier = new Supplier
            {
                supplierBranch = Request.Form["addBranch"],
                supplierEmail = Request.Form["addEmail"],
                supplierPhone = Request.Form["addPhone"],
                supplierAddress = Request.Form["addAddress"]
            };

            db.Suppliers.Add(newSupplier);
            db.SaveChanges();
            BindGrid(); // Refresh the grid after adding
        }

        private void DeleteSupplier(int id)
        {
            var supplier = db.Suppliers.Find(id);
            if (supplier != null)
            {
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }
        }
    }
}
