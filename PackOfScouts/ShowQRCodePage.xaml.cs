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
        _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
    }

    private async void OnReturnToStartPressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MatchSchedulePage(appState));
    }
}
