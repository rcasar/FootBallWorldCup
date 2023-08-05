namespace FootballWorldCup;
public class ScoreBoard : IScoreBoard
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gameFactory"> </param>
    public ScoreBoard(IGameFactory gameFactory)
    {
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    /// <param name="homeTeam">Name of the home team</param>
    /// <param name="awayTeam">Name of the away team</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">When team name is invalid or is already playing</exception>
    public Guid StartGame(string homeTeam, string awayTeam)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ends an existing game
    /// </summary>
    /// <param name="gameId">Id of the game to end</param>
    /// <exception cref="ArgumentException">When the game does not exists in the scoreboard</exception>
    public void EndGame(Guid gameId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the score of a game
    /// </summary>
    /// <param name="gameId">Id of the game to update</param>
    /// <param name="homeScore">New score for the home team</param>
    /// <param name="awayScore">new score for the away team</param>
    /// <exception cref="ArgumentException">When the game does not exists in the scoreboard</exception>
    public void UpdateScore(Guid gameId, int homeScore, int awayScore)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Returns a list of games sorted by total score and then by most recent start time
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IGame> GetSummary()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a game by its id
    /// </summary>
    /// <param name="gameId">Id of the game to return</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">When id is invalid</exception>
    public IGame GetGame(Guid gameId)
    {
        throw new NotImplementedException();
    }
}