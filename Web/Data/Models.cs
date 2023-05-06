using System.Text.RegularExpressions;

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
    public QLodge QLodge { get; set; } = new QLodge();

    public int Total { get; set; } = 0;

    public void Sum()
    {
        Total = 0;
        Total += QLodge.Total;
    }
}

public class QLodge
{
    public QLodge()
    {
        Questions = new List<Question>()
        {
            new Question(){Num = 1, Text = "Are all Security Officers wearing full uniform with their name badge?" },
            new Question(){Num = 2, Text = "Are Security Officers visible and vigilant whilst on duty at their post?" },
            new Question(){Num = 3, Text = "Are all Security Officers correctly trained for Nike Stores?" },
            new Question(){Num = 4, Text = "Are all radios, batteries and chargers in working order?"},
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

    [RegularExpression(@"^-?[0-2]", ErrorMessage = "Please select an option")]
    public int Score { get; set; } = -5;

    public Answer Answer { get; set; } = new Answer();
}

public class Answer
{
    public Answer()
    {
        Answers = new List<NameValue>()
        {
            new NameValue(){ Name = "Select an answer", Value = -5},
            new NameValue(){ Name = "Compliant", Value = 2},
            new NameValue(){ Name = "Non - Compliance", Value = -1},
            new NameValue(){ Name = "Not Applicable", Value = 0},
        };
    }

    public List<NameValue> Answers { get; set; }
}

public class NameValue
{
    public string Name { get; set; } = "";

    [RegularExpression(@"^-?[0-2]", ErrorMessage = "Please select an option")]
    public int Value { get; set; } = -5;
}