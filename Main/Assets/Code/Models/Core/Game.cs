using System;
using System.Collections.Generic;

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
        whitePieces = new List<Piece>();
        blackPieces = new List<Piece>();
        wKingAttacks = new List<List<short>>();
        bKingAttacks = new List<List<short>>();
        threatVec = new List<short>();
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
        foreach (Piece p in set)
        {
            List<Piece> team = p.GetTeam() == 0 ?
                whitePieces : blackPieces;

            team.Add(p);
        }

        IsActive = true;
    }

    // Attempts to return a piece from pieces at the given location
    static Piece FindPiece(List<Piece> team, int loc)
    {
        return team.Find(p => p.GetPosition() == (short)loc);
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

    void OnMove(short loc)
    {
        int prev = selectedPiece.GetPosition(),
            prevRow = prev / 8,
            prevCol = prev % 8;
        List<Piece> oppTeam, allyTeam;

        if (whiteTurn)
        {
            allyTeam = whitePieces;
            oppTeam = blackPieces;
        }
        else
        {
            allyTeam = blackPieces;
            oppTeam = whitePieces;
        }

        selectedPiece.SetPosition(loc);

        int pPos = selectedPiece.GetPosition();

        Piece defender = FindPiece(oppTeam, loc);

        // Pawn logic
        if (selectedPiece is Pawn)
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
                    p => p is Pawn &&
                    p.GetPosition() == defenderPos);
            }

            // If on the other side of the board,
            // promote to queen as a default for now
            if (row == promoRow)
            {
                Queen promotedPawn = new Queen(
                    (short)pPos, (short)selectedPiece.GetTeam());

                // Replace the pawn with the new queen
                allyTeam.Remove(FindPiece(allyTeam, pPos));
                allyTeam.Add(promotedPawn);
            };
        }

        // Castle logic
        if (selectedPiece is King && 
            !(selectedPiece as King).HasMoved)
        {

            bool canCastleS = activeMoveList[3].Count == 2 &&
                loc == activeMoveList[3][1];

            bool canCastleL = activeMoveList[4].Count == 2 &&
                loc == activeMoveList[4][1];

            if (canCastleS || canCastleL)
                DoCastle(selectedPiece as King,
                    FindPiece(allyTeam, pPos) as Rook);
        }
        else if (selectedPiece is Rook &&
            !(selectedPiece as Rook).HasMoved)
        {
            bool canCastleS = activeMoveList[0].Count > 2 && 
                loc == activeMoveList[0][2];

            bool canCastleL = activeMoveList[1].Count > 3 &&
                loc == activeMoveList[1][3];

            // See if the move is short castle
            if (canCastleS || canCastleL)
                DoCastle(whiteTurn ? wKing : bKing,
                    selectedPiece as Rook);
        }

        if (defender != null) OnCapture(defender);

        OnMoveEnd();
    }

    void OnCapture(Piece p)
    {
        List<Piece> oppTeam = whiteTurn ? blackPieces : whitePieces;

        PieceEventArgs e = new PieceEventArgs();
        e.Piece = p;

        oppTeam.Remove(p);
        OnPieceCapture(e);
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
        List<Piece> pawns = opps.FindAll(p => p is Pawn);
        pawns.ForEach(p =>
        {
            if ((p as Pawn).isVulnerable)
                (p as Pawn).isVulnerable = false;
        });

        // King is checked if there's an unblocked attack vector
        foreach (List<short> v in oppMoves)
        {
            List<short> danger = new List<short>();

            for (int i = 1; i < v.Count - 1; i++) danger.Add(v[i]);

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

        // Prevents further castling
        if (selectedPiece is King)
            (selectedPiece as King).HasMoved = true;
        else if (selectedPiece is Rook)
            (selectedPiece as Rook).HasMoved = true;

        // Clear the danger vectors if the king.GetType() != in danger
        if (!k.IsChecked) threatVec.Clear();

        // See if a king is in trouble
        CheckForKing(k);

        // Change turns if game hasn't ended
        if (IsActive) whiteTurn = !whiteTurn;

        OnPieceMove(null);
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

        GameEndEventArgs e = new GameEndEventArgs();

        e.IsChecked = isCheckmate;
        e.EndTurn = whiteTurn;
        OnGameEnd(e);
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
                int kIndex = v.Find(pos => pos == kPos);

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
        if (!(selectedPiece is King) && oppAttacks.Count > 0)
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

            Piece left = oppTeam.Find(o =>
                o.GetPosition() == pPos + offsetL ||
                (o is Pawn && o.GetPosition() == pPos - 1 &&
                 (o as Pawn).isVulnerable));
            Piece middle = oppTeam.Find(o =>
                o.GetPosition() == pPos + offsetM);
            Piece right = oppTeam.Find(o =>
                o.GetPosition() == pPos + offsetR ||
                (o is Pawn && o.GetPosition() == pPos + 1 &&
                 (o as Pawn).isVulnerable));

            if (right == null) fMoves[0].Clear();
            if (left == null) fMoves[2].Clear();

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


        // KING CASTLE FILTER -- Add castle move if legal

        if (selectedPiece.GetTeam() == p.GetTeam() &&
            (selectedPiece is King &&
            !(selectedPiece as King).HasMoved) ||
            (selectedPiece is Rook &&
            !(selectedPiece as Rook).HasMoved))
        {
            bool canCastleShort = false,
                 canCastleLong = false;
            int row = selectedPiece.GetPosition() / 8,
                targetPos = ((row + 1) * 8) - 1;

            List<short> castleRowS = new List<short>(),
                        castleRowL = new List<short>();
            List<Piece> rooks = allyTeam.FindAll(
                    a => a is Rook && !(a as Rook).HasMoved);

            if (rooks.Count > 0)
                // Create vectors of tiles that must be cleared
                for (int i = 0; i < 4; i++)
                {
                    if (i < 3)
                        castleRowS.Add((short)(targetPos - i));

                    // Long castle starts 3 tiles offset
                    castleRowL.Add((short)(targetPos - (i + 4)));
                }

            // If a rook is on right side, try enabling short castle
            if (rooks.Exists(r => r.GetPosition() == targetPos))
            {
                canCastleShort = CanCastle(
                    castleRowS, allyTeam, oppTeam);
            }
            // If a rook is on left side, try enabling long castle
            if (rooks.Exists(r => r.GetPosition() == targetPos - 7))
            {
                canCastleLong = CanCastle(
                    castleRowL, allyTeam, oppTeam);
            }

            // We can assume that the piece is a rook if not a king
            if (selectedPiece is King)
            {
                if (fMoves[3].Count > 0 && canCastleShort)
                    fMoves[3].Add((short)targetPos);

                if (fMoves[4].Count > 0 && canCastleLong)
                    fMoves[4].Add((short)(targetPos - 7));
            }
            else
            {
                short kingPos = 
                    (short)(whiteTurn ? wKing : bKing).GetPosition();

                if (fMoves[0].Count > 0 && canCastleShort)
                    fMoves[0].Add(kingPos);

                if (fMoves[1].Count > 0 && canCastleLong)
                    fMoves[1].Add(kingPos);
            }
        }


        // THREAT FILTER -- Remove moves that don't intersect a threat

        if (!(selectedPiece is King) && threatVec.Count > 0)
            fMoves.ForEach(v =>
            {
                if (!v.Exists(x => threatVec.Contains(x))) v.Clear();
            });


        return fMoves;
    }
    
    bool CanCastle(List<short> vec, List<Piece> a, List<Piece> o)
    {
        // This is really gross, but it'll do
        return !a.Exists(
            p => !(p is Rook) && vec.Contains(
                (short)p.GetPosition())) &&
            !vec.Exists(pos => o.Exists(
                opp => !(opp is King) &&
                FilterMoves(opp).Exists(v => v.Contains(pos))));
    }

    bool IsReinforced(Piece p, List<Piece> team)
    {
        bool success = false;

        foreach (Piece e in team)
        {
            List<List<short>> eMoves = e.CheckMoves();
            List<short> referenceVec = null;
            Piece defender = null;
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
                List<short> matchingVector = fMoves.Find(v =>
                {
                    return v.Count > 0 && v.Exists(
                        pos => referenceVec.Contains(pos));
                });

                if (matchingVector != null)
                {
                    matchingVector.Add((short)p.GetPosition());
                    success = true;

                    for (int i = 0; success && i < matchingVector.Count; i++)
                        success = matchingVector[i] == referenceVec[i];
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

    void DoCastle(King k, Rook r)
    {
        int rPos = r.GetPosition();

        if (k == null ||  r == null) return;

        if (selectedPiece is King)
        {
            if (rPos % 8 == 7)
            {
                // Do short castle
                r.SetPosition(rPos - 2);
                k.SetPosition(rPos - 1);
            }
            else
            {
                // Do long castle
                r.SetPosition(rPos + 3);
                k.SetPosition(rPos + 2);
            }
        }
        else
        {
            // If the move list contains a vector on the right
            // of the king, then do a short castle
            if (activeMoveList.Exists(v => v.Exists(
                pos => pos % 8 == (short)((rPos % 8) + 1))))
            {
                // Do short castle
                r.SetPosition(rPos + 1);
                k.SetPosition(rPos + 2);
            }
            else
            {
                // Do long castle
                r.SetPosition(rPos - 1);
                k.SetPosition(rPos - 2);
            }
        }

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

    protected virtual void OnGameEnd(GameEndEventArgs e)
    {
        GameEnd?.Invoke(this, e);
    }

    protected virtual void OnPieceCapture(PieceEventArgs e)
    {
        PieceCapture?.Invoke(this, e);
    }

    protected virtual void OnPieceMove(EventArgs e)
    {
        PieceMove?.Invoke(this, e);
    }

    public event EventHandler<GameEndEventArgs> GameEnd;
    public event EventHandler<PieceEventArgs> PieceCapture;
    public event EventHandler PieceMove;
}