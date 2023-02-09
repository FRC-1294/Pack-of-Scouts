using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    public ScoutPage()
    {
        InitializeComponent();


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
            _displayConeLabel.Text = string.Format("{0} cones scored", value);
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

    private async void OnDoneWithAutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Tele_op());
    }

    private async void OnShowQRCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowQRCodePage("https://www.google.com"));
    }
}

