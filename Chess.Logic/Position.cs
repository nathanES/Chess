namespace Chess.Logic;

public class Position
{
    public int Row { get; }
    public int Column { get; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public Color SquareColor() => SquareColor(Row, Column);

    public static Color SquareColor(int row, int column)
    {
        if ((row + column) % 2 == 0)
            return Color.White;

        return Color.Black;
    }

    public override bool Equals(object? obj) => obj is Position position && Row == position.Row && Column == position.Column;
    public override int GetHashCode() => HashCode.Combine(Row, Column);

    public static bool operator ==(Position? left, Position? right) => EqualityComparer<Position>.Default.Equals(left, right);
    public static bool operator !=(Position? left, Position? right) => !(left == right);

    public static Position operator +(Position pos, Direction dir) => new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
}
