using KChess.Domain;

namespace KChess.Core.GameStateChanger
{
    internal interface IGameStateChanger
    {
        public BoardState UpdateBoardState();
    }
}