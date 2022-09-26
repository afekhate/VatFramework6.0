using System.ComponentModel.DataAnnotations;



namespace VatFramework.Models.Domains.Permission
{
    public class Permission : BaseObject
    {
        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }


        public string Icon { get; set; }


        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        public int ParentId { get; set; }


    }
}
