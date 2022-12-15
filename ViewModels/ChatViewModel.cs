namespace MauiMessenger.ViewModels;

public class ChatViewModel : INotifyPropertyChanged, IQueryAttributable
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query == null || query.Count == 0) return;

        FromUserId = int.Parse(HttpUtility.UrlDecode(query["fromUserId"].ToString()));
        ToUserId = int.Parse(HttpUtility.UrlDecode(query["toUserId"].ToString()));

        Task.Run(async () =>
        {
            await GetMessagesAsync();
        }).GetAwaiter().OnCompleted(() =>
        {

        });
    }

    private ChatHub _chatHub;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ChatViewModel(ChatHub chatHub)   
    {
        Messages = new ObservableCollection<Message>();
        _chatHub = chatHub;
        _chatHub.AddReceivedMessageHandler(OnReceiveMessage);
        _chatHub.Connect();

        SendMessageCommand = new Command(async () =>
        {
            try
            {
                if (Message.Trim() != "")
                {
                    await _chatHub.SendMessageToUser(FromUserId, ToUserId, Message);

                    Messages.Add(new Models.Message
                    {
                        Content = Message,
                        FromUserId = FromUserId,
                        ToUserId = this.ToUserId,
                        SentDateTime = DateTime.Now,
                    });

                    Message = "";
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Чат на MAUI", ex.Message, "Ок");
            }
        });
    }

    async Task GetMessagesAsync()
    {
        var request = new MessageInitializeRequest()
        {
            FromUserId = this.FromUserId,
            ToUserId = this.ToUserId
        };

        var response = await ServiceProvider.GetInstance().CallWebApi<MessageInitializeRequest, MessageInitializeResponse>
            ("/api/Message/Initialize", HttpMethod.Post, request);

        if (response.StatusCode == 200)
        {
            FriendInfo = response.FriendInfo;
            Messages = new ObservableCollection<Message>(response.Messages);
        }
        else
        {
            await AppShell.Current.DisplayAlert("Чат на MAUI", response.StatusMessage, "Ок");
        }
    }

    public void Initialize()
    {
        IsRefreshing = false;
        Task.Run(async () =>
        {
            IsRefreshing = true;
            await GetMessagesAsync();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsRefreshing = false; 
        });
    }

    private void OnReceiveMessage(int fromuserId, string message)
    {
        Messages.Add(new Models.Message
        {
            Content = message,
            FromUserId = ToUserId,
            ToUserId = FromUserId,
            SentDateTime = DateTime.Now
        });
    }

    private int fromUserId;
    private int toUserId;
    private ChatUser friendInfo;
    private ObservableCollection<Message> messages;
    private bool isRefreshing;
    private string message;

    public int FromUserId
    {
        get => fromUserId;
        set
        {
            fromUserId = value;
            OnPropertyChanged();
        }
    }

    public int ToUserId
    {
        get => toUserId;
        set
        {
            toUserId = value;
            OnPropertyChanged();
        }
    }

    public ChatUser FriendInfo
    {
        get => friendInfo;
        set
        {
            friendInfo = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Message> Messages
    {
        get => messages;
        set
        {
            messages = value;
            OnPropertyChanged();
        }
    }

    public bool IsRefreshing
    {
        get => isRefreshing;
        set
        {
            isRefreshing = value;
            OnPropertyChanged();
        }
    }

    public ICommand SendMessageCommand { get; set; }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }
}
