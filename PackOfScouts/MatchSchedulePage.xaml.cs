namespace PackOfScouts;

public partial class MatchSchedulePage : ContentPage
{
    readonly ApplicationState appState;

    internal MatchSchedulePage(ApplicationState applicationState)
    {
        InitializeComponent();

        this.appState = applicationState;

        AddHeader();

        foreach (var entry in appState.ScheduleEntries)
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
                    },

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    },

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    },

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    },

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    },

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    },

                    new ColumnDefinition
                    {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    },
                }
            };

            g.Add(new Label
            {
                Text = entry.MatchNumber.ToString(),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
            });

            AddRobotButton(g, 1, Colors.Red, entry.MatchNumber, entry.RedRobot1);
            AddRobotButton(g, 2, Colors.Red, entry.MatchNumber, entry.RedRobot2);
            AddRobotButton(g, 3, Colors.Red, entry.MatchNumber, entry.RedRobot3);
            AddRobotButton(g, 4, Colors.Blue, entry.MatchNumber, entry.BlueRobot1);
            AddRobotButton(g, 5, Colors.Blue, entry.MatchNumber, entry.BlueRobot2);
            AddRobotButton(g, 6, Colors.Blue, entry.MatchNumber, entry.BlueRobot3);

            _tableView.Root[0].Add(new ViewCell
            {
                View = g,
            });
        }
    }

    private void AddRobotButton(Grid g, int column, Color color, int matchNum, int robotNum)
    {
        var b = new Button
        {
            Text = robotNum.ToString(),
            BackgroundColor = color,
            TextColor = Colors.White,
            FontSize = 20,
            CommandParameter = new ButtonInfo
            {
                MatchNum = matchNum,
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
        await Navigation.PushAsync(new ScoutPage(bi.MatchNum, bi.RobotNum, this.appState));
    }

    private void AddHeader()
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
                },

                new ColumnDefinition
                {
                    Width = new GridLength(80 * 6, GridUnitType.Absolute)
                },

            }
        };

        g.Add(new Label
        {
            Text = "Match #",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 15,
            TextColor = new Color(255, 255, 255)
        });

        g.Add(new Label
        {
            Text = "Robots to scout",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 15,
            TextColor = new Color(255, 255, 255)
        }, 1);

        _tableView.Root[0].Add(new ViewCell
        {
            View = g,
        });
    }

    private sealed class ButtonInfo
    {
        internal int MatchNum;
        internal int RobotNum;
    }
}
