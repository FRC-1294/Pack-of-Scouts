namespace PackOfScouts;

internal class ApplicationState
{
    internal List<MatchData> Matches { get; } = new();
    internal List<ScheduleEntry> ScheduleEntries { get; } = new();
}
