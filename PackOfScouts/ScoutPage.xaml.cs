using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Layouts;
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


    private async void OnDoneWithAutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeleOperator());
    }

    
}

