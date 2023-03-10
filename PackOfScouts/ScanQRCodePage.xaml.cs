using System.Drawing;
using System.Text.Json;
using PackOfScouts.QrCode;

namespace PackOfScouts;

public partial class ScanQRCodePage : ContentPage
{
    private readonly IDispatcherTimer _timer;
    private readonly Camera _camera = new();

    public ScanQRCodePage()
    {
        InitializeComponent();

        // this timer is used to update the preview with the latest snapshot
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(20);
        _timer.Tick += (s, e) => UpdatePreview();
        _timer.Start();
    }

    private void Cleanup()
    {
        _previewImage.Source = null;
        _timer.Stop();
        _camera.Dispose();
    }

    private void UpdatePreview()
    {
        var photo = _camera.GrabLastPhoto();
        if (photo != null)
        {
            using var img = System.Drawing.Image.FromStream(photo);
            var text = QrCodeUtils.DecodeQrCode((Bitmap)img);

            photo.Position = 0;
            _previewImage.Source = ImageSource.FromStream(() => photo);

            if (!string.IsNullOrEmpty(text))
            {
                var matches = JsonSerializer.Deserialize<List<MatchData>>(text);
                if (matches != null)
                {
                    Cleanup();
                    RecordMatchData(matches);
                }
            }
        }
    }

    private async void RecordMatchData(List<MatchData> incoming)
    {
        List<MatchData>? matches = null;

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchData.json");
        if (File.Exists(path))
        {
            var text = File.ReadAllText(path);
            matches = JsonSerializer.Deserialize<List<MatchData>>(text);
        }

        matches ??= new();

        foreach (var inc in incoming)
        {
            bool append = true;
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].MatchNum == inc.MatchNum && matches[i].RobotNum == inc.RobotNum)
                {
                    // just replace the previous data.
                    matches[i] = inc;
                    append = false;
                    break;
                }
            }

            if (append)
            {
                matches.Add(inc);
            }
        }

        File.WriteAllText(path, JsonSerializer.Serialize(matches));

        await DisplayAlert("Matches added!", $"{incoming.Count} matches were added to\n{path}", "OK");
        _ = await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        _ = await Navigation.PopAsync();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        Cleanup();
        base.OnNavigatedFrom(args);
    }
}
