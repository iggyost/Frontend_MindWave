using Frontend_MindWave.ApplicationData;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace Frontend_MindWave.Views.ContentPages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }
    public async Task LoadCollection(int categoryId, CollectionView collectionView, ActivityIndicator activityIndicator)
    {
        activityIndicator.IsRunning = true;

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}tests/get/{categoryId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Test[]>(content);
            collectionView.ItemsSource = data.ToList();
        }
        activityIndicator.IsRunning = false;
    }
    public async Task TestOpen(int testId)
    {
        await Navigation.PushModalAsync(new TestPage(testId));
    }
    //private async void topCollectionBorder_Tapped(object sender, TappedEventArgs e)
    //{
    //    Border border = sender as Border;
    //    int testId = int.Parse(border.AutomationId.ToString());
    //    await TestOpen(testId);
    //}

    //private async void centerCollectionBorder_Tapped(object sender, TappedEventArgs e)
    //{
    //    Border border = sender as Border;
    //    int testId = int.Parse(border.AutomationId.ToString());
    //    await TestOpen(testId);
    //}

    //private async void bottomCollectionBorder_Tapped(object sender, TappedEventArgs e)
    //{
    //    Border border = sender as Border;
    //    int testId = int.Parse(border.AutomationId.ToString());
    //    await TestOpen(testId);
    //}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await LoadCollection(1, topCollectionView, topLoadingIndicator);
        await LoadCollection(2, centerCollectionView, centerLoadingIndicator);
        await LoadCollection(3, bottomCollectionView, bottomLoadingIndicator);
    }

    //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    //{
    //    Border border = sender as Border;
    //    int testId = int.Parse(border.AutomationId.ToString());
    //    TestOpen(testId);
    //}

    //private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    //{
    //    Border border = sender as Border;
    //    int testId = int.Parse(border.AutomationId.ToString());
    //    TestOpen(testId);
    //}

    //private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    //{
    //    Border border = sender as Border;
    //    int testId = int.Parse(border.AutomationId.ToString());
    //    TestOpen(testId);
    //}

    private void topCollectionBorder_Tapped(object sender, EventArgs e)
    {
        Border border = sender as Border;
        int testId = int.Parse(border.AutomationId.ToString());
        TestOpen(testId);
    }

    private void centerCollectionBorder_Tapped(object sender, EventArgs e)
    {
        Border border = sender as Border;
        int testId = int.Parse(border.AutomationId.ToString());
        TestOpen(testId);
    }

    private void bottomCollectionBorder_Tapped(object sender, EventArgs e)
    {
        Border border = sender as Border;
        int testId = int.Parse(border.AutomationId.ToString());
        TestOpen(testId);
    }
}