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
    
    public partial class tbl_Receive
    {
        public tbl_Receive()
        {
            this.tbl_Issue = new HashSet<tbl_Issue>();
            this.tbl_Issue1 = new HashSet<tbl_Issue>();
        }
    
        public int id { get; set; }
        public string receiveNo { get; set; }
        public int GRN { get; set; }
        public int serialNo { get; set; }
        public int ItemId { get; set; }
        public int brandId { get; set; }
        public Nullable<int> colorId { get; set; }
        public System.DateTime dateTime { get; set; }
        public int demandId { get; set; }
        public int receiveQty { get; set; }
        public int unitId { get; set; }
    
        public virtual tbl_Brand tbl_Brand { get; set; }
        public virtual tbl_Color tbl_Color { get; set; }
        public virtual tbl_Demand tbl_Demand { get; set; }
        public virtual ICollection<tbl_Issue> tbl_Issue { get; set; }
        public virtual ICollection<tbl_Issue> tbl_Issue1 { get; set; }
        public virtual tbl_ItemMaster tbl_ItemMaster { get; set; }
        public virtual tbl_Unit tbl_Unit { get; set; }
    }
}
