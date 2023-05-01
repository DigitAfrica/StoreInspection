namespace Web.Data;

public class Bll
{
    public User.Info LogUserIn(User.Login loginDetails)
    {
        if (loginDetails.Email != "mkhan@lodgeservice.com" || loginDetails.Password != "Password")
        {
            return new User.Info();
        }

        return new User.Info()
        {
            Id = 1,
            Email = loginDetails.Email,
            Name = "Muhammed",
            Surname = "Khan",
            Token = "JwtToken"
        };
    }
}