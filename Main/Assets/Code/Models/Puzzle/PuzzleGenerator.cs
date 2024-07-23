using System;
using System.Collections.Generic;
using System.Linq;

public class PuzzleGenerator
{
    public Game Simulation;
    public int TargetRating;
    public int NumAttempts;
    public List<Scenario> ScenarioList;
    public int Score;

    public void GeneratePuzzle()
    {
        
        int marginOfError = 100;
        
        // Get a list of scenarios within the margin of error of the target rating
        List<Scenario> eligibleScenarios = ScenarioList.Where(s => Math.Abs(s.Rating - TargetRating) <= marginOfError).ToList();

        if (!eligibleScenarios.Any())
        {
            return;
        }

        Random random = new Random();
        Scenario selectedScenario = eligibleScenarios[random.Next(eligibleScenarios.Count)];

        // If the selected scenario's rating is much lower than the target rating, add noise
        if (selectedScenario.Rating < TargetRating - marginOfError)
        {
            AddNoise(selectedScenario);
        }

        // Start the puzzle with the selected scenario
        StartPuzzle(selectedScenario);

    }


    public PuzzleGenerator()
    {
        ScenarioList = new List<Scenario>();
        InitScenarioList();
        TargetRating = 100; // Starting rating
        Score = 0;
    }

