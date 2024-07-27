using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        PuzzleGenerator puzzleGenerator = new PuzzleGenerator();

        Console.WriteLine($"Initial Target Rating: {puzzleGenerator.TargetRating}");
        Console.WriteLine($"Initial Score: {puzzleGenerator.Score}");

        // Generate a puzzle
        puzzleGenerator.GeneratePuzzle();

        // Print information about the generated puzzle
        PrintPuzzleInfo(puzzleGenerator);

        // Test AddNoise method
        TestAddNoise(puzzleGenerator);

        // Update and generate a new puzzle
        puzzleGenerator.Update();

        Console.WriteLine("\nAfter Update:");
        Console.WriteLine($"New Target Rating: {puzzleGenerator.TargetRating}");
        Console.WriteLine($"New Score: {puzzleGenerator.Score}");

        // Generate a new puzzle with the updated rating
        puzzleGenerator.GeneratePuzzle();

        // Print information about the new generated puzzle
        PrintPuzzleInfo(puzzleGenerator);
    }

    static void PrintPuzzleInfo(PuzzleGenerator puzzleGenerator)
    {
        Console.WriteLine("\nGenerated Puzzle Information:");
        var selectedScenario = puzzleGenerator.ScenarioList.FirstOrDefault(s => Math.Abs(s.Rating - puzzleGenerator.TargetRating) <= 100);

        if (selectedScenario != null)
        {
            Console.WriteLine($"Selected Scenario Rating: {selectedScenario.Rating}");
            Console.WriteLine($"Number of Starting Pieces: {selectedScenario.StartingPieces.Count}");
            Console.WriteLine("Starting Pieces:");
            foreach (var piece in selectedScenario.StartingPieces)
            {
                Console.WriteLine($"  {piece.GetType().Name} at position {piece.GetPosition(piece)}, color: {piece.GetTeam(piece)}");
            }
            Console.WriteLine($"Number of Correct Moves: {selectedScenario.CorrectMoves.Count}");
            Console.WriteLine($"Number of Counter Moves: {selectedScenario.CounterMoves.Count}");
        }
        else
        {
            Console.WriteLine("No scenario selected within the margin of error.");
        }
    }

    static void TestAddNoise(PuzzleGenerator puzzleGenerator)
    {
        Console.WriteLine("\nTesting AddNoise method:");
        var testScenario = puzzleGenerator.ScenarioList.First();
        int originalPieceCount = testScenario.StartingPieces.Count;

        Console.WriteLine($"Original piece count: {originalPieceCount}");

        puzzleGenerator.AddNoise(testScenario);

        Console.WriteLine($"Piece count after adding noise: {testScenario.StartingPieces.Count}");
        Console.WriteLine($"Noise pieces added: {testScenario.StartingPieces.Count - originalPieceCount}");
    }
}
