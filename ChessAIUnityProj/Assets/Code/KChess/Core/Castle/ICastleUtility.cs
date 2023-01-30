using KChess.Domain;

namespace KChess.Core.Castle
{
    internal interface ICastleUtility
    {
        void TryMakeCastle(IPiece movedPiece);
    }
}