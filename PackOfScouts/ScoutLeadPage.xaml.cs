namespace PackOfScouts;

public partial class ScoutLeadPage : ContentPage
{
	public ScoutLeadPage()
	{
		InitializeComponent();
	}

    private async void OnOpenScoutLeadFileClicked(object sender, EventArgs e)
    {
		var d = new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{ DevicePlatform.WinUI, new[] {"*.json" } },
        };

		var options = new PickOptions
		{
			PickerTitle = "Select a Scout Lead File to load",
			FileTypes = new(d),
		};

		var fr = await FilePicker.PickAsync(options);
    }

    private async void OnSaveScoutLeadFileClicked(object sender, EventArgs e)
    {
        var d = new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] {"*.json" } },
        };

        var options = new PickOptions
        {
            PickerTitle = "Select where to save your Scout Lead file",
            FileTypes = new(d),
        };

        var fr = await FilePicker.PickAsync(options);
    }

    private async void OnImportScoutFileClicked(object sender, EventArgs e)
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
}