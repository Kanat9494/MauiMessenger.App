namespace MauiMessenger;

public partial class AppShell : Shell
{
	//public AppShell(LoginPage loginPage)
	//{
	//	InitializeComponent();

	//	Routing.RegisterRoute("ChatsPage", typeof(ChatsPage));

	//	this.CurrentItem = loginPage;
	//}

    public AppShell(ChatPage chatPage)
    {
        InitializeComponent();

        Routing.RegisterRoute("ChatsPage", typeof(ChatsPage));

        this.CurrentItem = chatPage;
    }
}
