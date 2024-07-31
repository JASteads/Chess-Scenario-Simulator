using System;

public class StandardMode : Mode
{
    public StandardMode(
        EventHandler hEvent, EventHandler refreshEvent) 
        : base(hEvent, refreshEvent)
    {

    }

    protected override void OnEndGame(bool endTurn, bool isChecked)
    {
        ui.EndStandardGame(endTurn, isChecked);
    }
}