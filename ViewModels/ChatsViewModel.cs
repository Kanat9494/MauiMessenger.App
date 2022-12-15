namespace MauiMessenger.ViewModels;

public class ChatsViewModel : INotifyPropertyChanged, IQueryAttributable
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ChatsViewModel()
    {
        UserInfo = new ChatUser();
        UserFriends = new ObservableCollection<ChatUser>();
        LastestMessages = new ObservableCollection<LastestMessage>();

        RefreshCommand = new Command(() =>
        {
            Task.Run(async () =>
            {
                IsRefreshing = true;
                await GetListFriends();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });
        });

        OpenChatPageCommand = new Command<int>(async (param) =>
        {
            await Shell.Current.GoToAsync($"ChatPage?fromUserId={UserInfo.UserId}&toUserId={param}");
        });
    }

    private ChatUser userInfo;
    private ObservableCollection<ChatUser> userFriends;
    private ObservableCollection<LastestMessage> lastestMessages;
    private bool isRefreshing;

    async Task GetListFriends()
    {
        try
        {
            var response = await ServiceProvider.GetInstance().CallWebApi<int, ChatsInitializeResponse>("/api/Chat/Initialize", HttpMethod.Post,
            UserInfo.UserId);

            if (response.StatusCode == 200)
            {
                UserInfo = response.User;
                UserFriends = new ObservableCollection<ChatUser>(response.UserFriends);
                LastestMessages = new ObservableCollection<LastestMessage>(response.LastestMessages);
            }
            else
            {
                await AppShell.Current.DisplayAlert("MAUI Чат", response.StatusMessage, "Ок");
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void Initialize()
    {
        Task.Run(async () =>
        {
            IsRefreshing = true;
            await GetListFriends();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsRefreshing = false;
        });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query == null || query.Count == 0) return;

        UserInfo.UserId = int.Parse(HttpUtility.UrlDecode(query["userId"].ToString()));

        Task.Run(async () =>
        {
            IsRefreshing = true;
            await GetListFriends();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsRefreshing = false;
        });
    }

    public ObservableCollection<ChatUser> UserFriends
    {
        get => userFriends;
        set
        {
            userFriends = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<LastestMessage> LastestMessages
    {
        get => lastestMessages;
        set
        {
            lastestMessages = value;
            OnPropertyChanged();
        }
    }

    public ChatUser UserInfo
    {
        get => userInfo;
        set
        {
            userInfo = value;
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

    public ICommand RefreshCommand { get; set; }

    public ICommand OpenChatPageCommand { get; set; }
}
