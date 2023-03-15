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

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnDownloadClicked(object sender, EventArgs e)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://frc-api.firstinspires.org/v3.0/2023/");

        // Set up request headers
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("akshaisrinivasan:e1bcd614-4086-4c40-9754-8448161d9f5e")));

        string downloadedScheduleData;
        try
        {
            downloadedScheduleData = await GetJsonAsync(httpClient, "schedule/" + compid + "?tournamentLevel=Qualification");
        }
        catch
        {
            await DisplayAlert("😢 Error 😢", $"Could not download the schedule for competition '{compid}'", "OK");
            return;
        }

        var schedule = ProcessDownloadedMatchData(downloadedScheduleData);
        var text = JsonSerializer.Serialize(schedule);

        var matchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchSchedule.json");
        File.WriteAllText(matchPath, text);

        Debug.WriteLine($"Match Schedule Data saved to '{matchPath}' successfully!");



        string downloadedTeamData;
        try
        {
            downloadedTeamData = await GetJsonAsync(httpClient, "rankings/" + compid);
        }
        catch
        {
            await DisplayAlert("😢 Error 😢", $"Could not download the list of teams for competition '{compid}'", "OK");
            return;
        }

        

        var teams = ProcessDownloadedTeamData(downloadedTeamData);
        
        
        
        var teamText = JsonSerializer.Serialize(teams);

        Debug.WriteLine(teamText);

        var teamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_Teams.json");
        File.WriteAllText(teamPath, teamText);

        Debug.WriteLine($"List of teams saved to '{teamPath}' successfully!");



        await DisplayAlert("Match Schedule and list of teams saved!", $"Schedule and list of teams saved to \n{matchPath} and \n{teamPath}", "OK");
        _ = await Navigation.PopAsync();



    }

    static List<ScheduleEntry> ProcessDownloadedMatchData(string downloadedData)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var dd = JsonSerializer.Deserialize<DownloadedMatchData>(downloadedData, options)!;

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

    static List<TeamEntry> ProcessDownloadedTeamData(string downloadedTeamData)
    {
        
       
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        

        var dd = JsonSerializer.Deserialize<DownloadedTeamData>(downloadedTeamData, options)!;

        
        var l = new List<TeamEntry>();
        foreach (var team in dd.Rankings)
        {
            var s = new TeamEntry
            {
                TeamNumber = team.TeamNumber
            };
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

    public class DownloadedMatchData
    {
        public List<Match> Schedule { get; set; } = new();
    }

    public class DownloadedTeamData
    {
        public List<Ranking> Rankings { get; set; } = new();
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

    public class Ranking
    {
        public int Rank { get; set; }
        public int TeamNumber { get; set; }

        public double SortOrder1 { get; set; }

        public double SortOrder2 { get; set; }

        public double SortOrder3 { get; set; }

        public double SortOrder4 { get; set; }

        public double SortOrder5 { get; set; }

        public double SortOrder6 { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public double QualAverage { get; set; }

        public int DQ { get; set; }

        public int MatchesPlayed { get; set; }


    }

}
