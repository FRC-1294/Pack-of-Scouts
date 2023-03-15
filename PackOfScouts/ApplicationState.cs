namespace PackOfScouts;

internal class ApplicationState
{
    internal List<MatchData> Matches { get; } = new();
    internal List<ScheduleEntry> ScheduleEntries { get; } = new();

    internal List<PitData> Teams { get; } = new();

    internal List<TeamEntry> TeamEntries { get; } = new();

}
