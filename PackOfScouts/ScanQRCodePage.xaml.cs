using System.Drawing;
using System.Text;
using System.Text.Json;

namespace PackOfScouts;

public partial class ScanQRCodePage : ContentPage
{
    string QrCode = "";

    public ScanQRCodePage()
    {
        InitializeComponent();

        Loaded += (s, e) => QrData.Focus();
    }

    private void ProcessTest(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            try
            {
                var matches = JsonSerializer.Deserialize<List<MatchData>>(text);
                if (matches != null)
                {
                    RecordMatchData(matches);
                }
            }
            catch
            {
                DisplayError();
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

    private void OnQrDataTextChanged(object sender, TextChangedEventArgs e)
    {
        this.QrCode = QrData.Text;
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        _ = await Navigation.PopAsync();
    }

    private async void OnDoneClicked(object sender, EventArgs e)
    {
        ProcessTest(this.QrCode);
        _ = await Navigation.PopAsync();
    }
}
