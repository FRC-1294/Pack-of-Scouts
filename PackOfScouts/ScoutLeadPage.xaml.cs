using System.Diagnostics;

namespace PackOfScouts;

public partial class ScoutLeadPage : ContentPage
{
    public ScoutLeadPage()
    {
        InitializeComponent();
    }

    private void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        string? text = QrCode.QrCodeUtils.CaptureQrCode();
        if (text != null)
        {
            Debug.WriteLine($"Got {text}");
        }
        else
        {
            Debug.WriteLine("Didn't get any text");
        }
    }
}