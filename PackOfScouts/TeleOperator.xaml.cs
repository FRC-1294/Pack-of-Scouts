namespace PackOfScouts;

public partial class TeleOperator : ContentPage
{
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

	protected override void OnAppearing()
	{
		base.OnAppearing();
		System.Diagnostics.Debug.WriteLine("Hello");
	}
}