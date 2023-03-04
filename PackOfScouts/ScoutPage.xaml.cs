using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Layouts;
using Windows.ApplicationModel.Activation;

namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    int conesScored = 0;
    int cubesScored = 0;
    int functioningAuto = 0;
    int moveOutOfZone = 0;
    int chargeStationIndex = -1;
    readonly int robotNum = 1294;
    readonly int matchNumber = 0;

    public ScoutPage(int match, int robot)
    {
        InitializeComponent();
        robotNum= robot;
        matchNumber=match;
        ScoutingTeamLabel.Text = "TEAM " + Convert.ToString(this.robotNum);

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

        if (value > conesScored)
        {
            conesScored++;
        }
        else
        {
            conesScored--;
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

        if (value > cubesScored)
        {
            cubesScored++;
        }
        else
        {
            cubesScored--;
        }
    }

    void OnFunctionAutoToggled(object sender, ToggledEventArgs e)
    {
        functioningAuto = 1;
    }

    void OnMoveOutOfZoneToggled(object sender, ToggledEventArgs e)
    {
        moveOutOfZone = 1;
    }

    void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        chargeStationIndex = chargeStationPicker.SelectedIndex;
    }

    private async void OnDoneWithAutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeleOperator(cubesScored, conesScored, functioningAuto, moveOutOfZone, chargeStationIndex, matchNumber, robotNum));
    }
}

