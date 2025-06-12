namespace Chess.Logic;
public class Board
{
    private readonly Piece?[,] _pieces = new Piece?[8, 8]; // I don't like this name

    public Piece? this[int row, int col]
    {
        get => _pieces[row, col];
        set => _pieces[row, col] = value;
    }

    public Piece? this[Position pos]
    {
        get => this[pos.Row, pos.Column];
        set => this[pos.Row, pos.Column] = value;
    }

    public static Board Initial() // rename to Initialize, i'm not super fan of this method
    {
        var board = new Board();
        board.AddStartPieces();
        return board;
    }

    public static bool IsInside(Position pos) => pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;

    public bool IsEmpty(Position pos) => this[pos] == null;

    public IEnumerable<Position> PiecePositions()
    {
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                var pos = new Position(r, c);

                if (!IsEmpty(pos))
                    yield return pos;
            }
        }
    }

    public IEnumerable<Position> PiecePositionsFor(Player player) => PiecePositions().Where(pos => this[pos]!.Player == player);

    public bool IsInCheck(Player player)
    {
        return PiecePositionsFor(player.Opponent()).Any(pos =>
        {
            Piece piece = this[pos]!;
            return piece.CanCaptureOpponentKing(pos, this);
        });
    }

    public Board Copy()
    {
        var copy = new Board();
        foreach (Position pos in PiecePositions())
        {
            copy[pos] = this[pos]!.Copy();
        }

        return copy;
    }

    private void AddStartPieces()
    {
        this[0, 0] = new Rook(Player.Black);
        this[0, 1] = new Knight(Player.Black);
        this[0, 2] = new Bishop(Player.Black);
        this[0, 3] = new Queen(Player.Black);
        this[0, 4] = new King(Player.Black);
        this[0, 5] = new Bishop(Player.Black);
        this[0, 6] = new Knight(Player.Black);
        this[0, 7] = new Rook(Player.Black);

        this[7, 0] = new Rook(Player.White);
        this[7, 1] = new Knight(Player.White);
        this[7, 2] = new Bishop(Player.White);
        this[7, 3] = new Queen(Player.White);
        this[7, 4] = new King(Player.White);
        this[7, 5] = new Bishop(Player.White);
        this[7, 6] = new Knight(Player.White);
        this[7, 7] = new Rook(Player.White);

        for (int i = 0; i < 8; i++)
        {
            this[1, i] = new Pawn(Player.Black);
            this[6, i] = new Pawn(Player.White);
        }
    }
}