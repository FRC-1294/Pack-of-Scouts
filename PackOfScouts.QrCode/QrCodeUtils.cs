using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Net.Codecrete.QrCodeGenerator;
using QRCodeDecoderLibrary;

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
        var qr = Net.Codecrete.QrCodeGenerator.QrCode.EncodeText(textToEncode, Net.Codecrete.QrCodeGenerator.QrCode.Ecc.High);
        var bm = qr.ToBitmap(scale: 10, border: 4);
        bm.Save(ms, ImageFormat.Png);
        ms.Position = 0;
        return ms;
    }

    public static string? DecodeQrCode(Bitmap bm)
    {
        string? text = null;

        var d = new QRDecoder();
        var r = d.ImageDecoder((Bitmap)bm);
        if (r != null)
        {
            text = SingleQRCodeResult(QRDecoder.ByteArrayToStr(r[0].DataArray));
        }

        return text;
    }

    /// <summary>
    /// Single QR Code result
    /// </summary>
    /// <param name="result">Input string</param>
    /// <returns>Output display string</returns>
    private static string SingleQRCodeResult(string result)
    {
        int index;
        for (index = 0; index < result.Length && (result[index] >= ' ' && result[index] <= '~' || result[index] >= 160); index++)
        {
            ;
        }

        if (index == result.Length)
        {
            return result;
        }

        StringBuilder sb = new(result[..index]);
        for (; index < result.Length; index++)
        {
            char ch = result[index];
            if (ch >= ' ' && ch <= '~' || ch >= 160)
            {
                _ = sb.Append(ch);
                continue;
            }

            if (ch == '\r')
            {
                _ = sb.Append("\r\n");
                if (index + 1 < result.Length && result[index + 1] == '\n')
                {
                    index++;
                }

                continue;
            }

            if (ch == '\n')
            {
                _ = sb.Append("\r\n");
                continue;
            }

            _ = sb.Append('¿');
        }

        return sb.ToString();
    }
}
