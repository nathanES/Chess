namespace Chess.Logic;

public class Queen : Piece
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

    public Queen(Player player) => Player = player;

    public override PieceType Type => PieceType.Queen;

    public override Player Player { get; }

    public override Piece Copy() => new Queen(Player) { HasMoved = HasMoved };

    public override IEnumerable<Move> GetMoves(Position from, Board board) => MovePositionsInDir(from, board, _dirs).Select(to => new NormalMove(from, to));
}
