namespace Chess.Logic;
public class GameState
{
    public Board Board { get; }
    public Player Player { get; private set; }

    public GameState(Board board, Player player)
    {
        Board = board;
        Player = player;
    }
}
