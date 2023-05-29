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

    public string Form { get; set; } = "Nike";
    public List<QSet> ListQSet { get; set; }
    public Shrinkage Shrinkage { get; set; } = new Shrinkage();
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

        if (ScoreMax == 0)
        {
            ScorePercent = 100;
            return;
        }

        ScorePercent = (ScoreTotal * 100) / ScoreMax;
        var scoreRemainder = (ScoreTotal * 100) % ScoreMax;
        if (scoreRemainder != 0 && ScorePercent > 0) ScorePercent++;
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

        if (ScoreMax == 0)
        {
            ScorePercent = 100;
            return;
        }

        ScorePercent = (ScoreTotal * 100) / ScoreMax;
        var scoreRemainder = (ScoreTotal * 100) % ScoreMax;
        if (scoreRemainder != 0 && ScorePercent > 0) ScorePercent++;
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
    public Alert Alert { get; set; } = new Alert(new QLink("", "", ""));

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

public class Alert
{
    public Alert(QLink qL)
    {
        qLink = qL;
    }

    public QLink qLink { get; set; }

    [Required]
    public string Action { get; set; } = "";

    [Required]
    public string Person { get; set; } = "";

    [Required]
    public DateTime Due { get; set; } = DateTime.Now;

    public byte[]? bytes { get; set; }
}

public class QLink
{
    public QLink(string form, string set, string question)
    {
        Form = form;
        Set = set;
        Question = question;
    }

    public string Form { get; set; }
    public string Set { get; set; }
    public string Question { get; set; }
}

public class Shrinkage
{
    public IntString ProcessFailure { get; set; } = new IntString(0, "Process Failure");
    public IntString InternalTheft { get; set; } = new IntString(0, "Internal Theft");
    public IntString ExternalTheft { get; set; } = new IntString(0, "External heft");
    public IntString SupplierFraud { get; set; } = new IntString(0, "Supplier Fraud");
}

public class IntString
{
    public IntString(int value, string name) 
    { 
        Value = value;
        Name = name;
    }
    public int Value { get; set; }
    public string Name { get; set; }
}