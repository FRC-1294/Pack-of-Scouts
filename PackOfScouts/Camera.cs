#define LOWLAG

using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;

#if !LOWLAG
using Windows.Storage.Streams;
#endif

namespace PackOfScouts;

internal sealed class Camera : IDisposable
{
    private volatile Stream? _lastPhoto;
    private volatile bool _stopCamera;

    public Camera()
    {
        RunCamera();
    }

    public void Dispose()
    {
        _stopCamera = true;
    }

    public Stream? GrabLastPhoto()
    {
        return Interlocked.Exchange(ref _lastPhoto, null);
    }

    /// <summary>
    /// This runs asynchronously and takes pictures until told to stop
    /// </summary>
    private async void RunCamera()
    {
        var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Back);
        var settings = new MediaCaptureInitializationSettings
        {
            VideoDeviceId = cameraDevice!.Id
        };

        var enc = ImageEncodingProperties.CreatePng();
        enc.Width = 1920;
        enc.Height = 1080;

        using var mediaCapture = new MediaCapture();
        await mediaCapture.InitializeAsync(settings);
#if LOWLAG
        var lowLag = await mediaCapture.PrepareLowLagPhotoCaptureAsync(enc);
#endif

        int count = 0;
        while (!_stopCamera)
        {
#if !LOWLAG
            using var stream = new InMemoryRandomAccessStream();
            await mediaCapture.CapturePhotoToStreamAsync(enc, stream);
            stream.Seek(0);
            using var s = stream.AsStreamForRead();
#else

            var photo = await lowLag.CaptureAsync();
            using var s = photo.Frame.AsStreamForRead();
#endif

            var ms = new MemoryStream();
            s.CopyTo(ms);
            ms.Position = 0;

            _lastPhoto = ms;
            Debug.WriteLine($"Took photo {count++}");
        }

#if LOWLAG
        await lowLag.FinishAsync();
#endif
        mediaCapture.Dispose();
    }

    /// <summary>
    /// Attempts to find and return a device mounted on the panel specified, and on failure to find one it will return the first device listed
    /// </summary>
    /// <param name="desiredPanel">The desired panel on which the returned device should be mounted, if available</param>
    /// <returns></returns>
    private static async Task<DeviceInformation?> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
    {
        // Get available devices for capturing pictures
        var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

        // Get the desired camera by panel
        DeviceInformation? desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

        // If there is no device mounted on the desired panel, return the first device found
        return desiredDevice ?? allVideoDevices.FirstOrDefault();
    }
}
