namespace PackOfScouts;
//using static CommunityToolkit.Maui.Markup.GridRowsColumns;

public partial class MatchSchedule : ContentPage
{
    private string[] listOfTeams;
	public MatchSchedule()
	{
		InitializeComponent();
		var match = (Grid)FindByName("test");
        var rows = new RowDefinitionCollection();
        var cols = new ColumnDefinitionCollection();

        for (int i = 0; i < 7; i++)
        {
            ColumnDefinition tempCol = new ColumnDefinition
            {
                Width = GridLength.Star,
            };
            cols.Add(tempCol);
            match.AddColumnDefinition(tempCol);
        }
        //listOfTeams = wait for file see what it looks like - dict, other

        foreach (string alliances in listOfTeams)
        {
            
            RowDefinition nextRow = new RowDefinition
            {
                Height = GridLength.Star
            };

            rows.Add(nextRow);

            //new Button().Text = alliances Row(0);

            match.AddRowDefinition(nextRow);
            match.Children.Add(new Button { Text = alliances });
            //match.Children.Add(new RowDefinition();

            /*make interative loop with ints
            new Label()
            .Text("This Label is in Row 0 Column 0")
            .Row(0).Column(0) */
        }

        for(int row = 0; row < rows.Count; row++)
        {
            for (int col = 0; col < cols.Count; col++)
            {

            }
        }
    }
}