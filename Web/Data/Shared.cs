using System.ComponentModel.DataAnnotations;

namespace Web.Data;

public class Reply
{
    public bool Win { get; set; } = true;
    public bool Fail { get; set; } = true;
    public string? Msg { get; set; }
    public string? Info { get; set; }
    public object? Data { get; set; }
}
