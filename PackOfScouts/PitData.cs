namespace PackOfScouts;

public enum HighestConeCapabilities { None, Low, Mid, High }
public enum HighestCubeCapabilities { None, Low, Mid, High }

public enum DriveBase {WestCoast,Mecanum, Swerve, Other }



public sealed class PitData
{
    public int RobotNum { get; set; }
    public DriveBase driveBase {get;set;}
    
    public int AutoCubes { get; set; }
    
    public int AutoCones { get; set; }//functioning auto
    
    public bool AutoChargeStation { get; set; }

    public bool TeleopChargeStation { get; set; }

    public HighestConeCapabilities ConeCap { get; set; } //Charge station auto
    public HighestCubeCapabilities CubeCap { get; set; }//charge station teleop
   

    
}