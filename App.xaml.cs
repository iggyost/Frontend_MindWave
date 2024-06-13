using Frontend_MindWave.ApplicationData;
using Frontend_MindWave.Views.ContentPages;

namespace Frontend_MindWave;

public partial class App : Application
{
    public static string conString = "http://192.168.0.10:45455/api/";
	public static User enteredUser;
	public static string enteredPhone;
	public static bool userResutlChanged = false;
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new WelcomePage());
		//MainPage = new AppShell();
	}
}
