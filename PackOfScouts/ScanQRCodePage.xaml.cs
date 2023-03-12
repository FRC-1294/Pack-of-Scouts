using System.Text.Json;

namespace PackOfScouts;

public partial class ScanQRCodePage : ContentPage
{
    public ScanQRCodePage()
    {
        InitializeComponent();

        Loaded += (s, e) => QrData.Focus();
    }

    private void ProcessText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            try
            {
                var match = JsonSerializer.Deserialize<MatchData>(text);
                if (match != null)
                {
                    List<MatchData> matches = new()
                    {
                        match
                    };

                    RecordMatchData(matches);
                }
            }
            catch
            {
                try
                {
                    List<MatchData> matches = new();
                    foreach (var line in text.Split('\n'))
                    {
                        var match = JsonSerializer.Deserialize<MatchData>(line);
                        if (match != null)
                        {
                            matches.Add(match);
                        }
                    }

                    RecordMatchData(matches);
                }
                catch
                {
                    DisplayError();
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

    private async void DisplayError()
    {
        await DisplayAlert("😢 Error 😢", "Unable to read JSON, please try again", "OK");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        _ = await Navigation.PopAsync();
    }

    private async void OnDoneClicked(object sender, EventArgs e)
    {
        ProcessText(this.QrData.Text);
        _ = await Navigation.PopAsync();
    }
}
