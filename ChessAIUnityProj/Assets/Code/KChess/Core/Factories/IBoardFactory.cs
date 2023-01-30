using KChess.Domain;

namespace KChess.Core.Factories
{
    internal interface IBoardFactory
    {
        public IBoard CreateStandardBoard();

        public IBoard Copy(IBoard sourceBoard);
    }   
}