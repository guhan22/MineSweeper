using MineSweeper.Models;
using MineSweeper.Utils;

class Program
{
	public static void ProcessSetupInput(string msg, bool isValid, out int output)
	{
		output = 0;
		while (!isValid)
		{
			Console.WriteLine(msg);
			isValid = ValidationUtils.ValidateIntInput(Console.ReadLine(), out int size);
			output = size;
		}
	}

	public static void ProcessRevealInput(string msg, bool isValid, int gridSize, out int[] output)
	{
		output = [];
		while (!isValid)
		{
			Console.WriteLine(msg);
			isValid = ValidationUtils.ValidateAlphaNumericInput(Console.ReadLine(), gridSize, out int[] size);
			output = size;
		}
	}

	static void Main(string[] args)
	{
		while (true)
		{
			int gridSize = 0, mineCount = 0;
			bool validSize = false, validCount = false, gameOver = false;

			Console.WriteLine("Welcome to Minesweeper!");

			while (!validSize)
			{
				ProcessSetupInput("Enter the size of the grid (e.g. 4 for a 4x4 grid):", false, out int size);
				validSize = ValidationUtils.ValidateGridSize(size);
				gridSize = size;
			}

			Console.WriteLine();

			while (!validCount)
			{
				ProcessSetupInput("Enter the number of mines to place on the grid (maximum is 35% of the total squares):", false, out int count);
				validCount = ValidationUtils.ValidateMineCount(gridSize, count);
				mineCount = count;
			}

			Console.WriteLine();

			Board board = new(gridSize, mineCount);

			Console.WriteLine("Here is your minefield:");
			BoardUtils.PrintBoard(board);
			Console.WriteLine();

			while (!gameOver)
			{
				ProcessRevealInput("Select a square to reveal (e.g. A1):", false, gridSize, out int[] coordinates);

				if (BoardUtils.RevealGrid(board, coordinates[0], coordinates[1]))
				{
					Console.WriteLine("Here is your updated minefield:");
					BoardUtils.PrintBoard(board);

					if (board.revealedCount == (gridSize * gridSize) - mineCount)
					{
						Console.WriteLine("Congratulations, you have won the game!");
						gameOver = true;
					}
				}
				else
				{
					Console.WriteLine("Oh no, you detonated a mine! Game over.");
					gameOver = true;
				}

				Console.WriteLine();
			}

			Console.WriteLine("Press any key to play again...");

			Console.ReadKey();
			Console.WriteLine();
		}
	}
}