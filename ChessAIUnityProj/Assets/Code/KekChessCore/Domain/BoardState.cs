namespace ChessAI.Domain
{
    public enum BoardState
    {
        Regular,
        CheckToWhite,
        CheckToBlack,
        MateToWhite,
        MateToBlack,
        Stalemate,
        RepeatDraw
    }
}