namespace MineSweeper.Utils
{
	// Utility methods for validation
	public static class ValidationUtils
	{
		// Validate if user inputs are integer
		public static bool ValidateIntInput(string? str, out int numb)
		{
			numb = 0;

			if (!string.IsNullOrEmpty(str) && int.TryParse(str, out int result))
				numb = result;
			else
			{
				// Failed validation
				Console.WriteLine("Incorect input.");
				return false;
			}

			return true;
		}

		// Validate if user inputs are alphanumeric
		public static bool ValidateAlphaNumericInput(string? str, int gridSize, out int[] coordinates)
		{
			coordinates = [];

			// Input should be 2 characters long
			if (!string.IsNullOrEmpty(str) && str.Length == 2)
			{
				List<int> tempCoordinates = [];

				// First character shoul be alphabet
				try
				{
					int row = char.ToUpper(str[0]) - 'A';
					// Should not be greater than the grid size
					if (row > gridSize)
					{
						// Failed validation
						Console.WriteLine("Incorect input.");
						return false;
					}

					tempCoordinates.Add(row);
				}
				catch (Exception)
				{
					// Failed validation
					Console.WriteLine("Incorect input.");
					return false;
				}

				// First character shoul be integer
				if (int.TryParse(str[1].ToString(), out int column))
				{
					// Should not be greater than the grid size
					if (column > gridSize)
					{
						// Failed validation
						Console.WriteLine("Incorect input.");
						return false;
					}

					tempCoordinates.Add(column - 1);
				}
				else
				{
					// Failed validation
					Console.WriteLine("Incorect input.");
					return false;
				}

				coordinates = [.. tempCoordinates];
				return true;
			}
			else
			{
				// Failed validation
				Console.WriteLine("Incorect input.");
				return false;
			}
		}

		public static bool ValidateGridSize(int gridSize)
		{
			// Minimum grid size
			if (gridSize < 2)
			{
				// Failed validation
				Console.WriteLine("Minimum size of grid is 2.");
				return false;
			}
			else if (gridSize > 10) // Maximum grid size
			{
				// Failed validation
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
				// Failed validation
				Console.WriteLine("Maximum number is 35% of total sqaures.");
				return false;
			}

			return true;
		}
	}
}
