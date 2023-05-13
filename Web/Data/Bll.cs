namespace Web.Data;

public class Bll
{
    private readonly IConfiguration _configuration;

    public Bll(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Reply LogIn(User.Login loginDetails)
    {
        try
        {
            var userConfig = $"Users:{loginDetails.Email}";
            var savedPass = _configuration[$"{userConfig}:Pass"];

            if (savedPass == null || loginDetails.Password != savedPass)
            {
                return new Reply() { Msg = "Incorrect email or password" };
            }

            int.TryParse(_configuration[$"{userConfig}:Id"], out var outId);
            var userInfo = new User.Info()
            {
                Id = outId,
                Email = loginDetails.Email,
                Name = _configuration[$"{userConfig}:Name"],
                Surname = _configuration[$"{userConfig}:Surname"],
                Token = "JwtToken"
            };

            return new Reply() { Win = true, Data = userInfo };
        }
        catch (Exception ex)
        {
            return new Reply() { Fail = true, Msg = ex.Message };
        }
    }
}