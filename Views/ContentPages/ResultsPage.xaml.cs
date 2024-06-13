using Frontend_MindWave.ApplicationData;
using Microcharts;
using Microcharts.Maui;
using Newtonsoft.Json;
using SkiaSharp;
using System.Linq.Expressions;

namespace Frontend_MindWave.Views.ContentPages;

public partial class ResultsPage : ContentPage
{
    public ResultsPage()
    {
        InitializeComponent();
    }
    public static List<UsersResult> currentUserResult = new List<UsersResult>();
    public static string depressionValue = null;
    public static string anxietyValue = null;
    public static string agressionValue = null;
    public async Task UpdateUserResult()
    {
        loadingIndicator.IsRunning = true;

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}usersresult/get/{App.enteredUser.UserId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            currentUserResult = JsonConvert.DeserializeObject<UsersResult[]>(content).ToList();
            if (currentUserResult.Where(x => x.CharacteristicId == 1) != null)
            {
                var userResult = Convert.ToDouble(currentUserResult.Where(x => x.CharacteristicId == 1).Select(x => x.Rating).FirstOrDefault());
                await depressionBar.ProgressTo(userResult / 100, 3, Easing.SinIn);
                depressionValue = userResult.ToString();
                switch (userResult)
                {
                    case 0:
                        levelDepressionLabel.Text = "��������� �����������";
                        depressionRecs.Text = "��� ������������";
                        break;
                    case var expression when (userResult > 1 && userResult < 20):
                        levelDepressionLabel.Text = "������";
                        depressionRecs.Text = "��� ������������";
                        break;
                    case var expression when (userResult >= 20 && userResult < 40):
                        levelDepressionLabel.Text = "���� ��������";
                        depressionRecs.Text = "���������� ��������� ��������� ������ �����, ������� �������, ��� � ����� �� ������� ��������.";
                        break;
                    case var expression when (userResult >= 40 && userResult < 61):
                        levelDepressionLabel.Text = "�������";
                        depressionRecs.Text = "���������� ������������ ��������� � ���������� ��� �������� ������ ������� � �������.";
                        break;
                    case var expression when (userResult >= 61 && userResult < 80):
                        levelDepressionLabel.Text = "���� ��������";
                        depressionRecs.Text = "������� ����� ��� �������, ������� �������� ��� ������������ � �������� ��������� �� ���������� ������.";
                        break;
                    case var expression when (userResult >= 80):
                        levelDepressionLabel.Text = "�������";
                        depressionRecs.Text = "���������� � ��������� ��� ��������� ��� ���������������� ������ � ���������.";
                        break;
                }
            }
            else
            {
                levelDepressionLabel.Text = "������ �����������";
            }

            if (currentUserResult.Where(x => x.CharacteristicId == 2) != null)
            {
                var userResult = Convert.ToDouble(currentUserResult.Where(x => x.CharacteristicId == 2).Select(x => x.Rating).FirstOrDefault());
                await anxietyBar.ProgressTo(userResult / 100, 3, Easing.SinIn);
                anxietyValue = userResult.ToString();
                switch (userResult)
                {
                    case 0:
                        levelAnxietyLabel.Text = "����������� �����������";
                        anxietyRecs.Text = "��� ������������";
                        break;
                    case var expression when (userResult > 1 && userResult < 20):
                        levelAnxietyLabel.Text = "������";
                        anxietyRecs.Text = "��� ������������";
                        break;
                    case var expression when (userResult >= 20 && userResult < 40):
                        levelAnxietyLabel.Text = "���� ��������";
                        anxietyRecs.Text = "���������� ���� ���� �������, ����� �������� ��������, ���������� ������� � ������������.";
                        break;
                    case var expression when (userResult >= 40 && userResult < 61):
                        levelAnxietyLabel.Text = "�������";
                        anxietyRecs.Text = "����������� ���������� ������ ����������, ����� ��� �������� �������, ���� ��� ���������.";
                        break;
                    case var expression when (userResult >= 61 && userResult < 80):
                        levelAnxietyLabel.Text = "���� ��������";
                        anxietyRecs.Text = "������� ������� ���������� ��������, ����� ��� ���������� ��������, ��������� ����� � �������� �������.";
                        break;
                    case var expression when (userResult >= 80):
                        levelAnxietyLabel.Text = "�������";
                        anxietyRecs.Text = "���������� � ��������� ��� ��������� ��� ���������������� ������ � ���������.";
                        break;
                }
            }
            else
            {
                levelAnxietyLabel.Text = "������ �����������";
            }

            if (currentUserResult.Where(x => x.CharacteristicId == 3) != null)
            {
                var userResult = Convert.ToDouble(currentUserResult.Where(x => x.CharacteristicId == 3).Select(x => x.Rating).FirstOrDefault());
                await agressionBar.ProgressTo(userResult / 100, 3, Easing.SinIn);
                agressionValue = userResult.ToString();
                switch (userResult)
                {
                    case 0:
                        levelAgressionLabel.Text = "�������� �����������";
                        agressionRecs.Text = "��� ������������";
                        break;
                    case var expression when (userResult > 1 && userResult < 20):
                        levelAgressionLabel.Text = "������";
                        agressionRecs.Text = "��� ������������";
                        break;
                    case var expression when (userResult >= 20 && userResult < 40):
                        levelAgressionLabel.Text = "���� ��������";
                        agressionRecs.Text = "����������� �������� ��������, ������� ����� ������� ��������, � ��������� ��������� ��������� ��������� �� ����������.";
                        break;
                    case var expression when (userResult >= 40 && userResult < 61):
                        levelAgressionLabel.Text = "�������";
                        agressionRecs.Text = "��������� ������� ���������� ��������, ����� ��� ����������� ����������, ��������� ��� ����.";
                        break;
                    case var expression when (userResult >= 61 && userResult < 80):
                        levelAgressionLabel.Text = "���� ��������";
                        agressionRecs.Text = "��������� ������� �������� �������� ��������� ����� ������, ����� ��� ����� ��� ���������� �������.";
                        break;
                    case var expression when (userResult >= 80):
                        levelAgressionLabel.Text = "�������";
                        agressionRecs.Text = "���������� � ��������� ��� ��������� ��� ���������������� ������ � ���������.";
                        break;
                }
            }
            else
            {
                levelAgressionLabel.Text = "������ �����������";
            }
        }
        loadingIndicator.IsRunning = false;
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        App.userResutlChanged = true;
        while (true)
        {
            if (App.userResutlChanged == true)
            {
                await UpdateUserResult();
                ChartEntry[] chartEntry = new[]
                {
                    new ChartEntry(float.Parse(depressionValue))
                    {
                        Label = "���������",
                        ValueLabel = depressionValue,
                        Color = SKColor.Parse("#008CFF")
                    },
                    new ChartEntry(float.Parse(anxietyValue))
                    {
                        Label = "�����������",
                        ValueLabel = anxietyValue,
                        Color = SKColor.Parse("#F69F3D")
                    },
                    new ChartEntry(float.Parse(agressionValue))
                    {
                        Label = "��������",
                        ValueLabel = agressionValue,
                        Color = SKColor.Parse("#FF5D81")
                    },
                };
                RadarChart radarChart = new RadarChart()
                {
                    Entries = chartEntry
                };
                radarChart.BackgroundColor = SKColor.Parse("00FFFFFF");
                radarChart.LabelTextSize = 32;
                radarChart.MaxValue = 100;
                mainChart.Chart = radarChart;

                App.userResutlChanged = false;
            }
            else
            {

            }
            await Task.Delay(2000);
        }
    }
}