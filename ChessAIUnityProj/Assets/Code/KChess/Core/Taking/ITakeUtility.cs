using KChess.Domain;

namespace KChess.Core.Taking
{
    internal interface ITakeUtility
    {
        void TryTake(IPiece movedPiece);
    }
}