    public void InitScenarioList()
    {
        Scenario s1 = new Scenario
        {
            Rating = 100
        };
        s1.StartingPieces.Add(new Queen (47,0));
        s1.StartingPieces.Add(new King (60,1));
        s1.StartingPieces.Add(new Queen(42,0));
        AddMove(s1.CorrectMoves, 47, 4, 0);
        AddMove(s1.CorrectMoves, 42, 4, 0);
        AddMove(s1.CounterMoves, 60, 5, 1);
        ScenarioList.Add(s1);


        Scenario s2 = new Scenario
        {
            Rating = 200
        };
        s2.StartingPieces.Add(new Bishop (61,1));
        s2.StartingPieces.Add(new Pawn (14,0));
        AddMove(s2.CorrectMoves, 61, 3, 1);
        AddMove(s2.CounterMoves, 14, 0, 0);
        ScenarioList.Add(s2);


        Scenario s3 = new Scenario
        {
            Rating = 300
        };
        s3.StartingPieces.Add(new King (40,0));
        s3.StartingPieces.Add(new Rook (32,1));
        AddMove(s3.CorrectMoves, 40, 5, 0);
        AddMove(s3.CounterMoves, 32, 1, 1);
        ScenarioList.Add(s3);

        Scenario s4 = new Scenario
        {
            Rating = 400
        };
        s4.StartingPieces.Add(new Bishop (14,1));
        s4.StartingPieces.Add(new Queen (14,0));
        AddMove(s4.CorrectMoves, 14, 3, 1);
        AddMove(s4.CounterMoves, 14, 4, 0);
        ScenarioList.Add(s4);


        Scenario s5 = new Scenario
        {
            Rating = 500
        };
        s5.StartingPieces.Add(new Bishop (52,1));
        s5.StartingPieces.Add(new Queen (52,0));
        s5.StartingPieces.Add(new Queen (52,1));
        s5.StartingPieces.Add(new Bishop (52,0));
        AddMove(s5.CorrectMoves, 52, 3, 1);
        AddMove(s5.CorrectMoves, 52, 4, 1);
        AddMove(s5.CounterMoves, 52, 4, 0);
        AddMove(s5.CounterMoves, 52, 3, 0);
        ScenarioList.Add(s5);


        Scenario s6 = new Scenario
        {
            Rating = 600
        };
        s6.StartingPieces.Add(new Knight (49,0));
        s6.StartingPieces.Add(new Queen (41,1));
        AddMove(s6.CorrectMoves, 49, 2, 0);
        AddMove(s6.CounterMoves, 41, 4, 1);
        ScenarioList.Add(s6);


        Scenario s7 = new Scenario
        {
            Rating = 700
        };
        s7.StartingPieces.Add(new Knight (25,0));
        s7.StartingPieces.Add(new Queen (9,1));
        AddMove(s7.CorrectMoves, 25, 2, 0);
        AddMove(s7.CounterMoves, 9, 4, 1);
        ScenarioList.Add(s7);


        Scenario s8 = new Scenario
        {
            Rating = 800
        };
        s8.StartingPieces.Add(new Queen (9,0));
        s8.StartingPieces.Add(new Rook (36,1));
        s8.StartingPieces.Add(new Queen (38,0));
        s8.StartingPieces.Add(new Rook (35,1));
        s8.StartingPieces.Add(new King (40,0));
        s8.StartingPieces.Add(new Rook (24,1));
        AddMove(s8.CorrectMoves, 9, 4, 0);
        AddMove(s8.CorrectMoves, 38, 4, 0);
        AddMove(s8.CorrectMoves, 40, 5, 0);
        AddMove(s8.CounterMoves, 36, 1, 1);
        AddMove(s8.CounterMoves, 35, 1, 1);
        AddMove(s8.CounterMoves, 24, 1, 1);
        ScenarioList.Add(s8);


        Scenario s9 = new Scenario
        {
            Rating = 900
        };
        s9.StartingPieces.Add(new Queen (40,0));
        s9.StartingPieces.Add(new Queen (40,1));
        AddMove(s9.CorrectMoves, 40, 4, 0);
        AddMove(s9.CorrectMoves, 40, 4, 1);
        ScenarioList.Add(s9);


        Scenario s10 = new Scenario
        {
            Rating = 1000
        };
        s10.StartingPieces.Add(new Rook (39,0));
        s10.StartingPieces.Add(new Rook (47,1));
        s10.StartingPieces.Add(new Rook (47,0));
        AddMove(s10.CorrectMoves, 39, 1, 0);
        AddMove(s10.CorrectMoves, 47, 1, 0);
        AddMove(s10.CounterMoves, 47, 1, 1);
        ScenarioList.Add(s10);

        Scenario s11 = new Scenario
        {
            Rating = 1100
        };
        s11.StartingPieces.Add(new Queen (62,0));
        AddMove(s11.CorrectMoves, 62, 4, 0);
        ScenarioList.Add(s11);


        Scenario s12 = new Scenario
        {
            Rating = 1200
        };
        s12.StartingPieces.Add(new Rook (24,0));
        s12.StartingPieces.Add(new Rook (24,1));
        AddMove(s12.CorrectMoves, 24, 1, 0);
        AddMove(s12.CounterMoves, 24, 1, 1);
        ScenarioList.Add(s12);


        Scenario s13 = new Scenario
        {
            Rating = 1300
        };
        s13.StartingPieces.Add(new Rook (39,0));
        AddMove(s13.CorrectMoves, 39, 1, 0);
        ScenarioList.Add(s13);


        Scenario s14 = new Scenario
        {
            Rating = 1400
        };
        s14.StartingPieces.Add(new Rook (15,0));
        s14.StartingPieces.Add(new Rook (23,1));
        s14.StartingPieces.Add(new Rook (23,0));
        AddMove(s14.CorrectMoves, 15, 1, 0);
        AddMove(s14.CorrectMoves, 23, 1, 0);
        AddMove(s14.CounterMoves, 23, 1, 1);
        ScenarioList.Add(s14);


        Scenario s15 = new Scenario
        {
            Rating = 1500
        };
        s15.StartingPieces.Add(new Bishop (19,1));
        s15.StartingPieces.Add(new King (48,0));
        s15.StartingPieces.Add(new Rook (39,1));
        AddMove(s15.CorrectMoves, 19, 3, 1);
        AddMove(s15.CorrectMoves, 39, 1, 1);
        AddMove(s15.CounterMoves, 48, 5, 0);
        ScenarioList.Add(s15);
    }

