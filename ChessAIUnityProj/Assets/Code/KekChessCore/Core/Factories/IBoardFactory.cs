using ChessAI.Domain;

namespace ChessAI.Core.Factories
{
    public interface IBoardFactory
    {
        public IBoard CreateStandardBoard();

        public IBoard Copy(IBoard sourceBoard);
    }   
}