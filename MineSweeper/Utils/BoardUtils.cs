using MineSweeper.Models;

namespace MineSweeper.Utils
{
	// Utility methods for board operations
	public static class BoardUtils
	{
		// Display initial board with hidden squares/grids in console
		public static void PrintBoard(Board board)
		{
			// Columns in integer starting from 1
			Console.WriteLine("  " + string.Join(" ", Enumerable.Range(1, board.gridSize).Select(x => x.ToString())));

			for (int i = 0; i < board.gridSize; i++)
			{
				// Rows in uppercase alphabets starting from A
				Console.Write((char)('A' + i) + " ");

				for (int j = 0; j < board.gridSize; j++)
				if (board.grids[i, j].IsRevealed && !board.grids[i, j].IsMine)
						Console.Write(board.grids[i, j].AdjacentCount + " ");
					else
						Console.Write("_ ");

				Console.WriteLine();
			}
		}

		// Display the grid selected by the user along
		public static bool RevealGrid(Board board, int row, int col)
		{
			if (row < 0 || row >= board.gridSize || col < 0 || col >= board.gridSize)
				return false;

			// Grid already revealed
			if (board.grids[row, col].IsRevealed)
			{
				Console.WriteLine("This square is already revealed.");
				Console.WriteLine();
				return true; 
			}

			board.grids[row, col].IsRevealed = true;
			board.revealedCount++;

			if (board.grids[row, col].IsMine)
				return false;
			else if (board.grids[row, col].AdjacentCount == 0)
				RevealAdjacentGrids(board, row, col); // Display adjacent grids which are not mines

			Console.WriteLine($"This square contains {board.grids[row, col].AdjacentCount} adjacent mines.");
			Console.WriteLine();
			return true;
		}

		// Display adjacent grids which are not mines
		private static void RevealAdjacentGrids(Board board, int row, int col)
		{
			for (int i = -1; i <= 1; i++)
			for (int j = -1; j <= 1; j++)
				{
					int x = row + i;
					int y = col + j;
					if (x >= 0 && x < board.gridSize && y >= 0 && y < board.gridSize)
					if (!board.grids[x, y].IsRevealed)
						{
							board.grids[x, y].IsRevealed = true;
							board.revealedCount++;
							if (board.grids[x, y].AdjacentCount == 0)
								RevealAdjacentGrids(board, x, y); // Recursion
						}
				}
		}
	}
}
