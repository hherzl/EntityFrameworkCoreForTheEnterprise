namespace OnLineStore.IdentityServer.Models
{
    public class User
    {
        public string UserID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsActive { get; set; }
    }
}
