namespace Chess.Logic;

public class GameState
{
    private readonly Dictionary<string, int> _stateHistory = new();
    private int _noCaptureOrPawnMove = 0;
    private string _stateString;

    public GameState(Board board, Player player)
    {
        Board = board;
        CurrentPlayer = player;
        _stateString = new StateString(CurrentPlayer, board).ToString();
        _stateHistory[_stateString] = 1;
    }

    public Board Board { get; }

    public Player CurrentPlayer { get; private set; }

    public Result? Result { get; private set; } = null;

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
        Board.SetPawnSkipPosition(CurrentPlayer, null);
        bool isCapturingOrMovePawn = move.Execute(Board);
        if (isCapturingOrMovePawn)
        {
            _noCaptureOrPawnMove = 0;
            _stateHistory.Clear();
        }
        else
        {
            _noCaptureOrPawnMove++;
        }

        CurrentPlayer = CurrentPlayer.Opponent();
        UpdateStateString();
        CheckForGameOver();
    }

    public IEnumerable<Move> AllLegalMovesFor(Player player)
    {
        IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
        {
            Piece piece = Board[pos]!;
            return piece.GetMoves(pos, Board);
        });
        return moveCandidates.Where(move => move.IsLegal(Board));
    }

    public bool IsGameOver() => Result != null;

    private void CheckForGameOver()
    {
        if (!AllLegalMovesFor(CurrentPlayer).Any()) // TODO : Maybe add an event when there is no legal Move
        {
            if (Board.IsInCheck(CurrentPlayer))
                Result = Result.Win(CurrentPlayer.Opponent());
            else
                Result = Result.Draw(EndReason.Stalemate);
        }
        else if (Board.IsInsufficientMaterial()) // TODO : Maybe not check that every play but when there is a capture or a promotion
        {
            Result = Result.Draw(EndReason.InsufficientMaterial);
        }
        else if (FiftyMoveRule())
        {
            Result = Result.Draw(EndReason.FiftyMoveRule);
        }
        else if (ThreefoldRepetition())
        {
            Result = Result.Draw(EndReason.ThreefoldRepetition);
        }
    }

    private void UpdateStateString()
    {
        _stateString = new StateString(CurrentPlayer, Board).ToString();
        if (!_stateHistory.ContainsKey(_stateString))
        {
            _stateHistory[_stateString] = 1;
        }
        else
        {
            _stateHistory[_stateString]++;
        }
    }

    private bool FiftyMoveRule()
    {
        int fullMoves = _noCaptureOrPawnMove / 2;
        return fullMoves == 50;
    }

    private bool ThreefoldRepetition() => _stateHistory[_stateString] == 3;
}
