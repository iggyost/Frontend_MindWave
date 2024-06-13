namespace Frontend_MindWave.Views.ContentPages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private void extiButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new WelcomePage();
        App.enteredUser = null;
        App.enteredPhone = null;
        App.userResutlChanged = false;
    }
}