using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace PackOfScouts;

public partial class TeleOperator : ContentPage
{
	string? notes;
    int missedScores;
    int chargeStationIndex = -1;
    bool defense;
    bool broke;

    private ApplicationState appState;
    int highCones;
    int midCones;
    int lowCones;
    int highCubes;
    int midCubes;
    int lowCubes;

    readonly int autoConesScored;
    readonly int autoCubesScored;
    readonly int functioningAuto;
    readonly int movedOutOfZone;
    readonly int autoChargeStation;
    readonly int matchNum;
    readonly int roboNum;

    internal TeleOperator(int cubesScored, int conesScored, int functioningAuto, int moveOutOfZone, int autoChargeStation, int matchNumber, int roboNumber, ApplicationState applicationState)
        : this()
    {
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
        if (appState.Data != null)
        {
            numOfMatches.Text = string.Format("Matches scouted: {0}", appState.Data.Count);
        }
        else
        {
            numOfMatches.Text = "Matches scouted: 0";
        }

    }

    internal TeleOperator()
	{
		InitializeComponent();
	}

    void OnStepperValueChangedHighCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            HighCone.Text = string.Format("{0}", value);
        }
        else
        {
            HighCone.Text = string.Format("{0}", value);
        }

        if (value > highCones)
        {
            highCones++;
        }
        else
        {
            highCones--;
        }
    }

    void OnStepperValueChangedMidCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        MidCone.Text = string.Format("{0}", value);
        
        if (value > midCones)
        {
            midCones++;
        }
        else
        {
            midCones--;
        }
    }

    void OnStepperValueChangedLowCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        LowCone.Text = string.Format("{0}", value);
        
        if (value > lowCones)
        {
            lowCones++;
        }
        else
        {
            lowCones--;
        }
    }

    void OnStepperValueChangedHighCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            HighCube.Text = string.Format("{0}", value);
        }
        else
        {
            HighCube.Text = string.Format("{0}", value);
        }

        if (value > highCubes)
        {
            highCubes++;
        }
        else
        {
            highCubes--;
        }
    }

    void OnStepperValueChangedMidCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        MidCube.Text = string.Format("{0}", value);

        if (value > midCubes)
        {
            midCubes++;
        }
        else
        {
            midCubes--;
        }
    }

    void OnStepperValueChangedLowCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        LowCube.Text = string.Format("{0}", value);

        if (value > lowCubes)
        {
            lowCubes++;
        }
        else
        {
            lowCubes--;
        }
    }

    protected override void OnAppearing()
	{
		base.OnAppearing();
		System.Diagnostics.Debug.WriteLine("Hello");
	}

    void OnNotesTextChanged(object sender, TextChangedEventArgs e)
    {
        notes = e.NewTextValue;
        CharacterCount.Text = string.Format("Characters remaining: {0}", (200 - notes.Length));
    }

    

    void OnStepperValueChangedMisses(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            _displayMistakesLabel.Text = string.Format("{0}", value);
        }
        else
        {
            _displayMistakesLabel.Text = string.Format("{0}", value);
        }

        if (value > missedScores)
        {
            missedScores++;
        }
        else
        {
            missedScores--;
        }
    }

    //void OnStepperValueChangedFouls(object sender, ValueChangedEventArgs e)
    //{
    //    double value = e.NewValue;
        
    //     _displayFoulLabel.Text = string.Format("{0}", value);
    //    if(value > fouls)
    //    {
    //        fouls++;
    //    } else
    //    {
    //        fouls--;
    //    }
    //}

    void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        chargeStationIndex = chargeStationPicker.SelectedIndex;
    }

    void SetVariables()
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
            //Fouls = fouls,
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
        if (appState.Data == null) {
            appState.Data = new List<MatchData> { m };
        } else
        {
            appState.Data.Add(m);
        }

        var json = System.Text.Json.JsonSerializer.Serialize(appState.Data);
        await Navigation.PushAsync(new ShowQRCodePage(json, appState));
    }
}