using System.Collections.Generic;

public class Game
{
    public List<Piece> whitePieces, blackPieces;
    public bool whiteTurn, IsActive;

    Piece selectedPiece;
    King wKing, bKing;
    List<short> activeMoveList;
    List<List<short>> wKingAttacks, bKingAttacks;

    public Game()
    {
        whitePieces = new List<Piece>();
        blackPieces = new List<Piece>();

        activeMoveList = new List<short>();
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
        selectedPiece = FindPiece(
            (whiteTurn ? whitePieces : blackPieces), loc);

        // Filter out illegal moves from the raw ones, store the rest
        activeMoveList = FilterMoves(selectedPiece);
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
            int row = selectedPiece.GetPosition() / 8;

            /* If on white team and on the other side of the board.
             * Promote to queen as a default for now
             */
            if (whiteTurn && row == 7)
                selectedPiece.SetKind(4);
            else if (!whiteTurn && row == 0)
                selectedPiece.SetKind(4);
        }

        OnMoveEnd();
    }

    void OnCapture(Piece p)
    {

    }

    void OnMoveEnd()
    {
        // Update the attack vectors of the current team
        UpdateKingAttack();

        // See if a king is in trouble
        CheckForKing(whiteTurn ? bKing : wKing);

        // Change turns
        whiteTurn = !whiteTurn;
    }

    void CheckForKing(King k)
    {
        if (FilterMoves(k).Count == 0)
            if (CheckForEnd(whiteTurn ? blackPieces : whitePieces))
                OnGameEnd(k.IsChecked ? true : false);
    }

    bool CheckForEnd(List<Piece> team)
    {
        foreach (Piece p in team)
        {
            if (FilterMoves(p).Count > 0)
                return false;
        }

        return true;
    }

    void OnGameEnd(bool isCheckmate)
    {

    }

    // Attempts to return a piece from pieces at the given location
    Piece FindPiece(List<Piece> pieces, short loc)
    {
        for (int i = 0; i < pieces.Count; i++)
            if (pieces[i].GetPosition() == loc)
                return pieces[i];

        return null;
    }

    void UpdateKingAttack()
    {
        if (whiteTurn)
            bKingAttacks = FindAttackVectors(bKing, whitePieces);
        else
            wKingAttacks = FindAttackVectors(wKing, blackPieces);
    }

    // Returns all tiles that create a line of sight on the given king
    List<List<short>> FindAttackVectors(King k, List<Piece> team)
    {
        short kPos = (short)k.GetPosition();

        List<List<short>> result = new List<List<short>>();

        foreach (Piece p in team)
        {
            List<int> indeces = new List<int>();

            // Get a list of all possible paths
            List<List<short>> attackVectors = p.CheckMoves();

            // Exclude all paths that don't include the enemy king
            for (int i = 0; i < attackVectors.Count; i++)
            {
                if (!attackVectors[i].Contains(kPos))
                    indeces.Add(i);
            }
            indeces.Reverse();

            foreach (int i in indeces)
                attackVectors.RemoveAt(i);

            result.AddRange(attackVectors);
        }

        return result;
    }

    List<short> FilterMoves(Piece p)
    {
        // Setup

        List<List<short>> fMoves = p.CheckMoves(), oppAttacks;
        List<Piece> oppTeam, allyTeam;

        if (p.GetTeam() == 0)
        {
            allyTeam = whitePieces;
            oppTeam = blackPieces;
            oppAttacks = bKingAttacks;
        }
        else
        {
            allyTeam = blackPieces;
            oppTeam = whitePieces;
            oppAttacks = wKingAttacks;
        }


        // GENERAL FILTER

        // Check each vector for an intercepting piece on either team
        fMoves.ForEach(v =>
        {
            int interception = v.Count;

            // Check enemy pieces
            for (int i = 0; i < v.Count; i++)
            {
                if (oppTeam.Find(
                    op => op.GetPosition() == v[i]) != null)
                {
                    interception = i;
                    break;
                }
            }

            // Check ally pieces. These have higher priority
            for (int i = 0; i < v.Count; i++)
            {
                if (allyTeam.Find(
                    ally => ally.GetPosition() == v[i]) != null)
                {
                    if (i <= interception)
                        interception = i - 1;
                    break;
                }
            }

            for (int i = v.Count - 1; i > interception; i--)
            {
                v.Remove(v[i]);
            }
        });


        // ATTACK VECTORS FILTER

        List<int> unsafeIndeces = new List<int>();

        for (int i = 0; i <= fMoves.Count; i++)
        {
            List<short> v = fMoves[i];

            // Check each tile in a line for an attack vector
            for (int j = 0; j < v.Count; j++)
            {
                // Find attack vector if any
                List<short> aVector = oppAttacks.Find(av =>
                {
                    return av.Contains(v[j]);
                });

                if (aVector != null)
                {
                    bool safe = false;

                    // If found, check for anything behind the piece
                    foreach (short pos in aVector)
                    {
                        if (allyTeam.Find(
                            ally => ally.GetPosition() == pos) != null)
                        {
                            safe = true;
                            break;
                        }
                    }

                    if (!safe)
                    {
                        unsafeIndeces.Add(i);
                    }
                }
            }
        }

        // Flip target indeces so we remove from the end first
        unsafeIndeces.Reverse();

        // Remove all unsafe vectors
        foreach (int x in unsafeIndeces)
            fMoves.RemoveAt(x);

        return null;
    }
}