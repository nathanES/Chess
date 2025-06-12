namespace Chess.UI;

using System.IO;
using System.Windows;
using System.Windows.Input;

public static class ChessCursors
{
    public static readonly Cursor WhiteCursor = LoadCursor("Assets/CursorW.cur");
    public static readonly Cursor BlackCursor = LoadCursor("Assets/CursorB.cur");

    private static Cursor LoadCursor(string filePath)
    {
        using Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
        return new Cursor(stream, true);
    }
}
