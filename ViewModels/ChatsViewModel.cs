
namespace MauiMessenger.ViewModels;

public class ChatsViewModel : INotifyPropertyChanged
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
