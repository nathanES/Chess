namespace Chess.UI;

using Chess.Logic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    private readonly Image[,] _pieceImages = new Image[8, 8];

    private readonly Rectangle[,] _highlights = new Rectangle[8, 8];
    private readonly Dictionary<Position, Move> _moveCache = new();

    private GameState _gameState;
    private Position? _selectedPos = null;

    public MainWindow()
    {
        InitializeComponent();
        InitializeBoard();
        _gameState = new GameState(Board.Initial(), Player.White);
        DrawBoard(_gameState.Board);
        SetCursor(_gameState.CurrentPlayer);
    }

    private void InitializeBoard()
    {
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                var image = new Image();
                _pieceImages[r, c] = image;
                PieceGrid.Children.Add(image);

                var highlight = new Rectangle();
                _highlights[r, c] = highlight;
                HighlightGrid.Children.Add(highlight);
            }
        }
    }

    private void DrawBoard(Board board)
    {
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Piece? piece = board[r, c];
                _pieceImages[r, c].Source = Images.GetImage(piece);
            }
        }
    }

    private void BoardGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (IsMenuOnScreen())
            return;

        Point point = e.GetPosition(BoardGrid);
        Position pos = ToSquarePosition(point);
        if (_selectedPos == null)
        {
            OnFromPositionSelected(pos);
        }
        else
        {
            OnToPositionSelected(pos);
        }
    }

    private void OnFromPositionSelected(Position pos)
    {
        IEnumerable<Move> moves = _gameState.LegalMovesForPiece(pos);
        if (moves.Any())
        {
            _selectedPos = pos;
            CacheMoves(moves);
            ShowHighlights();
        }
    }

    private void OnToPositionSelected(Position pos)
    {
        _selectedPos = null;
        HideHighlights();
        if (_moveCache.TryGetValue(pos, out Move? move))
            HandleMove(move);
    }

    private void HandleMove(Move move)
    {
        _gameState.MakeMove(move);
        DrawBoard(_gameState.Board);
        SetCursor(_gameState.CurrentPlayer);

        if (_gameState.IsGameOver())
            ShowGameOver();
    }

    private Position ToSquarePosition(Point point)
    {
        double squareSize = BoardGrid.ActualWidth / 8;
        int row = (int)(point.Y / squareSize);
        int col = (int)(point.X / squareSize);
        return new Position(row, col);
    }

    private void CacheMoves(IEnumerable<Move> moves)
    {
        _moveCache.Clear();
        foreach (Move move in moves)
        {
            _moveCache[move.ToPos] = move;
        }
    }

    private void ShowHighlights()
    {
        var color = Color.FromArgb(150, 125, 255, 125);
        foreach (Position to in _moveCache.Keys)
        {
            _highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
        }
    }

    private void HideHighlights()
    {
        foreach (Position to in _moveCache.Keys)
        {
            _highlights[to.Row, to.Column].Fill = Brushes.Transparent;
        }
    }

    private void SetCursor(Player player)
    {
        if (player == Player.White)
            Cursor = ChessCursors.WhiteCursor;
        else
            Cursor = ChessCursors.BlackCursor;
    }

    private bool IsMenuOnScreen() => MenuContainer.Content != null;

    private void ShowGameOver()
    {
        var gameOverMenu = new GameOverMenu(_gameState);
        MenuContainer.Content = gameOverMenu;

        gameOverMenu.OptionSelected += option =>
        {
            if (option == Option.Restart)
            {
                MenuContainer.Content = null;
                RestartGame();
            }
            else
            {
                Application.Current.Shutdown();
            }
        };
    }

    private void RestartGame()
    {
        HideHighlights();
        _moveCache.Clear();
        _gameState = new GameState(Board.Initial(), Player.White);
        DrawBoard(_gameState.Board);
        SetCursor(_gameState.CurrentPlayer);
    }
}
