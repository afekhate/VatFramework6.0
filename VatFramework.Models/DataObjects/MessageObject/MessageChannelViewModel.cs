using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VatFramework.Models.Domains;

namespace VatFramework.Models.DataObjects.MessageObject
{
    public class MessageChannelViewModel : BaseObjectDataIntegrity
    {
         [Required]
        public string MessageRoute { get; set; }
    }
}
