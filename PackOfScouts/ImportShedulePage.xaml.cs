using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PackOfScouts;

public partial class ImportSchedulePage : ContentPage
{
    private string? compid;

    public ImportSchedulePage()
    {
        InitializeComponent();

        Loaded += (s, e) => compIDTextBox.Focus();
    }

    private async void OnImportScheduleClicked(object sender, EventArgs e)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://frc-api.firstinspires.org/v3.0/2023/");

        // Set up request headers
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("akshaisrinivasan:e1bcd614-4086-4c40-9754-8448161d9f5e")));

        string downloadedData;
        try
        {
            downloadedData = await GetJsonAsync(httpClient, "schedule/" + compid + "?tournamentLevel=Qualification");
        }
        catch
        {
            await DisplayAlert("😢 Error 😢", $"Could not download the schedule for competition '{compid}'", "OK");
            return;
        }

        var schedule = ProcessDownloadedData(downloadedData);
        var text = JsonSerializer.Serialize(schedule);

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchSchedule.json");
        File.WriteAllText(path, text);

        Debug.WriteLine($"Data saved to '{path}' successfully!");

        await DisplayAlert("Schedule saved!", $"Schedule saved to\n{path}", "OK");
        _ = await Navigation.PopAsync();

    }

    static List<ScheduleEntry> ProcessDownloadedData(string downloadedData)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var dd = JsonSerializer.Deserialize<DownloadedData>(downloadedData, options)!;

        var l = new List<ScheduleEntry>();
        foreach (var match in dd.Schedule)
        {
            var s = new ScheduleEntry
            {
                MatchNumber = match.MatchNumber
            };

            foreach (var team in match.Teams)
            {
                switch (team.Station)
                {
                    case "Red1": s.RedRobot1 = team.TeamNumber; break;
                    case "Red2": s.RedRobot2 = team.TeamNumber; break;
                    case "Red3": s.RedRobot3 = team.TeamNumber; break;
                    case "Blue1": s.BlueRobot1 = team.TeamNumber; break;
                    case "Blue2": s.BlueRobot2 = team.TeamNumber; break;
                    case "Blue3": s.BlueRobot3 = team.TeamNumber; break;
                }
            }

            l.Add(s);
        }

        return l;
    }

    static async Task<string> GetJsonAsync(HttpClient httpClient, string path)
    {
        var response = await httpClient.GetAsync(path);
        _ = response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return json;
    }

    private void OnCompIDTextChanged(object sender, TextChangedEventArgs e)
    {
        compid = e.NewTextValue;
    }

    public class DownloadedData
    {
        public List<Match> Schedule { get; set; } = new();
    }

    public class Match
    {
        public string Description { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public int MatchNumber { get; set; }
        public string Field { get; set; } = string.Empty;
        public string TournamentLevel { get; set; } = string.Empty;
        public List<Team> Teams { get; set; } = new();
    }

    public class Team
    {
        public int TeamNumber { get; set; }
        public string Station { get; set; } = string.Empty;
        public bool Surrogate { get; set; }
    }
}
