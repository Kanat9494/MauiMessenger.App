namespace MauiMessenger.Models.DTOs.Responses;

public class ChatsInitializeResponse : BaseResponse
{
    public ChatUser CurrentUser { get; set; }
    public IEnumerable<ChatUser> UserFriends { get; set; }
    public IEnumerable<LastestMessage> LastestMessages { get; set; }
}
