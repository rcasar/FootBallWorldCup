namespace FootballWorldCup.UnitTests;

public class GameFactoryTests
{
    [Fact]
    public void CreateGame_ShouldReturnGame()
    {
        // Arrange
        IGameFactory gameFactory = new GameFactory();

        // Act
        IGame game = gameFactory.CreateGame("Home", "Away");

        // Assert
        Assert.NotNull(game);
        Assert.Equal("Home", game.HomeTeam);
        Assert.Equal("Away", game.AwayTeam);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void CreateGame_ShouldThrowArgumentException_WhenHomeTeamIsInvalid(string homeTeam)
    {
        // Arrange
        IGameFactory gameFactory = new GameFactory();

        // Act
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() => gameFactory.CreateGame(homeTeam, "Away"));

        // Assert
        Assert.Contains("homeTeam", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void CreateGame_ShouldThrowArgumentException_WhenAwayTeamIsInvalid(string awayTeam)
    {
        // Arrange
        IGameFactory gameFactory = new GameFactory();

        // Act
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() => gameFactory.CreateGame("Home", awayTeam));

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
    public void CreateGame_ShouldThrowArgumentException_WhenHomeTeamIsSameAsAwayTeam(string homeTeam, string awayTeam)
    {
        // Arrange
        IGameFactory gameFactory = new GameFactory();

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(() => gameFactory.CreateGame(homeTeam, awayTeam));

        // Assert
        Assert.Equal("Home team cannot be the same as away team", exception.Message);
    }



}
