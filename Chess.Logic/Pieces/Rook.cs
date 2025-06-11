namespace Chess.Logic;

public class Rook : Piece
{
    public override PieceType Type => PieceType.Rook;

    public override Player Color { get; }

    public Rook(Player color) => Color = color;

    public override Piece Copy() => new Rook(Color) { HasMoved = HasMoved };
}
