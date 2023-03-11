using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Net.Codecrete.QrCodeGenerator;

namespace PackOfScouts.QrCode;

/// <summary>
/// Utilities to manipulate QR codes.
/// </summary>
public static class QrCodeUtils
{
    /// <summary>
    /// Encodes a string into a QR Code saved as a PNG file.
    /// </summary>
    /// <param name="textToEncode">The text to encode. This must be limited to a few Ks in size.</param>
    /// <returns>The stream containing the PNG data.</returns>
    public static Stream MakeQrCode(string textToEncode)
    {
        var ms = new MemoryStream();
        var qr = Net.Codecrete.QrCodeGenerator.QrCode.EncodeText(textToEncode, Net.Codecrete.QrCodeGenerator.QrCode.Ecc.Medium);
        var bm = qr.ToBitmap(scale: 10, border: 4);
        bm.Save(ms, ImageFormat.Png);
        ms.Position = 0;
        return ms;
    }
}
