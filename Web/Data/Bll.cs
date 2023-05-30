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
            ColorUtil.ColorHexString(Color.Red.R, Color.Red.G, Color.Red.B),
            ColorUtil.ColorHexString(Color.Purple.R, Color.Purple.G, Color.Purple.B),
            ColorUtil.ColorHexString(Color.Blue.R, Color.Blue.G, Color.Blue.B),
            ColorUtil.ColorHexString(Color.Orange.R, Color.Orange.G, Color.Orange.B)
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
        //options.Title = new OptionsTitle()
        //{
        //    Display = true,
        //    Text = "ChartJs.Blazor Horizontal Bar Chart"
        //};

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
                BackgroundColor = ColorUtil.FromDrawingColor(qSet.Color),
                BorderColor = ColorUtil.FromDrawingColor(qSet.Color),
                BorderWidth = 1
            };

            _barConfig.Data.Datasets.Add(dataset);

        }
        
        //_barConfig.Data.Labels.Add("dataset 1");
        return _barConfig;
    }

    public BarConfig BarData(BarConfig _barConfig)
    {
        int InitalCount = 7;

        IDataset<int> dataset1 = new BarDataset<int>(RandomScalingFactor(InitalCount), horizontal: true)
        {
            Label = "Dataset 1",
            BackgroundColor = ColorUtil.FromDrawingColor(Color.Red),
            BorderColor = ColorUtil.FromDrawingColor(Color.Red),
            BorderWidth = 1
        };

        IDataset<int> dataset2 = new BarDataset<int>(RandomScalingFactor(InitalCount), horizontal: true)
        {
            Label = "Dataset 2",
            BackgroundColor = ColorUtil.FromDrawingColor(Color.Blue),
            BorderColor = ColorUtil.FromDrawingColor(Color.Blue),
            BorderWidth = 1
        };

        //_barConfig.Data.Labels.Add("dataset 1");
        _barConfig.Data.Datasets.Add(dataset1);
        _barConfig.Data.Datasets.Add(dataset2);

        return _barConfig;
    }

    private static int RandomScalingFactorThreadUnsafe(Random _rng) => _rng.Next(0, 100);

    public static int RandomScalingFactor()
    {
        Random _rng = new Random();
        lock (_rng)
        {
            return RandomScalingFactorThreadUnsafe(_rng);
        }
    }

    public static IEnumerable<int> RandomScalingFactor(int count)
    {
        Random _rng = new Random();
        int[] factors = new int[count];
        lock (_rng)
        {
            for (int i = 0; i < count; i++)
            {
                factors[i] = RandomScalingFactorThreadUnsafe(_rng);
            }
        }

        return factors;
    }
}