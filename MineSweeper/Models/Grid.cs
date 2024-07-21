namespace MineSweeper.Models
{
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
