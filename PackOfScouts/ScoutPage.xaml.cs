using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    public ScoutPage()
    {
        InitializeComponent();
        double screenWidth = (int)DeviceDisplay.MainDisplayInfo.Width / 5;
        //currently code can only math screen width of computer, not screen size of app -- fix later
        Finish.WidthRequest = screenWidth;
        SaveFile.WidthRequest = screenWidth;
        OpenFile.WidthRequest = screenWidth;


    }

    void OnStepperValueChangedCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            _displayConeLabel.Text = string.Format("{0} cone scored", value);
        }
        else
        {
            _displayConeLabel.Text = string.Format("{0} cone scored", value);
        }

    }

    void OnStepperValueChangedCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            _displayCubeLabel.Text = string.Format("{0} cube scored", value);
        }
        else
        {
            _displayCubeLabel.Text = string.Format("{0} cubes scored", value);
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

    private async void OnShowQRCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowQRCodePage("https://www.google.com"));
    }
}
