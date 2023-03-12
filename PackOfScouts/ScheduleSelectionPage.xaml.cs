using System.Net.NetworkInformation;
using System.Text.Json;

namespace PackOfScouts;

public partial class ScheduleSelectionPage : ContentPage
{
    private readonly ApplicationState _appState;

    internal ScheduleSelectionPage(ApplicationState appState)
    {
        _appState = appState;
        InitializeComponent();
    }

    private async void OnDownloadMatchScheduleButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ImportSchedulePage());
    }

    private async void OnUaeCurrentScheduleButtonClicked(object sender, EventArgs e)
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
