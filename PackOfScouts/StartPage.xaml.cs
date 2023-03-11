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
        await Navigation.PushAsync(new ScheduleSelectionPage(a));
	}

    private async void OnScoutLeadClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutLeadPage());
    }
}

