namespace FootballWorldCup;

public interface IGameFactory
{
    IGame CreateGame(string homeTeam, string awayTeam);
}