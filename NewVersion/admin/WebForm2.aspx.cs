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

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    var supplier = new Supplier
                    {
                        supplierBranch = addBranch.Text,
                        supplierEmail = addEmail.Text,
                        supplierPhone = addPhone.Text,
                        supplierAddress = addAddress.Text
                    };

                    context.Suppliers.Add(supplier);
                    context.SaveChanges();
                }

                addBranch.Text = string.Empty;
                addEmail.Text = string.Empty;
                addPhone.Text = string.Empty;
                addAddress.Text = string.Empty;

                BindGrid();

                ScriptManager.RegisterStartupScript(this, GetType(), "hideModal", "$('#editRowModal').modal('hide');", true);

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

        //protected void ButtonEdit_Click(object sender, EventArgs e)
        //{
        //    string branch = editBranch.Text;
        //    string email = editEmail.Text;
        //    string phone = editPhone.Text;
        //    string address = editAddress.Text;

        //    int supplierID = int.Parse(editSupplierID.Value);

        //    using (var context = new userEntities())
        //    {
        //        var supplier = context.Suppliers.Find(supplierID);
        //        if (supplier != null)
        //        {
        //            supplier.supplierBranch = branch;
        //            supplier.supplierEmail = email;
        //            supplier.supplierPhone = phone;
        //            supplier.supplierAddress = address;

        //            context.SaveChanges();
        //        }
        //    }

        //    BindGrid();

        //    ScriptManager.RegisterStartupScript(this, GetType(), "hideModal", "$('#editRowModal').modal('hide');", true);
        //}


    }
}
