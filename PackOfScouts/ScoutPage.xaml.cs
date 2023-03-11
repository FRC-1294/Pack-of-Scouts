namespace PackOfScouts;

public partial class ScoutPage : ContentPage
{
    private readonly int robotNum;
    private readonly int matchNumber;
    private readonly ApplicationState appState;
    private int conesScored;
    private int cubesScored;
    private int functioningAuto;
    private int moveOutOfZone;
    private int chargeStationIndex = 0;

    internal ScoutPage(int match, int robot, ApplicationState applicationState)
    {
        InitializeComponent();
        robotNum = robot;
        matchNumber = match;
        appState = applicationState;
        ScoutingTeamLabel.Text = $"SCOUTING TEAM {this.robotNum}";
        chargeStationPicker.SelectedItem = "No Attempt";
    }

    private void OnStepperValueChangedCone(object sender, ValueChangedEventArgs e)
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

    private void OnStepperValueChangedCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        _displayCubeLabel.Text = string.Format("{0}", value);

        if (value > cubesScored)
        {
            cubesScored++;
        }
        else
        {
            cubesScored--;
        }
    }

    private void OnFunctionAutoToggled(object sender, ToggledEventArgs e)
    {
        functioningAuto = 1;
    }

    private void OnMoveOutOfZoneToggled(object sender, ToggledEventArgs e)
    {
        moveOutOfZone = 1;
    }

    private void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        chargeStationIndex = chargeStationPicker.SelectedIndex;
    }

    private async void OnDoneWithAutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeleOperatorPage(cubesScored, conesScored, functioningAuto, moveOutOfZone, chargeStationIndex, matchNumber, robotNum, appState));
    }
}
