namespace PackOfScouts;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = 600;
        window.Height = 500;
        window.Title = "Pack of Scouts";

        return window;
    }
}
