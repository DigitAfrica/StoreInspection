using Newtonsoft.Json.Linq;
using System.Linq;
using System.Xml.Linq;

namespace Web.Data;

public class NikeForm
{
    public NikeForm()
    {
        var qInit = new QInit.Nike();
        ListQSet = new List<QSet>
        {
            new QSet("Lodge Security", qInit.LodgeSecurity),
            new QSet("Physical Security", qInit.PhysicalSecurity),
            new QSet("Sales Floor", qInit.SalesFloor),
            new QSet("Storeroom", qInit.Storeroom)
        };
    }

    public List<QSet> ListQSet { get; set; }
    public int ScoreTotal { get; set; } = 0;
    public int ScoreMax { get; set; } = 0;
    public int ScorePercent { get; set; } = 0;

    public void Sum()
    {
        ScoreTotal = 0;
        ScoreMax = 0;

        foreach (var q in ListQSet)
        {
            q.Sum(); // sum question set
            ScoreTotal += q.ScoreTotal; // add setTotal to grandtotal
            ScoreMax += q.ScoreMax; // Add setMaxScore to grandMaxScore
        }

        ScorePercent = (ScoreTotal * 100) / ScoreMax;
        if ((ScoreTotal * 100) % ScoreMax != 0) ScorePercent++;
    }
}

public class QSet
{
    public QSet(string title, List<string> listQuestions)
    {
        Title = title;
        Questions = new List<Question>();

        for (int i = 0; i < listQuestions.Count(); i++)
        {
            Questions.Add(new Question(i + 1, listQuestions[i]));
        }
    }

    public string Title { get; set; }
    public List<Question> Questions { get; set; }
    public int ScoreTotal { get; set; } = 0;
    public int ScoreMax { get; set; } = 0;
    public int ScorePercent { get; set; } = 0;

    public void Sum()
    {
        ScoreTotal = 0;
        ScoreMax = 0;
        foreach (var q in Questions)
        {
            if (q.Score != 0)
            {
                ScoreTotal += q.Score;
                ScoreMax += q.MaxPossibleValue;
            } // Na questions are not counted. only compliance and non compliance
        }

        ScorePercent = (ScoreTotal * 100) / ScoreMax;
        if ((ScoreTotal * 100) % ScoreMax != 0) ScorePercent++;
    }

    public bool Validate()
    {
        foreach (var q in Questions)
        {
            if (q.Score == -5) return false;
        }
        return true;
    }
}

public class Question
{
    public int Num { get; set; } = 0;
    public string Text { get; set; } = "";

    [Required]
    //[RegularExpression(@"^-?[0-2]", ErrorMessage = "Please select an option")]
    [Range(typeof(int), "-2", "2", ErrorMessage = "Please select an option")]
    public int Score { get; set; } = -5;

    public int MaxPossibleValue { get; set; } = 2;

    public List<NameValue> Answers { get; set; }

    public Question(int num, string text)
    {
        Num = num;
        Text = text;
        Answers = new List<NameValue>()
        {
            new NameValue(-5, "Please select"),
            new NameValue(2, "Compliant"),
            new NameValue(-1, "Non - Compliance"),
            new NameValue(0, "Not Applicable"),
        };
    }
}

public class NameValue
{
    public NameValue(int value, string name)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; } = "";

    [Required]
    [Range(typeof(int), "-2", "2", ErrorMessage = "Please select an option")]
    public int Value { get; set; }
}