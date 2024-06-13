using Frontend_MindWave.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_MindWave.Views.ContentPages;

public partial class PasswordEnterPage : ContentPage
{
	public PasswordEnterPage()
	{
		InitializeComponent();
	}

    private async void passwordEnter_Completed(object sender, EventArgs e)
    {
        passwordBorder.IsEnabled = false;
        loadingIndicator.IsRunning = true;

		if (passwordEnter.Text.Length > 3)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync($"{App.conString}users/enter/{App.enteredPhone}/{passwordEnter.Text}");
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				App.enteredUser = JsonConvert.DeserializeObject<User>(content);
				Application.Current.MainPage = new AppShell();
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
                await DisplayAlert("Неверный пароль!", "", "ОК");
            }
		}
		else
		{
			await DisplayAlert("Длина пароля должна быть больше 3 символов", "", "ОК");
		}
		passwordBorder.IsEnabled = true;
        loadingIndicator.IsRunning = false;
    }
}