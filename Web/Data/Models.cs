namespace Web.Data;

public class User
{
    public class Info
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? NameSurname
        { get { return Name + " " + Surname; } }

        public string? Token { get; set; }
    }

    public class Login
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public void LoginAsAdmin()
        {
            Email = "mkhan@lodgeservice.com";
            Password = "Password";
        } //todo - remove when publishing
    }
}

public class Reply
{
    public bool Win { get; set; } = true;
    public bool Fail { get; set; } = true;
    public string? Msg { get; set; }
    public string? Info { get; set; }
    public object? Data { get; set; }
}

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

public class QPhysicalSecurity
{
    public QPhysicalSecurity()
    {
        Questions = new List<Question>()
        {
            new Question(1, "Is the Alarm System working and used correctly daily?  Is the contact list up-to-date?"),
            new Question(2, "Does the store have a panic button (connected to armed response, centre security)?"),
            new Question(3, "Are the back door and the emergency doors alarmed when the store is open and trading?"),
            new Question(4, "Is the perimeter security effective (check security gates, perimeter doors and fire escape)?")
        };
    }

    public List<Question> Questions { get; set; }
    public int Total { get; set; } = 0;

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

public class QLodgeSecurity
{
    public QLodgeSecurity()
    {
        Questions = new List<Question>()
        {
            new Question(1, "Are all Security Officers wearing full uniform with their name badge?"),
            new Question(2, "Are Security Officers visible and vigilant whilst on duty at their post?"),
            new Question(3, "Are all Security Officers correctly trained for Nike Stores?"),
            new Question(4, "Are all radios, batteries and chargers in working order?")
        };
    }

    public List<Question> Questions { get; set; }
    public int Total { get; set; } = 0;

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