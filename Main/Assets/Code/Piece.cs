public abstract class Piece
{
    // public Graphic image;
    public short information;

    public abstract bool CheckMoves()
    {
        return true;
    }


    public void Move()
    {

    }

    public void OnCapture()
    {

    }
}

public class Pawn:Piece
{
    public void Promote() { }
    public void CanEnPassant() { }
}

public class Game
{
    public List<Piece> WhitePieces;
    public List<Piece> BlackPieces;
    public bool WhiteTurn;
    public bool IsActive;

    public void SetBoard() { }
    public void SelectPiece() { }
    public void OnMove() { }
}

public class Scenario
{
    public int Rating;
    public List<Piece> StartingPieces;
    public List<short> CorrectMoves;
    public List<short> CounterMoves;
}

public class PuzzleGenerator
{
    public Game Simulation;
    public int TargetRating;
    public int NumAttempts;
    public List<Scenario> ScenarioList;
    public int Score;

    public Game GeneratePuzzle
    {
        return null; 
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