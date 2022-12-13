namespace MauiMessenger.Views;

public partial class ChatPage : ContentPage
{
	public ChatPage()
	{
		InitializeComponent();

		this.BindingContext = new ChatViewModel();
	}
}