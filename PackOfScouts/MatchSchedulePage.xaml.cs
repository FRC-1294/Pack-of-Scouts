using OpenCvSharp;

namespace PackOfScouts;

public partial class MatchSchedulePage : ContentPage
{
	private readonly List<ScheduleEntry> _entries;

    public MatchSchedulePage(List<ScheduleEntry> entries)
    {
        InitializeComponent();

        _entries = entries;

        AddHeader();

        foreach (var entry in _entries)
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

            g.Add(new Button
            {
                Text = entry.RedRobot1.ToString(),
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                FontSize = 20,
            }, 1);

            g.Add(new Button
            {
                Text = entry.RedRobot2.ToString(),
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                FontSize = 20,
            }, 2);

            g.Add(new Button
            {
                Text = entry.RedRobot3.ToString(),
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                FontSize = 20,
            }, 3);

            g.Add(new Button
            {
                Text = entry.BlueRobot1.ToString(),
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White,
                FontSize = 20,
            }, 4);

            g.Add(new Button
            {
                Text = entry.BlueRobot2.ToString(),
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White,
                FontSize = 20,
            }, 5);

            g.Add(new Button
            {
                Text = entry.BlueRobot3.ToString(),
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White,
                FontSize = 20,
            }, 6);

            _tableView.Root[0].Add(new ViewCell
            {
                View = g,
            });
        }
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
        });

        g.Add(new Label
        {
            Text = "Robots to scout",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 15,
        }, 1);

        _tableView.Root[0].Add(new ViewCell
        {
            View = g,
        });
    }
}
