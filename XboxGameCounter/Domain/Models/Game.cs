namespace Company.Function.Domain.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Genre { get; set; }
    public List<string> Developers { get; set; }
    public List<string> Publishers { get; set; }
    public ReleaseDates ReleaseDates { get; set; }
}

public class ReleaseDates
{
    public string? Japan { get; set; } 
    public string? NorthAmerica { get; set; }
    public string? Europe { get; set; }
    public string? Australia { get; set; }
}
