using System.Collections.Generic;


public class Game
{
    public List<Piece> WhitePieces = new List<Piece>();
    public List<Piece> BlackPieces = new List<Piece>();
    public bool WhiteTurn;
    public bool IsActive;

    public void filterMoves()
    {

    }

    public void SetBoard() 
    { 
        // Start game with white going first
        WhiteTurn = true;
        
        // Add pieces to the board
        WhitePieces.Add(new Rook (0, 0));
        WhitePieces.Add(new Rook (7, 0));
        WhitePieces.Add(new Knight (1, 0));
        WhitePieces.Add(new Knight (6, 0));
        WhitePieces.Add(new Bishop (2, 0));
        WhitePieces.Add(new Bishop (5, 0));
        WhitePieces.Add(new Queen (3, 0));
        WhitePieces.Add(new King (4, 0));
        WhitePieces.Add(new Pawn (8, 0));
        WhitePieces.Add(new Pawn (9, 0));
        WhitePieces.Add(new Pawn (10, 0));
        WhitePieces.Add(new Pawn (11, 0));
        WhitePieces.Add(new Pawn (12, 0));
        WhitePieces.Add(new Pawn (13, 0));
        WhitePieces.Add(new Pawn (14, 0));
        WhitePieces.Add(new Pawn (15, 0));
       
        BlackPieces.Add(new Rook (56, 1));
        BlackPieces.Add(new Rook (63, 1));
        BlackPieces.Add(new Knight (57, 1));
        BlackPieces.Add(new Knight (62, 1));
        BlackPieces.Add(new Bishop (58, 1));
        BlackPieces.Add(new Bishop (61, 1));
        BlackPieces.Add(new Queen (59, 1));
        BlackPieces.Add(new King (60, 1));
        BlackPieces.Add(new Pawn (48, 1));
        BlackPieces.Add(new Pawn (49, 1));
        BlackPieces.Add(new Pawn (50, 1));
        BlackPieces.Add(new Pawn (51, 1));
        BlackPieces.Add(new Pawn (52, 1));
        BlackPieces.Add(new Pawn (53, 1));
        BlackPieces.Add(new Pawn (54, 1));
        BlackPieces.Add(new Pawn (55, 1));


    }
    public void SetBoard(List<Piece> pieces) 
    {
        // Start game with white going first
        WhiteTurn = true;

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
    public void SelectPiece(short loc) 
    {   
        Game wPiece = new Game();
        Game bPiece = new Game();
        // if a valid location is selected, check and see if a piece is occupying the space
        // if WhiteTurn is true, verify the piece is white, select the piece, and check all possible movements
        // if WhiteTurn is false, verify the piece is black, then check all possible movements
        while(loc > 0 && loc < 63)
        {
            if(WhiteTurn == true)
            {
                // check to see if it is a white piece
                

            }
            else if(WhiteTurn == false)
            {
                // check to see if it is a black piece  
                
            }
            else
            {
                // the location selected has no piece so do nothing
            }
        }
    }
    public void OnMove(Piece p, short loc) 
    {

    }

}