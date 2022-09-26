using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VatFramework.Models.DataObjects.ActivityLog
{
    public class ActivitlogSearchInfo
    {
        [Display(Name = "Controller")]
        public string SelectedController { get; set; }

        public IEnumerable<SelectListItem> Contollers { get; set; }

        public DateTime SelectedStartDate { get; set; }

        public DateTime SelectedEndDate { get; set; }

        [Display(Name = "Controller")]
        public string SelectedUser { get; set; }

        public string searchparameter { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }

    public class MiniSearchSearchInfo
    {
       
        public DateTime SelectedStartDate { get; set; }

        public DateTime SelectedEndDate { get; set; }

        [Display(Name = "Mini Plant")]
        public string selectedMiniPlantId { get; set; }

        public IEnumerable<SelectListItem> MiniPlant { get; set; }
    }

}
