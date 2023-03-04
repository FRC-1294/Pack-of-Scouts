namespace PackOfScouts;

public partial class TeleOperator : ContentPage
{
	string? notes;
    int missedScores;
    int chargeStationIndex = -1;
    bool defense;
    bool broke;
    
    readonly List<ScheduleEntry> entries = new();

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

    public TeleOperator(int cubesScored, int conesScored, int functioningAuto, int moveOutOfZone, int autoChargeStation, int matchNumber, int roboNumber, List<ScheduleEntry> entries)
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
        this.entries = entries;
        ScoutingTeamLabel.Text = "TEAM " + Convert.ToString(this.roboNum);

    }

    public TeleOperator()
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
            MatchNumber = matchNum,
            RobotNumber = roboNum,
            FunctioningAuto = functioningAuto,
            ConesScoredAuto = autoConesScored,
            CubesScoredAuto = autoCubesScored,
            MovedOutOfZoneAuto = movedOutOfZone,
            Broke = broke,
            Defense = defense,
            //Fouls = fouls,
            Notes = notes,
            ChargeStationTeleop = teleopChargeStaion,
            ChargeStationAuto = autoChargeStaion,
            HighConesScored = highCones,
            MidConesScored = midCones,
            LowConesScored = lowCones,
            HighCubesScored= highCubes,
            MidCubesScored= midCubes,
            LowCubesScored= lowCubes
        };

        var json = System.Text.Json.JsonSerializer.Serialize(m);
        await Navigation.PushAsync(new ShowQRCodePage(json, entries));
    }
}