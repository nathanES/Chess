namespace Chess.Logic;

public class PawnPromotion : Move
{
    private readonly PieceType _newType;

    public PawnPromotion(Position fromPos, Position toPos, PieceType newType)
    {
        FromPos = fromPos;
        ToPos = toPos;
        _newType = newType;
    }

    public override MoveType Type => MoveType.PawnPromotion;

    public override Position FromPos { get; }

    public override Position ToPos { get; }

    public override bool Execute(Board board)
    {
        Piece pawn = board[FromPos]!;
        board[FromPos] = null;

        Piece promotionPiece = CreatePromotionPiece(pawn.Player);
        promotionPiece.HasMoved = true;
        board[ToPos] = promotionPiece;
        return false;
    }

    private Piece CreatePromotionPiece(Player player)
    {
        return _newType switch
        {
            PieceType.Knight => new Knight(player),
            PieceType.Bishop => new Bishop(player),
            PieceType.Rook => new Rook(player),
            _ => new Queen(player),
        };
    }
}
