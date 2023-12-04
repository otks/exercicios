using Newtonsoft.Json;
using Questao2;

public class Program
{
    private static string _baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
    
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int GetTotalScoredGoals(string team, int year)
    {
        using HttpClient client = new();

        int page = 0;
        int totalGoals = 0;

        while(true)
        {
            string matchesRequestUri = $"{_baseUrl}?year={year}&team1={team}&page={page}";
            string jsonResult = client.GetStringAsync(matchesRequestUri).Result;

            FootballMatchResponse? matchResult = JsonConvert.DeserializeObject<FootballMatchResponse>(jsonResult);
            if (matchResult == null || !matchResult.Data.Any())
                return -1;

            totalGoals += matchResult.Data.Sum(x => int.Parse(x.Team1Goals));
            page++;
            
            if (page > matchResult.TotalPages)
                break;
        }
        
        return totalGoals;
    }

}