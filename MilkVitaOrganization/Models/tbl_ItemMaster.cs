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
    
    public partial class tbl_ItemMaster
    {
        public tbl_ItemMaster()
        {
            this.tbl_Demand = new HashSet<tbl_Demand>();
            this.tbl_Issue = new HashSet<tbl_Issue>();
            this.tbl_Price = new HashSet<tbl_Price>();
            this.tbl_Receive = new HashSet<tbl_Receive>();
        }
    
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Nullable<int> categoryId { get; set; }
        public Nullable<int> brandId { get; set; }
        public Nullable<int> colorId { get; set; }
        public string barCode { get; set; }
    
        public virtual tbl_Brand tbl_Brand { get; set; }
        public virtual tbl_Category tbl_Category { get; set; }
        public virtual tbl_Color tbl_Color { get; set; }
        public virtual ICollection<tbl_Demand> tbl_Demand { get; set; }
        public virtual ICollection<tbl_Issue> tbl_Issue { get; set; }
        public virtual ICollection<tbl_Price> tbl_Price { get; set; }
        public virtual ICollection<tbl_Receive> tbl_Receive { get; set; }
    }
}