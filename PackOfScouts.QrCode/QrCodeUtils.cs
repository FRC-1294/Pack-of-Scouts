using Genesis.QRCodeLib;
using Net.Codecrete.QrCodeGenerator;
using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QrGen = Net.Codecrete.QrCodeGenerator.QrCode;

namespace PackOfScouts.QrCode;

/// <summary>
/// Utilities to manipulate QR codes.
/// </summary>
public static class QrCodeUtils
{
    private static VideoCapture? _capture;

    /// <summary>
    /// Encodes a string as a QR Code in a PNG file.
    /// </summary>
    /// <param name="textToEncode">The text to encode. This must be limited to a few Ks in size.</param>
    /// <returns>The name of the file where the QR was saved.</returns>
    public static string SaveQrCode(string textToEncode)
    {
        var filename = Path.ChangeExtension(Path.GetTempFileName(), "png");
        var qr = QrGen.EncodeText(textToEncode, QrGen.Ecc.Medium);
        var bm = qr.ToBitmap(scale: 10, border: 4);
        bm.Save(filename, ImageFormat.Png);
        return filename;
    }

    /// <summary>
    /// Read and decode a QR code from a file.
    /// </summary>
    /// <param name="filenameWithCode">The image file to read from. This is typically a PNG file.</param>
    /// <returns>The string represented by the QR code.</returns>
    public static string LoadQrCode(string filenameWithCode)
    {
        var bm = Image.FromFile(filenameWithCode);
        var d = new QRDecoder();
        var b = d.ImageDecoder((Bitmap)bm);
        return QRDecoder.QRCodeResult(b);
    }

    /// <summary>
    /// Takes a picture using the computer's primary webcam.
    /// </summary>
    /// <returns>The name of the file where the picture was saved.</returns>
    /// <remarks>
    /// This opens a preview window on the screen, letting you
    /// </remarks>
    public static string CapturePicture()
    {
        var filename = Path.ChangeExtension(Path.GetTempFileName(), "png");

        if (_capture == null)
        {
            using Mat m = new(1080, 1920, MatType.CV_16S);
            using Window tmp = new("Initializing camera, this might take a minute or two...");
            tmp.ShowImage(m);

            _capture = new(0);
            _ = _capture.Set(VideoCaptureProperties.FrameWidth, 1920);
            _ = _capture.Set(VideoCaptureProperties.FrameHeight, 1080);
        }

        using Window window = new("Press a key to take a picture");
        using Mat image = new();

        while (true)
        {
            _ = _capture.Read(image);
            if (image.Empty())
            {
                break;
            }

            window.ShowImage(image);

            var x = Cv2.WaitKey(30);
            if (x >= 0)
            {
                _ = image.SaveImage(filename);
                break;
            }
        }

        return filename;
    }
}
