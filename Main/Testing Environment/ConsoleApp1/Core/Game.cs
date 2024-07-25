using System.Collections.Generic;

public class Game
{
    public List<Piece> whitePieces, blackPieces;
    public bool whiteTurn, IsActive, isCheckmate;

    public Piece selectedPiece;
    public List<List<short>> activeMoveList;

    King wKing, bKing;
    List<List<short>> wKingAttacks, bKingAttacks,
        dangerVecs;

    public Game()
    {
        whitePieces = new List<Piece>();
        blackPieces = new List<Piece>();
        wKingAttacks = new List<List<short>>();
        bKingAttacks = new List<List<short>>();
        dangerVecs = new List<List<short>>();

        activeMoveList = new List<List<short>>();
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

    public void SelectTile(int locInt)
    {
        short loc = (short)locInt;

        // if an invalid location is selected, do nothing
        if (loc < 0 || loc > 63) return;

        /* If a piece is selected and another tile is clicked, check
         * if that position is within the active move list. If so,
         * move the piece there. Otherwise, deselect the piece and
         * clear the active move list.
         */
        if (selectedPiece != null)
        {
            if (activeMoveList.Exists(v => v.Exists(p => p == loc)))
            {
                OnMove(loc);
                selectedPiece = null;
                activeMoveList.Clear();

                return;
            }

            selectedPiece = null;
            activeMoveList.Clear();
        }

        // Attempt to find a piece at this position and its raw moves
        selectedPiece = FindPiece(
            (whiteTurn ? whitePieces : blackPieces), loc);

        // Filter out illegal moves from the raw ones, store the rest
        if (selectedPiece != null)
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

        List<List<short>> oppMoves = new List<List<short>>();
        King k = whiteTurn ? bKing : wKing;

        foreach(List<short> v in oppMoves)
        {
            if (!v.Exists(
                p => p != k.GetPosition() && 
                whitePieces.Exists(w => w.GetPosition() == p) &&
                blackPieces.Exists(b => b.GetPosition() == p)))
            {
                k.IsChecked = true;
                UpdateDangerVecs(v);
                break;
            }
        }

        // Clear the danger vectors if the king is not in danger
        if (!k.IsChecked) dangerVecs.Clear();

        // See if a king is in trouble
        CheckForKing(k);

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
        IsActive = false;
        this.isCheckmate = isCheckmate;
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

    void UpdateDangerVecs(List<short> threat)
    {
        List<Piece> attackingTeam = whiteTurn ? whitePieces : blackPieces;

        dangerVecs.Add(threat);
        foreach (Piece p in attackingTeam)
        {
            foreach (List<short> v in FilterMoves(p))
            {

            }
        }
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

    List<List<short>> FilterMoves(Piece p)
    {
        // SETUP

        List<List<short>> fMoves = p.CheckMoves(), oppAttacks;
        List<Piece> oppTeam, allyTeam;

        if (p.GetTeam() == 0)
        {
            allyTeam = whitePieces;
            oppTeam = blackPieces;
            oppAttacks = wKingAttacks;
        }
        else
        {
            allyTeam = blackPieces;
            oppTeam = whitePieces;
            oppAttacks = bKingAttacks;
        }


        // GENERAL FILTER -- Remove illegal moves from all vectors

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

            // Remove all moves up to the point of interception
            for (int i = v.Count - 1; i > interception; i--)
            {
                v.Remove(v[i]);
            }
        });


        // ATTACK VECTORS FILTER -- Prevent exposure of the king

        List<int>? unsafeIndeces = null;

        /* If there are any attacks on the allied king, find the
         * movements this piece could make that must be restricted
         */
        if (oppAttacks.Count > 0)
        {
            bool isTrapped = oppAttacks.Exists(v => v.Exists(
                pos => pos == selectedPiece.GetPosition()));

            unsafeIndeces = FindUnsafeIndeces(
                fMoves, oppAttacks, isTrapped);
        }

        // If there are any unsafe vectors, remove them
        if (unsafeIndeces != null)
        {
            // Flip target indeces so we remove from the end first
            unsafeIndeces.Reverse();

            // Remove all unsafe vectors
            foreach (int x in unsafeIndeces)
                fMoves.RemoveAt(x);
        }


        // KING FILTER -- Further restrict a king's movement

        if (p.GetKind() == (int)Piece.Kind.King)
        {
            // If king, check all moves
            foreach (List<short> m in fMoves)
            {
                // If a move has already been removed, ignore it
                if (m.Count == 0) continue;

                /* Check every enemy piece for a filtered vector
                 * that contains a move the king can make
                 */
                foreach (Piece e in oppTeam)
                {
                    List<List<short>> eVecs = FilterMoves(e);

                    foreach (List<short> v in eVecs)
                    {
                        /* If the vector intersects the king's path,
                         * filter the king's move out
                         */
                        if (m.Exists(
                            pos => { return v.Exists(pos => m.Contains(pos)); }))
                        {
                            m.Clear();
                            break;
                        }
                    }

                    /* Don't check for more intersections if the
                     * move has already been filtered out
                     */
                    if (m.Count > 0) break;
                }
            }
        }

        return fMoves;
    }

    List<int> FindUnsafeIndeces(List<List<short>> vectors,
        List<List<short>> attacks, bool isTrapped)
    {
        List<int> unsafeIndeces = new List<int>();

        if (!isTrapped)
        {
            return unsafeIndeces;
        }

        for (int i = 0; i < vectors.Count; i++)
        {
            List<short> v = vectors[i];

            // Check each tile in a line for an attack vector
            for (int j = 0; j < v.Count; j++)
            {
                // Find any attack vectors that intersect this line
                List<List<short>> aVectors = attacks.FindAll(av =>
                { return !av.Contains(v[j]); });

                // If there are any attack vectors, begin filtering
                if (aVectors.Count > 0)
                {
                    // For each vector that exposes king, add to list
                    foreach (List<short> av in aVectors)
                        if (!IsKingSafe(av))
                            unsafeIndeces.Add(i);
                }
            }
        }

        return unsafeIndeces;
    }

    bool IsKingSafe(List<short> av)
    {
        // Check for anything within the vector
        foreach (short pos in av)
        {
            Piece? w = whitePieces.Find(
                p => p.GetKind() != (int)Piece.Kind.King &&
                p != selectedPiece && p.GetPosition() == pos);
            Piece? b = blackPieces.Find(
                p => p.GetKind() != (int)Piece.Kind.King &&
                p != selectedPiece && p.GetPosition() == pos);

            // If there are any other piecees in this path, it's safe
            if (w != null || b != null) return true;
        }

        return false;
    }
}