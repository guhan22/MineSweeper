namespace MineSweeper.Models
{
	public class Board
	{
		public Grid[,] grids { get; set; }
		public int gridSize { get; set; }
		public int mineCount { get; set; }
		public int revealedCount { get; set; }

		public Board(int gridSize, int mineCount)
		{
			this.gridSize = gridSize;
			this.mineCount = mineCount;

			revealedCount = 0;
			grids = new Grid[gridSize, gridSize];

			// Initialize empty grids
			for (int i = 0; i < gridSize; i++)
			for (int j = 0; j < gridSize; j++)
				grids[i, j] = new Grid();

			// Plot random mines based on mineCount
			PlotMines();

			// Calculate the adjacency value for each grid
			CalculateAdjacency();
		}

		private void PlotMines()
		{
			Random random = new();
			int minesToPlace = mineCount;

			while (minesToPlace > 0)
			{
				int row = random.Next(0, gridSize);
				int col = random.Next(0, gridSize);

				if (!grids[row, col].IsMine)
				{
					grids[row, col].IsMine = true;
					minesToPlace--;
				}
			}
		}

		private void CalculateAdjacency()
		{
			// Calculate adjacent mines for each cell
			for (int i = 0; i < gridSize; i++)
			for (int j = 0; j < gridSize; j++)
				{
					if (grids[i, j].IsMine)
						continue;
					int count = 0;
					for (int di = -1; di <= 1; di++)
					for (int dj = -1; dj <= 1; dj++)
						{
							int ni = i + di;
							int nj = j + dj;
							if (ni >= 0 && ni < gridSize && nj >= 0 && nj < gridSize && grids[ni, nj].IsMine)
								count++;
						}
					grids[i, j].AdjacentCount = count;
				}
		}
	}
}
