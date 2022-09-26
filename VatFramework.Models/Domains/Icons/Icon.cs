using System.ComponentModel.DataAnnotations.Schema;

namespace VatFramework.Models.Domains.Icons
{
    public class Icon : BaseObject
    {
      
        public string IconName { get; set; }

        public string IconCode { get; set; }
    }
}
