namespace PackOfScouts;

public partial class PitSchedulePage : ContentPage
{
    readonly ApplicationState appState;

    internal PitSchedulePage(ApplicationState applicationState)
    {
        InitializeComponent();

        this.appState = applicationState;

        foreach (var entry in appState.TeamEntries)
        {
            var g = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = GridLength.Auto
                    }
                },

                ColumnDefinitions =
                {
                    

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    }

                }
            };

           

            AddRobotButton(g, 1, Colors.Red, entry.TeamNumber);
            

            _tableView.Root[0].Add(new ViewCell
            {
                View = g,
            });
        }
    }

    private void AddRobotButton(Grid g, int column, Color color, int robotNum)
    {
        var b = new Button
        {
            Text = robotNum.ToString(),
            BackgroundColor = color,
            TextColor = Colors.White,
            FontSize = 20,
            
            CommandParameter = new ButtonInfo
            {
                
                RobotNum = robotNum,
            },
        };

        b.Clicked += TeamClicked;
        g.Add(b, column);
    }

    private async void TeamClicked(object? sender, EventArgs e)
    {
        var b = (Button)sender!;
        var bi = (ButtonInfo)b.CommandParameter;
        await Navigation.PushAsync(new PitScoutingPage(bi.RobotNum, this.appState));
    }

    private sealed class ButtonInfo
    {
        
        internal int RobotNum;
    }
}