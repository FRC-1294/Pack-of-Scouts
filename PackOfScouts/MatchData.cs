namespace PackOfScouts;

public enum ChargeStationStatusAuto { NoAttempt, Failed, NotEngaged, Engaged }
public enum ChargeStationStatusTeleop { NoAttempt, Failed, NotEngaged, Engaged }
public enum HighestConeScored { None, Low, Mid, High }
public enum HighestCubeScored { None, Low, Mid, High }

public sealed class MatchData
{
    public int MatchNum { get; set; }
    public int RobotNum { get; set; }
    public int AutoWork { get; set; } //functioning auto
    public ChargeStationStatusAuto ChargeA { get; set; } //Charge station auto
    public ChargeStationStatusTeleop ChargeT { get; set; } //charge station teleop
    public int CoScoredA { get; set; } //cones scored auto
    public int CuScoredA { get; set; } //cubes scored auto
    public int HCuScored { get; set; }//high subes scored
    public int MCuScored { get; set; } //mid cubes scored
    public int LCuScored { get; set; }//low cubes scoted
    public int HCoScored { get; set; }//high cones scored
    public int MCoScored { get; set; }//mid cones scored
    public int LCoScored { get; set; }//low cones scored
    public int MoveOutA { get; set; } //move out of zone auto
    public bool Broke { get; set; }
    public bool Def { get; set; } //defense
    public String? Notes { get; set; }
    //public int Fouls { get; set; }

    //public int TechFouls { get; set; }
    // NONE, YELLOW, RED
    //public String Cards { get; set; }
}
