using KChessUnity.Models.Board;

namespace KChessUnity.ViewModels.MovesDisplayer
{
    public class MovesDisplayerPayload : IMovesDisplayerPayload
    {
        public IBoardWorldPositionsCalculator BoardWorldPositionsCalculator { get; }

        public MovesDisplayerPayload(IBoardWorldPositionsCalculator boardWorldPositionsCalculator)
        {
            BoardWorldPositionsCalculator = boardWorldPositionsCalculator;
        }
        
    }
}