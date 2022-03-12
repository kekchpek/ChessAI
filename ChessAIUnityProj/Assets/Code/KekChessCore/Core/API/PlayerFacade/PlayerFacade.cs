using ChessAI.Core.MoveUtility;
using ChessAI.Core.TurnUtility;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.API.PlayerFacade
{
    public class PlayerFacade : IPlayerFacade
    {

        public PlayerFacade(IMoveUtility moveUtility, ITurnGetter turnGetter)
        {
            
        }
        
        public bool TryMovePiece()
        {
            throw new System.NotImplementedException();
        }

        public BoardCoordinates[] GetAvailableMoves(IPiece piece)
        {
            throw new System.NotImplementedException();
        }
    }
}