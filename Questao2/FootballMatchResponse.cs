namespace Questao2;

public class FootballMatchResponse
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public FootballMatch[] Data { get; set; }
}