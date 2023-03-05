namespace PackOfScouts;

internal enum ChargeStationStatusAuto { NoAttempt, Failed, NotEngaged, Engaged }
internal enum ChargeStationStatusTeleop { NoAttempt, Failed, NotEngaged, Engaged }
internal enum HighestConeScored { None, Low, Mid, High }
internal enum HighestCubeScored { None, Low, Mid, High }

internal sealed class MatchData
{
    internal int MatchNumber { get; set; }
    internal int RobotNumber { get; set; }
    internal int FunctioningAuto { get; set; }
    internal ChargeStationStatusAuto ChargeStationAuto { get; set; }
    internal ChargeStationStatusTeleop ChargeStationTeleop { get; set; }
    internal int ConesScoredAuto { get; set; }
    internal int CubesScoredAuto { get; set; }
    internal int HighCubesScored { get; set; }
    internal int MidCubesScored { get; set; } 
    internal int LowCubesScored { get; set; }
    internal int HighConesScored { get; set; }
    internal int MidConesScored { get; set; }
    internal int LowConesScored { get; set; }
    internal int MovedOutOfZoneAuto { get; set; }
    internal bool Broke { get; set; }
    internal bool Defense { get; set; }
    internal String? Notes { get; set; }
    //internal int Fouls { get; set; }

    //internal int TechFouls { get; set; }
    // NONE, YELLOW, RED
    //internal String Cards { get; set; }
}
