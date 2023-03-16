using System.Text.Json;

namespace PackOfScouts;

public partial class ScanQRCodePitPage : ContentPage
{
    public ScanQRCodePitPage()
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
                var team = JsonSerializer.Deserialize<PitData>(text);
                if (team != null)
                {
                    List<PitData> teams = new()
                    {
                        team
                    };

                    RecordTeamData(teams);
                }
            }
            catch
            {
                try
                {
                    List<PitData> teams = new();
                    foreach (var line in text.Split('\n'))
                    {
                        var team = JsonSerializer.Deserialize<PitData>(line);
                        if (team != null)
                        {
                            teams.Add(team);
                        }
                    }

                    RecordTeamData(teams);
                }
                catch
                {
                    DisplayError();
                }
            }
        }
    }

    private async void RecordTeamData(List<PitData> incoming)
    {
        List<PitData>? teams = null;

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_PitData.json");
        if (File.Exists(path))
        {
            var text = File.ReadAllText(path);
            teams = JsonSerializer.Deserialize<List<PitData>>(text);
        }

        teams ??= new();

        foreach (var inc in incoming)
        {
            bool append = true;
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].RobotNum == inc.RobotNum)
                {
                    // just replace the previous data.
                    teams[i] = inc;
                    append = false;
                    break;
                }
            }

            if (append)
            {
                teams.Add(inc);
            }
        }

        File.WriteAllText(path, JsonSerializer.Serialize(teams));

        await DisplayAlert("Teams added!", $"{incoming.Count} team were added to\n{path}", "OK");
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

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        ProcessText(this.QrData.Text);
        _ = await Navigation.PopAsync();
    }
}
