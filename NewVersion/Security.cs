using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NewVersion
{
    public class Security
    {

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