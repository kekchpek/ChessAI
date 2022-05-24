using KChess.Core.API.PlayerFacade;

namespace KChess.Core.API.BoardsManager
{
    public interface IBoardsManager
    {
        void CreateBoard(out IPlayerFacade whitePlayer, out IPlayerFacade blackPlayer);
    }
}