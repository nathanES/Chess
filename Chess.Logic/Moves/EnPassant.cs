namespace Chess.Logic.Moves;

public class EnPassant : Move
{
    private readonly Position _capturePos;

    public EnPassant(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
        _capturePos = new Position(FromPos.Row, ToPos.Column);
    }

    public override MoveType Type => MoveType.EnPassant;

    public override Position FromPos { get; }

    public override Position ToPos { get; }

    public override bool Execute(Board board)
    {
        new NormalMove(FromPos, ToPos).Execute(board);
        board[_capturePos] = null;
        return true;
    }
}
