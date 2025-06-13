namespace Chess.Logic;

public class King : Piece
{
    private static readonly Direction[] _dirs =
    [
        Direction.North,
        Direction.NorthEast,
        Direction.NorthWest,
        Direction.South,
        Direction.SouthEast,
        Direction.SouthWest,
        Direction.East,
        Direction.West,
    ];

    public King(Player player) => this.Player = player;

    public override PieceType Type => PieceType.King;

    public override Player Player { get; }

    public override Piece Copy() => new King(this.Player) { HasMoved = this.HasMoved };

    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        foreach (Position pos in this.MovePositions(from, board))
        {
            yield return new NormalMove(from, pos);
        }

        if (CanCastleKingSide(from, board))
        {
            yield return new Castle(MoveType.CastleKingSide, from);
        }

        if (CanCastleQueenSide(from, board))
        {
            yield return new Castle(MoveType.CastleQueenSide, from);
        }
    }

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return MovePositions(from, board).Any(to =>
        {
            Piece? piece = board[to];
            return piece != null && piece.Type == PieceType.King;
        });
    }

    private static bool IsUnmovedRook(Position pos, Board board)
    {
        if (board.IsEmpty(pos))
            return false;
        Piece piece = board[pos]!;
        return piece.Type == PieceType.Rook && !piece.HasMoved;
    }

    private static bool AllEmpty(IEnumerable<Position> postions, Board board) => postions.All(pos => board.IsEmpty(pos));

    private bool CanCastleKingSide(Position from, Board board)
    {
        if (HasMoved)
            return false;

        var rookPos = new Position(from.Row, 7);
        var betweenPositions = new Position[] { new(from.Row, 5), new(from.Row, 6) };
        return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
    }

    private bool CanCastleQueenSide(Position from, Board board)
    {
        if (HasMoved)
            return false;
        var rookPos = new Position(from.Row, 0);
        var betweenPositions = new Position[] { new(from.Row, 1), new(from.Row, 2), new(from.Row, 3) };
        return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
    }

    private IEnumerable<Position> MovePositions(Position from, Board board)
    {
        foreach (Direction dir in _dirs)
        {
            Position to = from + dir;
            if (!Board.IsInside(to))
                continue;

            if (board.IsEmpty(to) || board[to]!.Player != Player)
            {
                yield return to;
            }
        }
    }
}
