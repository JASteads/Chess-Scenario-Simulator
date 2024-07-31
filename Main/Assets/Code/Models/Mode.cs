using System;

public abstract class Mode
{
    protected Game g;
    protected BoardUI ui;

    public Mode(EventHandler hEvent, EventHandler refreshEvent)
    {
        g = new Game();
        ui = new BoardUI(hEvent, refreshEvent);
        g.GameEnd += (s, e) => OnEndGame(e.EndTurn, e.IsChecked);
        g.PieceCapture += (s, e) => OnCapture(e.Piece);
        g.PieceMove += (s, e) => OnPieceMove();
        ui.Selection += (s, e) => PlayTurn(e.Position);
    }

    public BoardUI GetBoardUI()
    {
        return ui;
    }

    public void StartGame()
    {
        g.SetBoard();
        UpdateBoard();
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
        ui.ChangeTurn();
    }

    void UpdateBoard()
    {
        ui.SetTiles(
            g.whitePieces, g.blackPieces, g.activeMoveList);
        Console.WriteLine("Tiles set");
    }

    protected abstract void OnEndGame(bool endTurn, bool isChecked);
}