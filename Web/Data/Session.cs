namespace Web.Data;

public class Session
{
    public User.Info _user { get; set; } = new User.Info();
    public Answers _answers { get; set; } = new Answers();
    public Answers.LodgeSecurity lodgeSecurityAnswers = new Answers.LodgeSecurity();
    public readonly Bll? _bll;

    public Session(Bll bll)
    {
        _bll = bll;
    }

    public void Login(User.Login loginDetails)
    {
        if (loginDetails == null || _bll == null)
        {
            _user = new User.Info();
            return;
        }

        _user = _bll.LogUserIn(loginDetails);
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