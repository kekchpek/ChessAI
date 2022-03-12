using ChessAI.Domain.Impl;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class BoardCoordinatesTests
    {

        [TestCase("a1", 0, 0)]
        [TestCase("a2", 0, 1)]
        [TestCase("a3", 0, 2)]
        [TestCase("a4", 0, 3)]
        [TestCase("a5", 0, 4)]
        [TestCase("a6", 0, 5)]
        [TestCase("a7", 0, 6)]
        [TestCase("a8", 0, 7)]
        
        [TestCase("b1", 1, 0)]
        [TestCase("b2", 1, 1)]
        [TestCase("b3", 1, 2)]
        [TestCase("b4", 1, 3)]
        [TestCase("b5", 1, 4)]
        [TestCase("b6", 1, 5)]
        [TestCase("b7", 1, 6)]
        [TestCase("b8", 1, 7)]
        
        [TestCase("c1", 2, 0)]
        [TestCase("c2", 2, 1)]
        [TestCase("c3", 2, 2)]
        [TestCase("c4", 2, 3)]
        [TestCase("c5", 2, 4)]
        [TestCase("c6", 2, 5)]
        [TestCase("c7", 2, 6)]
        [TestCase("c8", 2, 7)]
        
        [TestCase("d1", 3, 0)]
        [TestCase("d2", 3, 1)]
        [TestCase("d3", 3, 2)]
        [TestCase("d4", 3, 3)]
        [TestCase("d5", 3, 4)]
        [TestCase("d6", 3, 5)]
        [TestCase("d7", 3, 6)]
        [TestCase("d8", 3, 7)]
        
        [TestCase("e1", 4, 0)]
        [TestCase("e2", 4, 1)]
        [TestCase("e3", 4, 2)]
        [TestCase("e4", 4, 3)]
        [TestCase("e5", 4, 4)]
        [TestCase("e6", 4, 5)]
        [TestCase("e7", 4, 6)]
        [TestCase("e8", 4, 7)]
        
        [TestCase("f1", 5, 0)]
        [TestCase("f2", 5, 1)]
        [TestCase("f3", 5, 2)]
        [TestCase("f4", 5, 3)]
        [TestCase("f5", 5, 4)]
        [TestCase("f6", 5, 5)]
        [TestCase("f7", 5, 6)]
        [TestCase("f8", 5, 7)]
        
        [TestCase("g1", 6, 0)]
        [TestCase("g2", 6, 1)]
        [TestCase("g3", 6, 2)]
        [TestCase("g4", 6, 3)]
        [TestCase("g5", 6, 4)]
        [TestCase("g6", 6, 5)]
        [TestCase("g7", 6, 6)]
        [TestCase("g8", 6, 7)]
        
        [TestCase("h1", 7, 0)]
        [TestCase("h2", 7, 1)]
        [TestCase("h3", 7, 2)]
        [TestCase("h4", 7, 3)]
        [TestCase("h5", 7, 4)]
        [TestCase("h6", 7, 5)]
        [TestCase("h7", 7, 6)]
        [TestCase("h8", 7, 7)]
        public void Creation_FromString(string source, int coord1, int coord2)
        {
            BoardCoordinates bc = source;

            Assert.AreEqual(source, bc.ToString());
            Assert.AreEqual((coord1, coord2), bc.ToNumeric());
        }

    }
}