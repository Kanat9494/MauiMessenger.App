namespace MauiMessenger.Models.DTOs.Responses;

public class AuthenticateResponse : BaseResponse
{
    public int UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
}
