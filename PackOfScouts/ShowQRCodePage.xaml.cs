using System.Diagnostics;

namespace PackOfScouts;

public partial class ShowQRCodePage : ContentPage
{
	public ShowQRCodePage(string text)
	{
		InitializeComponent();

		var filename = QrCode.QrCodeUtils.SaveQrCode(text);
		Debug.WriteLine($"QR Code for {text} saved to {filename}");

		var qrCode = ImageSource.FromFile(filename);
        Debug.WriteLine($"Read QR Code from {filename}");

		_qrImage.Source = qrCode;
    }
}
