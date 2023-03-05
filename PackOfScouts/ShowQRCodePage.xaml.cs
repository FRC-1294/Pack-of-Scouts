using System.Diagnostics;
using System.Text.Json;

namespace PackOfScouts;

public partial class ShowQRCodePage : ContentPage
{
    readonly ApplicationState appState;

    internal ShowQRCodePage(ApplicationState applicationState)
	{
		InitializeComponent();

        this.appState = applicationState;

        var json = JsonSerializer.Serialize(appState.Matches);

        var filename = QrCode.QrCodeUtils.SaveQrCode(json);
		Debug.WriteLine($"QR Code for {json} saved to {filename}");

		var qrCode = ImageSource.FromFile(filename);
        Debug.WriteLine($"Read QR Code from {filename}");

		_qrImage.Source = qrCode;
    }

    private async void OnReturnToStartPressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MatchSchedulePage(appState));
    }
}
