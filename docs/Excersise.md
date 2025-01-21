# Coding Exercise
Implementing a Football World Cup Score Board Simple Library

## Instructions
Provide the implementation of the Football World Cup Score Board as a simple library.

## Guidelines
- **Keep it simple**: Stick to the requirements and try to implement the simplest solution you can possibly think of that works and don't forget about edge cases.
- **In-memory store**: Use an in-memory store solution (for example, just use collections to store the information you might require).
- **Not a REST API**: We are NOT looking for a REST API, a Web Service, or Microservice. Just a simple implementation of a library.
- **Focus on Code Quality**:
  - Use Test-Driven Development (TDD).
  - Pay attention to Object-Oriented design, Clean Code, and adherence to good code design standards and principles (e.g., SOLID).
- **Approach**:
  - Code the solution according to your standards.
  - Share your solution with a link to a source control repository (e.g., GitHub, GitLab, BitBucket) as we would like to see your progress (commit history is important).
- **Documentation**:
  - Add a `README.md` file where you can make notes of any assumptions or things you would like to mention about your solution.
- **Frontend-specific guidelines** (if applicable):
  - If written in a specific UI framework or library, implement the simplest components to serve the described functionality. Do not spend time making it look good.
  - If written in plain JavaScript, implement the solution as a simple service or module.

## Requirements
You are working at a sports data company, and we would like you to develop a new **Live Football World Cup Score Board** that shows matches and scores.  
The board supports the following operations:

1. **Start a game**:
   - When a game starts, it should capture the following (initial score: 0 – 0):
     - Home team
     - Away team

2. **Finish a game**:
   - Remove a match from the scoreboard.

3. **Update score**:
   - Update a game score using a pair of values: the home team's score and the away team's score.

4. **Get a summary of games**:
   - Games should be summarized by total score, with ties ordered by the most recently added game.

### Example Scenario
Given the following data in the system:

a) **Mexico - Canada**: 0 - 5
b) **Spain - Brazil**: 10 – 2
c) **Germany - France**: 2 – 2
d) **Uruguay - Italy**: 6 – 6
e) **Argentina - Australia**: 3 - 1

The summary should display:
1. Uruguay 6 - Italy 6
2. Spain 10 - Brazil 2
3. Mexico 0 - Canada 5
4. Argentina 3 - Australia 1
5. Germany 2 - France

## Recommendations
Despite the emphasis on simplicity, consider the following:
- This test is used to evaluate your **technical coding skills**.
- Complete the test as best as you can, showcasing your current skills and abilities.
- Ensure your solution shows some degree of **good code design**.
- **Testing is important**:
  - Write tests to ensure the reliability of your code.
- Apply **good coding practices**.
- When completed, consider whether your solution is **maintainable and testable**.


