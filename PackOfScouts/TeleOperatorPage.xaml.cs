using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace PackOfScouts;

public partial class TeleOperatorPage : ContentPage
{
    private readonly ApplicationState appState;
    private readonly int autoConesScored;
    private readonly int autoCubesScored;
    private readonly int functioningAuto;
    private readonly int movedOutOfZone;
    private readonly int autoChargeStation;
    private readonly int matchNum;
    private readonly int roboNum;

    private string? notes;
    private int missedScores;
    private int chargeStationIndex = -1;
    private bool defense;
    private bool broke;
    private int highCones;
    private int midCones;
    private int lowCones;
    private int highCubes;
    private int midCubes;
    private int lowCubes;

    internal TeleOperatorPage(int cubesScored, int conesScored, int functioningAuto, int moveOutOfZone, int autoChargeStation, int matchNumber, int roboNumber, ApplicationState applicationState)
    {
        InitializeComponent();

        // use cubesScored and conesScored
        this.autoCubesScored = cubesScored;
        this.autoConesScored = conesScored;
        this.functioningAuto = functioningAuto;
        this.movedOutOfZone = moveOutOfZone;
        this.autoChargeStation = autoChargeStation;
        this.matchNum = matchNumber;
        this.roboNum = roboNumber;
        this.appState = applicationState;
        ScoutingTeamLabel.Text = "SCOUTING TEAM " + Convert.ToString(this.roboNum);
        if (appState.Matches != null)
        {
            numOfMatches.Text = string.Format("Matches scouted: {0}", appState.Matches.Count);
            if (appState.Matches.Count >= 2)
            {
                Next.Text = "Save file and go to scout lead";
            }
        }
        else
        {
            numOfMatches.Text = "Matches scouted: 0";
        }
        
    }

    private void OnStepperValueChangedHighCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        HighCone.Text = value.ToString();

        if (value > highCones)
        {
            highCones++;
        }
        else
        {
            highCones--;
        }
    }

    private void OnStepperValueChangedMidCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        MidCone.Text = value.ToString();
        
        if (value > midCones)
        {
            midCones++;
        }
        else
        {
            midCones--;
        }
    }

    private void OnStepperValueChangedLowCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        LowCone.Text = value.ToString();
        
        if (value > lowCones)
        {
            lowCones++;
        }
        else
        {
            lowCones--;
        }
    }

    private void OnStepperValueChangedHighCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        HighCube.Text = value.ToString();

        if (value > highCubes)
        {
            highCubes++;
        }
        else
        {
            highCubes--;
        }
    }

    private void OnStepperValueChangedMidCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        MidCube.Text = value.ToString();

        if (value > midCubes)
        {
            midCubes++;
        }
        else
        {
            midCubes--;
        }
    }

    private void OnStepperValueChangedLowCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        LowCube.Text = value.ToString();

        if (value > lowCubes)
        {
            lowCubes++;
        }
        else
        {
            lowCubes--;
        }
    }

    private void OnNotesTextChanged(object sender, TextChangedEventArgs e)
    {
        notes = e.NewTextValue;
        CharacterCount.Text = string.Format("Characters remaining: {0}", (200 - notes.Length));
    }

    private void OnStepperValueChangedMisses(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        _displayMistakesLabel.Text = value.ToString();

        if (value > missedScores)
        {
            missedScores++;
        }
        else
        {
            missedScores--;
        }
    }

    private void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        chargeStationIndex = chargeStationPicker.SelectedIndex;
    }

    private void SetVariables()
    {

        if (def.IsToggled)
        {
            defense = true;
        }

        if (broken.IsToggled)
        {
            broke = true;
        }

        notes = notesTextBox.Text;
        
    }

    private async void OnSaveFileClicked(object sender, EventArgs e)
    {
        return;
    }

    private async void OnShowQRCodeClicked(object sender, EventArgs e)
    {
        SetVariables();
        var teleopChargeStaion = chargeStationIndex switch
        {
            0 => ChargeStationStatusTeleop.NoAttempt,
            1 => ChargeStationStatusTeleop.Failed,
            2 => ChargeStationStatusTeleop.NotEngaged,
            3 => ChargeStationStatusTeleop.Engaged,
            _ => ChargeStationStatusTeleop.NoAttempt,
        };

        var autoChargeStaion = autoChargeStation switch
        {
            0 => ChargeStationStatusAuto.NoAttempt,
            1 => ChargeStationStatusAuto.Failed,
            2 => ChargeStationStatusAuto.NotEngaged,
            3 => ChargeStationStatusAuto.Engaged,
            _ => ChargeStationStatusAuto.NoAttempt,
        };

        var m = new MatchData
        {
            MatchNum = matchNum,
            RobotNum = roboNum,
            AutoWork = functioningAuto,
            CoScoredA = autoConesScored,
            CuScoredA = autoCubesScored,
            MoveOutA = movedOutOfZone,
            Broke = broke,
            Def = defense,
            Notes = notes,
            ChargeT = teleopChargeStaion,
            ChargeA = autoChargeStaion,
            HCoScored = highCones,
            MCoScored = midCones,
            LCoScored = lowCones,
            HCuScored= highCubes,
            MCuScored= midCubes,
            LCuScored= lowCubes
        };

        appState.Matches.Add(m);

        await Navigation.PushAsync(new ShowQRCodePage(appState));
    }
}