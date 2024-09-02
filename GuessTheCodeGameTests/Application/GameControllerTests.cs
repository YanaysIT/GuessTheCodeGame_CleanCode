namespace GuessTheCodeGame.Application.Tests;

[TestClass()]
public class GameControllerTests
{
    private const string ExpectedPlayerName = "Player1";
    private const string ScoresFilePath = "test_results.txt";
    //mock random numbers for goal = "1234" (1rst round) goal = "5678" (if 2nd round)
    private readonly IRandomNumberGenerator _mockRandomNumberGenerator = new MockRandomNumberGenerator([1, 2, 3, 4, 5, 6, 7, 8]);
    private IPlayerScoresRepository _playerScoresRepository;
    private MockFileStreamIO _mockFileStreamIO;
    private IGameLogic _gameLogic;

    [TestInitialize]
    public void Initialize()
    {
        var initialFileContents = new List<string>();
        _mockFileStreamIO = new MockFileStreamIO(initialFileContents);
        _playerScoresRepository = new PlayerScoresRepository(ScoresFilePath, _mockFileStreamIO);
        _gameLogic = new MooGameLogic(_mockRandomNumberGenerator, new GuessComparer());
    }

    [TestMethod]
    public void GameController_ShouldOnlyAcceptValidPlayerNames()
    {
        var userInputs = new List<string>
        {
            "", "   ", ExpectedPlayerName, // Player names input, with valid name last
            "1",                           //Selects Moo(1)
            "1111", "1234",
            "n"
        };

        var gameController = CreateGameControllerWithInputs(userInputs);

        gameController.Play();
        List<IPlayer> savedScores = _playerScoresRepository.GetPlayerScoresByGame(GameTypes.Moo).ToList();

        Assert.IsTrue(savedScores.All(s => s.PlayerName == ExpectedPlayerName), $"Only the valid player name {ExpectedPlayerName} should have been saved.");
    }

    [TestMethod]
    public void Play_ShouldContinue_WhenPlayerWantsToContinue()
    {
        var userInputs = new List<string>
        {
            "1111", "1234",          // First round guesses
            "y",                     // Chooses to continue
            "2222", "2456", "5678",  // Second round guesses
            "n"                      // Chooses to stop
        };

        var gameController = CreateGameControllerWithInputs(userInputs);
        gameController.Play();

        IPlayer savedScores = _playerScoresRepository.GetPlayerScoresByGame(GameTypes.Moo).Single();
        int actualRoundsPlayed = savedScores.GamesPlayed;
        int expectedRoundsPlayed = 2;

        Assert.AreEqual(expectedRoundsPlayed, actualRoundsPlayed, $"Expected rounds played were {expectedRoundsPlayed}, but got {actualRoundsPlayed}.");
    }

    private GameController CreateGameControllerWithInputs(IEnumerable<string> userInputs)
    {
        var queueInputs = new Queue<string>(userInputs);
        var mockUI = new MockUI(queueInputs);
        return new GameController(_playerScoresRepository, mockUI, _gameLogic);
    }

    [TestMethod]
    public void Play_ShouldStopGame_WhenPlayerDeclinesToContinue()
    {
        var userInputs = new List<string>
        {
            "1111", "1234",     // First round guesses
            "n"                 // Chooses to stop
        };

        var gameController = CreateGameControllerWithInputs(userInputs);

        gameController.Play();
        IPlayer savedScores = _playerScoresRepository.GetPlayerScoresByGame(GameTypes.Moo).Single();
        int actualRoundsPlayed = savedScores.GamesPlayed;
        int expectedRoundsPlayed = 1;

        Assert.AreEqual(expectedRoundsPlayed, actualRoundsPlayed, $"Expected rounds played were {expectedRoundsPlayed}, but got {actualRoundsPlayed}.");
    }

   
    [TestMethod]
    public void Play_ShouldCorrectlySavePlayerScores()
    {
        var userInputs = new List<string>
        {
            ExpectedPlayerName,
            "1111", "1234",            // First round guesses
            "y",                       // Chooses to stop
            "2222", "2456", "5678",    // Second round guesses
            "n"                        // Chooses to stop
        };

        GameController gameController = CreateGameControllerWithInputs(userInputs);
        int expectedTotalOfGuesses = 5;

        gameController.Play();
        var savedPlayerScore = _playerScoresRepository.GetPlayerScoresByGame(GameTypes.Moo).Single();
        int actualTotalOfGuesses = savedPlayerScore.TotalOfGuesses;

        Assert.AreEqual(expectedTotalOfGuesses, actualTotalOfGuesses, $"Expected total number of guesses was {expectedTotalOfGuesses}, but got {actualTotalOfGuesses}");
    }
}