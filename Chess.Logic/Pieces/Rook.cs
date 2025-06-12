namespace Chess.Logic;

public class Rook : Piece
{
    private static readonly Direction[] _dirs =
    [
        Direction.North,
        Direction.South,
        Direction.East,
        Direction.West,
    ];

    public Rook(Player player) => Player = player;

    public override PieceType Type => PieceType.Rook;

    public override Player Player { get; }

    public override Piece Copy() => new Rook(Player) { HasMoved = HasMoved };

    public override IEnumerable<Move> GetMoves(Position from, Board board) => MovePositionsInDir(from, board, _dirs).Select(to => new NormalMove(from, to));
}
