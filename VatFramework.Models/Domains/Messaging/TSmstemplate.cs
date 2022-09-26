namespace VatFramework.Models.Domains.Messaging
{
    public partial class TSmstemplate : BaseObject
    {
        public int SmstemplateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
