using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Layouts;
using System.Text.Json;
using Windows.ApplicationModel.Activation;

namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    int conesScored = 0;
    int cubesScored = 0;
    bool functioningAuto = false;
    bool moveOutOfZone = false;
    string chargeStationStatus = null;
    public ScoutPage()
    {
        InitializeComponent();
        //int conesScored = 0;
        //int cubesScored = 0;


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
        conesScored++;

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
        cubesScored++;
    }

    void OnFunctionAutoToggled(object sender, ToggledEventArgs e)
    {
        functioningAuto = true;
    }

    void OnMoveOutOfZoneToggled(object sender, ToggledEventArgs e)
    {
        moveOutOfZone = true;
    }

    void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            chargeStationStatus = (string)picker.ItemsSource[selectedIndex];
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
        await Navigation.PushAsync(new TeleOperator());
    }

    private async void OnShowQRCodeClicked(object sender, EventArgs e)
    {
        var m = new MatchData
        {
            MatchNumber = 15,
            RobotNumber = 1294,
            FunctioningAuto = 1,
            ChargeStationPointsAuto = 12,
            ChargeStationPointsTeleop = 10,
            ConesScoredLowAuto = 0,
            ConesScoredMidAuto = 0,
            ConesScoredHighAuto = 2,
            CubesScoredLowAuto = 0,
            CubesScoredMidAuto = 0,
            CubesScoredHighAuto = 1,
            CubesScoredLowTeleop = 3,
            CubesScoredMidTeleop = 3,
            CubesScoredHighTeleop = 9,
            ConesScoredLowTeleop = 3,
            ConesScoredMidTeleop = 4,
            ConesScoredHighTeleop = 5,
            Notes = "This is the best robot ever",
        };

        var json = JsonSerializer.Serialize(m);
        await Navigation.PushAsync(new ShowQRCodePage(json));
    }
}

