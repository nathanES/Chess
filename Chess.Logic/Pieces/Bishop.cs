namespace Chess.Logic;

public class Bishop : Piece
{
    private static readonly Direction[] _dirs =
    [
        Direction.NorthEast,
        Direction.NorthWest,
        Direction.SouthEast,
        Direction.SouthWest
    ];

    public Bishop(Player player) => Player = player;

    public override PieceType Type => PieceType.Bishop;

    public override Player Player { get; }

    public override Piece Copy() => new Bishop(Player) { HasMoved = HasMoved };

    public override IEnumerable<Move> GetMoves(Position from, Board board) => MovePositionsInDir(from, board, _dirs).Select(to => new NormalMove(from, to));
}
