namespace VatFramework.Models.DataObjects.Account
{
    public class RoleViewModel
    {
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string roledesc { get; set; }
        public bool issuperuser { get; set; }
        public int numberofusers { get; set; }
        public string conconrrency { get; set; }
    }
}
