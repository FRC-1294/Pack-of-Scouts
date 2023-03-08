using System.Diagnostics;
using System.Net.Http.Headers;
using Newtonsoft.Json;
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

    private async void OnImportDataButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ImportDataPage());
    }





}
