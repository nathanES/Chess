namespace Chess.Logic;

public class Queen : Piece
{
    public override PieceType Type => PieceType.Queen;

    public override Player Color { get; }

    public Queen(Player color) => Color = color;

    public override Piece Copy() => new Queen(Color) { HasMoved = HasMoved };
}
