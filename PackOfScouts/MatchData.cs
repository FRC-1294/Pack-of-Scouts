namespace PackOfScouts;

internal sealed class MatchData
{
    public int MatchNumber { get; set; }
    public int RobotNumber { get; set; }
    public int FunctioningAuto { get; set; }
    public int ChargeStationPointsAuto { get; set; }
    public int ChargeStationPointsTeleop { get; set; }
    public int ConesScoredLowAuto { get; set; }
    public int ConesScoredMidAuto { get; set; }
    public int ConesScoredHighAuto { get; set; }
    public int CubesScoredLowAuto { get; set; }
    public int CubesScoredMidAuto { get; set; }
    public int CubesScoredHighAuto { get; set; }
    public int ConesScoredLowTeleop { get; set; }
    public int ConesScoredMidTeleop { get; set; }
    public int ConesScoredHighTeleop { get; set; }
    public int CubesScoredLowTeleop { get; set; }
    public int CubesScoredMidTeleop { get; set; }
    public int CubesScoredHighTeleop { get; set; }
    public String Notes { get; set; }
    public int Fouls { get; set; }
    public int TechFouls { get; set; }
    // NONE, YELLOW, RED
    public String Cards { get; set; }

    public void CreateFile()
    {
        string path = @"C:\Users\user\randomData";
        using StreamWriter outputFile = new(path + RobotNumber + "_" + MatchNumber);
        outputFile.WriteLine(MatchNumber);
        outputFile.WriteLine(RobotNumber);
        outputFile.WriteLine(FunctioningAuto);
        outputFile.WriteLine(ChargeStationPointsAuto);
        outputFile.WriteLine(ChargeStationPointsTeleop);
    }
}
