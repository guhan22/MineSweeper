using MineSweeper.Models;

namespace MineSweeper.Utils
{
	public static class BoardUtils
	{
		public static void PrintBoard(Board board)
		{
			Console.WriteLine("  " + string.Join(" ", Enumerable.Range(1, board.gridSize).Select(x => x.ToString())));

			for (int i = 0; i < board.gridSize; i++)
			{
				Console.Write((char)('A' + i) + " ");

				for (int j = 0; j < board.gridSize; j++)
				if (board.grids[i, j].IsRevealed && !board.grids[i, j].IsMine)
						Console.Write(board.grids[i, j].AdjacentCount + " ");
					else
						Console.Write("_ ");

				Console.WriteLine();
			}
		}

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
				RevealAdjacentGrids(board, row, col);

			Console.WriteLine($"This square contains {board.grids[row, col].AdjacentCount} adjacent mines.");
			Console.WriteLine();
			return true;
		}

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
								RevealAdjacentGrids(board, x, y);
						}
				}
		}
	}
}
