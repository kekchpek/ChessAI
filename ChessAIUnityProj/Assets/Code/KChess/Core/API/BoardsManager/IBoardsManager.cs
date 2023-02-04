using System;
using KChess.Core.API.PlayerFacade;

namespace KChess.Core.API.BoardsManager
{
    public interface IBoardsManager
    {
        /// <summary>
        /// Creates a board for playing.
        /// </summary>
        /// <param name="whitePlayer">Facade for white player.</param>
        /// <param name="blackPlayer">Facade for black player.</param>
        /// <param name="universalPlayerFacade">Facade for any player.</param>
        /// <returns>Board id</returns>
        Guid CreateBoard(
            out IPlayerFacade whitePlayer, 
            out IPlayerFacade blackPlayer,
            out IPlayerFacade universalPlayerFacade);

        void ReleaseBoard(Guid boardId);
    }
}