    public void StartPuzzle(Scenario SelectedScenario)
    {
       
    }
   
    public void Update()
    {       
        Random random = new Random();
        int increaseAmount = random.Next(50, 151); 
        TargetRating += increaseAmount;

        // Increment the score
        Score++;


        GeneratePuzzle();
    }
    

    public void AddNoise(Scenario SelectedScenario)
    {
        Random random = new Random();
        
        int noiseCount = random.Next(0, 5); 

        // Keep track of occupied positions to avoid placing multiple pieces in the same position
        HashSet<int> occupiedPositions = new HashSet<int>();

        // Add positions of the starting pieces to the occupied positions
        foreach (var piece in SelectedScenario.StartingPieces)
        {
            occupiedPositions.Add(piece.GetPosition(piece));
        }


        List<Piece> noisePieces = new List<Piece>();


        // Add noise pieces
        for (int i = 0; i < noiseCount; i++)
        {
            int position;
            do
            {
                position = random.Next(0, 64); // 0-63, maxvalue(64) is exclusive 
            } while (occupiedPositions.Contains(position));

            occupiedPositions.Add(position);

            int pieceType = random.Next(0, 6);
            int color = random.Next(0, 2);

            Piece newPiece = null;
            switch (pieceType)
            {
                case 0:
                    SelectedScenario.StartingPieces.Add(new Pawn((ushort)position, (ushort)color));
                    break;
                case 1:
                    SelectedScenario.StartingPieces.Add(new Knight((ushort)position, (ushort)color));
                    break;
                case 2:
                    SelectedScenario.StartingPieces.Add(new Bishop((ushort)position, (ushort)color));
                    break;
                case 3:
                    SelectedScenario.StartingPieces.Add(new Rook((ushort)position, (ushort)color));
                    break;
                case 4:
                    SelectedScenario.StartingPieces.Add(new Queen((ushort)position, (ushort)color));
                    break;
                case 5:
                    SelectedScenario.StartingPieces.Add(new King((ushort)position, (ushort)color));
                    break;
            }

            if (newPiece != null)
            {
                SelectedScenario.StartingPieces.Add(newPiece);
                noisePieces.Add(newPiece);
            }

        }

        // Simulate the next move in the scenario
        foreach (var move in SelectedScenario.CorrectMoves)
        {
            int fromPosition = move & 63;
            int toPosition = (move >> 6) & 63;

            foreach (var noisePiece in noisePieces)
            {
                if (IsMoveObstructed(fromPosition, toPosition, noisePiece.GetPosition(noisePiece)))
                {
                    // Handle the obstruction by removing or demoting the noise piece
                    SelectedScenario.StartingPieces.Remove(noisePiece);
                    noisePieces.Remove(noisePiece);
                    break;
                }
            }
        }
    }

    private bool IsMoveObstructed(int from, int to, int obstacle)
    {
        int fromRow = from / 8, fromCol = from % 8;
        int toRow = to / 8, toCol = to % 8;
        int obstacleRow = obstacle / 8, obstacleCol = obstacle % 8;

        if (fromRow == toRow) // Horizontal move
        {
            return obstacleRow == fromRow && ((fromCol < obstacleCol && obstacleCol < toCol) || (toCol < obstacleCol && obstacleCol < fromCol));
        }
        if (fromCol == toCol) // Vertical move
        {
            return obstacleCol == fromCol && ((fromRow < obstacleRow && obstacleRow < toRow) || (toRow < obstacleRow && obstacleRow < fromRow));
        }
  
        return false;
    }
    
    private void AddMove(List<short> mList, ushort position, ushort type, ushort team)
    {
        bool success = 
            position >= 0 && position <= 63 &&
            type >= 0 && type <= 5 &&
            team <= 0 && team >= 1;

        if (success)
            mList.Add((short)(position | (type << 6) | (team << 9)));
        else
            throw new Exception(
                "At least one argument provided is out of bounds.");
    }

    public void OnMove()
    {

    }

}

    

