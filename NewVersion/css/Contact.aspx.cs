using System;
using System.Linq;
using System.Web.UI;
using NewVersion.Models;

namespace NewVersion.css
{
    public partial class Contact : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {

            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string message = txtMessage.Text.Trim();

            var supportRequest = new Support
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Message = message,
                CreatedAt = DateTime.Now,
                Status = "Pending"
            };

            db.Supports.Add(supportRequest);
            db.SaveChanges();

            FeedbackLabel.Text = "Thanks for contacting us! We will reply you as soon as we can...(maybe)";
            FeedbackLabel.CssClass = "text-success";
        }
    }
}
