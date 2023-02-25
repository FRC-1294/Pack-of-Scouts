using Microsoft.Maui.Controls;

namespace PackOfScouts;

public partial class TeleOperator : ContentPage
{
	string notes = null;
    int conesScored = 0;
    int cubesScored = 0;
    int missedScores = 0;
    string chargeStationStatus = null;
    string highestCube = null;
    string highestCone = null;
    bool defense = false;
    bool broke = false;
    int fouls = 0;

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
            highestCone = "high cone";

        }
        else
        {
            LowCone.IsToggled = false;
            MidCone.IsToggled = false;
            highestCone = null;

        }
    }

    private void OnMidConeToggled(object sender, ToggledEventArgs e)
    {
        if (MidCone.IsToggled) 
        {
            LowCone.IsToggled = true;
            highestCone = "mid cone";
        }
        else
        {
            LowCone.IsToggled = false;
            highestCone= null;
        }
    }

    void OnHighCubeToggled(object sender, ToggledEventArgs e)
    {
        if (HighCube.IsToggled)
        {
            LowCube.IsToggled = true;
            MidCube.IsToggled = true;
            highestCube= "high cube";
        }
        else
        {
            LowCube.IsToggled = false;
            MidCube.IsToggled = false;
            highestCube= null;
        }
    }

    private void OnMidCubeToggled(object sender, ToggledEventArgs e)
    {
        if (MidCube.IsToggled)
        {
            LowCube.IsToggled = true;
            highestCube= "mid cube";
        }
        else
        {
            LowCube.IsToggled = false;
            highestCube=null;
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

    void OnStepperValueChangedMisses(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        if (value == 1)
        {
            _displayMistakesLabel.Text = string.Format("{0} cube scored", value);
        }
        else
        {
            _displayMistakesLabel.Text = string.Format("{0} cubes scored", value);
        }
        missedScores++;
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
        fouls++;
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

    void SetVariables()
    {
        if (highestCube == null && LowCube.IsToggled)
        {
            highestCube = "low cone";
            
        }
        if (highestCone == null && LowCone.IsToggled)
        {
            highestCone = "low cone";
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
        await Navigation.PushAsync(new ShowQRCodePage("https://www.google.com"));
    }
}