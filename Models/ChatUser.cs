namespace MauiMessenger.Models;

public class ChatUser
{
    public int UserId { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string AvatarSourceName { get; set; } = null!;
    public bool IsOnline { get; set; }
    public DateTime LastLogonTime { get; set; }
    public string AwayDuration { get; set; } = null!;
}
