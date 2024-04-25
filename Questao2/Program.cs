
using System.Text.Json;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static async Task<int> getTotalScoredGoalsAsync(string team, int year)
    {
        var totalPages = GetTotalPages(team, year);
        var tasks = new List<Task<int>>();

        for (int teamNumber = 1; teamNumber <= 2; teamNumber++)
        {
            for (int i = 1; i <= totalPages; i++)
            {
                tasks.Add(GetGoalsFromPageAsync(team, year, i, teamNumber));
            }
        }
        await Task.WhenAll(tasks);

        return tasks.Sum(t => t.Result);
    }

    private static async Task<int> GetGoalsFromPageAsync(string team, int year, int page, int teamNumber)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{teamNumber}={team}&page={page}");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseSerializer = JsonSerializer.Deserialize<FootballMatchResult>(responseBody);

            return responseSerializer!.data.Sum(match => int.Parse(teamNumber == 1 ? match.team1goals : match.team2goals));
        }
        else
        {
            Console.WriteLine("Erro: Código de status HTTP " + (int)response.StatusCode);
            return 0;
        }
    }

    private static int GetTotalPages(string team, int year)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}").Result;
        int totalPages = 0;

        if (response.IsSuccessStatusCode)
        {
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var responseSerializer = JsonSerializer.Deserialize<FootballMatchResult>(responseBody);

            totalPages = responseSerializer!.total_pages;
        }
        else
        {
            Console.WriteLine("Erro: Código de status HTTP " + (int)response.StatusCode);
        }

        return totalPages;
    }

    class FootballMatchResult
    {
        public List<FootballMatch> data { get; set; }
        public int total_pages { get; set; }
    }

    class FootballMatch
    {
        public string team1goals { get; set; }
        public string team2goals { get; set; }
        // outras propriedades omitidas por brevidade
    }

}