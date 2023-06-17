using ChartJs.Blazor.BarChart;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using Newtonsoft.Json.Linq;
using System.Drawing;

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

            var pFName = shrinkage.ProcessFailure.Name;
            var pFValue = shrinkage.ProcessFailure.Value;

            var iTName = shrinkage.InternalTheft.Name;
            var iTValue = shrinkage.InternalTheft.Value;

            var eTName = shrinkage.ExternalTheft.Name;
            var eTValue = shrinkage.ExternalTheft.Value;

            var sFName = shrinkage.SupplierFraud.Name;
            var sFValue = shrinkage.SupplierFraud.Value;

            _pieConfig.Data.Labels.Add(shrinkage.ProcessFailure.Name);
            _pieConfig.Data.Labels.Add(shrinkage.InternalTheft.Name);
            _pieConfig.Data.Labels.Add(shrinkage.ExternalTheft.Name);
            _pieConfig.Data.Labels.Add(shrinkage.SupplierFraud.Name);
         
            return _pieConfig;
        }
        catch (Exception ex)
        {
            return new PieConfig();
        }
    }

    public PieConfig PieData(Shrinkage shrinkage, PieConfig _pieConfig)
    {
        var pFC = shrinkage.ProcessFailure.Color;
        var iTC = shrinkage.InternalTheft.Color;
        var eTC = shrinkage.ExternalTheft.Color;
        var sFC = shrinkage.SupplierFraud.Color;

        PieDataset<int> dataset = new PieDataset<int>(new[]
        {
            shrinkage.ProcessFailure.Value,
            shrinkage.InternalTheft.Value,
            shrinkage.ExternalTheft.Value,
            shrinkage.SupplierFraud.Value
        });

        dataset.BackgroundColor = new[]
        {
            "#" + pFC.R.ToString("X2") + pFC.G.ToString("X2") + pFC.B.ToString("X2"),
            "#" + iTC.R.ToString("X2") + iTC.G.ToString("X2") + iTC.B.ToString("X2"),
            "#" + eTC.R.ToString("X2") + eTC.G.ToString("X2") + eTC.B.ToString("X2"),
            "#" + sFC.R.ToString("X2") + sFC.G.ToString("X2") + sFC.B.ToString("X2")
        };

        _pieConfig.Data.Datasets.Add(dataset);

        return _pieConfig;
    }

    public BarConfig BarSetup(NikeForm report)
    {
        try
        {
            var _barConfig = BarLabels();
            return BarData(_barConfig, report.ListQSet);
        }
        catch (Exception ex)
        {
            return new BarConfig();
        }
    }

    public BarConfig BarLabels()
    {
        var _barConfig = new BarConfig(horizontal: true);

        var options = new BarOptions();
        options.Responsive = true;
        options.Legend = new Legend() { Position = Position.Bottom };
        
        _barConfig.Options = options;

        return _barConfig;
    }

    public BarConfig BarData(BarConfig _barConfig, List<QSet> qSets)
    {
        foreach (var qSet in qSets)
        {
            var scorePercent = qSet.ScorePercent < 0 ? 0 : qSet.ScorePercent;
            var scorePercents = new List<int>() { scorePercent };

            IDataset<int> dataset = new BarDataset<int>(scorePercents, horizontal: true)
            {
                Label = qSet.Title,
                BackgroundColor = ColorUtil.FromDrawingColor(qSet.BarColor),
                BorderColor = ColorUtil.FromDrawingColor(qSet.BarColor),
                BorderWidth = 1
            };

            _barConfig.Data.Datasets.Add(dataset);

        }
        
        return _barConfig;
    }
}