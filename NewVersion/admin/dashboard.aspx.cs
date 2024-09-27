using System;
using System.Linq;
using System.Data.Entity;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        private readonly userEntities _context = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInventoryData();
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            var totalMembers = _context.MemberUsers.Count();
            var totalAdmins = _context.AdminUsers.Count(); 
            var totalSales = _context.Products.Sum(s => s.Quantity); // Replace with transaction!!!!!
            var totalOrders = _context.Orders.Count();

            MembersCountLabel.Text = totalMembers.ToString();
            AdminsCountLabel.Text = totalAdmins.ToString();
            SalesAmountLabel.Text = $"RM {totalSales:F2}";
            OrdersCountLabel.Text = totalOrders.ToString();
        }

        private void LoadInventoryData()
        {
            var products = _context.Products.Include(p => p.Supplier).ToList();

            var inventoryData = products.Select(p => new
            {
                ProductName = p.ProductName,
                Supplier = p.Supplier != null ? p.Supplier.supplierBranch : "",
                Quantity = p.Quantity,
                Status = GetStockStatus(p.Quantity)
            }).ToList();

            InventoryGridView.DataSource = inventoryData;
            InventoryGridView.DataBind();
        }



        private string GetStockStatus(int quantity)
        {
            if (quantity < 1)
                return "Out of Stock";
            if (quantity < 20)
                return "Low";
            return "Sufficient";
        }
    }
}
