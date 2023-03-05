namespace PackOfScouts;

internal class ApplicationState
{
    internal List<MatchData> Data { get; } = new();
    internal List<ScheduleEntry> Entries { get; } = new();
}
