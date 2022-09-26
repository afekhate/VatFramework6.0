using System;

namespace VatFramework.Models.DataObjects.Account
{
    public class ReactivationRequestDTO
    {
        public string userid { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public string Requester { get; set; }
        public DateTime StatusChangeDate { get; set; }
    }
}
