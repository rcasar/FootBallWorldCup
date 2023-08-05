namespace FootballWorldCup
{
    public interface IScoreBoard
    {
        Guid StartGame(string homeTeam, string awayTeam);

        void FinishGame(Guid gameId);
        IGame GetGame(Guid gameId);

        void UpdateScore(Guid gameId, int homeScore, int awayScore);

        IEnumerable<IGame> GetSummary();
    }
}