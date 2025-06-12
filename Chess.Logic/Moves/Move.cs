namespace Chess.Logic;

public abstract class Move
{
    public abstract MoveType Type { get; }

    public abstract Position FromPos { get; }

    public abstract Position ToPos { get; }

    public abstract void Execute(Board board);

    public virtual bool IsLegal(Board board) // Not optimal
    {
        Player player = board[FromPos]!.Player;
        Board boardCopy = board.Copy();
        Execute(boardCopy);
        return !boardCopy.IsInCheck(player);
    }
}
