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

        var qInit = new QSetInitNew.NikeTest();
        ListQSet = new List<QSet>
        {
            new QSet("Lodge Security", qInit.LodgeSecurity, bc1),
            new QSet("Physical Security", qInit.PhysicalSecurity, bc2),
            new QSet("Sales Floor", qInit.SalesFloor, bc3),
            new QSet("Storeroom", qInit.Storeroom, bc4)
        };

        TotShrink = new();
    }

    public FHeader FHeader { get; set; } = new FHeader();
    public List<QSet> ListQSet { get; set; }
    public ShrinkageRename Shrinkage { get; set; } = new ShrinkageRename();
    public int ScoreTotal { get; set; } = 0;
    public int ScoreMax { get; set; } = 0;
    public int ScorePercent { get; set; } = 0;
    public Shrinkages TotShrink { get; set; }

    public void Sum()
    {
        foreach (var q in ListQSet)
        {
            q.Sum(); // sum question set
        }

        SumScore();
        SumShrinkage();
    }

    private void SumShrinkage()
    {
        TotShrink.ExternalTheft.ShrinkTotal = 0;
        TotShrink.ExternalTheft.ShrinkMax = 0;
        TotShrink.InternalTheft.ShrinkTotal = 0;
        TotShrink.InternalTheft.ShrinkMax = 0;
        TotShrink.ProcessFailure.ShrinkTotal = 0;
        TotShrink.ProcessFailure.ShrinkMax = 0;
        TotShrink.SupplierFraud.ShrinkTotal = 0;
        TotShrink.SupplierFraud.ShrinkMax = 0;

        foreach (var q in ListQSet)
        {
            TotShrink.ExternalTheft.ShrinkMax += q.TotShrink.ExternalTheft.ShrinkMax;
            TotShrink.InternalTheft.ShrinkMax += q.TotShrink.InternalTheft.ShrinkMax;
            TotShrink.ProcessFailure.ShrinkMax += q.TotShrink.ProcessFailure.ShrinkMax;
            TotShrink.SupplierFraud.ShrinkMax += q.TotShrink.SupplierFraud.ShrinkMax;

            TotShrink.ExternalTheft.ShrinkTotal += q.TotShrink.ExternalTheft.ShrinkTotal;
            TotShrink.InternalTheft.ShrinkTotal += q.TotShrink.InternalTheft.ShrinkTotal;
            TotShrink.ProcessFailure.ShrinkTotal += q.TotShrink.ProcessFailure.ShrinkTotal;
            TotShrink.SupplierFraud.ShrinkTotal += q.TotShrink.SupplierFraud.ShrinkTotal;
        }

        Shrinkage.ExternalTheft.Value = Shrinkpercent(TotShrink.ExternalTheft);
        Shrinkage.InternalTheft.Value = Shrinkpercent(TotShrink.InternalTheft);
        Shrinkage.ProcessFailure.Value = Shrinkpercent(TotShrink.ProcessFailure);
        Shrinkage.SupplierFraud.Value = Shrinkpercent(TotShrink.SupplierFraud);

        //TotShrink.ExternalTheft.ShrinkPercent = Shrinkpercent(TotShrink.ExternalTheft);
        //TotShrink.InternalTheft.ShrinkPercent = Shrinkpercent(TotShrink.InternalTheft);
        //TotShrink.ProcessFailure.ShrinkPercent = Shrinkpercent(TotShrink.ProcessFailure);
        //TotShrink.SupplierFraud.ShrinkPercent = Shrinkpercent(TotShrink.SupplierFraud);
    }

    private int Shrinkpercent(Shrinkage shrinkage)
    {
        if (shrinkage == null) return 0;
        if (shrinkage.ShrinkMax ==  0) return 100;

        var shrinkPercent = (shrinkage.ShrinkTotal * 100) / shrinkage.ShrinkMax;
        var shrinkRemainder = (shrinkage.ShrinkTotal * 100) % shrinkage.ShrinkMax;
        if (shrinkRemainder != 0 && shrinkPercent > 0) shrinkPercent++;

        return shrinkPercent;
    }

    private void SumScore()
    {
        ScoreTotal = 0;
        ScoreMax = 0;

        foreach (var q in ListQSet)
        {
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
    public QSet(string title, List<QInit> listQInit, Color barColor)
    {
        Title = title;
        Questions = new List<Question>();
        TotShrink = new();
        BarColor = barColor;

        foreach (var qInit in listQInit)
        {
            Questions.Add(new Question(qInit));
        }
    }

    public string Title { get; set; }
    public List<Question> Questions { get; set; }
    public Color BarColor { get; set; }
    public int ScoreTotal { get; set; } = 0;
    public int ScoreMax { get; set; } = 0;
    public int ScorePercent { get; set; } = 0;
    public Shrinkages TotShrink { get; set; }

    public void Sum()
    {
        SumScore();
        SumShrinkage();
    }

    private void SumShrinkage()
    {
        TotShrink.ExternalTheft.ShrinkTotal = 0;
        TotShrink.ExternalTheft.ShrinkMax = 0;
        TotShrink.InternalTheft.ShrinkTotal = 0;
        TotShrink.InternalTheft.ShrinkMax = 0;
        TotShrink.ProcessFailure.ShrinkTotal = 0;
        TotShrink.ProcessFailure.ShrinkMax = 0;
        TotShrink.SupplierFraud.ShrinkTotal = 0;
        TotShrink.SupplierFraud.ShrinkMax = 0;

        foreach (var q in Questions)
        {
            if (q.Score != 0)
            {
                TotShrink.ExternalTheft.ShrinkMax += q.QInit.ExternalTheft;
                TotShrink.InternalTheft.ShrinkMax += q.QInit.InternalTheft;
                TotShrink.ProcessFailure.ShrinkMax += q.QInit.ProcessFailure;
                TotShrink.SupplierFraud.ShrinkMax += q.QInit.SupplierFraud;
            } // Na questions are not counted. only compliance and non compliance

            if (q.Score < 0)
            {
                TotShrink.ExternalTheft.ShrinkTotal += q.QInit.ExternalTheft;
                TotShrink.InternalTheft.ShrinkTotal += q.QInit.InternalTheft;
                TotShrink.ProcessFailure.ShrinkTotal += q.QInit.ProcessFailure;
                TotShrink.SupplierFraud.ShrinkTotal += q.QInit.SupplierFraud;
            } // Na questions are not counted. only compliance and non compliance
        }
    }

    private void SumScore()
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
    public QInit QInit { get; set; }

    [Required]
    //[RegularExpression(@"^-?[0-2]", ErrorMessage = "Please select an option")]
    [Range(typeof(int), "-2", "2", ErrorMessage = "Please select an option")]
    public int Score { get; set; } = -5;

    public int MaxPossibleValue { get; set; } = 2;

    public List<NameValue> Answers { get; set; }
    public Alert Alert { get; set; } = new Alert(new QLink("", "", ""));

    public Question(QInit qInit)
    {
        QInit = qInit;

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

public class ShrinkageRename
{
    public ShrinkageRename()
    {
        Color pc1 = System.Drawing.ColorTranslator.FromHtml("#335899");
        Color pc2 = System.Drawing.ColorTranslator.FromHtml("#3F6AB7");
        Color pc3 = System.Drawing.ColorTranslator.FromHtml("#7991CE");
        Color pc4 = System.Drawing.ColorTranslator.FromHtml("#B3BEDF");

        ProcessFailure = new ShrinkRename(0, "Process Failure", pc1);
        InternalTheft = new ShrinkRename(0, "Internal Theft", pc2);
        ExternalTheft = new ShrinkRename(0, "External Theft", pc3);
        SupplierFraud = new ShrinkRename(0, "Supplier Fraud", pc4);
    }

    public ShrinkRename ProcessFailure { get; set; }
    public ShrinkRename InternalTheft { get; set; }
    public ShrinkRename ExternalTheft { get; set; }
    public ShrinkRename SupplierFraud { get; set; }
}

public class ShrinkRename
{
    public ShrinkRename(int value, string name, System.Drawing.Color color) 
    { 
        Value = value;
        Name = name;
        Color = color;
    }
    public int Value { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
}