namespace Chess.Logic;

public class Result
{
    public Result(Player winner, EndReason reason)
    {
        Winner = winner;
        Reason = reason;
    }

    public Player Winner { get; }

    public EndReason Reason { get; }

    public static Result Win(Player winner) => new(winner, EndReason.Checkmate);

    public static Result Draw(EndReason reason) => new(Player.None, reason);
}
