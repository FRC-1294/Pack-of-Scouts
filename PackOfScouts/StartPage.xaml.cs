namespace PackOfScouts;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

	private async void OnScoutClicked(object sender, EventArgs e)
	{

        var s = new List<ScheduleEntry>
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

#if false
        var s = new List<ScheduleEntry> { };
        //Change to relative path later
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\User 83nfud73\match_schedule.txt");

        foreach (string teams in lines) {
            string[] words = teams.Split(' ');
			s.Add(new ScheduleEntry
            {
                MatchNumber = int.Parse(words[0]),
                RedRobot1 = int.Parse(words[1]),
                RedRobot2 = int.Parse(words[2]),
                RedRobot3 = int.Parse(words[3]),
                BlueRobot1 = int.Parse(words[4]),
                BlueRobot2 = int.Parse(words[5]),
                BlueRobot3 = int.Parse(words[6]),
            });
                
        }

#endif
        await Navigation.PushAsync(new MatchSchedulePage(s));

	}

	private async void OnScoutLeadClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutLeadPage());
    }
}

