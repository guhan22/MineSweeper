namespace MineSweeper.Models
{
	// Class defenition of a grid
	public class Grid
	{
		public bool IsMine { get; set; }
		public bool IsRevealed { get; set; }
		public int AdjacentCount { get; set; }

		public Grid()
		{
			IsMine = false;
			IsRevealed = false;
			AdjacentCount = 0;
		}
	}
}
