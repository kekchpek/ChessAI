﻿namespace KekChessCore.Domain
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