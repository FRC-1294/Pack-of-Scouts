namespace PackOfScouts;

public partial class Tele_op : ContentPage
{
	public Tele_op()
	{
		InitializeComponent();
	}

    void OnHighConeToggled(object sender, ValueChangedEventArgs e)
	{
        OnMidConeToggled(sender, e);
		MidCone.IsToggled = true;
	}

    void OnMidConeToggled(object sender, ValueChangedEventArgs e)
    {
		LowCone.IsToggled = true;
    }
}