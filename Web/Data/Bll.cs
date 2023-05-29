using ChartJs.Blazor.Common;
using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using Newtonsoft.Json.Linq;

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

    public PieConfig PieInit(Shrinkage shrinkage)
    {
        try
        {
            var _pieConfig = PieLables(shrinkage);
            return PieData(shrinkage, _pieConfig);
        }
        catch (Exception ex)
        {
            return new PieConfig();
        }
    }

    public PieConfig PieLables(Shrinkage shrinkage)
    {
        try
        {
            var _pieConfig = new PieConfig();
            return _pieConfig;

            //var title = new OptionsTitle()
            //{
            //    Display = true,
            //    Text = "% Developers using Chart.js in Blazor"
            //};

            //_pieConfig.Options = new PieOptions()
            //{
            //    Responsive = true,
            //    Title = title
            //};

            //var pFName = shrinkage.ProcessFailure.Name;
            //var pFValue = shrinkage.ProcessFailure.Value;

            //var iTName = shrinkage.InternalTheft.Name;
            //var iTValue = shrinkage.InternalTheft.Value;

            //var eTName = shrinkage.ExternalTheft.Name;
            //var eTValue = shrinkage.ExternalTheft.Value;

            //var sFName = shrinkage.SupplierFraud.Name;
            //var sFValue = shrinkage.SupplierFraud.Value;

            //_pieConfig.Data.Labels.Add($"{pFName} {pFValue}%");
            //_pieConfig.Data.Labels.Add($"{iTName} {iTValue}%");
            //_pieConfig.Data.Labels.Add($"{eTName} {eTValue}%");
            //_pieConfig.Data.Labels.Add($"{sFName} {sFValue}%");
        }
        catch (Exception ex)
        {
            return new PieConfig();
        }
    }

    public PieConfig PieData(Shrinkage shrinkage, PieConfig _pieConfig)
    {
        var pFValue = shrinkage.ProcessFailure.Value;
        var iTValue = shrinkage.InternalTheft.Value;
        var eTValue = shrinkage.ExternalTheft.Value;
        var sFValue = shrinkage.SupplierFraud.Value;

        PieDataset<int> dataset = new PieDataset<int>(new[]
        {
            pFValue, iTValue, eTValue, sFValue
        });

        dataset.BackgroundColor = new[]
        {
                ColorUtil.ColorHexString(255, 205, 86), // Slice 2 aka "Yellow"
                ColorUtil.ColorHexString(75, 192, 192), // Slice 3 aka "Green"
                ColorUtil.ColorHexString(255, 99, 132), // Slice 1 aka "Red"
                ColorUtil.ColorHexString(54, 162, 235), // Slice 4 aka "Blue"
            };

        _pieConfig.Data.Datasets.Add(dataset);

        return _pieConfig;
    }

    public PieConfig BarSetup(Shrinkage shrinkage)
    {
        try
        {
            var _pieConfig = PieLables(shrinkage);
            return PieData(shrinkage, _pieConfig);
        }
        catch (Exception ex)
        {
            return new PieConfig();
        }
    }
}