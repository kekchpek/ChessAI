using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KChess.Auxiliary.WeakReferenceDecorator;

namespace KChess.Domain.BoardIndices
{
    internal sealed class BoardIndicesProvider
    {
        private static readonly Dictionary<TransientWeakReference<IBoard>, IBoardIndex> _indices =
        new();

        private static bool _updating = true;

        static BoardIndicesProvider()
        {
            Task.Run(UpdateLinks);
        }
        
        public static IBoardIndex GetIndex(IBoard board)
        {
            var weakRef = new TransientWeakReference<IBoard>(board);
            if (!_indices.ContainsKey(weakRef))
            {
                _indices.Add(weakRef, new BoardIndex(weakRef));
            }
            return _indices[weakRef];
        }

        private static async void UpdateLinks()
        {
            while (_updating)
            {
                var boardLinks = _indices.Keys.ToArray();
                foreach (var boardLink in boardLinks)
                {
                    if (!boardLink.TryGetTarget(out _))
                    {
                        _indices.Remove(boardLink);
                    }
                }

                await Task.Delay(10000);
            }
        }
    }
}