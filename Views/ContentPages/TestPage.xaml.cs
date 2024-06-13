using Frontend_MindWave.ApplicationData;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Platform;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Frontend_MindWave.Views.ContentPages;

public partial class TestPage : ContentPage
{
    public TestPage()
    {
        InitializeComponent();

    }
    public TestPage(int TestId)
    {
        InitializeComponent();
        testId = TestId;
        titleGrid.IsVisible = true;
        titleGrid.IsEnabled = true;
        testGrid.IsVisible = false;
        testGrid.IsEnabled = false;

        checkedAnswersId.Clear();
        totalMark = 0;
        selectedQuestionId = 1;
        isAnswered = false;
        testAnswerId = 0;
    }

    public static int testId = 0;
    public static List<TestsView> testView = new List<TestsView>();
    public static List<int> checkedAnswersId = new List<int>();
    public static decimal totalMark = 0;
    public static int selectedQuestionId = 1;
    public async Task LoadTest(int testId)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}testsview/get/{testId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            testView = JsonConvert.DeserializeObject<TestsView[]>(content).ToList();
        }
    }
    public async Task LoadQuestion(int questionId)
    {
        loadingQuestionIndicator.IsRunning = true;

        nextQuestion.IsEnabled = false;
        answerCollectionView.IsEnabled = false;
        questionLabel.Text = testView.GroupBy(x => x.TestQuestionId).Select(x => x.First()).Where(x => x.TestQuestionId == questionId).Select(x => x.Question).FirstOrDefault();
        await Task.Delay(1000);
        answerCollectionView.IsEnabled = true;
        nextQuestion.IsEnabled = true;

        loadingQuestionIndicator.IsRunning = false;
    }
    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await LoadTest(testId);
        while (true)
        {
            if (testId != 0)
            {
                nameLabel.Text = testView.Select(x => x.Name).FirstOrDefault();
                descLabel.Text = testView.Select(x => x.Description).FirstOrDefault();
                break;
            }
        }
    }

    private async void startBtn_Clicked(object sender, EventArgs e)
    {
        titleGrid.IsVisible = false;
        titleGrid.IsEnabled = false;
        testGrid.IsVisible = true;
        testGrid.IsEnabled = true;
        await LoadQuestion(selectedQuestionId);
        answerCollectionView.ItemsSource = testView.Take(4);
    }

    private async void endTest_Clicked(object sender, EventArgs e)
    {
        checkedAnswersId.Add(testAnswerId);
        decimal percent;
        var questionsCount = testView.GroupBy(x => x.TestQuestionId).Select(x => x.First()).ToList().Count();
        if (checkedAnswersId.Count == questionsCount)
        {
            foreach (var item in checkedAnswersId)
            {
                var currentAnswer = testView.Where(x => x.TestAnswerId == item).Select(x => x.Mark).FirstOrDefault();
                totalMark = totalMark + currentAnswer;
            }
            percent = Math.Round(totalMark / questionsCount * 100, 0);

            //ВЫВОД РЕЗУЛЬТАТОВ И ПЕРЕМЕННЫХ
            //await DisplayAlert("result", $"percent: {percent}% \n totalMark: {totalMark}", "OK");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}usersresult/set/{App.enteredUser.UserId}/{testView.Select(x => x.CharacteristicId).FirstOrDefault()}/{Convert.ToInt32(percent)}");
            if (response.IsSuccessStatusCode)
            {
                App.userResutlChanged = true;
                await DisplayAlert("Ваши результаты записаны", "Вы можете перейти на страницу результатов для просмотра обновленной статистики", "ОК");
                await Navigation.PopModalAsync();

            }
            else
            {
                await DisplayAlert("Ошибка при учете результатов", "", "ОК");
                await Navigation.PopModalAsync();
            }
        }
        else
        {
            await DisplayAlert("Ответьте на все вопросы", "Пожалуйста, отметьте каждый вопрос одним указанным ответом", "ОК");
        }
    }

    //private async void previewQuestion_Clicked(object sender, EventArgs e)
    //{
    //    if (isAnswered == true)
    //    {
    //        if (selectedQuestionId == 1)
    //        {

    //        }
    //        else
    //        {
    //            selectedQuestionId--;
    //            await LoadQuestion(selectedQuestionId);
    //        }
    //    }
    //    else
    //    {
    //        await DisplayAlert("Ответьте на вопрос", "", "ОК");
    //    }
    //}

    private async void nextQuestion_Clicked(object sender, EventArgs e)
    {
        if (isAnswered == true)
        {
            if (selectedQuestionId == testView.GroupBy(x => x.TestQuestionId).Select(x => x.First()).Count())
            {
                nextQuestion.IsVisible = false;
            }
            else
            {
                selectedQuestionId++;
                checkedAnswersId.Add(testAnswerId);
                isAnswered = false;
                answerCollectionView.ItemsSource = testView.Take(4);
                await LoadQuestion(selectedQuestionId);
            }
        }
        else
        {
            await DisplayAlert("Ответьте на вопрос", "", "ОК");
        }
    }
    public static bool isAnswered = false;
    public static int testAnswerId;
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton radioButton = sender as RadioButton;
        if (radioButton.IsChecked == true)
        {
            testAnswerId = int.Parse(radioButton.AutomationId.ToString());
            isAnswered = true;
        }
    }
}