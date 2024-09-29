
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace NewVersion
{
    public class Security
    {

        public static void LoginUser(string username, string role, string userType, bool rememberMe)
        {
            HttpContext ctx = HttpContext.Current;
            try
            {
                // Retrieve old ticket
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, rememberMe);
                // Decrypt the old ticket
                FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(authCookie.Value);

                // Combine role and user type into UserData for ticket
                string userData = $"{role},{userType}";

                // Create a new ticket with updated role and user type information
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                    oldTicket.Version,
                    oldTicket.Name,
                    oldTicket.IssueDate,
                    DateTime.Now.AddMinutes(30), // Set a new expiration time
                    oldTicket.IsPersistent,
                    userData  // Store role and userType in UserData
                );

                // Encrypt the new ticket
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                ctx.Response.Cookies.Add(authCookie);

                // Redirect user based on role and user type
                if (role == "Super Admin" || role == "Admin")
                {
                    ctx.Response.Redirect("~/admin/dashboard.aspx");  // Redirect to admin dashboard
                }
                else if (role == "Member")
                {
                    ctx.Response.Redirect("~/css/Home2.aspx");  // Redirect to member's home page
                }
                else
                {
                    throw new Exception("Unknown user type");  // Handle any undefined user types
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., logging)
                throw new Exception("Error during user login: " + ex.Message);
            }
        }


        public static void ProcessRoles()
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.User != null && ctx.User.Identity.IsAuthenticated && ctx.User.Identity is FormsIdentity identity)
            {
                // Extract role and user type from UserData
                string[] userData = identity.Ticket.UserData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string role = userData.Length > 0 ? userData[0] : string.Empty;
                string userType = userData.Length > 1 ? userData[1] : string.Empty;

                // Dynamically set roles based on user type
                string[] roles;
                switch (userType.ToLower())
                {
                    case "superadmin":
                        roles = new[] { "SuperAdmin", role };
                        break;
                    case "admin":
                        roles = new[] { "Admin", role };
                        break;
                    case "member":
                        roles = new[] { "Member", role };
                        break;
                    default:
                        roles = new[] { role };  // Handle any undefined roles
                        break;
                }

                // Create a new GenericPrincipal with roles
                GenericPrincipal principal = new GenericPrincipal(identity, roles);
                ctx.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }

        // Function to hash a password using SHA256
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //encode the original password to become binary
                byte[] binPass = Encoding.Default.GetBytes(password);

                //generate hash function
                //crytography library
                SHA256 sha = SHA256.Create();

                //perform hash function
                byte[] binHash = sha.ComputeHash(binPass);

                //in order to store the hash value into the database we need to convert it from binary back to string 
                string strHash = Convert.ToBase64String(binHash);

                return strHash;
            }
        }


    }
}