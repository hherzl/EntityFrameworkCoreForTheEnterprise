namespace OnlineStore.Core
{
    public interface IUserInfo
    {
        string UserName { get; set; }

        string Email { get; set; }

        string Role { get; set; }

        string GivenName { get; set; }

        string MiddleName { get; set; }

        string FamilyName { get; set; }
    }
}
