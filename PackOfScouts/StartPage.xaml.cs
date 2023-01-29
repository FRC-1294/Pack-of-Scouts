namespace PackOfScouts;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

	private async void OnScoutClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutPage());
	}

	private async void OnScoutLeadClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ScoutLeadPage());
    }
}

