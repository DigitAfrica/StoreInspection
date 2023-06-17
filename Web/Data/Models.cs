﻿namespace Web.Data;

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

    public class Register
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? Surname { get; set; }
    }

    public class Notyf
    {
        public bool Win { get; set; } = false;
        public bool Fail { get; set; } = false;
        public string Msg { get; set; } = "";
    }
}

public class Reply
{
    public bool Win { get; set; } = false;
    public bool Fail { get; set; } = false;
    public string? Msg { get; set; }
    public string? Info { get; set; }
    public object? Data { get; set; }
}

public class MailObject
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Cc { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}