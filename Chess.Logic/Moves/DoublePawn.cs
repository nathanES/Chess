namespace Chess.Logic;

public class DoublePawn : Move
{
    private readonly Position _skippedPos;

    public DoublePawn(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
        _skippedPos = new Position((fromPos.Row + toPos.Row) / 2, fromPos.Column);
    }

    public override MoveType Type => MoveType.DoublePawn;

    public override Position FromPos { get; }

    public override Position ToPos { get; }

    public override void Execute(Board board)
    {
        Player player = board[FromPos]!.Player;
        board.SetPawnSkipPosition(player, _skippedPos);
        new NormalMove(FromPos, ToPos).Execute(board);
    }
}
