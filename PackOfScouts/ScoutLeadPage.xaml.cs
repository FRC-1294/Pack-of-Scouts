using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PackOfScouts;

public partial class ScoutLeadPage : ContentPage
{
    public ScoutLeadPage()
    {
        InitializeComponent();
    }

    private async void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        string? text = QrCode.QrCodeUtils.CaptureQrCode();
        if (text != null)
        {
            var matchData = JsonSerializer.Deserialize<MatchData>(text);
            if (matchData != null)
            {
                await RecordMatchData(matchData);
            }
        }
    }

    private async Task RecordMatchData(MatchData matchData)
    {
        MatchDataSet? set = null;

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchData.json");
        if (File.Exists(path))
        {
            var text = File.ReadAllText(path);
            set = JsonSerializer.Deserialize<MatchDataSet>(text);
        }

        set ??= new();
        _ = set.Natches.Add(matchData);

        Debug.WriteLine($"Added match #{matchData.MatchNumber} for robot #{matchData.RobotNumber} to\n{path}");
        Debug.WriteLine($"{set.Natches.Count} total matches recorded");
        File.WriteAllText(path, JsonSerializer.Serialize(set));

        await DisplayAlert("Match added!", $"Match #{matchData.MatchNumber} for robot #{matchData.RobotNumber} was added to\n{path}", "OK");
    }
}
