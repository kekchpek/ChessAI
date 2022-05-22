using KChess.Core.API.PlayerFacade;

namespace KChess.Core.API.BoardsManager
{
    public interface IBoardsManger
    {
        void CreateBoard(out IPlayerFacade whitePlayer, out IPlayerFacade blackPlayer);
    }
}