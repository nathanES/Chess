namespace Chess.Logic;

public enum EndReason
{
    Checkmate,
    Stalemate,
    FiftyMoveRule,
    InsufficientMaterial,
    ThreefoldRepetition,
}
