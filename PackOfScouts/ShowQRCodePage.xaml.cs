using System.Diagnostics;

namespace PackOfScouts;

public partial class ShowQRCodePage : ContentPage
{
    private readonly List<ScheduleEntry> entries=new();

    public ShowQRCodePage(string text, List<ScheduleEntry> entries)
	{
		InitializeComponent();
        this.entries= entries;

		var filename = QrCode.QrCodeUtils.SaveQrCode(text);
		Debug.WriteLine($"QR Code for {text} saved to {filename}");

		var qrCode = ImageSource.FromFile(filename);
        Debug.WriteLine($"Read QR Code from {filename}");

		_qrImage.Source = qrCode;
    }

    private async void OnReturnToStartPressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MatchSchedulePage(entries));
    }
}
