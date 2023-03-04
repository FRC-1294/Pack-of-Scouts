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

    private void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        string? text = QrCode.QrCodeUtils.CaptureQrCode();
        if (text != null)
        {
            var matchData = JsonSerializer.Deserialize<MatchData>(text);
            if (matchData != null)
            {
                RecordMatchData(matchData);
            }
        }
    }

    private void RecordMatchData(MatchData matchData)
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

        Debug.WriteLine($"Added match #{matchData.MatchNumber} for robot #{matchData.RobotNumber} to file `{path}");
        Debug.WriteLine($"{set.Natches.Count} total matches recorded");
        File.WriteAllText(path, JsonSerializer.Serialize(set));
    }
}
