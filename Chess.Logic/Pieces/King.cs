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
    }

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return MovePositions(from, board).Any(to =>
        {
            Piece? piece = board[to];
            return piece != null && piece.Type == PieceType.King;
        });
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
