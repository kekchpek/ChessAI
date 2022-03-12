using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.MoveUtility;
using KekChessCore.TurnUtility;

namespace KekChessCore.API.PlayerFacade
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