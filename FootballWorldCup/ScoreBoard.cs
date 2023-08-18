namespace FootballWorldCup;
public class ScoreBoard : IScoreBoard
{
    private readonly Dictionary<Guid, IGame> _games = new();
    private readonly IGameFactory _gameFactory;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gameFactory"> </param>
    public ScoreBoard(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
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
        IGame game = _gameFactory.CreateGame(homeTeam, awayTeam);

        if (CheckIfAlreadyPlaying(homeTeam))
        {
            throw new ArgumentException($"{homeTeam} is already playing");
        }
        if (CheckIfAlreadyPlaying(awayTeam))
        {
            throw new ArgumentException($"{awayTeam} is already playing");
        }


        _games[game.Id] = game;
        return game.Id;

    }

    /// <summary>
    /// Ends an existing game
    /// </summary>
    /// <param name="gameId">Id of the game to end</param>
    /// <exception cref="ArgumentException">When the game does not exists in the scoreboard</exception>
    public void FinishGame(Guid gameId)
    {
        if (!_games.ContainsKey(gameId))
        {
            throw new ArgumentException("Invalid game id");
        }
        _games.Remove(gameId);
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
        if (!_games.ContainsKey(gameId))
        {
            throw new ArgumentException("Invalid game id");
        }

        _games[gameId].UpdateScore(homeScore, awayScore);

    }

    /// <summary>
    /// Returns a list of games sorted by total score and then by most recent start time
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IGame> GetSummary()
    {
        return _games.Values.OrderByDescending(o => o.HomeScore + o.AwayScore).ThenByDescending(m => m.UTCStartTime);
    }

    /// <summary>
    /// Returns a game by its id
    /// </summary>
    /// <param name="gameId">Id of the game to return</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">When id is invalid</exception>
    public IGame GetGame(Guid gameId)
    {
        if (!_games.ContainsKey(gameId))
        {
            throw new ArgumentException("Invalid game id");
        }
        return _games[gameId];
    }


    /// <summary>
    /// Checks if a team is already playing
    /// </summary>
    /// <param name="team">Team name</param>
    /// <returns>True if the team is already playing</returns>
    private bool CheckIfAlreadyPlaying(string team)
    {
        string normalizedTeamName = NormalizeTeamName(team);

        IGame? game = _games.Values.FirstOrDefault(o => NormalizeTeamName(o.HomeTeam) == normalizedTeamName || NormalizeTeamName(o.AwayTeam) == normalizedTeamName);
        
        if (game == default)
        {
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// Returns a normalized team name, used for case insensitive comparison
    /// </summary>
    private static string NormalizeTeamName(string team) => team.Trim().ToLower();

}