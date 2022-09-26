
using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Models.DataObjects.ReportDTO
{
    public class DashboardModel
    {
        public List<UnitAnalysis> UnitAnalyses { get; set; }
    }



    public class UnitAnalysis
    {
        public string Description { get; set; }

        public long Quantity { get; set; }

    }
    public class MiniPlantReques
    {
        public string PlantName { get; set; }
        public string StateName { get; set; }
        public long QtyRequested { get; set; }
        public long QtyApproved { get; set; }
        public string Status { get; set; }
    }

    public class ReportData
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public string Total { get; set; }
        public string MappingItem { get; set; }
        public string PlantName { get; set; }

    }
}
