using KChess.GameManagement.Domain;

namespace KChess.GameManagement.Core.Pgn
{
    public interface IPgnGameParser
    {
        IGameHistory[] ParseFile(string pgnFile);

        IGameHistory ParseGame(string pgnGame);
    }
}