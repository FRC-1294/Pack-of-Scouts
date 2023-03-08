using System.Diagnostics;
using System.Net.Http.Headers;

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
        await SaveJsonToFileAsync(matches, "C:\\Users\\aksha\\Documents\\GitHub\\Pack-of-Scouts\\matches.json");

        Debug.WriteLine("Data saved to files successfully!");
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