namespace MauiMessenger;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("ChatsPage", typeof(ChatsPage));
	}
}
