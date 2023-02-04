using System;
using System.IO;
using KChess.GameManagement.Domain;

namespace KChess.GameManagement.Core.Pgn
{
    public class PgnGameParser : IPgnGameParser
    {
        public IGameHistory[] ParseFile(string pgnFile)
        {
            throw new System.NotImplementedException();
        }

        public IGameHistory ParseGame(string pgnGame)
        {
            var reader = new StreamReader(pgnGame);
            
            // Read to the game start
            while (reader.ReadLine() != String.Empty)
            {
            }

            return null;

        }
    }
}