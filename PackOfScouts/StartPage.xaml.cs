namespace PackOfScouts;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

	private async void OnScoutClicked(object sender, EventArgs e)
	{

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







        await Navigation.PushAsync(new MatchSchedulePage(s));
	}

	private async void OnScoutLeadClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutLeadPage());
    }
}

