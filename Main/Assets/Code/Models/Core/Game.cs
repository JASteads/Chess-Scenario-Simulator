using System.Collections.Generic;

public class Game
{
    public List<Piece> whitePieces, blackPieces;
    public bool whiteTurn, IsActive;

    Piece selectedPiece;
    King wKing, bKing;
    List<short> activeMoveList, wKingAttacks, bKingAttacks;

    public Game()
    {
        whitePieces = new List<Piece>();
        blackPieces = new List<Piece>();

        activeMoveList = new List<short>();
    }

    public List<short> FilterMoves(List<short> m)
    {
        // General filter



        // Attack vector filter



        return m;
    }

    public void SetBoard()
    {
        // Start game with white going first
        whiteTurn = true;
        IsActive = true;

        // Add pieces to the board
        whitePieces.Add(new Rook(0, 0));
        whitePieces.Add(new Rook(7, 0));
        whitePieces.Add(new Knight(1, 0));
        whitePieces.Add(new Knight(6, 0));
        whitePieces.Add(new Bishop(2, 0));
        whitePieces.Add(new Bishop(5, 0));
        whitePieces.Add(new Queen(3, 0));
        whitePieces.Add(new King(4, 0));

        // Keep track of the white king for checks
        wKing = (King)whitePieces[whitePieces.Count - 1];

        for (short i = 8; i < 16; i++)
            whitePieces.Add(new Pawn(i, 0));

        blackPieces.Add(new Rook(56, 1));
        blackPieces.Add(new Rook(63, 1));
        blackPieces.Add(new Knight(57, 1));
        blackPieces.Add(new Knight(62, 1));
        blackPieces.Add(new Bishop(58, 1));
        blackPieces.Add(new Bishop(61, 1));
        blackPieces.Add(new Queen(59, 1));
        blackPieces.Add(new King(60, 1));

        // Keep track of the black king for checks
        bKing = (King)blackPieces[blackPieces.Count - 1];

        for (short i = 48; i < 56; i++)
            blackPieces.Add(new Pawn(i, 1));
    }
    public void SetBoard(List<Piece> pieces)
    {
        // Start game with white going first
        whiteTurn = true;
        IsActive = true;

        // Add pieces from the scenario
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].GetTeam() == 0)
            {
                whitePieces.Add(pieces[i]);
            }
            else
            {
                blackPieces.Add(pieces[i]);
            }
        }
    }

    public void SelectTile(short loc)
    {
        List<short> moves; // This will store unfiltered moves

        // if an invalid location is selected, do nothing
        if (loc < 0 || loc > 63) return;

        /* If a piece is selected and another tile is clicked, check
         * if that position is within the active move list. If so,
         * move the piece there. Otherwise, deselect the piece and
         * clear the active move list.
         */
        if (selectedPiece != null)
            if (activeMoveList.Exists(pos => pos == loc))
            {
                OnMove(loc);
                return;
            }
            else
            {
                selectedPiece = null;
                activeMoveList = null;
            }

        // Attempt to find a piece at this position and its raw moves
        moves = TryGetRawMoves(
            (whiteTurn ? whitePieces : blackPieces),
            loc, out selectedPiece);

        // Filter out illegal moves from the raw ones, store the rest
        activeMoveList = FilterMoves(moves);
    }

    public void OnMove(short loc)
    {
        selectedPiece.SetPosition(loc);

        Piece defender = whiteTurn ? 
            blackPieces.Find(p => p.GetPosition() == loc) :
            whitePieces.Find(p => p.GetPosition() == loc);

        if (defender != null)
        {
            OnCapture(defender);
        }

        // Pawn logic
        if (selectedPiece.GetKind() == 0)
        {
            int pos = selectedPiece.GetPosition();

            // If on white team and on the other side of the board
            if (selectedPiece.GetTeam() == 0 && 
                (pos / 8 == 7))
            {
                // Promote to queen as a default for now
                selectedPiece.SetKind(4);
            }
        }

        OnMoveEnd();
    }

    void OnCapture(Piece p)
    {

    }

    void OnMoveEnd()
    {
        // See if a king is in trouble
        CheckForKing(whiteTurn ? bKing : wKing);

        // Change turns
        whiteTurn = !whiteTurn;
    }

    void CheckForKing(King k)
    {
        if (FilterMoves(k.CheckMoves()).Count == 0)
            if (CheckForEnd(whiteTurn ? blackPieces : whitePieces))
                OnGameEnd(k.IsChecked ? true : false);
    }

    bool CheckForEnd(List<Piece> team)
    {
        foreach (Piece p in team)
        {
            if (FilterMoves(p.CheckMoves()).Count > 0)
                return false;
        }

        return true;
    }

    void OnGameEnd(bool isCheckmate)
    {

    }

    /* Attempts to find a piece from pieces at the given location.
     * if found, return a list of its unfiltered moves.
     */
    List<short> TryGetRawMoves(
        List<Piece> pieces, short loc, out Piece target)
    {
        target = null;

        for (int i = 0; i < pieces.Count; i++)
            if (pieces[i].GetPosition() == loc)
            {
                target = pieces[i];
                return pieces[i].CheckMoves();
            }

        return null;
    }
}