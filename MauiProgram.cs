namespace MauiMessenger;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "IconFontTypes");
            });

		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<ChatsPage>();
        builder.Services.AddSingleton<ChatPage>();
        builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<ChatsViewModel>();
		builder.Services.AddSingleton<ChatViewModel>();
		builder.Services.AddSingleton<ServiceProvider>();

		return builder.Build();
	}
}
