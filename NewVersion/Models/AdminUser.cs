//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewVersion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminUser
    {
        public int AdminID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Phone { get; set; }
    }
}
