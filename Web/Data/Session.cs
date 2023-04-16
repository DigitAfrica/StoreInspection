namespace Web.Data;

public class Session
{
    public User.Info _user { get; set; } = new User.Info();

    public void Login(User.Login loginDetails)
    {
        if (loginDetails == null)
        {
            _user = new User.Info();
            return;
        }

        if (loginDetails.Email != "abc@xyz.com" || loginDetails.Password != "Password")
        {
            _user = new User.Info();
            return;
        }

        _user = new User.Info()
        {
            Id = 1,
            Email = loginDetails.Email,
            Name = "FirstName",
            Surname = "LastName",
            Token = "JwtToken"
        };
    }

    public bool IsLoggedIn()
    {
        if (string.IsNullOrEmpty(_user.Token)
            || string.IsNullOrEmpty(_user.Email)
            || _user.Id == 0)
        {
            return false;
        }

        return true;
    }

    public void logOut()
    {
        _user = new User.Info();
    }
}
