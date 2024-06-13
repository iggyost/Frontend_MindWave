using Frontend_MindWave.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_MindWave.Views.ContentPages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private async void passwordEnter_Completed(object sender, EventArgs e)
    {
        if (passwordEnter.Text.Length > 3)
        {
            if (nameEnter.Text.Length >= 1)
            {
                loadingIndicator.IsRunning = true;
                passwordBorder.IsEnabled = false;
                nameBorder.IsEnabled = false;

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync($"{App.conString}users/reg/{nameEnter.Text}/{App.enteredPhone}/{passwordEnter.Text}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    App.enteredUser = JsonConvert.DeserializeObject<User>(content);
                    Application.Current.MainPage = new AppShell();
                }
            }
            else
            {
                await DisplayAlert("Имя пользователя не может быть пустым", "", "ОК");
            }
        }
        else
        {
            await DisplayAlert("Длина пароля должна быть больше 3 символов", "", "ОК");
        }
        loadingIndicator.IsRunning = false;
        passwordBorder.IsEnabled = true;
        nameBorder.IsEnabled = true;
    }

    private void nameEnter_Completed(object sender, EventArgs e)
    {

    }
}