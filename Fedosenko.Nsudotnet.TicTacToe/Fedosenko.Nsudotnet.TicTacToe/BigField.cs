using System;

namespace TicTacToe
{
	public class BigField : MapField
	{
		public static int WIDTH = 3, HEIGHT = 3;
		SmallField[][] field;

		public BigField()
		{
			symbol = NOL;
			this.field = new SmallField[HEIGHT][WIDTH];
			for (int i = 0; i < height; i++) {
				for (int j = 0; i < width; j++) {
					field [i] [j] = new CharField ();
				}
			}
		}
		public SmallField getField(int x, int y){
			return field [y] [x];
		}
	}
}

