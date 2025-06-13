namespace Chess.Logic;

using System.Text;

public class StateString
{
    private readonly StringBuilder _sb = new();

    public StateString(Player currentPlayer, Board board)
    {
        AddPiecePlacement(board);
        _sb.Append(' ');

        AddCurrentPlayer(currentPlayer);
        _sb.Append(' ');

        AddCastilingRights(board);
        _sb.Append(' ');

        AddEnPassant(board, currentPlayer);
    }

    public override string ToString() => _sb.ToString();

    private static char PieceChar(Piece piece)
    {
        char c = piece.Type switch
        {
            PieceType.Pawn => 'p',
            PieceType.Knight => 'n',
            PieceType.Rook => 'r',
            PieceType.Bishop => 'b',
            PieceType.Queen => 'q',
            PieceType.King => 'k',
            _ => ' '
        };
        if (piece.Player == Player.White)
            return char.ToUpper(c);
        return c;
    }

    private void AddPiecePlacement(Board board)
    {
        for (int r = 0; r < 8; r++)
        {
            if (r != 0)
            {
                _sb.Append('/');
            }

            AddRowData(board, r);
        }
    }

    private void AddCurrentPlayer(Player currentPlayer)
    {
        if (currentPlayer == Player.White)
        {
            _sb.Append('w');
        }
        else
        {
            _sb.Append('b');
        }
    }

    private void AddCastilingRights(Board board)
    {
        bool castleWhiteKingSide = board.CastleRightKingSide(Player.White);
        bool castleWhiteQueenSide = board.CastleRightQueenSide(Player.White);

        bool castleBlackKingSide = board.CastleRightKingSide(Player.Black);
        bool castleBlackQueenSide = board.CastleRightQueenSide(Player.White);
        if (!(castleWhiteKingSide || castleWhiteQueenSide || castleBlackKingSide || castleBlackQueenSide))
        {
            _sb.Append('-');
            return;
        }

        if (castleWhiteKingSide)
        {
            _sb.Append('K');
        }

        if (castleWhiteQueenSide)
        {
            _sb.Append('Q');
        }

        if (castleBlackKingSide)
        {
            _sb.Append('k');
        }

        if (castleBlackQueenSide)
        {
            _sb.Append('q');
        }
    }

    private void AddEnPassant(Board board, Player currentPlayer)
    {
        if (!board.CanCaptureEnPassant(currentPlayer))
        {
            _sb.Append('-');
            return;
        }

        Position pos = board.GetPawnSkipPosition(currentPlayer.Opponent()) !;
        char file = (char)('a' + pos.Column);
        int rank = 8 - pos.Row;
        _sb.Append(file);
        _sb.Append(rank);
    }

    private void AddRowData(Board board, int row)
    {
        int empty = 0;
        for (int c = 0; c < 8; c++)
        {
            if (board[row, c] == null)
            {
                empty++;
                continue;
            }

            if (empty > 0)
            {
                _sb.Append(empty);
                empty = 0;
            }

            _sb.Append(PieceChar(board[row, c]!));
        }

        if (empty > 0)
        {
            _sb.Append(empty);
        }
    }
}
