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
                        levelDepressionLabel.Text = "Депрессия отсутствует";
                        depressionRecs.Text = "Нет рекомендаций";
                        break;
                    case var expression when (userResult > 1 && userResult < 20):
                        levelDepressionLabel.Text = "Низкая";
                        depressionRecs.Text = "Нет рекомендаций";
                        break;
                    case var expression when (userResult >= 20 && userResult < 40):
                        levelDepressionLabel.Text = "Ниже среднего";
                        depressionRecs.Text = "Старайтесь следовать здоровому образу жизни, включая питание, сон и отказ от вредных привычек.";
                        break;
                    case var expression when (userResult >= 40 && userResult < 61):
                        levelDepressionLabel.Text = "Средняя";
                        depressionRecs.Text = "Попробуйте практиковать медитацию и релаксацию для снижения уровня стресса и тревоги.";
                        break;
                    case var expression when (userResult >= 61 && userResult < 80):
                        levelDepressionLabel.Text = "Выше среднего";
                        depressionRecs.Text = "Найдите хобби или занятие, которое приносит вам удовольствие и помогает отвлечься от негативных мыслей.";
                        break;
                    case var expression when (userResult >= 80):
                        levelDepressionLabel.Text = "Высокая";
                        depressionRecs.Text = "Обратитесь к психологу или психиатру для профессиональной помощи и поддержки.";
                        break;
                }
            }
            else
            {
                levelDepressionLabel.Text = "Данные отсутствуют";
            }

            if (currentUserResult.Where(x => x.CharacteristicId == 2) != null)
            {
                var userResult = Convert.ToDouble(currentUserResult.Where(x => x.CharacteristicId == 2).Select(x => x.Rating).FirstOrDefault());
                await anxietyBar.ProgressTo(userResult / 100, 3, Easing.SinIn);
                anxietyValue = userResult.ToString();
                switch (userResult)
                {
                    case 0:
                        levelAnxietyLabel.Text = "Тревожность отсутствует";
                        anxietyRecs.Text = "Нет рекомендаций";
                        break;
                    case var expression when (userResult > 1 && userResult < 20):
                        levelAnxietyLabel.Text = "Низкая";
                        anxietyRecs.Text = "Нет рекомендаций";
                        break;
                    case var expression when (userResult >= 20 && userResult < 40):
                        levelAnxietyLabel.Text = "Ниже среднего";
                        anxietyRecs.Text = "Планируйте свой день заранее, чтобы избежать ситуаций, вызывающих тревогу и беспокойство.";
                        break;
                    case var expression when (userResult >= 40 && userResult < 61):
                        levelAnxietyLabel.Text = "Средняя";
                        anxietyRecs.Text = "Практикуйте регулярные методы релаксации, такие как глубокое дыхание, йога или медитация.";
                        break;
                    case var expression when (userResult >= 61 && userResult < 80):
                        levelAnxietyLabel.Text = "Выше среднего";
                        anxietyRecs.Text = "Изучите техники управления стрессом, такие как позитивное мышление, установка целей и принятие решений.";
                        break;
                    case var expression when (userResult >= 80):
                        levelAnxietyLabel.Text = "Высокая";
                        anxietyRecs.Text = "Обратитесь к психологу или психиатру для профессиональной помощи и поддержки.";
                        break;
                }
            }
            else
            {
                levelAnxietyLabel.Text = "Данные отсутствуют";
            }

            if (currentUserResult.Where(x => x.CharacteristicId == 3) != null)
            {
                var userResult = Convert.ToDouble(currentUserResult.Where(x => x.CharacteristicId == 3).Select(x => x.Rating).FirstOrDefault());
                await agressionBar.ProgressTo(userResult / 100, 3, Easing.SinIn);
                agressionValue = userResult.ToString();
                switch (userResult)
                {
                    case 0:
                        levelAgressionLabel.Text = "Агрессия отсутствует";
                        agressionRecs.Text = "Нет рекомендаций";
                        break;
                    case var expression when (userResult > 1 && userResult < 20):
                        levelAgressionLabel.Text = "Низкая";
                        agressionRecs.Text = "Нет рекомендаций";
                        break;
                    case var expression when (userResult >= 20 && userResult < 40):
                        levelAgressionLabel.Text = "Ниже среднего";
                        agressionRecs.Text = "Попытайтесь избегать ситуации, которые могут вызвать агрессию, и займитесь развитием стратегий уклонения от конфликтов.";
                        break;
                    case var expression when (userResult >= 40 && userResult < 61):
                        levelAgressionLabel.Text = "Средняя";
                        agressionRecs.Text = "Обучитесь навыкам управления эмоциями, таким как дыхательные упражнения, медитация или йога.";
                        break;
                    case var expression when (userResult >= 61 && userResult < 80):
                        levelAgressionLabel.Text = "Выше среднего";
                        agressionRecs.Text = "Займитесь поиском здоровых способов выражения своих чувств, таких как спорт или творческие занятия.";
                        break;
                    case var expression when (userResult >= 80):
                        levelAgressionLabel.Text = "Высокая";
                        agressionRecs.Text = "Обратитесь к психологу или психиатру для профессиональной помощи и поддержки.";
                        break;
                }
            }
            else
            {
                levelAgressionLabel.Text = "Данные отсутствуют";
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
                        Label = "Депрессия",
                        ValueLabel = depressionValue,
                        Color = SKColor.Parse("#008CFF")
                    },
                    new ChartEntry(float.Parse(anxietyValue))
                    {
                        Label = "Тревожность",
                        ValueLabel = anxietyValue,
                        Color = SKColor.Parse("#F69F3D")
                    },
                    new ChartEntry(float.Parse(agressionValue))
                    {
                        Label = "Агрессия",
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