using System;

namespace TicTacToe
{
	public class BigField : MapField
	{
		public static int WIDTH = 3, HEIGHT = 3;
		SmallField[,] field;

		public BigField()
		{
			symbol = NOL;
			this.field = new SmallField[HEIGHT, WIDTH];
			for (int i = 0; i < HEIGHT; i++) {
				for (int j = 0; j < WIDTH; j++) {
					field [i, j] = new SmallField();
				}
			}
		}
        public void SetSymbol(int x, int y, char symbol)
        {
            field[y, x].SetSymbol(symbol);
        }
		public SmallField GetField(int x, int y){
			return field [y, x];
		}
        public char GetSymbol(int x, int y)
        {
            return field[y, x].GetSymbol();
        }
    }
}

