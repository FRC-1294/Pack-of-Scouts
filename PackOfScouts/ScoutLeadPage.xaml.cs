using System.Text.Json;

namespace PackOfScouts;

public partial class ScoutLeadPage : ContentPage
{
    internal ScoutLeadPage()
    {
        InitializeComponent();
    }

    private async void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        string? text = QrCode.QrCodeUtils.CaptureQrCode();
        if (text != null)
        {
            var matches = JsonSerializer.Deserialize<List<MatchData>>(text);
            if (matches != null)
            {
                await RecordMatchData(matches);
            }
        }
    }

    private async Task RecordMatchData(List<MatchData> incoming)
    {
        List<MatchData>? matches = null;

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchData.json");
        if (File.Exists(path))
        {
            var text = File.ReadAllText(path);
            matches = JsonSerializer.Deserialize<List<MatchData>>(text);
        }

        matches ??= new();
        matches.AddRange(incoming);

        File.WriteAllText(path, JsonSerializer.Serialize(matches));

        await DisplayAlert("Matches added!", $"{incoming.Count} matches were added to\n{path}", "OK");
    }
}
