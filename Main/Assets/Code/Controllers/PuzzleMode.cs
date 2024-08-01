using System;

public class PuzzleMode : Mode
{
    PuzzleGenerator generator;

    public PuzzleMode(EventHandler hEvent) :  base(hEvent)
    {
        // generator = new PuzzleGenerator();
    }

    public override void StartGame()
    {
        g.SetBoard(new System.Collections.Generic.List<Piece>());
        UpdateBoard();
    }

    protected override void OnEndGame(bool endTurn, bool isChecked)
    {

    }
}