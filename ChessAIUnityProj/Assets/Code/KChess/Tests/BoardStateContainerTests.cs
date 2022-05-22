using System;
using KChess.Core.BoardStateUtils;
using KChess.Domain;
using NUnit.Framework;

namespace KChess.Tests
{
    public class BoardStateContainerTests
    {
        private BoardStateContainer CreateContainer()
        {
            return new BoardStateContainer();
        }

        [Test]
        public void Creation_InitedWithRegularState()
        {
            // Arrange
            var container = CreateContainer();
            
            // Act
            // no act
            
            // Assert
            Assert.AreEqual(BoardState.Regular, container.Get());
        }
        
        [Test]
        public void SetState_ReturnsSameState()
        {
            foreach (BoardState state in Enum.GetValues(typeof(BoardState)))
            {
                // Arrange
                var container = CreateContainer();
            
                // Act
                container.Set(state);
            
                // Assert
                Assert.AreEqual(state, container.Get());
            }
        }
        
        [Test]
        public void SetState_EventCalled()
        {
            foreach (BoardState state in Enum.GetValues(typeof(BoardState)))
            {
                // Arrange
                var container = CreateContainer();
            
                // Act
                BoardState? setState = null;
                container.StateChanged += x => setState = x;
                container.Set(state);
            
                // Assert
                Assert.AreEqual(state, setState);
            }
        }
        
    }
}