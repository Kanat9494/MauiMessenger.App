namespace MauiMessenger.Hubs;

public class ChatHub
{
    private readonly HubConnection hubConnection;
    private List<Action<int, string>> onReceiveMessageHandler;
    ServiceProvider _serviceProvider;

    public ChatHub()
    {
        var serverRootUrl = "http://localhost:5266";
        _serviceProvider = ServiceProvider.GetInstance();
        hubConnection = new HubConnectionBuilder().WithUrl(serverRootUrl + "/ChatHub", options =>
        {
            options.Headers.Add("ChatHubBearer", _serviceProvider._accessToken);
        }).Build();

        onReceiveMessageHandler = new List<Action<int, string>>();
        hubConnection.On<int, string>("ReceiveMessage", OnReceiveMessage);
    }

    public async Task Connect()
    {
        await hubConnection.StartAsync();
    }

    public async Task Disconnect()
    {
        await hubConnection.StopAsync();
    }

    public async Task SendMessageToUser(int fromUserId, int toUserId, string message)
    {
        await hubConnection.InvokeAsync("SendMessageToUser", fromUserId, toUserId, message); ;
    }

    public void AddReceivedMessageHandler(Action<int, string> handler)
    {
        onReceiveMessageHandler.Add(handler);
    }

    void OnReceiveMessage(int userId, string message)
    {
        foreach (var handler in onReceiveMessageHandler)
        {
            handler(userId, message);
        }
    }
}
