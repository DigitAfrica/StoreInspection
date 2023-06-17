using Newtonsoft.Json;

namespace Web.Data;

public class Session
{
    private readonly IConfiguration _configuration;

    public Session(Bll bll, IConfiguration configuration)
    {
        _configuration = configuration;
        _bll = bll;
    }

    public User.Info _user { get; set; } = new();
    public User.Notyf _notyf { get; set; } = new();
    public NikeForm _nikeForm { get; set; } = new();

    public readonly Bll? _bll;

    public void Register(User.Register regiserDeatils)
    {
        try
        {
            if (regiserDeatils == null || _bll == null) return;
            
            var email = new MailObject();
            var subject = $"{_configuration["Portal:Name"]} - New User Registration";
            var body = "Hi, below are the details of the user requesting access<br/><br/>"
                + $"Full Name: {regiserDeatils.Name} {regiserDeatils.Surname}<br/>"
                + $"Email {regiserDeatils.Email}<br/><br/>"
                + "Have a great day!<br/>";

            email.Subject = subject;
            email.Body = body;

            var mail = new Mail(_configuration);
            mail.sendMail(email);
        }
        catch (Exception ex)
        {
            _notyf = new User.Notyf() { Fail = true, Msg = ex.Message };

        }
    }

    public void Login(User.Login loginDetails)
    {
        try
        {
            _user = new User.Info();

            if (loginDetails == null || _bll == null) return;

            var reply = _bll.LogIn(loginDetails);

            var msg = string.IsNullOrEmpty(reply.Msg) ? "Failed to log in" : reply.Msg;

            if (reply.Fail)
            {
                _notyf = new User.Notyf() { Fail = true, Msg = msg };
                return;
            }

            if (!reply.Win)
            {
                _notyf = new User.Notyf() { Msg = msg };
                return;
            }

            if (reply.Data == null)
            {
                _notyf = new User.Notyf() { Msg = "Failed to log in" };
                return;
            }

            _user = (User.Info)reply.Data;
        }
        catch (Exception ex)
        {
            _notyf = new User.Notyf() { Fail = true, Msg = ex.Message };
        }
    }

    public bool IsLoggedIn()
    {
        try
        {
            if (string.IsNullOrEmpty(_user.Token)
            || string.IsNullOrEmpty(_user.Email)
            || _user.Id == 0)
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public void logOut()
    {
        _user = new User.Info();
        _nikeForm = new NikeForm();
    }
}