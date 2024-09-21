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

        public static void LoginUser(string username, string role, bool rememberMe)
        {
            HttpContext ctx = HttpContext.Current;
            try
            {
                // Retrieve old ticket
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, rememberMe);
                // Decrypt the old ticket
                FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(authCookie.Value);

                // Create a new ticket with updated role information
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                    oldTicket.Version,
                    oldTicket.Name,
                    oldTicket.IssueDate,
                    DateTime.Now.AddMinutes(30), // Set a new expiration time
                    oldTicket.IsPersistent,
                    role
                );

                // Encrypt the new ticket
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                ctx.Response.Cookies.Add(authCookie);

                // Redirect user to the appropriate URL
                string redirectUrl = FormsAuthentication.GetRedirectUrl(username, rememberMe);
                ctx.Response.Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., logging)
                throw new Exception("Error during user login: " + ex.Message);
            }
        }

        public static void ProcessRoles()
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.User != null && ctx.User.Identity.IsAuthenticated && ctx.User.Identity is FormsIdentity identity)
            {
                string[] roles = identity.Ticket.UserData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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