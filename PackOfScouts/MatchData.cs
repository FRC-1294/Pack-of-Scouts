

namespace PackOfScouts;

public class MatchData {
    internal int matchNumber;
    internal int robotNumber;
    internal int functioningAuto;
    internal int chargeStationPointsAuto;
    internal int chargeStationPointsTeleop;
    internal int conesScoredLowAuto;
    internal int conesScoredMidAuto;
    internal int conesScoredHighAuto;
    internal int cubesScoredLowAuto;
    internal int cubesScoredMidAuto;
    internal int cubesScoredHighAuto;
    internal int conesScoredLowTeleop;
    internal int conesScoredMidTeleop;
    internal int conesScoredHighTeleop;
    internal int cubesScoredLowTeleop;
    internal int cubesScoredMidTeleop;
    internal int cubesScoredHighTeleop;
    internal String notes;
    internal int fouls;
    internal int tech_fouls;
    // NONE, YELLOW, RED
    internal String cards;



    //NOT DONE YET
    public MatchData(int matchNumber, 
        int robotNumber,
        int functioningAuto, 
        int chargeStationPointsAuto, 
        int chargeStationPointsTeleop, 
        int conesScoredLowAuto,
        int conesScoredMidAuto,
        int conesScoredHighAuto,
        int cubesScoredLowAuto,
        int cubesScoredMidAuto,
        int cubesScoredHighAuto,
        int cubesScoredLowTeleop,
        int cubesScoredMidTeleop,
        int cubesScoredHighTeleop,
        int conesScoredLowTeleop,
        int conesScoredMidTeleop,
        int conesScoredHighTeleop,
        String notes) {
        this.matchNumber = matchNumber;
        this.robotNumber = robotNumber;
        this.functioningAuto = functioningAuto;
        this.chargeStationPointsTeleop= chargeStationPointsTeleop;
        this.chargeStationPointsAuto = chargeStationPointsAuto;
        this.conesScoredLowAuto = conesScoredLowAuto;
        this.conesScoredLowTeleop = conesScoredLowTeleop;
        this.conesScoredMidAuto = conesScoredMidAuto;
        this.conesScoredMidTeleop = conesScoredMidTeleop;
        this.conesScoredHighTeleop = conesScoredHighTeleop;
        this.conesScoredHighAuto = conesScoredHighAuto;
        this.cubesScoredLowAuto = cubesScoredLowAuto;
        this.cubesScoredLowTeleop = cubesScoredLowTeleop;
        this.cubesScoredMidAuto = cubesScoredMidAuto;
        this.cubesScoredMidTeleop = cubesScoredMidTeleop;
        this.cubesScoredHighTeleop = cubesScoredHighTeleop;
        this.cubesScoredHighAuto = cubesScoredHighAuto;
        this.notes = notes;
        
    }

    public void createFile() {
        string path = @"C:\Users\user\randomData";
        using (StreamWriter outputFile = new StreamWriter(path + robotNumber + "_" + matchNumber)) {
            outputFile.WriteLine(matchNumber);
            outputFile.WriteLine(robotNumber);
            outputFile.WriteLine(functioningAuto);
            outputFile.WriteLine(chargeStationPointsAuto);
            outputFile.WriteLine(chargeStationPointsTeleop);
            
        }
    }



}

