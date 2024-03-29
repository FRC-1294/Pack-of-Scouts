namespace PackOfScouts;

public partial class ScoutLeadPage : ContentPage
{
    internal ScoutLeadPage()
    {
        InitializeComponent();
    }

    private async void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScanQRCodePage());
    }

    private async void OnScanScoutQRCodePitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScanQRCodePitPage());
    }




}
