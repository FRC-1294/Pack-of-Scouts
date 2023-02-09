using System.Diagnostics;

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

    private void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        string filename = QrCode.QrCodeUtils.CapturePicture();
        Debug.WriteLine($"Saving picture to {filename}");

        string text = QrCode.QrCodeUtils.LoadQrCode(filename);
        Debug.WriteLine($"Code was {text}");
    }
}