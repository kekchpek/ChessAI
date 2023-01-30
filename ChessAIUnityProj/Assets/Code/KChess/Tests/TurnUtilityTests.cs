using KChess.Core.TurnUtility;
using KChess.Domain;
using NUnit.Framework;

namespace KChess.Tests
{
    public class TurnUtilityTests
    {
        private TurnUtility CreateTurnUtility()
        {
            return new TurnUtility();
        }

        [Test]
        public void Creation_WhiteTurn()
        {
            // Arrange
            var turnUtility = CreateTurnUtility();
            
            // Act 
            // no act
            
            // Assert
            Assert.AreEqual(PieceColor.White, turnUtility.GetTurn());
        }

        [Test]
        public void SetTurn_EventCalled()
        {
            // Arrange
            var turnUtility = CreateTurnUtility();
            
            // Act
            PieceColor? turnColor = null;
            turnUtility.TurnChanged += x => turnColor = x;
            turnUtility.NextTurn();
            
            // Assert
            Assert.AreEqual(PieceColor.Black, turnColor);
        }


        [Test]
        public void SetTurn_TurnChanged()
        {
            // Arrange
            var turnUtility = CreateTurnUtility();
            
            // Act
            turnUtility.NextTurn();
            
            // Assert
            Assert.AreEqual(PieceColor.Black, turnUtility.GetTurn());
        }
    }
}