using KChess.Domain;
using KChess.GameManagement.Domain;

namespace KChess.Core.Factories
{
    internal interface IBoardFactory
    {
        public IBoard CreateStandardBoard();

        public IBoard Create(Position position);

        public IBoard Copy(IBoard sourceBoard);
    }   
}