using NUnit.Framework;

namespace PieceTests
{
    public class TestPiece : Piece
    {
        public override bool CheckMoves()
        {
            return true;
        }
    }

    public class PieceTests
    {
        private TestPiece _piece;

        public void SetUp()
        {
            _piece = new TestPiece();
        }

        public void Test_CheckMoves_ReturnsTrue()
        {
            var result = _piece.CheckMoves();

            Assert.IsTrue(result, "CheckMoves should return true");
        }

        public void Test_Move_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => _piece.Move(), "Move should not throw an exception");
        }

        public void Test_OnCapture_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => _piece.OnCapture(), "OnCapture should not throw an exception");
        }

        public void Test_Information_Property_SetAndGetValue()
        {
            short expectedValue = 5;

            _piece.information = expectedValue;
            short result = _piece.information;

            Assert.AreEqual(expectedValue, result, "Information property value should match the set value");
        }
    }
}
