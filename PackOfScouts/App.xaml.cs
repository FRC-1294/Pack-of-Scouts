using Microsoft.Maui.LifecycleEvents;

namespace PackOfScouts;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Title = "Pack of Scouts";
        window.Width = 800;
        window.Height = 600;

        // Center the window
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
        window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;

        return window;
    }
}
