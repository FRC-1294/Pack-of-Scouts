using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace PackOfScouts;

public partial class PitScoutingPage : ContentPage
{
    private readonly ApplicationState _appState;
    private readonly int robotNum;
    private bool autoChargeStation;
    private bool teleopChargeStation;
    private int driveBaseIndex = 0;
    private int highestConeIndex = 0;
    private int highestCubeIndex = 0;
    private int autoCones = 0;
    private int autoCubes = 0;
    
    internal PitScoutingPage(int robotNum, ApplicationState appState)
	{
        
        
        _appState = appState;
        InitializeComponent();
        


        driveBasePicker.SelectedItem = "Swerve";
        coneCapPicker.SelectedItem = "None";
        cubeCapPicker.SelectedItem = "None";
        this.robotNum = robotNum;
        ScoutingTeamLabel.Text = $"PIT SCOUTING TEAM {this.robotNum}";
    }

    private void OnStepperValueChangedAutoCone(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;

        _displayAutoConeLabel.Text = string.Format("{0}", value);

        if (value > autoCones)
        {
            autoCones++;
        }
        else
        {
            autoCones--;
        }
    }

    private void OnStepperValueChangedAutoCube(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        _displayAutoCubeLabel.Text = string.Format("{0}", value);

        if (value > autoCubes)
        {
            autoCubes++;
        }
        else
        {
            autoCubes--;
        }
    }

    private void OnChargeStationAutoChanged(object sender, EventArgs e)
    {
        autoChargeStation = true;
    }

    private void OnChargeStationTeleopChanged(object sender, EventArgs e)
    {
        teleopChargeStation = true;
    }

    private void OnHighestConesChanged(object sender, EventArgs e)
    {
        highestConeIndex = coneCapPicker.SelectedIndex;
    }

    private void OnHighestCubesChanged(object sender, EventArgs e)
    {
        highestConeIndex = cubeCapPicker.SelectedIndex;
    }

    private void OnDriveBaseChanged(object sender, EventArgs e)
    {
        driveBaseIndex = driveBasePicker.SelectedIndex;
    }

    private void AddMatchData()
    {
        var Drivebase = driveBaseIndex switch

        {
            0 => DriveBase.Swerve,
            1 => DriveBase.Mecanum,
            2 => DriveBase.WestCoast,
            3 => DriveBase.Other,
            _ => DriveBase.Other,
        }; 


        var ConeCapabilities = highestConeIndex switch
        {
            0 => HighestConeCapabilities.None,
            1 => HighestConeCapabilities.Low,
            2 => HighestConeCapabilities.Mid,
            3 => HighestConeCapabilities.High,
            _ => HighestConeCapabilities.None,
        };

        var CubeCapabilities = highestCubeIndex switch
        {
            0 => HighestCubeCapabilities.None,
            1 => HighestCubeCapabilities.Low,
            2 => HighestCubeCapabilities.Mid,
            3 => HighestCubeCapabilities.High,
            _ => HighestCubeCapabilities.None,
        };
        var m = new PitData
        {

            RobotNum = this.robotNum,
            driveBase = Drivebase,
            AutoCubes=this.autoCubes,
            AutoCones=this.autoCones,
            AutoChargeStation=this.autoChargeStation,
            TeleopChargeStation=this.teleopChargeStation,
            ConeCap=ConeCapabilities,
            CubeCap=CubeCapabilities,



        };
        _appState.Teams.Add(m);

        var text = JsonSerializer.Serialize(_appState.Teams);
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_PitData.json");
        File.WriteAllText(path, text);
    }

    private async void OnNextTeamClicked(object sender, EventArgs e)
    {
        AddMatchData();
        List<Page> list = new();

        foreach (Page page in Navigation.NavigationStack)
        {
            if (page is PitScoutingPage)
            {
                list.Add(page);
            }
        }

        foreach (Page _page in list)
        {
            Navigation.RemovePage(_page);
        }

        await Navigation.PushAsync(new PitSchedulePage(_appState));
    }

    private async void OnShowQRCodeClicked(object sender, EventArgs e)
    {
        AddMatchData();
        await Navigation.PushAsync(new ShowQRCodePagePit(_appState));
    }




}