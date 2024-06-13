namespace Frontend_MindWave.Views.ContentPages;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        welcomeStackLayout.Opacity = 0;
        await Task.Delay(1500);
        await welcomeStackLayout.FadeTo(1, 800);
        await Task.Delay(3000);
        await Navigation.PushAsync(new PhoneEnterPage());
    }
}