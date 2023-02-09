namespace PackOfScouts;

public partial class Tele_op : ContentPage
{
	public Tele_op()
	{
		InitializeComponent();
	}

    void OnHighConeToggled(object sender, ToggledEventArgs e)
	{
		LowCone.IsToggled = true;
		MidCone.IsToggled = true;
	}

    void OnMidConeToggled(object sender, ToggledEventArgs e)
    {
		LowCone.IsToggled = true;
    }
}