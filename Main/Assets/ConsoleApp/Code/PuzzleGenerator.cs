using System.Collections.Generic;

public class PuzzleGenerator
{
    public Game Simulation;
    public int TargetRating;
    public int NumAttempts;
    public List<Scenario> ScenarioList;
    public int Score;

    public Game GeneratePuzzle()
    {
        return null; 
    }


    public PuzzleGenerator()
    {
        ScenarioList = new List<Scenario>();
    }

    public void InitScenarioList()
    {
        Scenario s1 = new Scenario
        {
            Rating = 100
        };
        s1.StartingPieces.Add(new Queen (61,0));
        s1.StartingPieces.Add(new King (39,1));
        s1.StartingPieces.Add(new Queen(21,0));
        s1.CorrectMoves.Add(); 
        s1.CounterMoves.Add(); 
        ScenarioList.Add(s1);


        Scenario s2 = new Scenario
        {
            Rating = 200
        };
        s2.StartingPieces.Add(new Bishop (47,1));
        s2.StartingPieces.Add(new Pawn (49,0));
        s2.CorrectMoves.Add(); 
        s2.CounterMoves.Add(); 
        ScenarioList.Add(s2);


        Scenario s3 = new Scenario
        {
            Rating = 300
        };
        s3.StartingPieces.Add(new King (5,0));
        s3.StartingPieces.Add(new Rook (4,1));
        s3.CorrectMoves.Add(); 
        s3.CounterMoves.Add(); 
        ScenarioList.Add(s3);

        Scenario s4 = new Scenario
        {
            Rating = 400
        };
        s4.StartingPieces.Add(new Bishop (49,1));
        s4.StartingPieces.Add(new Queen (49,0));
        s4.CorrectMoves.Add(); 
        s4.CounterMoves.Add(); 
        ScenarioList.Add(s4);


        Scenario s5 = new Scenario
        {
            Rating = 500
        };
        s5.StartingPieces.Add(new Bisop (38,1));
        s5.StartingPieces.Add(new Queen (38,0));
        s5.StartingPieces.Add(new Queen (38,1));
        s5.StartingPieces.Add(new Bishop (38,0));
        s5.CorrectMoves.Add(); 
        s5.CounterMoves.Add(); 
        ScenarioList.Add(s5);


        Scenario s6 = new Scenario
        {
            Rating = 600
        };
        s6.StartingPieces.Add(new Knight (14,0));
        s6.StartingPieces.Add(new Queen (13,1));
        s6.CorrectMoves.Add(); 
        s6.CounterMoves.Add(); 
        ScenarioList.Add(s6);


        Scenario s7 = new Scenario
        {
            Rating = 700
        };
        s7.StartingPieces.Add(new Knight (11,0));
        s7.StartingPieces.Add(new Queen (9,1));
        s7.CorrectMoves.Add(); 
        s7.CounterMoves.Add(); 
        ScenarioList.Add(s7);


        Scenario s8 = new Scenario
        {
            Rating = 800
        };
        s8.StartingPieces.Add(new Queen (9,0));
        s8.StartingPieces.Add(new Rook (36,1));
        s8.StartingPieces.Add(new Queen (52,0));
        s8.StartingPieces.Add(new Rook (28,1));
        s8.StartingPieces.Add(new King (5,0));
        s8.StartingPieces.Add(new Rook (3, 1));
        s8.CorrectMoves.Add(); 
        s8.CounterMoves.Add(); 
        ScenarioList.Add(s8);


        Scenario s9 = new Scenario
        {
            Rating = 900
        };
        s9.StartingPieces.Add(new Queen (5,0));
        s9.StartingPieces.Add(new Queen (5,1));
        s9.CorrectMoves.Add(); 
        s9.CounterMoves.Add(); 
        ScenarioList.Add(s9);


        Scenario s10 = new Scenario
        {
            Rating = 1000
        };
        s10.StartingPieces.Add(new Rook (60,0));
        s10.StartingPieces.Add(new Rook (61,1));
        s10.StartingPieces.Add(new Rook (61,0));
        s10.CorrectMoves.Add(); 
        s10.CounterMoves.Add(); 
        ScenarioList.Add(s10);

        Scenario s11 = new Scenario
        {
            Rating = 1100
        };
        s11.StartingPieces.Add(new Queen (55,0));
        s11.CorrectMoves.Add(); 
        s11.CounterMoves.Add(); 
        ScenarioList.Add(s11);


        Scenario s12 = new Scenario
        {
            Rating = 1200
        };
        s12.StartingPieces.Add(new Rook (3,0));
        s12.StartingPieces.Add(new Rook (3,1));
        s12.CorrectMoves.Add(); 
        s12.CounterMoves.Add(); 
        ScenarioList.Add(s12);


        Scenario s13 = new Scenario
        {
            Rating = 1300
        };
        s13.StartingPieces.Add(new Rook (60,0));
        
        s13.CorrectMoves.Add(); 
        s13.CounterMoves.Add(); 
        ScenarioList.Add(s13);


        Scenario s14 = new Scenario
        {
            Rating = 1400
        };
        s14.StartingPieces.Add(new Rook (57,0)););
        s14.StartingPieces.Add(new Rook (58,1));
        s14.StartingPieces.Add(new Rook (58,0));
        s14.CorrectMoves.Add(); 
        s14.CounterMoves.Add(); 
        ScenarioList.Add(s14);



        Scenario s15 = new Scenario
        {
            Rating = 1500
        };
        s15.StartingPieces.Add(new Bishop (26,1));
        s15.StartingPieces.Add(new King (6,0));
        s15.StartingPieces.Add(new Rook (60,1));
        s15.CorrectMoves.Add(); 
        s15.CounterMoves.Add(); 
        ScenarioList.Add(s15);
        
    }

    public void StartPuzzle()
    {

    }
   
    public void Update()
    {

    }
    
    public void AddNoise()
    {

    }
    
    public void OnMove()
    {

    }
}