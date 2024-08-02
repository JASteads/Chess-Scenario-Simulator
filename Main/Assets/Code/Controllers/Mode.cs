using System;

public abstract class Mode
{
    protected Game g;
    protected BoardUI ui;

    public Mode(EventHandler hEvent)
    {
        g = new Game();
        ui = new BoardUI(hEvent);
        g.GameEnd += (s, e) => OnEndGame(e.EndTurn, e.IsChecked);
        g.PieceCapture += (s, e) => OnCapture(e.Piece);
        g.PieceMove += (s, e) => OnPieceMove();
        ui.Selection += (s, e) => PlayTurn(e.Position);
    }

    public BoardUI GetBoardUI()
    {
        return ui;
    }
    
    void PlayTurn(int pos)
    {
        g.SelectTile(pos);
        UpdateBoard();
    }

    void OnCapture(Piece p)
    {
        // This can do something fancy if we want it to
    }

    void OnPieceMove()
    {
        if (g.IsActive) ui.ChangeTurn();
    }

    protected void UpdateBoard()
    {
        ui.SetTiles(
            g.whitePieces, g.blackPieces, g.activeMoveList);
        Console.WriteLine("Tiles set");
    }

    public abstract void StartGame();
    protected abstract void OnEndGame(bool endTurn, bool isChecked);

}