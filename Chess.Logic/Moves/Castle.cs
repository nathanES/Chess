namespace Chess.Logic;

internal class Castle : Move
{
    private readonly Direction _kingMoveDir;
    private readonly Position _rookFromPos;
    private readonly Position _rookToPos;

    public Castle(MoveType type, Position kingPos)
    {
        Type = type;
        FromPos = kingPos;

        if (type == MoveType.CastleKingSide)
        {
            _kingMoveDir = Direction.East;
            ToPos = new Position(FromPos.Row, 6);
            _rookFromPos = new Position(FromPos.Row, 7);
            _rookToPos = new Position(FromPos.Row, 5);
        }
        else if (type == MoveType.CastleQueenSide)
        {
            _kingMoveDir = Direction.West;
            ToPos = new Position(FromPos.Row, 2);
            _rookFromPos = new Position(FromPos.Row, 0);
            _rookToPos = new Position(FromPos.Row, 3);
        }
        else
        {
            throw new ArgumentException(nameof(MoveType) + "Not Valid");
        }
    }

    public override MoveType Type { get; }

    public override Position FromPos { get; }

    public override Position ToPos { get; }

    public override bool Execute(Board board)
    {
        new NormalMove(FromPos, ToPos).Execute(board);
        new NormalMove(_rookFromPos, _rookToPos).Execute(board);
        return false;
    }

    public override bool IsLegal(Board board)
    {
        Player player = board[FromPos]!.Player;

        if (board.IsInCheck(player))
            return false;

        Board copy = board.Copy();
        Position kingPosInCopy = FromPos;
        for (int i = 0; i < 2; i++)
        {
            new NormalMove(kingPosInCopy, kingPosInCopy + _kingMoveDir).Execute(copy);
            kingPosInCopy += _kingMoveDir;

            if (copy.IsInCheck(player))
                return false;
        }

        return true;
    }
}
