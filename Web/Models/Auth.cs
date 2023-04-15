using System.ComponentModel.DataAnnotations;

namespace Models;

public class Login
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public void LoginAsAdmin()
    {
        Email = "abc@xyz.com";
        Password = "C@non123";
    } //todo - remove when publishing
}