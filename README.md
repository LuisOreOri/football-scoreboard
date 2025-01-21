# Football Scoreboard 🏆

## 📖 Overview
Football Scoreboard is a .NET 8 class library developed as part of a **coding exercise** 
for a job interview. It provides functionalities to manage live football games, 
including tracking scores, starting and finishing games, and retrieving a summary 
of ongoing games.

The coding exercise instructions are documented in `docs/Exercise.md` file. 

This is version `(v2.0.0)`, which builds upon the **MVP version (v1.0.0)**.
The MVP focused on meeting the core requirements of the coding exercise and
is available in the `v1.0.0` tag. The current version enhances the design by:

- General Refactoring
- Implementing the **Repository Pattern** and **Strategy Pattern** for flexibility and scalability.
- Adding comprehensive **XML documentation** for improved usability.
- Improving test coverage with additional unit, integration, and functional tests.

---

## 🚀 Features
- **Game Lifecycle Management**: Start and finish football games.
- **Live Score Updates**: Update scores for ongoing games.
- **Game Summaries**: Retrieve a summary of games, sorted by total score and start time.
- **Clean Architecture**:
  - Repository Pattern for abstracting data storage.
  - Strategy Pattern for flexible sorting of game summaries.
- **Testability**: Fully tested using Unit, Integration, and Functional tests.

---

## 📦 Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/LuisOreOri/football-scoreboard.git
   ```
   *To use the MVP version (v1.0.0), checkout the corresponding tag:* 
    ```
    git checkout tags/v1.0.0
    ```
1. Open the `FootballScoreBoard.sln` solution in **Visual Studio 2022**.
1. Ensure you have the **.NET 8 SDK** installed.
1. Build the solution to restore dependencies.

---
## 🛠 Usage

### Creating a ScoreBoard
The ScoreBoard class provides multiple constructors to accommodate different use cases.
Here are the available options:

#### Default Constructor:

- **Description:** Uses `InMemoryGameRepository` for in-memory storage and DefaultGameSortingStrategy for sorting.
- **Example:** 
```
var scoreboard = new ScoreBoard();
``` 

#### Custom Repository Constructor:

- **Description:** Allows you to provide a custom implementation of IGameRepository. The default sorting strategy (DefaultGameSortingStrategy) is used.
``` 
var customRepository = new CustomGameRepository(); // Implement your own repository
var scoreboard = new ScoreBoard(customRepository);
``` 

#### Custom Sorting Strategy Constructor:

- **Description:** Allows you to provide a custom implementation of IGameSortingStrategy. The default repository (InMemoryGameRepository) is used.
- **Example:**
```
var customSortingStrategy = new CustomSortingStrategy(); // Implement your own sorting logic
var scoreboard = new ScoreBoard(customSortingStrategy);
```

#### Fully Custom Constructor:

- **Description:** Allows you to provide both a custom IGameRepository and a custom IGameSortingStrategy for maximum flexibility.
- **Example:**
```
var customRepository = new CustomGameRepository();
var customSortingStrategy = new CustomSortingStrategy();
var scoreboard = new ScoreBoard(customRepository, customSortingStrategy);
```


### Starting a Game
To start a game, create an instance of the `ScoreBoard` class and call `StartGame`:

```
var scoreboard = new ScoreBoard();
var game = new Game("Team A", "Team B");
scoreboard.StartGame(game);
```

### Updating Scores
Update the scores for an ongoing game:

```
scoreboard.UpdateScore(game, 3, 2); // Team A: 3, Team B: 2
```

### Retrieving Game Summaries
Get a sorted summary of games:

```
var summary = scoreboard.GetSummary();
foreach (var game in summary)
{
    Console.WriteLine($"{game.HomeTeam} {game.HomeScore} - {game.AwayTeam} {game.AwayScore}");
}

