namespace MauiMessenger.Models.DTOs.Responses;

public class MessageInitializeResponse : BaseResponse
{
    public ChatUser FriendInfo { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}
