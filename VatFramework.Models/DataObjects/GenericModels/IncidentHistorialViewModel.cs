using System;
using System.Collections.Generic;
using System.Text;

namespace FRSCInventory.Models.DataObjects.GenericModels
{
   public  class IncidentHistorialViewModel
    {
        public string Approval_Reject_Comment { get; set; }
        public DateTime? TreatedDate { get; set; }
        public string TreatedBy { get; set; }
        public long ID { get; set; }
    }
}
