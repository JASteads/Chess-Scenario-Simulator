public class Game
{
    public List<Piece> whitePieces, blackPieces;
    public bool whiteTurn, IsActive, isCheckmate;

    public Piece selectedPiece;
    public List<List<short>> activeMoveList;

    King wKing, bKing;
    List<List<short>> wKingAttacks, bKingAttacks;
    List<short> threatVec;

    public Game()
    {
        whitePieces = [];
        blackPieces = [];
        wKingAttacks = [];
        bKingAttacks = [];
        threatVec = [];
        activeMoveList = [];
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

        wKing = new King(4, 0);
        whitePieces.Add(wKing);

        for (short i = 8; i < 16; i++)
            whitePieces.Add(new Pawn(i, 0));

        blackPieces.Add(new Rook(56, 1));
        blackPieces.Add(new Rook(63, 1));
        blackPieces.Add(new Knight(57, 1));
        blackPieces.Add(new Knight(62, 1));
        blackPieces.Add(new Bishop(58, 1));
        blackPieces.Add(new Bishop(61, 1));
        blackPieces.Add(new Queen(59, 1));

        bKing = new King(60, 1);
        blackPieces.Add(bKing);

        for (short i = 48; i < 56; i++)
            blackPieces.Add(new Pawn(i, 1));
    }
    public void SetBoard(List<Piece> set)
    {
        // TODO: Use the scenario's first correct move 
        // to determine starting turn
        whiteTurn = true;

        // Add pieces from the scenario
        foreach(Piece p in set)
        { 
            List<Piece> team = p.GetTeam() == 0 ?
                whitePieces : blackPieces;

            team.Add(p);
        }

        IsActive = true;
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
        int prev = selectedPiece.GetPosition(),
            prevRow = prev / 8,
            prevCol = prev % 8;
        List<Piece> oppTeam = whiteTurn ? blackPieces : whitePieces;

        selectedPiece.SetPosition(loc);

        int pPos = selectedPiece.GetPosition();

        Piece? defender = FindPiece(oppTeam, loc);

        // Pawn logic
        if (selectedPiece.GetKind() == (int)Piece.Kind.Pawn)
        {
            int row = pPos / 8,
                col = pPos % 8,
                promoRow = whiteTurn ? 7 : 0;
            bool isNowVulnerable = whiteTurn ?
                prevRow == row - 2 :
                prevRow == row + 2;

            // The pawn becomes vulnerable if it's taken two steps
            (selectedPiece as Pawn).isVulnerable = isNowVulnerable;
            
            // Try en passant if a defender hasn't been found yet
            if (defender == null && prevCol != col)
            {
                int defenderPos = loc + (whiteTurn ? -8 : 8);

                defender = oppTeam.Find(
                    p => p.GetPosition() == defenderPos &&
                    p.GetKind() == (int)Piece.Kind.Pawn);
            }   

            // If on the other side of the board.
            // Promote to queen as a default for now
            if (row == promoRow) selectedPiece.SetKind(4);
        }

        if (defender != null) OnCapture(defender);

        OnMoveEnd();
    }

    void OnCapture(Piece p)
    {
        List<Piece> oppTeam = whiteTurn ? blackPieces : whitePieces;

        oppTeam.Remove(p);
    }

    void OnMoveEnd()
    {
        List<List<short>> oppMoves;
        King k;
        List<Piece> opps;

        // Update attack vectors
        UpdateKingAttack();

        if (whiteTurn)
        {
            oppMoves = bKingAttacks;
            opps = blackPieces;
            k = bKing;
        }
        else
        {
            oppMoves = wKingAttacks;
            opps = whitePieces;
            k = wKing;
        }

        // Remove stale pawn vulnerability
        List<Piece> pawns = opps.FindAll(
            p => p.GetKind() == (int)Piece.Kind.Pawn);

        pawns.ForEach(p =>
        {
            if ((p as Pawn).isVulnerable)
                (p as Pawn).isVulnerable = false;
        });

        // King is checked if there's an unblocked attack vector
        foreach (List<short> v in oppMoves)
        {
            List<short> danger = v.Slice(1, v.Count - 1);

            if (!danger.Exists(
                p => p != k.GetPosition() &&
                (whitePieces.Exists(w => w.GetPosition() == p) ||
                 blackPieces.Exists(b => b.GetPosition() == p))))
            {
                k.IsChecked = true;
                threatVec = v;
                break;
            }
        }
            

        // Clear the danger vectors if the king is not in danger
        if (!k.IsChecked) threatVec.Clear();

        // See if a king is in trouble
        CheckForKing(k);

        // Change turns if game hasn't ended
        if (IsActive) whiteTurn = !whiteTurn;
    }

    void CheckForKing(King k)
    {
        if (FilterMoves(k).TrueForAll(v => v.Count == 0))
            if (CheckForEnd(whiteTurn ? blackPieces : whitePieces))
                OnGameEnd(k.IsChecked);
    }

    bool CheckForEnd(List<Piece> team)
    {
        return team.TrueForAll(
            p => FilterMoves(p).TrueForAll(v => v.Count == 0));
    }

    void OnGameEnd(bool isCheckmate)
    {
        IsActive = false;
        this.isCheckmate = isCheckmate;
    }

    // Attempts to return a piece from pieces at the given location
    Piece? FindPiece(List<Piece> team, short loc)
    {
        return team.Find(p => p.GetPosition() == loc);
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
            attackVectors.RemoveAll(v => !v.Contains(kPos));

            foreach (List<short> v in attackVectors)
            {
                int kIndex = v.Find(p => p == kPos);

                for (int i = v.Count - 1; i > kPos; i--)
                    v.RemoveAt(i);
            }

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


        // INTERCEPTION FILTER -- Reduce vectors that are intercepted

        fMoves.ForEach(v =>
        {
            int interception = v.Count;

            // Check ally pieces. These have higher priority
            for (int i = 0; i < interception; i++)
                if (FindPiece(allyTeam, v[i]) != null)
                {
                    if (i == 0)
                    {
                        interception = 0;
                        v.Clear();
                    }
                    else interception = i - 1;
                    break;
                }

            // Check enemy pieces
            for (int i = 0; i < interception; i++)
                if (FindPiece(oppTeam, v[i]) != null)
                {
                    interception = i;
                    break;
                }

            if (v.Count > 0)
                // Remove all moves up to the point of an interception
                for (int i = v.Count - 1; i > interception; i--)
                    v.Remove(v[i]);
        });


        // ATTACK VECTORS FILTER -- Prevent exposure of the king

        // If there are any attacks on the allied king, find the
        // movements this piece could make that must be restricted
        if (oppAttacks.Count > 0)
        {
            bool isTrapped = oppAttacks.Exists(v => v.Exists(
                pos => pos == selectedPiece.GetPosition()));

            if (isTrapped)
                RemoveUnsafeVecs(fMoves, oppAttacks);
        }


        // PAWN FILTER -- Further restrict a pawn's movement

        if (p is Pawn)
        {
            int pPos = p.GetPosition(),
                offsetL = 7,
                offsetM = 8,
                offsetR = 9; 
            bool onStartRow;

            if (p.GetTeam() == 0)
                onStartRow = p.GetPosition() / 8 == 1;
            else
            {
                offsetL *= -1;
                offsetM *= -1;
                offsetR *= -1;
                onStartRow = p.GetPosition() / 8 == 6;
            }

            Piece? left = oppTeam.Find(o =>
                o.GetPosition() == pPos + offsetL ||
                (o is Pawn && o.GetPosition() == pPos - 1 &&
                 (o as Pawn).isVulnerable));
            Piece? middle = oppTeam.Find(o =>
                o.GetPosition() == pPos + offsetM);
            Piece? right = oppTeam.Find(o =>
                o.GetPosition() == pPos + offsetR ||
                (o is Pawn && o.GetPosition() == pPos + 1 &&
                 (o as Pawn).isVulnerable));

            if (right == null) fMoves[0].Clear();
            if (left == null)  fMoves[2].Clear();

            // Filter adds these pawn moves so they won't be 
            // considered attack vectors (pawn can't attack in front)
            if (middle == null)
            {
                fMoves[1].Add((short)(p.GetPosition() + offsetM));

                if (onStartRow)
                {
                    int doubleStep = pPos + (offsetM * 2);

                    if (!oppTeam.Exists(
                        o => o.GetPosition() == doubleStep))
                    {
                        fMoves[1].Add((short)doubleStep);
                    }
                }
            }
        }


        // KING FILTER -- Further restrict a king's movement

        if (p is King)
            foreach (List<short> m in fMoves)
            {
                // If a move has already been removed, ignore it
                if (m.Count == 0) continue;

                // Check every enemy piece for a filtered vector
                // that contains a move the king can make
                foreach (Piece e in oppTeam)
                {
                    // Exclude the king to prevent a stack overflow
                    if (e is King) continue;

                    if (e is Pawn)
                    {
                        // Pawns can only attack diagonally
                        List<List<short>> eMoves = FilterMoves(e);

                        if (eMoves[0].Contains(m[0]) || 
                            eMoves[2].Contains(m[0])) m.Clear();
                    }
                    else
                    {
                        // Remove king's move if in another vector
                        foreach (List<short> v in FilterMoves(e))
                            if (v.Exists(pos => m[0] == pos))
                            { 
                                m.Clear();
                                break;
                                }
                    }

                    if (m.Count == 0) break;

                    // Check if this piece is reinforced to prevent 
                    // the king from attacking it
                    if (m.Contains((short)e.GetPosition()) &&
                        IsReinforced(e, oppTeam))
                    {
                        m.Clear();
                        break;
                    }
                }
            }


        // THREAT FILTER -- Remove moves that don't intersect a threat

        if (selectedPiece is not King && threatVec.Count > 0)
            fMoves.ForEach(v =>
            {
                if (!v.Exists(p => threatVec.Contains(p))) v.Clear();
            });


        return fMoves;
    }

    bool IsReinforced(Piece p, List<Piece> team)
    {
        bool success = false;

        foreach (Piece e in team)
        {
            List<List<short>> eMoves = e.CheckMoves();
            List<short>? referenceVec = null;
            Piece? defender = null;
            int referenceIndex = -1;

            foreach (List<short> v in eMoves)
            {
                int i = v.FindIndex(pos => pos == p.GetPosition());

                // If p is found in the full movelist, remove
                // every move up to p (keep p's position)
                if (i != -1)
                {
                    v.RemoveRange(i + 1, v.Count - (i + 1));

                    referenceVec = v;
                    defender = e;
                    referenceIndex = eMoves.IndexOf(v);
                    break;
                }
            }

            if (defender != null && referenceVec != null)
            {
                List<List<short>> fMoves = FilterMoves(defender);
                List<short>? matchingVector = fMoves.Find(v =>
                {
                    return v.Count > 0 && v.Exists(pos =>
                    {
                        return referenceVec.Contains(pos);
                    });
                });

                if (matchingVector != null)
                {
                    matchingVector.Add((short)p.GetPosition());
                    success = matchingVector.SequenceEqual(referenceVec);
                }

                if (success) break;
            }
        }

        return success;
    }

    void RemoveUnsafeVecs(List<List<short>> vectors,
        List<List<short>> attacks)
    {
        vectors.ForEach(v =>
        {
            foreach (short p in v)
            {
                List<List<short>> aVectors = attacks.FindAll(
                    av => !av.Contains(p));

                // If any attack vector exposes the king, v is unsafe
                //if (aVectors.TrueForAll(v => IsKingSafe(v))) 
                //    return true;
                
                foreach (List<short> av in aVectors)
                    if (!IsKingSafe(av)) v.Clear();

                if (v.Count == 0) break;
            }
        });
    }

    bool IsKingSafe(List<short> av)
    {
        // King is safe if there's any piece inside the vector
        return av.TrueForAll(pos =>
            whitePieces.Exists(
                p => p.GetKind() != (int)Piece.Kind.King &&
                p != selectedPiece && p.GetPosition() == pos) ||
            blackPieces.Exists(
                p => p.GetKind() != (int)Piece.Kind.King &&
                p != selectedPiece && p.GetPosition() == pos));
    }
}