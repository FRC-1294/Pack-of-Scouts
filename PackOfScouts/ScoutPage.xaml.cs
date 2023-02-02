namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
	public ScoutPage()
	{
		InitializeComponent();
	}

    private async void OnOpenScoutFileClicked(object sender, EventArgs e)
    {
        var d = new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] {"*.json" } },
        };

        var options = new PickOptions
        {
            PickerTitle = "Select a Scout file to load",
            FileTypes = new(d),
        };

        var fr = await FilePicker.PickAsync(options);
    }

    private async void OnSaveScoutFileClicked(object sender, EventArgs e)
    {
        var d = new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] {"*.json" } },
        };

        var options = new PickOptions
        {
            PickerTitle = "Select where to save your Scout file",
            FileTypes = new(d),
        };

        var fr = await FilePicker.PickAsync(options);
    }
}