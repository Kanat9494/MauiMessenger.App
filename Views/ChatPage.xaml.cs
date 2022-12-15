namespace MauiMessenger.Views;

public partial class ChatPage : ContentPage
{
	public ChatPage()
	{
		InitializeComponent();

		this.BindingContext = new ChatViewModel();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(this.BindingContext as ChatViewModel).Initialize();
	}
}