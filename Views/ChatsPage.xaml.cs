namespace MauiMessenger.Views;

public partial class ChatsPage : ContentPage
{
	public ChatsPage()
	{
		InitializeComponent();

		this.BindingContext = new ChatsViewModel();
	}

    //private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    //{
    //    (this.BindingContext as ChatsViewModel).Initialize();
    //}
}