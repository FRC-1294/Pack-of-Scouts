using Net.Codecrete.QrCodeGenerator;
using System.Text.Json;

namespace PackOfScouts;

public partial class ShowQRCodePage : ContentPage
{
    readonly ApplicationState appState;
    int count = -1;
    internal ShowQRCodePage(ApplicationState applicationState)
	{
		InitializeComponent();
        this.appState = applicationState;
        this.count++;
        var json = JsonSerializer.Serialize(this.appState.Matches[count]);
        _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
        if (this.appState.Matches.Count == 1)
        {
            NextQrCode.IsVisible = false;
        }

        
    }

    private void OnNextQrCodePressed (object sender, EventArgs e)
    {   
        if (this.count < this.appState.Matches.Count - 1)
        {
            PreviousQrCode.IsVisible = true;
            this.count++;
            var json = JsonSerializer.Serialize(this.appState.Matches[count]);
            _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
            if (this.count == this.appState.Matches.Count - 1)
            {
                NextQrCode.IsVisible = false;
            }
        }
    }

    private void OnPreviousQrCodePressed(object sender, EventArgs e)
    {
        if (this.count > 0)
        {
            this.count--;
            NextQrCode.IsVisible = true;
            var json = JsonSerializer.Serialize(this.appState.Matches[count]);
            _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
            if (this.count == 0)
            {
                PreviousQrCode.IsVisible = false;
            }
        }
    }

    private void OnClearDataPressed(object sender, EventArgs e)
    {
        clearData.IsVisible = false;
        YouSure.IsVisible = true;
        ImSure.IsVisible = true;
        NotSure.IsVisible = true;
    }

    private async void OnImSurePressed(object sender, EventArgs e)
    {
        this.appState.Matches.Clear();
        await Navigation.PushAsync(new MatchSchedulePage(appState));
    }

    private void OnNotSurePressed(Object sender, EventArgs e)
    {
        clearData.IsVisible = true;
        YouSure.IsVisible = false;
        ImSure.IsVisible = false;
        NotSure.IsVisible = false;
    }
}
