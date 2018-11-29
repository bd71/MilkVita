using System;
using System.Collections.Generic;

namespace MilkVitaOrganization.Models
{
    public class DemandViewModel
    {
        public const string DemandKey = "DemandKey";
        public int DemandId { get; set; }
        public int DemandNo { get; set; }
        public int ItemId { get; set; }
        public string Item { get; set; }
        public int SerialNo { get; set; }
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public int DemandQty { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }

        public DemandViewModel()
        {
            DemandId = 0;
            DemandNo = 0;
            ItemId = 0;
            Item=string.Empty;
            SerialNo = 0;
            SupplierId = 0;
            Supplier = string.Empty;
            DemandQty = 0;
            Unit = string.Empty;
            UnitId = 0;


        }

    }

    public class DemandsViewModel
    {
        public const string DemandsKey = "DemandsKey";
        public DemandViewModel DemandViewModel { get; set; }
        public List<DemandViewModel> DemandViewModels { get; set; }

        public DemandsViewModel()
        {
            DemandViewModel=new DemandViewModel();
            DemandViewModels=new List<DemandViewModel>();
        }
    }
}