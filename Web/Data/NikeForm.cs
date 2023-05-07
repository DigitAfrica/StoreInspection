namespace Web.Data;

public class NikeForm
{
    public QLodgeSecurity QLodgeSecurity { get; set; } = new QLodgeSecurity();
    public QPhysicalSecurity QPhysicalSecurity { get; set; } = new QPhysicalSecurity();

    public int Total { get; set; } = 0;

    public void Sum()
    {
        Total = 0;
        Total += QLodgeSecurity.Total;
        Total += QPhysicalSecurity.Total;
    }
}

public class QLodgeSecurity
{
    public QLodgeSecurity()
    {
        Questions = new List<Question>();

        for (int i = 0; i < listQuestions.Count(); i++)
        {
            Questions.Add(new Question(i + 1, listQuestions[i]));
        }
    }

    public List<Question> Questions { get; set; }
    public int Total { get; set; } = 0;

    public List<string> listQuestions = new List<string>()
    {
        "Are all Security Officers wearing full uniform with their name badge?",
        "Are Security Officers visible and vigilant whilst on duty at their post?",
        "Are all Security Officers correctly trained for Nike Stores?",
        "Are all radios, batteries and chargers in working order?",
        "Is the security team making regular shoplifting preventions?",
        "Has the store been issued with relevant registers (OB Book, Visitors, Staff Purchases)?",
        "Are the Security Officers randomly polygraph tested every 6 months?",
        "Is the Security Officer present at the front door?",
        "Is the visitors register used and kept up-to-date at the staff entrance?",
        "Are security meetings held with the management team on a regular basis?",
        "Are Lodge Employees extra vigilant during high-risk times (CIT, opening & closing times)? Security posted outside the store at opening and closing times?",
        "Are staff purchases checked and recorded by the Security?",
        "Are staff items / apparel declared before entering the store - checked by Security?"
    };

    public void Sum()
    {
        Total = 0;
        foreach (var q in Questions)
        {
            Total += q.Score;
        }
    }

    public bool Validate()
    {
        foreach (var q in Questions)
        {
            if (q.Score == -5)
            {
                return false;
            }
        }

        return true;
    }
}

public class QPhysicalSecurity
{
    public QPhysicalSecurity()
    {
        Questions = new List<Question>();

        for (int i = 0; i < listQuestions.Count(); i++)
        {
            Questions.Add(new Question(i + 1, listQuestions[i]));
        }
    }

    public List<Question> Questions { get; set; }
    public int Total { get; set; } = 0;

    public List<string> listQuestions = new List<string>()
    {
        "Is the Alarm System working and used correctly daily?  Is the contact list up-to-date?",
        "Does the store have a panic button (connected to armed response, centre security)?",
        "Are the back door and the emergency doors alarmed when the store is open and trading?",
        "Is the perimeter security effective (check security gates, perimeter doors and fire escape)?",
        "Is the CCTV system working and recording correctly?",
        "Are all CCTV Cameras and devices working correctly?",
        "Is the cash office closed and only accessible by managers?"
    };

    public void Sum()
    {
        Total = 0;
        foreach (var q in Questions)
        {
            Total += q.Score;
        }
    }

    public bool Validate()
    {
        foreach (var q in Questions)
        {
            if (q.Score == -5)
            {
                return false;
            }
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

    public List<Response> Answers { get; set; }

    public Question(int num, string text)
    {
        Num = num;
        Text = text;
        Answers = new List<Response>()
        {
            new Response(-5, Text),
            new Response(2, "Compliant"),
            new Response(-1, "Non - Compliance"),
            new Response(0, "Not Applicable"),
        };
    }
}

public class Response
{
    public Response(int value, string name)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; } = "";

    [Required]
    [Range(typeof(int), "-2", "2", ErrorMessage = "Please select an option")]
    public int Value { get; set; }
}