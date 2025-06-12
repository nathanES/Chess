namespace Chess.Logic;

public enum EndReason
{
    Checkmate,
    Stalemate,
    FiftyMoveRule,
    InsufficentMaterial,
    ThreefoldRepetition,
}
