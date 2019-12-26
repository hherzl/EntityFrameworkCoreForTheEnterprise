namespace RothschildHouse.IdentityServer.Domain
{
    public class User
    {
        public User()
        {
        }

        public User(string userID, string email, string password, bool isActive)
        {
            UserID = userID;
            Email = email;
            Password = password;
            IsActive = isActive;
        }

        public string UserID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsActive { get; set; }
    }
}
