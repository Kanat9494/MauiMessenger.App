namespace MauiMessenger.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected  virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public LoginViewModel()
    {
        PhoneNumber = "";
        Password = "";
        IsProcessing = false;

        LoginCommand = new Command(() =>
        {
            if (IsProcessing) return;

            if (string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password))
            {
                //AppShell.Current.DisplayAlert("MAUI Чат", "Пожалуйста введите логин и пароль", "");
                return;
            }

            IsProcessing = true;
            Login().GetAwaiter().OnCompleted(() =>
            {
                IsProcessing = false;
            });
        });
    }

    async Task Login()
    {
        try
        {
            var request = new AuthenticateRequest()
            {
                PhoneNumber = this.PhoneNumber,
                Password = this.Password,
            };

            var response = await ServiceProvider.GetInstance().Authenticate(request);
            if (response.StatusCode == 200)
            {
                await AppShell.Current.DisplayAlert("MAUI Чат", $"Вход успешно выполнен!\nЛогин: {response.PhoneNumber}" +
                    $"\nТокен: {response.Token}", "Ок");
            }
            else
            {
                await AppShell.Current.DisplayAlert("MAUI Чат", $"{response.StatusMessage}", "Ок");
            }
        }
        catch (Exception ex)
        {
            await AppShell.Current.DisplayAlert("MAUI Чат", ex.Message, "Ок");
        }
    }

    private string phoneNumber;
    private string password;
    private bool isProcessing;

    public string PhoneNumber
    {
        get => phoneNumber;
        set
        {
            phoneNumber = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => password;
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }

    public bool IsProcessing
    {
        get => isProcessing; 
        set
        {
            isProcessing = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; set; }
}
