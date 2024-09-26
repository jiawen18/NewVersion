using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class newShipper : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        private List<Shipper> AddedShippers
        {
            get
            {
                if (Session["AddedShippers"] == null)
                {
                    Session["AddedShippers"] = new List<Shipper>();
                }
                return (List<Shipper>)Session["AddedShippers"];
            }
            set
            {
                Session["AddedShippers"] = value;
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
                var shippers = context.Shippers.ToList();
                GridView1.DataSource = shippers;
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

                    var shipper = new Shipper
                    {
                        shipperName = addName.Text,
                        shipperEmail = addEmail.Text,
                        shipperPhone = addPhone.Text,
                        shipperAddress = addAddress.Text
                    };

                    context.Shippers.Add(shipper);
                    context.SaveChanges();

                    AddedShippers.Add(shipper);


                    GridView1.DataSource = new List<Shipper> { shipper };
                    GridView1.DataBind();

                    ClearFields();

                    FeedbackLabel.Text = "Shipper added successfully!";
                    FeedbackLabel.CssClass = "text-success";
                }
            }
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null)
            {
                int shipperID = Convert.ToInt32(editButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var shipper = context.Shippers.Find(shipperID);
                    if (shipper != null)
                    {

                        addName.Text = shipper.shipperName;
                        addEmail.Text = shipper.shipperEmail;
                        addPhone.Text = shipper.shipperPhone;
                        addAddress.Text = shipper.shipperAddress;

                        HiddenShipperID.Value = shipperID.ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#addRowModal').modal('show');", true);
                    }
                }
            }
        }

        protected void RemoveShipperButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int shipperId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {
                var shipperToRemove = context.Shippers.Find(shipperId);
                if (shipperToRemove != null)
                {
                    context.Shippers.Remove(shipperToRemove);
                    context.SaveChanges();
                }
            }

            BindGrid();
            FeedbackLabel.Text = "Shipper removed successfully!";
            FeedbackLabel.CssClass = "text-success";
        }

        private void ClearFields()
        {
            addName.Text = string.Empty;
            addEmail.Text = string.Empty;
            addPhone.Text = string.Empty;
            addAddress.Text = string.Empty;
        }
    }
}
