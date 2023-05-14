using Newtonsoft.Json.Linq;
using System.Linq;
using System.Xml.Linq;

namespace Web.Data;

public class QInit
{
    public List<string> LodgeSecurity = new List<string>()
    {
        "Are all Security Officers wearing full uniform with their name badge?",
        //"Are Security Officers visible and vigilant whilst on duty at their post?",
        //"Are all Security Officers correctly trained for Nike Stores?",
        //"Are all radios, batteries and chargers in working order?",
        //"Is the security team making regular shoplifting preventions?",
        //"Has the store been issued with relevant registers (OB Book, Visitors, Staff Purchases)?",
        //"Are the Security Officers randomly polygraph tested every 6 months?",
        //"Is the Security Officer present at the front door?",
        //"Is the visitors register used and kept up-to-date at the staff entrance?",
        //"Are security meetings held with the management team on a regular basis?",
        "Are Lodge Employees extra vigilant during high-risk times (CIT, opening & closing times)? Security posted outside the store at opening and closing times?",
        "Are staff purchases checked and recorded by the Security?",
        "Are staff items / apparel declared before entering the store - checked by Security?"
    };

    public List<string> PhysicalSecurity = new List<string>()
    {
        //"Is the Alarm System working and used correctly daily?  Is the contact list up-to-date?",
        //"Does the store have a panic button (connected to armed response, centre security)?",
        //"Are the back door and the emergency doors alarmed when the store is open and trading?",
        //"Is the perimeter security effective (check security gates, perimeter doors and fire escape)?",
        //"Is the CCTV system working and recording correctly?",
        //"Are all CCTV Cameras and devices working correctly?",
        "Is the cash office closed and only accessible by managers?"
    };

    public List<string> SalesFloor = new List<string>()
    {
        //"Are EAS gates (store entrance) working and tested daily?",
        //"Are EAS tags correctly applied to all relevant high-risk items?",
        //"Are EAS tags correctly removed / deactivated at the tills?",
        //"Are blind spots monitored / patrolled by Security & Staff?",
        //"Are fitting room checks (pilfering) conducted on a regular basis during trading hours?",
        //"Are pilfered items (tags / empty boxes / hangers) collected daily and recorded?",
        //"Are tills correctly manned and is the Head Coach or Assistant Coach visible?",
        "Is the sales floor neat and tidy (no clutter or boxes left in the aisle)?"
    };

    public List<string> Storeroom = new List<string>()
    {
        //"Are all fire exits and escape routes clear?",
        //"Is all wastage checked before disposal?",
        //"Is the receiving area neat and tidy (stock not left out in the open)?",
        //"Are deliveries double checked against the invoice before being processed?",
        //"Is the storeroom neat and tidy (no clutter or loose stock left in the storeroom)?",
        //"Is there any evidence of pilfering in the storeroom?",
        "Is the high-risk locker secured (unauthorised access restricted)?"
    };
}

public class NikeForm
{
    public NikeForm()
    {
        ListQSet = new List<QSet>
        {
            new QSet("Lodge Security", QInit.LodgeSecurity),
            new QSet("Physical Security", QInit.PhysicalSecurity),
            new QSet("Sales Floor", QInit.SalesFloor),
            new QSet("Storeroom", QInit.Storeroom)
        };
    }

    public List<QSet> ListQSet { get; set; }
    public QInit QInit { get; set; } = new QInit();
    public int Total { get; set; } = 0;

    public void Sum()
    {
        Total = 0;

        foreach (var q in ListQSet)
        {
            q.Sum();
            Total += q.ScoreTotal;
        }
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
        ScoreMax = 0;
        ScoreTotal = 0;
        foreach (var q in Questions)
        {
            if (q.Score != 0)
            {
                ScoreMax += q.MaxPossibleValue;
            } // Na questions are not counted. only compliance and non compliance

            ScoreTotal += q.Score;
        }

        ScorePercent = ScoreTotal * 100 / ScoreMax;
        if (ScoreTotal % ScoreMax != 0) ScorePercent++;
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
            new NameValue(-5, Text),
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