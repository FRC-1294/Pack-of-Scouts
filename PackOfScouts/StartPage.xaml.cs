namespace PackOfScouts;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

	private async void OnScoutClicked(object sender, EventArgs e)
	{
        var a = new ApplicationState();

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchSchedule.json");
        if (!File.Exists(path))
        {
			var next = new ImportSchedulePage();
			await Navigation.PushAsync(next);
			Navigation.InsertPageBefore(new ScheduleSelectionPage(a), next);
        }
		else
		{
            await Navigation.PushAsync(new ScheduleSelectionPage(a));
        }
    }

    private async void OnScoutLeadClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutLeadPage());
    }
}

