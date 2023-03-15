using System.Diagnostics;
using System.Text.Json;

namespace PackOfScouts;

public partial class ShowQRCodePagePit : ContentPage
{
    readonly ApplicationState appState;
    int count = -1;

    internal ShowQRCodePagePit(ApplicationState applicationState)
    {
        InitializeComponent();
        this.appState = applicationState;
        this.count++;
        
        var json = JsonSerializer.Serialize(this.appState.Teams[count]);
        

        _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
        if (this.appState.Teams.Count == 1)
        {
            NextQrCode.IsVisible = false;
        }
    }



    private void OnNextQrCodePressedPit(object sender, EventArgs e)
    {

        if (this.count < this.appState.Teams.Count - 1)
        {
            PreviousQrCode.IsVisible = true;
            this.count++;
            var json = JsonSerializer.Serialize(this.appState.Teams[count]);
            _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
            if (this.count == this.appState.Teams.Count - 1)
            {
                NextQrCode.IsVisible = false;
            }
        }

    }


   


    private void OnPreviousQrCodePressedPit(object sender, EventArgs e)
    {


        if (this.count > 0)
        {
            this.count--;
            NextQrCode.IsVisible = true;
            var json = JsonSerializer.Serialize(this.appState.Teams[count]);
            _qrImage.Source = ImageSource.FromStream(() => QrCode.QrCodeUtils.MakeQrCode(json));
            if (this.count == 0)
            {
                PreviousQrCode.IsVisible = false;
            }
        }


    }


    private void OnDonePressedPit(object sender, EventArgs e)
    {
        Done.IsVisible = false;
        YouSure.IsVisible = true;
        ImSure.IsVisible = true;
        NotSure.IsVisible = true;
    }

    

    private async void OnImSurePressedPit(object sender, EventArgs e)
    {
        this.appState.Teams.Clear();

        var text = JsonSerializer.Serialize(appState.Teams);
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_PitData.json");
        File.WriteAllText(path, text);

        List<Page> list = new();
        foreach (Page page in Navigation.NavigationStack)
        {
            if (page is PitScoutingPage || page is ShowQRCodePagePit)
            {
                list.Add(page);
            }
        }

        foreach (Page _page in list)
        {

            Navigation.RemovePage(_page);
            
        }

        await Navigation.PushAsync(new PitSchedulePage(appState));

    }

    private void OnNotSurePressedPit(Object sender, EventArgs e)
    {
        Done.IsVisible = true;
        YouSure.IsVisible = false;
        ImSure.IsVisible = false;
        NotSure.IsVisible = false;
    }
}
