using System;
using System.Threading.Tasks;

namespace SpareTime
{
	public class Tile
	{
		public enum DirectionFlag
		{
			Up,
			Down,
			Left,
			Right,
			None
		}
		public string Value { get; set; }
		public int Row{ get; set; }
		public int Column { get; set; }
		public DirectionFlag Direction { get; set; }

		public Tile()
		{
			
		}

		public async Task Move(DirectionFlag direction)
		{
			
		}
	}
}
