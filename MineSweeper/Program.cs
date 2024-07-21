using MineSweeper.Models;
using MineSweeper.Utils;

class Program
{
	// Read and validate user inputs during board creation and mines plotting
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

	// Read and validate user inputs during game play
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
		// Start over after game completion
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

			// Create the board based on user inputs
			Board board = new(gridSize, mineCount);

			Console.WriteLine("Here is your minefield:");
			BoardUtils.PrintBoard(board);
			Console.WriteLine();

			// Game play
			while (!gameOver)
			{
				ProcessRevealInput("Select a square to reveal (e.g. A1):", false, gridSize, out int[] coordinates);

				// Continue till mine detected (or) reveal all grids without mines
				if (BoardUtils.RevealGrid(board, coordinates[0], coordinates[1]))
				{
					Console.WriteLine("Here is your updated minefield:");
					BoardUtils.PrintBoard(board);

					if (board.revealedCount == (gridSize * gridSize) - mineCount) // Game won
					{
						Console.WriteLine("Congratulations, you have won the game!");
						gameOver = true;
					}
				}
				else // Game lost
				{
					Console.WriteLine("Oh no, you detonated a mine! Game over.");
					gameOver = true;
				}

				Console.WriteLine();
			}

			// Restart the game, press Ctrl + d to quit if user don't want to start over
			Console.WriteLine("Press any key to play again...");

			Console.ReadKey();
			Console.WriteLine();
		}
	}
}