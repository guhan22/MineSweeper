using MineSweeper.Models;
using MineSweeper.Utils;

namespace MineSweeperTest
{
	public class Tests
	{
		[Test]
		public void TestBoardInit()
		{
			// Arrange
			int gridSize = 5;
			int mineCount = 5;

			// Act
			Board board = new(gridSize, mineCount);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(board.gridSize, Is.EqualTo(gridSize));
				Assert.That(board.mineCount, Is.EqualTo(mineCount));
				Assert.That(board.grids, Is.Not.Null);
			});
			Assert.Multiple(() =>
			{
				Assert.That(board.grids.GetLength(0), Is.EqualTo(gridSize));
				Assert.That(board.grids.GetLength(1), Is.EqualTo(gridSize));
			});
		}

		[Test]
		public void TestPrintBoard()
		{
			// Arrange
			var board = new Board(2, 1);
			board.grids[0, 0].IsRevealed = true;
			board.grids[0, 0].AdjacentCount = 1;

			using (var sw = new StringWriter())
			{
				Console.SetOut(sw);

				// Act
				BoardUtils.PrintBoard(board);

				// Assert
				var expected = "  1 2\r\nA 1 _ \r\nB _ _ \r\n";
				Assert.That(sw.ToString(), Is.EqualTo(expected));
			}
		}

		[TestCase(0, 0, false, true)]
		[TestCase(0, 1, true, false)]
		[TestCase(-1, 0, false, false)]
		[TestCase(0, 2, false, false)]
		public void TestRevealGrid(int row, int col, bool isMine, bool expectedResult)
		{
			// Arrange
			var board = new Board(2, 1);
			if (row >= 0 && row < board.gridSize && col >= 0 && col < board.gridSize)
			{
				board.grids[row, col].IsMine = isMine;
			}

			// Act
			bool result = BoardUtils.RevealGrid(board, row, col);

			// Assert
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void TestRevealAdjacentGrids()
		{
			// Arrange
			var board = new Board(3, 0);
			board.grids[1, 1].AdjacentCount = 0;

			// Act
			BoardUtils.RevealGrid(board, 1, 1);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(board.grids[0, 0].IsRevealed, Is.True);
				Assert.That(board.grids[0, 1].IsRevealed, Is.True);
				Assert.That(board.grids[0, 2].IsRevealed, Is.True);
				Assert.That(board.grids[1, 0].IsRevealed, Is.True);
				Assert.That(board.grids[1, 1].IsRevealed, Is.True);
				Assert.That(board.grids[1, 2].IsRevealed, Is.True);
				Assert.That(board.grids[2, 0].IsRevealed, Is.True);
				Assert.That(board.grids[2, 1].IsRevealed, Is.True);
				Assert.That(board.grids[2, 2].IsRevealed, Is.True);
			});
		}

		[TestCase("123", true, 123)]
		[TestCase("abc", false, 0)]
		[TestCase("", false, 0)]
		public void TestValidateIntInput(string input, bool expectedResult, int expectedNumb)
		{
			bool result = ValidationUtils.ValidateIntInput(input, out int numb);
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.EqualTo(expectedResult));
				Assert.That(numb, Is.EqualTo(expectedNumb));
			});
		}

		[TestCase("A1", 5, true, new[] { 0, 0 })]
		[TestCase("B3", 5, true, new[] { 1, 2 })]
		[TestCase("A6", 5, false, new int[0])]
		[TestCase("1A", 5, false, new int[0])]
		[TestCase("", 5, false, new int[0])]
		public void TestValidateAlphaNumericInput(string input, int gridSize, bool expectedResult, int[] expectedCoordinates)
		{
			bool result = ValidationUtils.ValidateAlphaNumericInput(input, gridSize, out int[] coordinates);
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.EqualTo(expectedResult));
				Assert.That(coordinates, Is.EqualTo(expectedCoordinates));
			});
		}

		[TestCase(1, false)]
		[TestCase(2, true)]
		[TestCase(10, true)]
		[TestCase(11, false)]
		public void TestValidateGridSize(int gridSize, bool expectedResult)
		{
			bool result = ValidationUtils.ValidateGridSize(gridSize);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[TestCase(5, 0, false)]
		[TestCase(5, 4, true)]
		[TestCase(5, 9, false)]
		[TestCase(5, 10, false)]
		public void TestValidateMineCount(int gridSize, int mineCount, bool expectedResult)
		{
			bool result = ValidationUtils.ValidateMineCount(gridSize, mineCount);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

	}
}
