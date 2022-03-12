using KekChessCore.Domain;
using KekChessCore.XRayUtility;
using KekChessCore.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KekChessCore.XRayUtility.XRayPiecesUtilities.QueenXRayUtility;
using KekChessCore.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests.PiecesXRays
{
    public class QueenXRayUtilityTests
    {

        private QueenXRayUtility CreateQueenXRayUtility(
            out IBishopXRayUtility bishopXRayUtility,
            out IRookXRayUtility rookXRayUtility)
        {
            bishopXRayUtility = Substitute.For<IBishopXRayUtility>();
            rookXRayUtility = Substitute.For<IRookXRayUtility>();
            return new QueenXRayUtility(bishopXRayUtility, rookXRayUtility);
        }

        [Test]
        public void GetXRays_GetsFromBishopAndRook()
        {
            // Arrange
            var queenXRayUtility = CreateQueenXRayUtility(
                out var bishopXRayUtility,
                out var rookXRayUtility);
            
            var xRay1 = Substitute.For<IXRay>();
            var xRay2 = Substitute.For<IXRay>();
            var xRay3 = Substitute.For<IXRay>();
            var xRay4 = Substitute.For<IXRay>();
            var xRay5 = Substitute.For<IXRay>();

            bishopXRayUtility.GetXRays(Arg.Any<IPiece>()).Returns(new[] {xRay1, xRay2, xRay3});
            rookXRayUtility.GetXRays(Arg.Any<IPiece>()).Returns(new[] {xRay4, xRay5});

            // Act
            var xRays = queenXRayUtility.GetXRays(Substitute.For<IPiece>());
            
            // Assert
            Assert.AreEqual(5, xRays.Length);
            Assert.Contains(xRay1, xRays);
            Assert.Contains(xRay2, xRays);
            Assert.Contains(xRay3, xRays);
            Assert.Contains(xRay4, xRays);
            Assert.Contains(xRay5, xRays);
        }
        
    }
}