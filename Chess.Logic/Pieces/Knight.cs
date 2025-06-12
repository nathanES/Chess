namespace Chess.Logic;

public class Knight : Piece
{
    public Knight(Player player) => Player = player;

    public override PieceType Type => PieceType.Knight;

    public override Player Player { get; }

    public override Piece Copy() => new Knight(Player) { HasMoved = HasMoved };

    public override IEnumerable<Move> GetMoves(Position from, Board board) => MovePositions(from, board).Select(pos => new NormalMove(from, pos));

    private static IEnumerable<Position> PotentialToPositions(Position from)
    {
        foreach (Direction vDir in new Direction[] { Direction.North, Direction.South })
        {
            foreach (Direction hDir in new Direction[] { Direction.West, Direction.East })
            {
                yield return from + (2 * vDir) + hDir;
                yield return from + (2 * hDir) + vDir;
            }
        }
    }

    private IEnumerable<Position> MovePositions(Position from, Board board) => PotentialToPositions(from).Where(pos => Board.IsInside(pos) && (board.IsEmpty(pos) || board[pos]!.Player != Player));
}
