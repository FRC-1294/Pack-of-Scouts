using Microsoft.Maui.Controls;
using System.Text.Json;
using static PackOfScouts.MatchData;

namespace PackOfScouts;

public partial class TeleOperator : ContentPage
{
	string notes = null;
    int conesScored = 0;
    int cubesScored = 0;
    int missedScores = 0;
    int chargeStationIndex = -1;
    int highestCube = -1;
    int highestCone = -1;
    bool defense = false;
    bool broke = false;
    int fouls = 0;
    int autoConesScored = 0;
    int autoCubesScored = 0;
    int functioningAuto = 0;
    int movedOutOfZone = 0;
    int autoChargeStation = -1;
    int matchNum = 0;
    int roboNum = 1294;

    private MatchData matchData = new MatchData();

    public TeleOperator(int cubesScored, int conesScored, int functioningAuto, int moveOutOfZone, int autoChargeStation, int matchNumber, int roboNumber)
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

    }


    public TeleOperator()
	{
		InitializeComponent();
	}

    void OnHighConeToggled(object sender, ToggledEventArgs e)
	{
		if (HighCone.IsToggled)
        {
            LowCone.IsToggled = true;
            MidCone.IsToggled = true;
            highestCone = 2;

        }
        else
        {
            LowCone.IsToggled = false;
            MidCone.IsToggled = false;
            highestCone = -1;

        }
    }

    private void OnMidConeToggled(object sender, ToggledEventArgs e)
    {
        if (MidCone.IsToggled) 
        {
            LowCone.IsToggled = true;
            highestCone = 1;
        }
        else
        {
            LowCone.IsToggled = false;
            highestCone= -1;
        }
    }

    void OnHighCubeToggled(object sender, ToggledEventArgs e)
    {
        if (HighCube.IsToggled)
        {
            LowCube.IsToggled = true;
            MidCube.IsToggled = true;
            highestCube = 2;
        }
        else
        {
            LowCube.IsToggled = false;
            MidCube.IsToggled = false;
            highestCube= -1;
        }
    }

    private void OnMidCubeToggled(object sender, ToggledEventArgs e)
    {
        if (MidCube.IsToggled)
        {
            LowCube.IsToggled = true;
            highestCube= 1;
        }
        else
        {
            LowCube.IsToggled = false;
            highestCube=-1;
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

    void OnStepperValueChangedMisses(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            _displayMistakesLabel.Text = string.Format("{0} miss", value);
        }
        else
        {
            _displayMistakesLabel.Text = string.Format("{0} misses", value);
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

    void OnStepperValueChangedFouls(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            _displayFoulLabel.Text = string.Format("{0} foul", value);
        }
        else
        {
            _displayFoulLabel.Text = string.Format("{0} fouls", value);
        }
        if(value > fouls)
        {
            fouls++;
        } else
        {
            fouls--;
        }
    }

    void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        chargeStationIndex = chargeStationPicker.SelectedIndex;
    }

    void SetVariables()
    {
        if (highestCube == -1 && LowCube.IsToggled)
        {
            highestCube = 0;
            
        }
        if (highestCone == -1 && LowCone.IsToggled)
        {
            highestCone = 0;
        }
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
            -1 => ChargeStationStatusTeleop.NoAttempt,
            0 => ChargeStationStatusTeleop.NoAttempt,
            1 => ChargeStationStatusTeleop.Failed,
            2 => ChargeStationStatusTeleop.NotEngaged,
            3 => ChargeStationStatusTeleop.Engaged
        };

        var autoChargeStaion = autoChargeStation switch
        {
            -1 => ChargeStationStatusAuto.NoAttempt,
            0 => ChargeStationStatusAuto.NoAttempt,
            1 => ChargeStationStatusAuto.Failed,
            2 => ChargeStationStatusAuto.NotEngaged,
            3 => ChargeStationStatusAuto.Engaged
        };

        var highCone = highestCone switch
        {
            -1 => HighestConeScored.None,
            0 => HighestConeScored.Low,
            1 => HighestConeScored.Mid,
            2 => HighestConeScored.High
        };

        var highCube = highestCube switch
        {
            -1 => HighestCubeScored.None,
            0 => HighestCubeScored.Low,
            1 => HighestCubeScored.Mid,
            2 => HighestCubeScored.High
        };

        var m = new MatchData
        {
            MatchNumber = matchNum,
            RobotNumber = roboNum,
            FunctioningAuto = functioningAuto,
            ConesScoredAuto = autoConesScored,
            ConesScoredTeleop = conesScored,
            CubesScoredAuto = autoCubesScored,//
            CubesScoredTeleop = cubesScored,//
            MovedOutOfZoneAuto = movedOutOfZone,
            Broke = broke,
            Defense = defense,
            Fouls = fouls,
            Notes = notes,
            ChargeStationTeleop = teleopChargeStaion,
            ChargeStationAuto = autoChargeStaion,
            ConeHeight = highCone,
            CubeHeight = highCube,
        };

        
        

        var json = System.Text.Json.JsonSerializer.Serialize(m);
        await Navigation.PushAsync(new ShowQRCodePage(json));

       
    }
}