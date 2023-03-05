namespace PackOfScouts;

public enum ChargeStationStatusAuto { NoAttempt, Failed, NotEngaged, Engaged }
public enum ChargeStationStatusTeleop { NoAttempt, Failed, NotEngaged, Engaged }
public enum HighestConeScored { None, Low, Mid, High }
public enum HighestCubeScored { None, Low, Mid, High }

public sealed class MatchData
{
    public int MatchNumber { get; set; }
    public int RobotNumber { get; set; }
    public int FunctioningAuto { get; set; }
    public ChargeStationStatusAuto ChargeStationAuto { get; set; }
    public ChargeStationStatusTeleop ChargeStationTeleop { get; set; }
    public int ConesScoredAuto { get; set; }
    public int CubesScoredAuto { get; set; }
    public int HighCubesScored { get; set; }
    public int MidCubesScored { get; set; } 
    public int LowCubesScored { get; set; }
    public int HighConesScored { get; set; }
    public int MidConesScored { get; set; }
    public int LowConesScored { get; set; }
    public int MovedOutOfZoneAuto { get; set; }
    public bool Broke { get; set; }
    public bool Defense { get; set; }
    public String? Notes { get; set; }
}
