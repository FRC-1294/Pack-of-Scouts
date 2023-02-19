using Microsoft.Maui.Controls;

namespace PackOfScouts;

public partial class TeleOperator : ContentPage
{
	string notes = null;
    int conesScored = 0;
    int cubesScored = 0;
    int missedScores = 0;
    string chargeStationStatus = null;
    public TeleOperator()
	{
		InitializeComponent();
	}

    void OnHighConeToggled(object sender, ToggledEventArgs e)
	{
		LowCone.IsToggled = true;
		MidCone.IsToggled = true;
	}

    private void OnMidConeToggled(object sender, ToggledEventArgs e)
    {
		LowCone.IsToggled = true;
    }

    void OnHighCubeToggled(object sender, ToggledEventArgs e)
    {
        LowCube.IsToggled = true;
        MidCube.IsToggled = true;
    }

    private void OnMidCubeToggled(object sender, ToggledEventArgs e)
    {
        LowCube.IsToggled = true;
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

    void OnChargeStationStatusChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            chargeStationStatus = (string)picker.ItemsSource[selectedIndex];
        }
    }
}