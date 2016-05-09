using System;

namespace TicTacToe
{
	public class SmallField : MapField
	{
		public static int WIDTH = 3, HEIGHT = 3;
		CharField[][] field;

		public SmallField ()
		{
			symbol = NOL;
			this.field = new CharField[HEIGHT][WIDTH];
			for (int i = 0; i < height; i++) {
				for (int j = 0; i < width; j++) {
					field [i] [j] = new CharField ();
				}
			}
		}

		public char setSymbol(int x, int y, char symbol){
			return field [y] [x].setSymbol (symbol);
		}
		public CharField getField(int x, int y){
			return field [y] [x];
		}
	}
}

