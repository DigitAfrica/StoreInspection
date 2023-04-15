using System.ComponentModel.DataAnnotations;

namespace Models;

public class ResponseMessage
{
    public bool Success { get; set; } = true;
    public string? FullDescription { get; set; }
    public string? ShortDescription { get; set; }
    public string? Token { get; set; }
    public object? Object { get; set; }
}
