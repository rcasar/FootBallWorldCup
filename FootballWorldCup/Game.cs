namespace FootballWorldCup;

public class Game : IGame
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string HomeTeam { get; private set; }
    public string AwayTeam { get; private set; }
    public int HomeScore { get; private set; } = 0;
    public int AwayScore { get; private set; } = 0;
    public DateTime UTCStartTime { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="homeTeam">Name of the home team</param>
    /// <param name="awayTeam">Name of the away team</param>
    /// <exception cref="ArgumentException">When team names are not valid</exception>
    public Game(string homeTeam, string awayTeam)
    {
        string? trimmedHomeTeam = homeTeam?.Trim();
        ArgumentNullException.ThrowIfNullOrEmpty(trimmedHomeTeam, nameof(homeTeam));
        string? trimmedAwayTeam = awayTeam?.Trim();
        ArgumentNullException.ThrowIfNullOrEmpty(trimmedAwayTeam, nameof(awayTeam));

        if (string.Equals(trimmedHomeTeam, trimmedAwayTeam, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ArgumentException("Home team cannot be the same as away team");
        }

        HomeTeam = homeTeam!;
        AwayTeam = awayTeam!;
    }

    /// <summary>
    /// Updates the score of the game
    /// </summary>
    /// <param name="homeScore">New score for the home team</param>
    /// <param name="awayScore">New score for the away team</param>
    /// <exception cref="ArgumentException">When the new scores are not valid</exception>
    public void UpdateScore(int homeScore, int awayScore)
    {
        if (homeScore < HomeScore)
        {
            throw new ArgumentException("New home score cannot be lower than current home score");
        }
        if (awayScore < AwayScore)
        {
            throw new ArgumentException("New away score cannot be lower than current away score");
        }

        HomeScore = homeScore;
        AwayScore = awayScore;
    }
}
