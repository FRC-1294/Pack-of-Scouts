using System.Diagnostics;

namespace PackOfScouts;

public partial class ShowQRCodePage : ContentPage
{
    readonly ApplicationState appState;

    internal ShowQRCodePage(string text, ApplicationState applicationState)
	{
		InitializeComponent();
        this.appState = applicationState;

		var filename = QrCode.QrCodeUtils.SaveQrCode(text);
		Debug.WriteLine($"QR Code for {text} saved to {filename}");

		var qrCode = ImageSource.FromFile(filename);
        Debug.WriteLine($"Read QR Code from {filename}");

		_qrImage.Source = qrCode;
    }

    private async void OnReturnToStartPressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MatchSchedulePage(appState.Entries, appState));
    }
}
