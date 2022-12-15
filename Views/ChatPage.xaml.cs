namespace MauiMessenger.Views;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatViewModel chatViewModel)
	{
		InitializeComponent();

		this.BindingContext = chatViewModel;
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(this.BindingContext as ChatViewModel).Initialize();
	}
}