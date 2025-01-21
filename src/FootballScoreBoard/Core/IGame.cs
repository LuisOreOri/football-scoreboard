namespace FootballScoreBoard.Core;

/// <summary>
/// Represents a game between two teams, including scores, start time, and related operations.
/// </summary>
public interface IGame
{
    /// <summary>
    /// Gets the score of the away team.
    /// </summary>
    int AwayScore { get; }

    /// <summary>
    /// Gets the name of the away team.
    /// </summary>
    string AwayTeam { get; }

    /// <summary>
    /// Gets the score of the home team.
    /// </summary>
    int HomeScore { get; }

    /// <summary>
    /// Gets the name of the home team.
    /// </summary>
    string HomeTeam { get; }

    /// <summary>
    /// Gets the unique identifier of the game, typically derived from the team names.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the start time of the game. This value is null if the game has not started.
    /// </summary>
    DateTime? StartTime { get; }

    /// <summary>
    /// Starts the game and sets the start time.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the game has already started.</exception>
    void Start();

    /// <summary>
    /// Updates the scores for the home and away teams.
    /// </summary>
    /// <param name="homeScore">The new score for the home team.</param>
    /// <param name="awayScore">The new score for the away team.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game has not started.</exception>
    /// <exception cref="ArgumentException">Thrown if any of the scores are negative.</exception>
    void UpdateScore(int homeScore, int awayScore);
}
