namespace DTO
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string displayName { get; set; }
        public string? ou { get; set; }

        public bool? accountActive { get; set; }

        public int? userAccountControl { get; set; }
        public int? logonCount { get; set; }

        public string? pwdExpires { get; set; }
        public string? whenCreated { get; set; }
        public string? pwdLastSet { get; set; }
    }
}