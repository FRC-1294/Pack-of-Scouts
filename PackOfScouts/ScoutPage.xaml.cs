namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    private string[] listOfTeams;
    public ScoutPage()
    {
        InitializeComponent();
        var layout = (VerticalStackLayout)FindByName("lamo");
        //CHANGE TO YOUR USER PATH
        listOfTeams = File.ReadAllLines("C:\\Users\\user\\PackOfScouts\\PackOfScouts\\bin\\Debug\\net7.0-windows10.0.19041.0\\win10-x64\\PackOfScoutsData.txt");
        
        foreach (string team in listOfTeams)
        {
            layout.Children.Add(new Button { Text = team });
        }
    }

    private async void OnOpenScoutFileClicked(object sender, EventArgs e)
    {
        var d = new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] {"*.json" } },
        };

        var options = new PickOptions
        {
            PickerTitle = "Select a Scout file to load",
            FileTypes = new(d),
        };

        var fr = await FilePicker.PickAsync(options);
    }

    private async void OnSaveScoutFileClicked(object sender, EventArgs e)
    {
        var d = new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] {"*.json" } },
        };

        var options = new PickOptions
        {
            PickerTitle = "Select where to save your Scout file",
            FileTypes = new(d),
        };

        var fr = await FilePicker.PickAsync(options);
    }
}