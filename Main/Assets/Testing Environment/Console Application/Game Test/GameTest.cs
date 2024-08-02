using System;

public static class GameTest
{
    static readonly int TEST_AMOUNT = 50000;

    public static void Game_TryCastling()
    {
        Game g = new Game();
        Random r = new Random();
        int runNum = 0;

        Console.WriteLine("Attempting all castling moves..\n");
        Console.WriteLine($"Number of successful runs : ");
        for (int i = 0; i < TEST_AMOUNT; i++)
        {
            ++runNum;
            try
            {
                // NOTE : Black team's pieces will be scrambled across
                //        the board. This is to make an unpredictable
                //        scenario for each test run

                g.Reset(); // Initialize standard board


                // WHITE KING SHORT CASTLE

                // Place the white pieces in predetermined locations
                g.whitePieces[5].SetPosition(19);  // Move white bishop
                g.whitePieces[12].SetPosition(20); // Move white pawn
                g.whitePieces[6].SetPosition(21);  // Move white knight

                // Attempt the castle as intended
                g.SelectTile(4);
                g.SelectTile(7);

                // Assert desired outcome
                if (!(g.whitePieces[4].GetPosition() == 6 &&
                    g.whitePieces[7].GetPosition() == 5))
                {
                    throw new Exception(
                        "White king short castle failed.");
                }


                // BLACK KING SHORT CASTLE

                // Scramble black's pieces except for the king and
                // target rook
                g.blackPieces.ForEach(p =>
                {
                    int next;
                    if (p != g.blackPieces[7] &&
                        p != g.blackPieces[4])
                    {
                        do { next = r.Next(17, 55); }
                        while (g.blackPieces.Exists(
                            b => b.GetPosition() == next));

                        p.SetPosition(r.Next(17, 55));
                    }
                });

                // Attempt the castle as intended
                g.SelectTile(60);
                g.SelectTile(63);

                // Assert desired outcome
                if (!(g.blackPieces[4].GetPosition() == 62 &&
                    g.blackPieces[7].GetPosition() == 61))
                {
                    throw new Exception(
                        "Black king short castle failed.");
                }


                g.Reset(); // Reset the board for the next test


                // WHITE ROOK SHORT CASTLE

                // Place the white pieces in predetermined locations
                g.whitePieces[5].SetPosition(19);  // Move white bishop
                g.whitePieces[12].SetPosition(20); // Move white pawn
                g.whitePieces[6].SetPosition(21);  // Move white knight

                // Attempt the castle as intended
                g.SelectTile(7);
                g.SelectTile(4);

                // Assert desired outcome
                if (!(g.whitePieces[4].GetPosition() == 6 &&
                    g.whitePieces[7].GetPosition() == 5))
                {
                    throw new Exception(
                        "White rook short castle failed.");
                } 


                // BLACK ROOK SHORT CASTLE

                // Scramble black's pieces except for the king and
                // target rook
                g.blackPieces.ForEach(p =>
                {
                    int next;
                    if (p != g.blackPieces[7] &&
                        p != g.blackPieces[4])
                    {
                        do { next = r.Next(17, 55); }
                        while (g.blackPieces.Exists(
                            b => b.GetPosition() == next));

                        p.SetPosition(r.Next(17, 55));
                    }
                });

                // Attempt the castle as intended
                g.SelectTile(63);
                g.SelectTile(60);

                // Assert desired outcome
                if (!(g.blackPieces[4].GetPosition() == 62 &&
                    g.blackPieces[7].GetPosition() == 61))
                {
                    throw new Exception(
                        "Black rook short castle failed.");
                }
                


                g.Reset(); // Reset the board for the next test


                // WHITE KING LONG CASTLE

                // Place the white pieces in predetermined locations
                g.whitePieces[1].SetPosition(18);  // Move white B1 knight
                g.whitePieces[11].SetPosition(19);  // Move white D2 pawn
                g.whitePieces[2].SetPosition(20); // Move white C1 bishop
                g.whitePieces[3].SetPosition(11);  // Move white queen

                // Attempt the castle as intended
                g.SelectTile(4);
                g.SelectTile(0);

                // Assert desired outcome
                if (!(g.whitePieces[4].GetPosition() == 2 &&
                    g.whitePieces[0].GetPosition() == 3))
                {
                    throw new Exception(
                        "White king long castle failed.");
                }


                // BLACK KING LONG CASTLE

                // Scramble black's pieces except for the king and
                // target rook
                g.blackPieces.ForEach(p =>
                {
                    int next;
                    if (p != g.blackPieces[0] &&
                        p != g.blackPieces[4])
                    {
                        do { next = r.Next(17, 55); }
                        while (g.blackPieces.Exists(
                            b => b.GetPosition() == next));

                        p.SetPosition(r.Next(17, 55));
                    }
                });

                // Attempt the castle as intended
                g.SelectTile(60);
                g.SelectTile(56);

                // Assert desired outcome
                if (!(g.blackPieces[4].GetPosition() == 58 &&
                    g.blackPieces[0].GetPosition() == 59))
                {
                    throw new Exception(
                        "Black king long castle failed.");
                }


                g.Reset(); // Reset the board for the next test


                // WHITE ROOK LONG CASTLE

                // Place the white pieces in predetermined locations
                g.whitePieces[1].SetPosition(18);  // Move white B1 knight
                g.whitePieces[11].SetPosition(19);  // Move white D2 pawn
                g.whitePieces[2].SetPosition(20); // Move white C1 bishop
                g.whitePieces[3].SetPosition(11);  // Move white queen

                // Attempt the castle as intended
                g.SelectTile(0);
                g.SelectTile(4);

                // Assert desired outcome
                if (!(g.whitePieces[4].GetPosition() == 2 &&
                    g.whitePieces[0].GetPosition() == 3))
                {
                    throw new Exception(
                        "White rook long castle failed.");
                }


                // BLACK ROOK LONG CASTLE

                // Scramble black's pieces except for the king and
                // target rook
                g.blackPieces.ForEach(p =>
                {
                    int next;
                    if (p != g.blackPieces[0] &&
                        p != g.blackPieces[4])
                    {
                        do { next = r.Next(17, 55); }
                        while (g.blackPieces.Exists(
                            b => b.GetPosition() == next));

                        p.SetPosition(r.Next(17, 55));
                    }
                });

                // Attempt the castle as intended
                g.SelectTile(56);
                g.SelectTile(60);

                // Assert desired outcome
                if (!(g.blackPieces[4].GetPosition() == 58 &&
                    g.blackPieces[0].GetPosition() == 59))
                {
                    throw new Exception(
                        "Black rook long castle failed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"FAIL : {e.Message}");
                break;
            }

            Console.Write($"\r{i + 1}");
        }

        Console.WriteLine();
        Console.WriteLine("\nUnit test for castling is complete!");
    }
}
