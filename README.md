# Football World Cup

This is a simple library in C# to manage information about the games in the Football World Cup. It uses a dictionary to store the games, which makes it efficient to find a specific game.

## Installation

Simply reference `FootBallWorldCup.dll` and you are good to go!

## Usage
To use the library, you can create a new ScoreBoard object and then start, finish, and update the score of games. For example:

```
var scoreBoard = new ScoreBoard(new GameFactory());
var gameId = scoreBoard.StartGame("Mexico", "Canada");
scoreBoard.UpdateScore(gameId, 1, 0);
scoreBoard.FinishGame(gameId);
```
You can also get a summary of the games by calling the `GetSummary()` method on the `ScoreBoard` object. The summary will be a list of games, sorted by total score and most recent start time.

As an example, being the current games in the system (ordered by ascending start time):

| Home team | Away team | Score |
|-----------|-----------|-------|
|Mexico     |Canada     | 0 - 5 |
|Spain      |Brazil     | 10 - 2 |
|Germany    |France     | 2 - 2 |
|Uruguay    |Italy      | 6 - 6 |
|Argentina  |Australia  | 3 - 1 |


The summary will provide with the following information:
| Home team | Away team | Score |
|-----------|-----------|-------|
|Uruguay    |Italy      | 6 - 6 |
|Spain      |Brazil     | 10 - 2 |
|Mexico     |Canada     | 0 - 5 |
|Argentina  |Australia  | 3 - 1 |
|Germany    |France     | 2 - 2 |

## Unit Tests
The solution includes unit tests that you can run to ensure that it is working correctly. To run the unit tests, open a terminal and navigate to the directory where the solution is located. Then, run the following command:

```
PS > dotnet test
```

## License
The library is licensed under the MIT License.
