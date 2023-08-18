namespace FootballWorldCup.UnitTests;

public class GameTests
{
    [Fact]
    public void CreateNewGame_Ok()
    {
        // Arrange
        IGame game = new Game("Brazil", "Argentina");

        // Act
        string homeTeam = game.HomeTeam;
        string awayTeam = game.AwayTeam;
        int homeScore = game.HomeScore;
        int awayScore = game.AwayScore;
        Guid id = game.Id;
        DateTime utcStartTime = game.UTCStartTime;

        // Assert
        Assert.Equal("Brazil", homeTeam);
        Assert.Equal("Argentina", awayTeam);
        Assert.Equal(0, homeScore);
        Assert.Equal(0, awayScore);
        Assert.NotEqual(Guid.Empty, id);
        Assert.True(utcStartTime < DateTime.UtcNow);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void CreateNewGame_Fails_WhenHomeTeamIsEmpty(string homeTeam)
    {
        // Arrange
        // Act
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() => new Game(homeTeam, "Argentina"));

        // Assert
        Assert.Contains("homeTeam", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void CreateNewGame_Fails_WhenAwayTeamIsEmpty(string awayTeam)
    {
        // Arrange
        // Act
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() => new Game("Brazil", awayTeam));

        // Assert
        Assert.Contains("awayTeam", exception.Message);
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
    public void CreateNewGame_Fails_WhenHomeTeamIsSameAsAwayTeam(string homeTeam, string awayTeam)
    {
        // Arrange
        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Game(homeTeam, awayTeam));

        // Assert
        Assert.Equal("Home team cannot be the same as away team", exception.Message);
    }   


    [Fact]
    public void UpdateScore_Ok()
    {
        // Arrange
        IGame game = new Game("Brazil", "Argentina");

        // Act
        game.UpdateScore(1, 0);

        // Assert
        Assert.Equal(1, game.HomeScore);
        Assert.Equal(0, game.AwayScore);
    }

    [Fact]
    public void UpdateScore_Fails_WhenHomeScoreIsInvalid()
    {
        // Arrange
        IGame game = new Game("Brazil", "Argentina");

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(() => game.UpdateScore(-1, 0));

        // Assert
        Assert.Equal("New home score cannot be lower than current home score", exception.Message);
    }

    [Fact]
    public void UpdateScore_Fails_WhenAwayScoreIsInvalid()
    {
        // Arrange
        IGame game = new Game("Brazil", "Argentina");

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(() => game.UpdateScore(0, -1));

        // Assert
        Assert.Equal("New away score cannot be lower than current away score", exception.Message);
    }
}
