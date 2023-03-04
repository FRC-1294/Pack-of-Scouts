namespace PackOfScouts;

public enum ChargeStationStatusAuto { NoAttempt, Failed, NotEngaged, Engaged }
public enum ChargeStationStatusTeleop { NoAttempt, Failed, NotEngaged, Engaged }
public enum HighestConeScored { None, Low, Mid, High }
public enum HighestCubeScored { None, Low, Mid, High }

internal sealed class MatchData
{
    public int MatchNumber { get; set; }
    public int RobotNumber { get; set; }
    public int FunctioningAuto { get; set; }
    public ChargeStationStatusAuto ChargeStationAuto { get; set; }
    public ChargeStationStatusTeleop ChargeStationTeleop { get; set; }
    public int ConesScoredAuto { get; set; }
    public int ConesScoredTeleop { get; set; }
    public int CubesScoredAuto { get; set; }
    public int CubesScoredTeleop { get; set; }
    public HighestConeScored ConeHeight { get; set; }
    public HighestCubeScored CubeHeight { get; set; }
    public int MovedOutOfZoneAuto { get; set; }
    public bool Broke { get; set; }
    public bool Defense { get; set; }
    public String? Notes { get; set; }
    public int Fouls { get; set; }
    //public int TechFouls { get; set; }
    // NONE, YELLOW, RED
    //public String Cards { get; set; }
}
