using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRSCInventory.Models.DataObjects.Reports
{
    public class ReportSearchInfo
    {
        [Display(Name = "LGA")]
        public long? SelectedLga { get; set; }

        public IEnumerable<SelectListItem> Lgas { get; set; }

        public string SelectedStartDate { get; set; }
        public string SituationOfficer { get; set; }

        public string SelectedEndDate { get; set; }
        public int? Status { get; set; }
        public long? MessageChannel { get; set; }

        [Display(Name = "Responder")]
        public long? SelectedResponderType { get; set; }

        public string searchparameter { get; set; }
        public IEnumerable<SelectListItem> Responders { get; set; }
    }
}
