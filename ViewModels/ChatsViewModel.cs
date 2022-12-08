﻿namespace MauiMessenger.ViewModels;

public class ChatsViewModel : INotifyPropertyChanged, IQueryAttributable
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ChatsViewModel()
    {
        UserFriends = new ObservableCollection<ChatUser>();
        LastestMessages = new ObservableCollection<LastestMessage>();
    }

    private ChatUser userInfo;
    private ObservableCollection<ChatUser> userFriends;
    private ObservableCollection<LastestMessage> lastestMessages;  

    async Task GetListFriends()
    {
        var response = await ServiceProvider.GetInstance().CallWebApi<int, ChatsInitializeResponse>("", HttpMethod.Post,
            UserInfo.UserId);

        if (response.StatusCode == 200)
        {
            UserInfo = response.CurrentUser;
            UserFriends = new ObservableCollection<ChatUser>(response.UserFriends);
            LastestMessages = new ObservableCollection<LastestMessage>(response.LastestMessages);
        }
        else
        {
            await AppShell.Current.DisplayAlert("MAUI Чат", response.StatusMessage, "Ок");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query == null || query.Count == 0) return;

        UserInfo.UserId = int.Parse(HttpUtility.UrlDecode(query[""].ToString()));

        Task.Run(async () =>
        {
            await GetListFriends();
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
}