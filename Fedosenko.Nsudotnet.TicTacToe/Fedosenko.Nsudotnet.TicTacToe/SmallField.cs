using System;

namespace TicTacToe
{
	public class SmallField : MapField
	{
		public static int WIDTH = 3, HEIGHT = 3;
		CharField[,] field;

		public SmallField ()
		{
			symbol = NOL;
			this.field = new CharField[HEIGHT, WIDTH];
			for (int i = 0; i < HEIGHT; i++) {
				for (int j = 0; j < WIDTH; j++) {
					field [i, j] = new CharField ();
				}
			}
		}

		public void SetSymbol(int x, int y, char symbol){
            if (this.symbol == NOL)
                field[y, x].SetSymbol(symbol);
            else throw new ChangingNotNOLSynbolException();
		}
        public char GetSymbol(int x, int y)
        {
            return field[y, x].GetSymbol();
        }
        public CharField GetField(int x, int y){
			return field [y, x];
		}
	}
}

