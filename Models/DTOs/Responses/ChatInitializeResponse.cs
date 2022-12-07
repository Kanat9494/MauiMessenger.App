namespace MauiMessenger.Models.DTOs.Responses;

public class ChatInitializeResponse : BaseResponse
{
    public ChatUser ChatUser { get; set; }
    public IEnumerable<ChatUser> UserFriends { get; set; }
    public IEnumerable<LastestMessage> LastestMessages { get; set; }
}
