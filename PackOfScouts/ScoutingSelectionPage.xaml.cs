using System.Text.Json;

namespace PackOfScouts;

public partial class ScoutingSelectionPage : ContentPage
{
    private readonly ApplicationState _appState;
    internal ScoutingSelectionPage(ApplicationState appState)
	{
        _appState = appState;
        InitializeComponent();
	}

    /**
    private async void OnPitScoutingClicked(object sender, EventArgs e)
    {
        var a = new ApplicationState();
        await Navigation.PushAsync(new PitScoutingPage(a));
    }
    **/

    private async void OnPitScoutingClicked(object sender, EventArgs e)
    {
        List<TeamEntry> s;

        _appState.TeamEntries.Clear();

        try
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_PitData.json");
            var text = File.ReadAllText(path);
            var pitdata = JsonSerializer.Deserialize<List<PitData>>(text)!;
            _appState.Teams.Clear();
            _appState.Teams.AddRange(pitdata);
        }
        catch
        {
            // never mind...
        }

        try
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_Teams.json");
            var text = File.ReadAllText(path);
            s = JsonSerializer.Deserialize<List<TeamEntry>>(text!)!;
        }
        catch
        {
            s = new List<TeamEntry>
            {
                new TeamEntry
                {
                    TeamNumber = 123
                    
                },

                new TeamEntry
                {
                    TeamNumber = 456
                },

                new TeamEntry
                {
                   TeamNumber = 789
                }
            };
        }

        _appState.TeamEntries.AddRange(s);
        await Navigation.PushAsync(new PitSchedulePage(_appState));
    }

    private async void OnMatchScoutingClicked(object sender, EventArgs e)
    {
        List<ScheduleEntry> s;

        _appState.ScheduleEntries.Clear();

        try
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_ScoutData.json");
            var text = File.ReadAllText(path);
            var matches = JsonSerializer.Deserialize<List<MatchData>>(text)!;
            _appState.Matches.Clear();
            _appState.Matches.AddRange(matches);
        }
        catch
        {
            // never mind...
        }

        try
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchSchedule.json");
            var text = File.ReadAllText(path);
            s = JsonSerializer.Deserialize<List<ScheduleEntry>>(text!)!;
        }
        catch
        {
            s = new List<ScheduleEntry>
            {
                new ScheduleEntry
                {
                    MatchNumber = 123,
                    RedRobot1 = 1,
                    RedRobot2 = 2,
                    RedRobot3 = 3,
                    BlueRobot1 = 4,
                    BlueRobot2 = 5,
                    BlueRobot3 = 6,
                },

                new ScheduleEntry
                {
                    MatchNumber = 456,
                    RedRobot1 = 11,
                    RedRobot2 = 12,
                    RedRobot3 = 13,
                    BlueRobot1 = 14,
                    BlueRobot2 = 15,
                    BlueRobot3 = 16,
                },

                new ScheduleEntry
                {
                    MatchNumber = 789,
                    RedRobot1 = 21,
                    RedRobot2 = 22,
                    RedRobot3 = 23,
                    BlueRobot1 = 24,
                    BlueRobot2 = 25,
                    BlueRobot3 = 26,
                }
            };
        }

        _appState.ScheduleEntries.AddRange(s);
        await Navigation.PushAsync(new MatchSchedulePage(_appState));
    }
}