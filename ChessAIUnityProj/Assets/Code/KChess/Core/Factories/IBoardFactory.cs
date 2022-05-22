using KChess.Domain;

namespace KChess.Core.Factories
{
    public interface IBoardFactory
    {
        public IBoard CreateStandardBoard();

        public IBoard Copy(IBoard sourceBoard);
    }   
}