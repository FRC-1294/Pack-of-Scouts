using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace PackOfScouts;


public partial class ImportDataPage : ContentPage
{
    private string? compid="WASNO";
    public ImportDataPage()
	{
		InitializeComponent();
	}

    private async void OnImportDataClicked(object sender, EventArgs e)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://frc-api.firstinspires.org/v3.0/2023/");

        // Set up request headers
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("akshaisrinivasan:e1bcd614-4086-4c40-9754-8448161d9f5e")));
        
        var matches = await GetJsonAsync(httpClient, "schedule/" + compid + "?tournamentLevel=Qualification");

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PackOfScouts_MatchSchedule_{compId}.json");
        await SaveJsonToFileAsync(matches, path);

        Debug.WriteLine($"Data saved to '{path}' successfully!");

        await DisplayAlert("Schedule saved!", $"Schedule saved to\n{path}", "OK");
        _ = await Navigation.PopAsync();
    }

    static async Task<string> GetJsonAsync(HttpClient httpClient, string path)
    {
        var response = await httpClient.GetAsync(path);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return json;
    }

    static async Task SaveJsonToFileAsync(string json, string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        using var streamWriter = File.CreateText(filePath);
        await streamWriter.WriteAsync(json);
    }

    private void onCompIDTextChanged(object sender, TextChangedEventArgs e)
    {
        compid = e.NewTextValue;
    }
}