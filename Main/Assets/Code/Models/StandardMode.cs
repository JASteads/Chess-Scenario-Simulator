using System;

public class StandardMode : Mode
{
    public StandardMode(EventHandler hEvent) : base(hEvent)
    {

    }

    public override void StartGame()
    {
        g.SetBoard();
        UpdateBoard();
    }

    protected override void OnEndGame(bool endTurn, bool isChecked)
    {
        ui.EndStandardGame(endTurn, isChecked);
    }
}