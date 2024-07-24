using System.Collections.Generic;
using System.Security;


public class Game
{
    public List<Piece> WhitePieces, BlackPieces;
    public bool WhiteTurn, IsActive;

    Piece selectedPiece;
    List<short> activeMoveList;

    public Game()
    {
        WhitePieces = new List<Piece>();
        BlackPieces = new List<Piece>();
        activeMoveList = new List<short>();
    }

    public List<short> FilterMoves(List<short> m)
    {
        return m;
    }

    public void SetBoard() 
    {
        // Start game with white going first
        WhiteTurn = true;
        IsActive = true;
        
        // Add pieces to the board
        WhitePieces.Add(new Rook (0, 0));
        WhitePieces.Add(new Rook (7, 0));
        WhitePieces.Add(new Knight (1, 0));
        WhitePieces.Add(new Knight (6, 0));
        WhitePieces.Add(new Bishop (2, 0));
        WhitePieces.Add(new Bishop (5, 0));
        WhitePieces.Add(new Queen (3, 0));
        WhitePieces.Add(new King (4, 0));
        for (int i = 8; i < 16; i++)
            WhitePieces.Add(new Pawn (i, 0));
       
        BlackPieces.Add(new Rook (56, 1));
        BlackPieces.Add(new Rook (63, 1));
        BlackPieces.Add(new Knight (57, 1));
        BlackPieces.Add(new Knight (62, 1));
        BlackPieces.Add(new Bishop (58, 1));
        BlackPieces.Add(new Bishop (61, 1));
        BlackPieces.Add(new Queen (59, 1));
        BlackPieces.Add(new King (60, 1));
        for (int i = 48; i < 56; i++)
            BlackPieces.Add(new Pawn (i, 1));
    }
    public void SetBoard(List<Piece> pieces) 
    {
        // Start game with white going first
        WhiteTurn = true;
        IsActive = true;

        // Add pieces from the scenario
        for(int i = 0; i < pieces.Count; i++)
        {
            if(pieces[i].GetTeam() == 0)
            {
                WhitePieces.Add(pieces[i]);
            }
            else
            {
                BlackPieces.Add(pieces[i]);
            }
        }
    }
    public void SelectPiece(ushort loc) 
    {   
        Piece target;
        List<short> moves = new List<short>();
        
        // if an invalid location is selected, do nothing
        if (loc < 0 || loc > 63) return;
        
        if (selectedPiece != null)
        {
            
        }

        moves = TryGetRawMoves(
            (WhiteTurn ? WhitePieces : BlackPieces), loc);
        activeMoveList = FilterMoves(moves);
        
        
    }
    public void OnMove(Piece p, ushort loc) 
    {
       filterMoves();
       p.SetPosition(loc);
    }

    /* Attempts to find a piece from pieces at the given location.
     * if found, return a list of its unfiltered moves.
     */
    List<short> TryGetRawMoves(List<Piece> pieces, short loc)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].GetPosition() == loc)
                return pieces[i].CheckMoves();
        }
    }

}