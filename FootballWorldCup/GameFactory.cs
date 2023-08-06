namespace FootballWorldCup;

public class GameFactory : IGameFactory
{
    public IGame CreateGame(string homeTeam, string awayTeam)
    {
        return new Game(homeTeam, awayTeam);
    }
}
