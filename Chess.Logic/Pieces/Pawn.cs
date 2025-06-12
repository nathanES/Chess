namespace Chess.Logic;

public class Pawn : Piece
{
    private readonly Direction _forward;

    public Pawn(Player player)
    {
        Player = player;

        if (player == Player.White)
            _forward = Direction.North;
        else
            _forward = Direction.South;
    }

    public override PieceType Type => PieceType.Pawn;

    public override Player Player { get; }

    public override Piece Copy() => new Pawn(Player) { HasMoved = HasMoved };

    public override IEnumerable<Move> GetMoves(Position from, Board board) => ForwardMoves(from, board).Concat(DiagonalMoves(from, board));

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return DiagonalMoves(from, board).Any(move =>
        {
            Piece? piece = board[move.ToPos];
            return piece != null && piece.Type == PieceType.King;
        });
    }

    private static bool CanMoveTo(Position pos, Board board) => Board.IsInside(pos) && board.IsEmpty(pos);

    private bool CanCaptureAt(Position pos, Board board)
    {
        if (!Board.IsInside(pos) || board.IsEmpty(pos))
            return false;

        return board[pos]!.Player != Player;
    }

    private IEnumerable<Move> ForwardMoves(Position from, Board board)
    {
        Position oneMovePos = from + _forward;
        if (CanMoveTo(oneMovePos, board))
        {
            yield return new NormalMove(from, oneMovePos);

            Position twoMovesPos = oneMovePos + _forward;
            if (!HasMoved && CanMoveTo(twoMovesPos, board))
                yield return new NormalMove(from, twoMovesPos);
        }
    }

    private IEnumerable<Move> DiagonalMoves(Position from, Board board)
    {
        foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
        {
            Position to = from + _forward + dir;
            if (CanCaptureAt(to, board))
                yield return new NormalMove(from, to);
        }
    }
}
