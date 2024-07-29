public class Program
{
    static Game g;

    static void Main()
    {
        StartGame();
    }

    // For debugging
    static void Premove()
    {

    }

    static void StartGame()
    {
        g = new Game();
        g.SetBoard();

        Premove();

        while (g.IsActive)
        {
            PlayTurn();
        }

        DetermineOutcome();
    }

    static void PlayTurn()
    {
        ShowBoard();
        PromptSelection();
    }

    static void ShowBoard()
    {
        // Create the visual board
        char[][] board = new char[8][];
        for (int i = 0; i < 8; i++)
        {
            board[i] = new char[8];

            for (int j = 0; j < 8; j++)
            {
                board[i][j] = '.';
            }
        }

        foreach (Piece p in g.whitePieces)
        {
            int[] loc = GetRowAndCol(p.GetPosition());
            char name = GetPieceName(p);

            board[loc[0]][loc[1]] = name;
        }

        foreach (Piece p in g.blackPieces)
        {
            int[] loc = GetRowAndCol(p.GetPosition());

            // Convoluted way of distinguishing black team
            char name = GetPieceName(p).ToString()
                .ToUpper().ToCharArray()[0];

            board[loc[0]][loc[1]] = name;
        }

        // Display selected piece's moves
        foreach (List<short> line in g.activeMoveList)
        {
            foreach (short tile in line)
            {
                int[] loc = GetRowAndCol(tile);

                board[loc[0]][loc[1]] = 'O';
            }
        }

        // Draw board
        for (int i = 7; i >= 0; i--)
        {
            for (int j = 0; j < 8; j++)
            {
                Console.Write($"{board[i][j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static int[] GetRowAndCol(int pos)
    {
        return [pos / 8, pos % 8];
    }

    static void PromptSelection()
    {
        Console.WriteLine("Select a tile : Use chess notation");

        bool valid;
        int pos = 0;

        do
        {
            string? input = Console.ReadLine();
            valid = GetLocationFromStr(input, ref pos);

            if (valid)
            {
                valid = pos < 65 && pos >= 0;
            }
            
            if (!valid)
            {
                Console.WriteLine(
                    "Invalid input. Please use chess notation");
            }
        } while (!valid);

        Select(pos);
    }

    static void Select(int pos)
    {
        Console.WriteLine(
            "Selecting tile " + GetNotation(pos) + "...");

        g.SelectTile(pos);

        if (g.selectedPiece != null)
            ShowInfo(g.selectedPiece);
    }

    static void ShowInfo(Piece p)
    {
        string team = p.GetTeam() == 0 ? "White" : "Black";
        Console.WriteLine("Selected Piece");
        Console.WriteLine($"Position : {p.GetPosition()}");
        Console.WriteLine($"Type     : {GetPieceName(p)}");
        Console.WriteLine($"Team     : {team}");
    }

    static void DetermineOutcome()
    {
        ShowBoard();
        if (g.isCheckmate)
        {
            string winner = g.whiteTurn ? "White" : "Black";

            Console.WriteLine($"Checkmate!! {winner} team wins.");
        }
        else
            Console.WriteLine("Stalemate! Nobody wins.");
    }

    static string GetNotation(int pos)
    {
        int[] rowAndCol = GetRowAndCol(pos);

        string result = "";
        result += (char)('A' + rowAndCol[1]);
        result += (char)('1' + rowAndCol[0]);

        return result;
    }

    static bool GetLocationFromStr(string str, ref int pos)
    {
        bool success = false;
        char letter = str.Substring(0, 1).ToUpper()[0];

        if (str != null && str.Length >= 2 &&
            letter >= 'A' && letter < 'I' &&
            str[1] > '0' && str[1] < '9')
        {
            pos = ((str[1] - '0' - 1) * 8) + (letter - 'A');
            success = true;
        }

        return success;
    }

    static char GetPieceName(Piece p)
    {
        char name;

        switch (p.GetKind())
        {
            case 0:
                name = 'p';
                break;
            case 1:
                name = 'r';
                break;
            case 2:
                name = 'n';
                break;
            case 3:
                name = 'b';
                break;
            case 4:
                name = 'q';
                break;
            case 5:
                name = 'k';
                break;
            default:
                name = 'x';
                break;
        }

        return name;
    }
}