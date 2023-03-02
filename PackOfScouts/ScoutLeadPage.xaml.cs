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
        string filename = QrCode.QrCodeUtils.CapturePicture();
        Debug.WriteLine($"Saving picture to {filename}");

        string text = QrCode.QrCodeUtils.LoadQrCode(filename);
        Debug.WriteLine($"Code was {text}");
    }
}