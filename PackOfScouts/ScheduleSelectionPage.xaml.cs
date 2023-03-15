using System.Net.NetworkInformation;
using System.Text.Json;

namespace PackOfScouts;

public partial class ScheduleSelectionPage : ContentPage
{
    private readonly ApplicationState _appState;

    internal ScheduleSelectionPage(ApplicationState appState)
    {
        _appState = appState;
        InitializeComponent();
    }

    private async void OnDownloadMatchScheduleButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ImportSchedulePage());
    }

    private async void OnUseCurrentScheduleButtonClicked(object sender, EventArgs e)
    {
        var a = new ApplicationState();
        await Navigation.PushAsync(new ScoutingSelectionPage(a));
    }

}
