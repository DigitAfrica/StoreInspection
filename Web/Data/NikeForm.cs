using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace Web.Data;

public class NikeForm
{
    public NikeForm()
    {
        Color bc1 = System.Drawing.ColorTranslator.FromHtml("#DAE3F3");
        Color bc2 = System.Drawing.ColorTranslator.FromHtml("#4472C4");
        Color bc3 = System.Drawing.ColorTranslator.FromHtml("#8FAADC");
        Color bc4 = System.Drawing.ColorTranslator.FromHtml("#4472C4");

        var qInit = new QInit2.Nike();
        ListQSet = new List<QSet>
        {
            new QSet("Lodge Security", qInit.LodgeSecurity, bc1),
            new QSet("Physical Security", qInit.PhysicalSecurity, bc2),
            new QSet("Sales Floor", qInit.SalesFloor, bc3),
            new QSet("Storeroom", qInit.Storeroom, bc4)
        };
    }

    public FHeader FHeader { get; set; } = new FHeader();
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

public class FHeader
{
    [Required]
    public string Client { get; set; } = "Nike";

    [Required]
    public string Store { get; set; }
    
    [Required]
    public string AuditType { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public string PreviousScore { get; set; }
    
    [Required]
    public string CurrentShrinkage { get; set; }
    
    [Required]
    public string Address { get; set; }
}

public class QSet
{
    public QSet(string title, List<string> listQuestions, Color barColor)
    {
        Title = title;
        Questions = new List<Question>();
        BarColor = barColor;

        for (int i = 0; i < listQuestions.Count(); i++)
        {
            Questions.Add(new Question(i + 1, listQuestions[i]));
        }
    }

    public string Title { get; set; }
    public List<Question> Questions { get; set; }
    public Color BarColor { get; set; }
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
    public Shrinkage()
    {
        Color pc1 = System.Drawing.ColorTranslator.FromHtml("#335899");
        Color pc2 = System.Drawing.ColorTranslator.FromHtml("#3F6AB7");
        Color pc3 = System.Drawing.ColorTranslator.FromHtml("#7991CE");
        Color pc4 = System.Drawing.ColorTranslator.FromHtml("#B3BEDF");

        ProcessFailure = new Shrink(0, "Process Failure", pc1);
        InternalTheft = new Shrink(0, "Internal Theft", pc2);
        ExternalTheft = new Shrink(0, "External Theft", pc3);
        SupplierFraud = new Shrink(0, "Supplier Fraud", pc4);
    }

    public Shrink ProcessFailure { get; set; }
    public Shrink InternalTheft { get; set; }
    public Shrink ExternalTheft { get; set; }
    public Shrink SupplierFraud { get; set; }
}

public class Shrink
{
    public Shrink(int value, string name, System.Drawing.Color color) 
    { 
        Value = value;
        Name = name;
        Color = color;
    }
    public int Value { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
}