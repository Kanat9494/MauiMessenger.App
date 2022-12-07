
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
        UserFriends = new ObservableCollection<string>();
        LastestMessages = new ObservableCollection<string>();

        for (int i = 0; i < 15; i++)
        {
            UserFriends.Add(i.ToString());
            LastestMessages.Add(i.ToString());
        }
    }

    private ObservableCollection<string> userFriends;
    private ObservableCollection<string> lastestMessages;

    public ObservableCollection<string> UserFriends
    {
        get => userFriends;
        set
        {
            userFriends = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> LastestMessages
    {
        get => lastestMessages;
        set
        {
            lastestMessages = value;
            OnPropertyChanged();
        }
    }
}
