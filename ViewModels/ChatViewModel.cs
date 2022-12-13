namespace MauiMessenger.ViewModels;

public class ChatViewModel : INotifyPropertyChanged, IQueryAttributable
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        throw new NotImplementedException();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ChatViewModel()
    {
        Messages = new ObservableCollection<string>();

        for (int i = 0; i < 20; i++)
            Messages.Add($"Message {i}");
    }

    private ObservableCollection<string> messages;

    public ObservableCollection<string> Messages
    {
        get => messages;
        set
        {
            messages = value;
            OnPropertyChanged();
        }
    }
}
