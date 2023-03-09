namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    int conesScored;
    int cubesScored;
    int functioningAuto;
    int moveOutOfZone;
    int chargeStationIndex = -1;
    readonly int robotNum = 1294;
    readonly int matchNumber = 0;
    readonly ApplicationState appState;

    internal ScoutPage(int match, int robot, ApplicationState applicationState)
    {
        InitializeComponent();
        robotNum= robot;
        matchNumber=match;
        appState = applicationState;
        ScoutingTeamLabel.Text = "SCOUTING TEAM " + Convert.ToString(this.robotNum);
        if (appState.Data != null )
        {
            numOfMatches.Text = string.Format("Matches scouted: {0}", appState.Data.Count);
        } else
        {
            numOfMatches.Text = "Matches scouted: 0";
        }
        

    }

    void OnStepperValueChangedCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        _displayConeLabel.Text = string.Format("{0}", value);


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
        _displayCubeLabel.Text = string.Format("{0} cube scored", value);

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
        await Navigation.PushAsync(new TeleOperator(cubesScored, conesScored, functioningAuto, moveOutOfZone, chargeStationIndex, matchNumber, robotNum, appState));
    }
}
