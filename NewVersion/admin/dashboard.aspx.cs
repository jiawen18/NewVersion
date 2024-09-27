using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                LoadPendingRefunds();
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

        private void LoadPendingRefunds()
        {
            var pendingRefunds = GetPendingRefunds(PendingRefundsGridView.PageIndex, PendingRefundsGridView.PageSize);
            PendingRefundsGridView.DataSource = pendingRefunds;
            PendingRefundsGridView.DataBind();
        }

        private List<RefundViewModel> GetPendingRefunds(int pageIndex, int pageSize)
        {
            return _context.Refunds
                .Where(r => r.RefundStatus == "Pending")
                .OrderBy(r => r.RefundRequestDate)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(r => new RefundViewModel
                {
                    OrderID = r.OrderID,
                    TotalAmount = _context.Orders
                        .Where(o => o.OrderID == r.OrderID)
                        .Select(o => o.ProductID) // change to order amount!!!!!!!!
                        .FirstOrDefault(),
                    RefundRequestDate = r.RefundRequestDate,
                    RefundStatus = r.RefundStatus
                }).ToList();
        }


        protected void InventoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            InventoryGridView.PageIndex = e.NewPageIndex;
            LoadInventoryData();
        }

        protected void PendingRefundsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PendingRefundsGridView.PageIndex = e.NewPageIndex;
            LoadPendingRefunds();
        }


        private string GetStockStatus(int quantity)
        {
            if (quantity < 1)
                return "Out of Stock";
            if (quantity < 20)
                return "Low";
            return "Sufficient";
        }

        public string GetTimeAgo(DateTime refundRequestDate)
        {
            TimeSpan timeSpan = DateTime.Now - refundRequestDate;

            if (timeSpan.TotalSeconds < 60)
                return $"{timeSpan.Seconds} seconds ago";
            else if (timeSpan.TotalMinutes < 60)
                return $"{timeSpan.Minutes} minutes ago";
            else if (timeSpan.TotalHours < 24)
                return $"{timeSpan.Hours} hours ago";
            else if (timeSpan.TotalDays < 7)
                return $"{timeSpan.Days} days ago";
            else if (timeSpan.TotalDays < 30)
                return $"{timeSpan.Days / 7} weeks ago";
            else if (timeSpan.TotalDays < 365)
                return $"{timeSpan.Days / 30} months ago";
            else
                return $"{timeSpan.Days / 365} years ago";
        }

    }

    public class RefundViewModel
    {
        public string OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime RefundRequestDate { get; set; }
        public string RefundStatus { get; set; }
    }
}
