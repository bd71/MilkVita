//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MilkVitaOrganization.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Registration
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string name { get; set; }
        public Nullable<int> contact { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> registrationDate { get; set; }
        public string address { get; set; }
    }
}
