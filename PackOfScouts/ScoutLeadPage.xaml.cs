using System.Diagnostics;

namespace PackOfScouts;

public partial class ScoutLeadPage : ContentPage
{
    private Dictionary<string, string> dropDownFiller = new Dictionary<string, string>
        {
            { "Option1", "a"}, { "Option2", "b"}, { "Option3", "c" }, { "Option4", "Option3" }
        };

    private Dictionary<string, string> tableFiller = new Dictionary<string, string>
        {
            { "Team1", "Stat1"}, { "Team2", "Stat2"}, { "Team3", "Stat3" }, { "Team4", "Stat4" }
        };
    public ScoutLeadPage()
    {
        InitializeComponent();

        foreach (string option in dropDownFiller.Keys) {
            Stats.Items.Add(option);
        }

        Selection.Root = new TableRoot() {
           new TableSection () {
                new TextCell () {
                    Text = "Select a team by using the buttons"
                }
           }
        };

        TableSection sec = new TableSection(){};
        foreach (string team in tableFiller.Keys)
        {
            Grid g = new Grid() {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star)}
                }
            };
            g.Add(new Label 
            {
                Text = team,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 50,
                FontSize = 15
            });
            g.Add(new Label
            {
                Text = tableFiller[team],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 1, 0);
            sec.Add(
                new ViewCell()
                {
                    View = g
                }
            );
        }
        Selection.Root.Add( sec );
     }

    private async void OnOpenScoutLeadFileClicked(object sender, EventArgs e)
    {
		var d = new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{ DevicePlatform.WinUI, new[] {"*.json" } },
        };

		var options = new PickOptions
		{
			PickerTitle = "Select a Scout Lead File to load",
			FileTypes = new(d),
		};

		var fr = await FilePicker.PickAsync(options);
    }

    private async void OnSaveScoutLeadFileClicked(object sender, EventArgs e)
    {
        var d = new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] {"*.json" } },
        };

        var options = new PickOptions
        {
            PickerTitle = "Select where to save your Scout Lead file",
            FileTypes = new(d),
        };

        var fr = await FilePicker.PickAsync(options);
    }

    private void OnScanScoutQRCodeClicked(object sender, EventArgs e)
    {
        string filename = QrCode.QrCodeUtils.CapturePicture();
        Debug.WriteLine($"Saving picture to {filename}");

        string text = QrCode.QrCodeUtils.LoadQrCode(filename);
        Debug.WriteLine($"Code was {text}");
    }

    

    private void OnTeamSelection(object sender, EventArgs e)
    {
        
    }
}