namespace FootballWorldCup
{
    public interface IGame
    {
        int AwayScore { get; }
        string AwayTeam { get; }
        int HomeScore { get; }
        string HomeTeam { get; }
        Guid Id { get; }
        DateTime UTCStartTime { get; }

        void UpdateScore(int homeScore, int awayScore);
    }
}