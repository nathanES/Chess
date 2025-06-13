namespace Chess.Logic;

public class NormalMove : Move
{
    public NormalMove(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
    }

    public override MoveType Type => MoveType.Normal;

    public override Position FromPos { get; }

    public override Position ToPos { get; }

    public override bool Execute(Board board)
    {
        Piece? piece = board[FromPos];
        if (piece == null)
            throw new Exception($"{nameof(piece)} is null while doing {nameof(Execute)} method");
        bool capture = !board.IsEmpty(ToPos);
        board[ToPos] = piece;
        board[FromPos] = null;
        piece.HasMoved = true;
        return capture || piece.Type == PieceType.Pawn;
    }
}