```


### Finishing a Game
Remove a game from the scoreboard:
```
scoreboard.FinishGame(game);
```

---

## 🧪 Running Tests
The project includes a comprehensive set of tests:
  
1. **Unit Tests:** 
    - Validates individual components like `Game`, `InMemoryGameRepository`, and `DefaultGameSortingStrategy`.
1. **Integration Tests:**
    - Ensures interaction between components, such as ScoreBoard and InMemoryGameRepository.
1. **Functional Tests:** 
    - Simulates real-world scenarios, like retrieving game summaries.

Run all tests using the following command:

```
dotnet test
```

---

## 📌 Development Practices
### Principles and Patterns
- **Test-Driven Development (TDD):**
    - Implemented functionality incrementally, guided by comprehensive unit tests.

- **Clean Code:**
    - Followed **SOLID principles** for better maintainability and scalability.
    - Ensured adherence to **KISS** (Keep It Simple, Stupid) and **YAGNI** (You Aren’t Gonna Need It) principles.
- **Repository Pattern:**
    - Abstracted data storage for flexibility and scalability.
- **Strategy Pattern:**
    - Used for sorting game summaries, allowing easy extension for alternative sorting rules.

### Data Structures
- Used **Dictionary<string, IGame>** in the repository for optimal lookups by game ID, ensuring:
    - *O(1)* complexity for adding, retrieving, and removing games.
    - Efficient operations suitable for handling large datasets.

---

## 📋 Considerations and Assumptions
### Version History:
- `v1.0.0`: Initial MVP implementation, available as a Git tag.
- `v2.0.0`: Current version with enhanced design patterns, documentation, and testing.
### Designed as a Library:
- The project is intended to be used as a class library by external .NET projects.
- Interfaces (`IGame`, `IScoreBoard`, `IGameRepository`) ensure testability and flexibility for dependency injection.
- Comprehensive **XML Documentation:** 
    - All public classes, interfaces, methods, and properties include XML documentation to provide guidance and assist users when integrating the library.
    - This improves the developer experience by offering IntelliSense support in IDEs like Visual Studio and Visual Studio Code.
### Focus on Football (Soccer):
- The library currently handles football matches. Extending it to other sports would require additional domain-specific classes.
### Sorting Logic:
- Default sorting prioritizes games with higher scores and, for ties, games started later.
- Custom sorting strategies can be injected as needed.
### Scalability:
- The `InMemoryGameRepository` is optimized for small-scale, in-memory operations but can be replaced with a database-backed implementation.
### Error Handling:
- Ensures robust error handling with meaningful exceptions (e.g., `ArgumentNullException`, `InvalidOperationException`).

---

## 📂 File Structure
```
src/
│── FootballScoreBoard/
│   ├── Core/               # Core interfaces and business logic (IGame, IScoreBoard, Game, ScoreBoard)
│   ├── Infrastructure/     # Infrastructure components (IGameRepository, InMemoryGameRepository)
tests/
│── FunctionalTests/        # Tests simulating real-world scenarios
│── IntegrationTests/       # Tests for interactions between components
│── Mocks/                  # Helpers for mocking 
│── UnitTests/              # Unit tests for core components
docs/
│── Exercise.md            # Coding exercise instructions (with company references removed)
```

---
## 🚀 Future Improvements
While the library meets the current requirements and adheres to the KISS principle as suggested
in the exercise, I have carefully balanced simplicity with extensibility.
In some cases, I hesitated to apply additional complexity, ensuring that the solution
remains straightforward and aligns with the challenge's scope.
However, here are potential areas for future development:

### Support for Other Sports:
- Extend the library to handle different sports by introducing a more generic game model and domain-specific extensions (e.g., basketball, tennis).

### Custom Exceptions:

- Introduce domain-specific exceptions (e.g., `GameNotFoundException`, `DuplicateGameException`) to improve error handling and provide more meaningful feedback.

### Persistent Storage:

- Replace the in-memory repository with a database (e.g., SQL, NoSQL) or a cloud storage provider.

###  Add Sorting Strategies
- Provide more sophisticated sorting strategies based on additional criteria (e.g., alphabetical order, regional prioritization).

### Performance Optimization:

- Study (algorithm and betchmarking) if it is possible to improve the repository and sorting logic for handling a larger volume of games in real-time scenarios.

### Concurrency Handling:

- Introduce mechanisms to handle concurrent operations safely (e.g., simultaneous updates or game deletions).
Integration 

### Package Distribution:

Prepare the library for distribution as a **NuGet package**, including proper versioning, metadata, and automated builds.


---

## ⚠️ Disclaimer
This project was developed as part of a coding challenge for a job interview.
It is not intended for production use or public distribution.
