namespace FootballWorldCup.UnitTests;

public class ScoreBoardTests

{
    [Fact]
    public void StartGame_Ok()
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());

        var gameId = scoreBoard.StartGame("Brazil", "Argentina");

        // Act
        var game = scoreBoard.GetGame(gameId);

        // Assert
        Assert.NotNull(game);
        Assert.NotEqual(Guid.Empty, game.Id);
        Assert.Equal(gameId, game.Id);
        Assert.Equal("Brazil", game.HomeTeam);
        Assert.Equal("Argentina", game.AwayTeam);
        Assert.Equal(0, game.HomeScore);
        Assert.Equal(0, game.AwayScore);
        Assert.True(game.UTCStartTime < DateTime.UtcNow);        

    }

    [Theory]
    [InlineData("Brazil", "Argentina", "Brazil", "Uruguay")]
    [InlineData("Brazil", "Argentina", "BRAZIL", "Uruguay")]
    [InlineData("Brazil", "Argentina", " Brazil   ", "Uruguay")]
    public void StartGame_Fails_WhenHomeTeamAlreadyPlaying(string homeTeam1, string awayTeam1, string homeTeam2, string awayTeam2)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        Guid gameId = scoreBoard.StartGame(homeTeam1, awayTeam1);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.StartGame(homeTeam2, awayTeam2));

        // Assert
        Assert.Equal($"{homeTeam2} is already playing", exception.Message);
        Assert.Single(scoreBoard.GetSummary());

    }

    [Theory]
    [InlineData("Brazil", "Argentina", "Uruguay", "Argentina")]
    [InlineData("Brazil", "Argentina", "Uruguay", "ARGENTINA")]
    [InlineData("Brazil", "Argentina", "Uruguay", " Argentina ")]
    public void StartGame_Fails_WhenAwayTeamAlreadyPlaying(string homeTeam1, string awayTeam1, string homeTeam2, string awayTeam2)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        scoreBoard.StartGame(homeTeam1, awayTeam1);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.StartGame(homeTeam2, awayTeam2));

        // Assert
        Assert.Equal($"{awayTeam2} is already playing", exception.Message);
        Assert.Single(scoreBoard.GetSummary());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void StartGame_Fails_WhenHomeTeamIsEmpty(string homeTeam)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());

        // Act
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.StartGame(homeTeam, "Argentina"));

        // Assert
        Assert.Equal("Home team cannot be null or empty", exception.Message);
        Assert.Empty(scoreBoard.GetSummary());
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void StartGame_Fails_WhenAwayTeamIsEmpty(string awayTeam)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());

        // Act
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.StartGame("Brazil", awayTeam));

        // Assert
        Assert.Equal("Away team cannot be null or empty", exception.Message);
        Assert.Empty(scoreBoard.GetSummary());
    }

    [Theory]
    [InlineData("Brazil", "Brazil")]
    [InlineData("Brazil", "brazil")]
    [InlineData("Brazil", "BRAZIL")]
    [InlineData("Brazil", "Brazil ")]
    [InlineData("Brazil", " Brazil")]
    [InlineData("Brazil", " Brazil ")]
    [InlineData("Brazil", "Brazil\t")]
    [InlineData("Brazil", "\tBrazil")]
    [InlineData("Brazil", "\tBrazil\t")]
    [InlineData("Brazil", "Brazil\n")]
    [InlineData("Brazil", "\nBrazil")]
    [InlineData("Brazil", "\nBrazil\n")]
    public void StartGame_Fails_WhenHomeTeamIsSameAsAwayTeam(string homeTeam, string awayTeam)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());

        // Act
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.StartGame(homeTeam, awayTeam));

        // Assert
        Assert.Equal("Home team cannot be the same as away team", exception.Message);
        Assert.Empty(scoreBoard.GetSummary());
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void UpdateScore_Ok(int homeScore, int awayScore)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        var gameId = scoreBoard.StartGame("Brazil", "Argentina");

        // Act
        scoreBoard.UpdateScore(gameId, homeScore, awayScore);

        IGame game = scoreBoard.GetGame(gameId);

        // Assert
        Assert.NotNull(game);
        Assert.Equal(homeScore, game.HomeScore);
        Assert.Equal(awayScore, game.AwayScore);
    }

    [Fact]
    public void UpdateScore_Fails_WhenGameIdIsInvalid()
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        var gameId = Guid.NewGuid();

        // Act
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.UpdateScore(gameId, 1, 1));

        // Assert   
        Assert.Equal("Invalid game id", exception.Message);
    }


    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, -1)]   
    public void UpdateScore_Fails_WhenHomeScoreIsInvalid(int originalHomeScore, int updatedHomeScore)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        var gameId = scoreBoard.StartGame("Brazil", "Argentina");

        // Act
        scoreBoard.UpdateScore(gameId, originalHomeScore, 0);
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.UpdateScore(gameId, updatedHomeScore, 0));

        // Assert
        Assert.Equal("New home score cannot be lower than current home score", exception.Message);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, -1)]
    public void UpdateScore_Fails_WhenAwayScoreIsInvalid(int originalAwayScore, int updatedAwayScore)
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        var gameId = scoreBoard.StartGame("Brazil", "Argentina");

        // Act
        scoreBoard.UpdateScore(gameId, 0, originalAwayScore);
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.UpdateScore(gameId, 0, updatedAwayScore));

        // Assert
        Assert.Equal("New away score cannot be lower than current away score", exception.Message);
    }



    [Fact]
    public void FinishGame_Ok()
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        var gameId = scoreBoard.StartGame("Brazil", "Argentina");

        // Act
        scoreBoard.FinishGame(gameId);

        // Assert
        Assert.Empty(scoreBoard.GetSummary());
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.GetGame(gameId));
    }

    [Fact]
    public void FinishGame_Fails_WhenGameIdIsInvalid()
    {
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());

        // Act
        var gameId = Guid.NewGuid();

        // Assert
        var exception = Assert.Throws<ArgumentException>(() => scoreBoard.FinishGame(gameId));
    }


    [Fact]
    public void GetSummary_Ok()
    { 
        // Arrange
        var scoreBoard = new ScoreBoard(new GameFactory());
        /*
            a. Mexico - Canada: 0 - 5
            b. Spain - Brazil: 10 – 2
            c. Germany - France: 2 – 2
            d. Uruguay - Italy: 6 – 6
            e. Argentina - Australia: 3 - 1
        */

        var gameId1 = scoreBoard.StartGame("Mexico", "Canada");
        var gameId2 = scoreBoard.StartGame("Spain", "Brazil");
        var gameId3 = scoreBoard.StartGame("Germany", "France");
        var gameId4 = scoreBoard.StartGame("Uruguay", "Italy");
        var gameId5 = scoreBoard.StartGame("Argentina", "Australia");

        scoreBoard.UpdateScore(gameId1, 0, 5);
        scoreBoard.UpdateScore(gameId2, 10, 2);
        scoreBoard.UpdateScore(gameId3, 2, 2);
        scoreBoard.UpdateScore(gameId4, 6, 6);
        scoreBoard.UpdateScore(gameId5, 3, 1);


        // Act
        var summary = scoreBoard.GetSummary();

        /*
          The summary would provide with the following information:
            1. Uruguay 6 - Italy 6
            2. Spain 10 - Brazil 2
            3. Mexico 0 - Canada 5
            4. Argentina 3 - Australia 1
            5. Germany 2 - France 2
        */

        // Assert
        Assert.Equal(5, summary.Count());

        Assert.Equal("Uruguay", summary.ElementAt(0).HomeTeam);
        Assert.Equal("Italy", summary.ElementAt(0).AwayTeam);
        Assert.Equal(6, summary.ElementAt(0).HomeScore);
        Assert.Equal(6, summary.ElementAt(0).AwayScore);

        Assert.Equal("Spain", summary.ElementAt(1).HomeTeam);
        Assert.Equal("Brazil", summary.ElementAt(1).AwayTeam);
        Assert.Equal(10, summary.ElementAt(1).HomeScore);
        Assert.Equal(2, summary.ElementAt(1).AwayScore);

        Assert.Equal("Mexico", summary.ElementAt(2).HomeTeam);
        Assert.Equal("Canada", summary.ElementAt(2).AwayTeam);
        Assert.Equal(0, summary.ElementAt(2).HomeScore);
        Assert.Equal(5, summary.ElementAt(2).AwayScore);

        Assert.Equal("Argentina", summary.ElementAt(3).HomeTeam);
        Assert.Equal("Australia", summary.ElementAt(3).AwayTeam);
        Assert.Equal(3, summary.ElementAt(3).HomeScore);
        Assert.Equal(1, summary.ElementAt(3).AwayScore);

        Assert.Equal("Germany", summary.ElementAt(4).HomeTeam);
        Assert.Equal("France", summary.ElementAt(4).AwayTeam);
        Assert.Equal(2, summary.ElementAt(4).HomeScore);
        Assert.Equal(2, summary.ElementAt(4).AwayScore);


    }
}