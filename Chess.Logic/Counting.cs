namespace Chess.Logic;

public class Counting
{
    private readonly Dictionary<PieceType, int> _whiteCount = new();
    private readonly Dictionary<PieceType, int> _blackCount = new();

    public Counting()
    {
        foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
        {
            _whiteCount[type] = 0;
            _blackCount[type] = 0;
        }
    }

    public int TotalCount { get; private set; }

    public void Increment(Player player, PieceType type)
    {
        if (player == Player.White)
        {
            _whiteCount[type]++;
        }
        else
        {
            _blackCount[type]++;
        }

        TotalCount++;
    }

    public int White(PieceType type) => _whiteCount[type];

    public int Black(PieceType type) => _blackCount[type];
}
