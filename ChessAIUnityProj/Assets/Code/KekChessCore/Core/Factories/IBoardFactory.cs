using KekChessCore.Domain;

namespace KekChessCore.Factories
{
    public interface IBoardFactory
    {
        public IBoard CreateStandardBoard();

        public IBoard Copy(IBoard sourceBoard);
    }   
}