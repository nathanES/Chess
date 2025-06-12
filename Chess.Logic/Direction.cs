namespace Chess.Logic;

public class Direction
{
    public static readonly Direction North = new(-1, 0);
    public static readonly Direction South = new(1, 0);
    public static readonly Direction East = new(0, 1);
    public static readonly Direction West = new(0, -1);
    public static readonly Direction NorthEast = North + East;
    public static readonly Direction NorthWest = North + West;
    public static readonly Direction SouthEast = South + East;
    public static readonly Direction SouthWest = South + West;

    public Direction(int rowDelta, int columnDelta)
    {
        RowDelta = rowDelta;
        ColumnDelta = columnDelta;
    }

    public int RowDelta { get; }

    public int ColumnDelta { get; }

    public static Direction operator +(Direction dir1, Direction dir2) => new(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);

    public static Direction operator *(int scalar, Direction dir) => new(dir.RowDelta * scalar, dir.ColumnDelta * scalar);
}
