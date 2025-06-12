namespace Chess.Logic;
public abstract class Piece
{
    public abstract PieceType Type { get; }

    public abstract Player Player { get; }

    public bool HasMoved { get; set; }

    public abstract Piece Copy();

    public abstract IEnumerable<Move> GetMoves(Position from, Board board);

    public virtual bool CanCaptureOpponentKing(Position from, Board board) // Rename to IsCheckingOpponnentKing
    {
        return GetMoves(from, board).Any(move =>
        {
            Piece? piece = board[move.ToPos];
            return piece != null && piece.Type == PieceType.King; // Don't test player because it's already filtered out in GetMoves
        });
    }

    protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir)
    {
        for (Position pos = from + dir; Board.IsInside(pos); pos += dir)
        {
            if (board.IsEmpty(pos))
            {
                yield return pos;
                continue;
            }

            Piece piece = board[pos]!;
            if (piece.Player != Player)
            {
                yield return pos;
            }

            yield break;
        }
    }

    protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction[] dirs) => dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
}
