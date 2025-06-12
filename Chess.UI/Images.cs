namespace Chess.UI;

using Chess.Logic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public static class Images
{
    private static readonly Dictionary<PieceType, ImageSource> _whiteSources = new()
    {
        { PieceType.Pawn, LoadImage("Assets/PawnW.png") },
        { PieceType.Bishop, LoadImage("Assets/BishopW.png") },
        { PieceType.Knight, LoadImage("Assets/KnightW.png") },
        { PieceType.Rook, LoadImage("Assets/RookW.png") },
        { PieceType.Queen, LoadImage("Assets/QueenW.png") },
        { PieceType.King, LoadImage("Assets/KingW.png") },
    };

    private static readonly Dictionary<PieceType, ImageSource> _blackSources = new()
    {
        { PieceType.Pawn, LoadImage("Assets/PawnB.png") },
        { PieceType.Bishop, LoadImage("Assets/BishopB.png") },
        { PieceType.Knight, LoadImage("Assets/KnightB.png") },
        { PieceType.Rook, LoadImage("Assets/RookB.png") },
        { PieceType.Queen, LoadImage("Assets/QueenB.png") },
        { PieceType.King, LoadImage("Assets/KingB.png") },
    };

    public static ImageSource? GetImage(Player player, PieceType pieceType)
    {
        return player switch
        {
            Player.White => _whiteSources[pieceType],
            Player.Black => _blackSources[pieceType],
            _ => null
        };
    }

    public static ImageSource? GetImage(Piece? piece)
    {
        if (piece == null)
            return null;

        return GetImage(piece.Player, piece.Type);
    }

    private static ImageSource LoadImage(string filePath) => new BitmapImage(new Uri(filePath, UriKind.Relative));
}
