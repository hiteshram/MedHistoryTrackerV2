using System.ComponentModel;

namespace MedHistoryTrackerV2.Data.Models;

public class LoginForm
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [PasswordPropertyText]
    public string? Password { get; set; }
}
