using System.Text.Json;

namespace PackOfScouts;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

	private async void OnScoutClicked(object sender, EventArgs e)
	{
        List<ScheduleEntry> s;
            
        var file = await PickScheduleFile();
        if (file == null)
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
        else
        {
            var text = File.ReadAllText(file);
            s = JsonSerializer.Deserialize<List<ScheduleEntry>>(text!)!;
        }

        var a = new ApplicationState();
        a.ScheduleEntries.AddRange(s);

        await Navigation.PushAsync(new MatchSchedulePage(a));
	}

    private async Task<String?> PickScheduleFile()
    {
        var options = new PickOptions
        {
            PickerTitle = "Select a match schedule file",
            FileTypes = new(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".json" } },
            }),
        };

        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                return result.FullPath;
            }
        }
        catch
        {
        }

        return null;
    }

    private async void OnScoutLeadClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutLeadPage());
    }
}

