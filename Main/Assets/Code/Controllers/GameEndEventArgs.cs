using System;

public class GameEndEventArgs : EventArgs
{
    public bool EndTurn { get; set; }
    public bool IsChecked { get; set; }
}