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
    }
}
