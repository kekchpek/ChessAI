using KChess.Domain;

namespace KChess.Core.CheckMate
{
    internal interface ICheckMateUtility
    {
        public BoardState UpdateBoardState();
    }
}