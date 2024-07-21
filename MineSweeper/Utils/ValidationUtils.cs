namespace MineSweeper.Utils
{
	public static class ValidationUtils
	{
		public static bool ValidateIntInput(string? str, out int numb)
		{
			numb = 0;

			if (!string.IsNullOrEmpty(str) && int.TryParse(str, out int result))
				numb = result;
			else
			{
				Console.WriteLine("Incorect input.");
				return false;
			}

			return true;
		}

		public static bool ValidateAlphaNumericInput(string? str, int gridSize, out int[] coordinates)
		{
			coordinates = [];

			if (!string.IsNullOrEmpty(str) && str.Length == 2)
			{
				List<int> tempCoordinates = [];
				try
				{
					int row = char.ToUpper(str[0]) - 'A';
					if (row > gridSize)
					{
						Console.WriteLine("Incorect input.");
						return false;
					}

					tempCoordinates.Add(row);
				}
				catch (Exception)
				{
					Console.WriteLine("Incorect input.");
					return false;
				}

				if (int.TryParse(str[1].ToString(), out int column))
				{
					if (column > gridSize)
					{
						Console.WriteLine("Incorect input.");
						return false;
					}

					tempCoordinates.Add(column - 1);
				}
				else
				{
					Console.WriteLine("Incorect input.");
					return false;
				}

				coordinates = [.. tempCoordinates];
				return true;
			}
			else
			{
				Console.WriteLine("Incorect input.");
				return false;
			}
		}

		public static bool ValidateGridSize(int gridSize)
		{
			// Minimum grid size
			if (gridSize < 2)
			{
				Console.WriteLine("Minimum size of grid is 2.");
				return false;
			}
			else if (gridSize > 10) // Maximum grid size
			{
				Console.WriteLine("Maximum size of grid is 10.");
				return false;
			}

			return true;
		}

		public static bool ValidateMineCount(int gridSize, int count)
		{
			// Minimum one mine
			if (count < 1)
			{
				Console.WriteLine("There must be at least 1 mine.");
				return false;
			}

			// Maximum mines allowed is 35% of total grids
			double maxCount = 0.35 * (gridSize * gridSize);

			// Check if count is less than or equal to 35% of total grids
			if (count > maxCount)
			{
				Console.WriteLine("Maximum number is 35% of total sqaures.");
				return false;
			}

			return true;
		}
	}
}
