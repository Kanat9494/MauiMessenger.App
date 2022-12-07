namespace MauiMessenger.Models;

public class LastestMessage
{
    public int MessageId { get; set; }
    public int ChatUserId { get; set; }
    public ChatUser UserFriendInfo { get; set; }
    public string Content { get; set; }
    public DateTime SentDateTime { get; set; }
    public bool IsRead { get; set; }
}
