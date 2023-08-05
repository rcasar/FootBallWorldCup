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
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the score of the game
    /// </summary>
    /// <param name="homeScore">New score for the home team</param>
    /// <param name="awayScore">New score for the away team</param>
    /// <exception cref="ArgumentException">When the new scores are not valid</exception>
    public void UpdateScore(int homeScore, int awayScore)
    {
        throw new NotImplementedException();
    }
}
