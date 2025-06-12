namespace Chess.Logic;

public class GameState
{
    public GameState(Board board, Player player)
    {
        Board = board;
        CurrentPlayer = player;
    }

    public Board Board { get; }

    public Player CurrentPlayer { get; private set; }

    public Result? Result { get; private set; } = null;

    public IEnumerable<Move> LegalMovesForPiece(Position pos)
    {
        if (Board.IsEmpty(pos) || Board[pos]!.Player != CurrentPlayer)
            return [];

        Piece piece = Board[pos]!;
        IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
        return moveCandidates.Where(move => move.IsLegal(Board));
    }

    public void MakeMove(Move move)
    {
        move.Execute(Board);
        CurrentPlayer = CurrentPlayer.Opponent();
        CheckForGameOver();
    }

    public IEnumerable<Move> AllLegalMovesFor(Player player)
    {
        IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
        {
            Piece piece = Board[pos]!;
            return piece.GetMoves(pos, Board);
        });
        return moveCandidates.Where(move => move.IsLegal(Board));
    }

    public bool IsGameOver() => Result != null;

    private void CheckForGameOver()
    {
        if (!AllLegalMovesFor(CurrentPlayer).Any())
        {
            if (Board.IsInCheck(CurrentPlayer))
                Result = Result.Win(CurrentPlayer.Opponent());
            else
                Result = Result.Draw(EndReason.Stalemate);
        }
    }
}
