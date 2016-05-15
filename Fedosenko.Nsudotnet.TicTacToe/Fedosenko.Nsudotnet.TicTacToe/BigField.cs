using System;

namespace TicTacToe
{
	public class BigField : MapField
	{
		public const int Width = 3, Height = 3;
		private SmallField[,] _field;

		public BigField()
		{
			symbol = Nol;
			this._field = new SmallField[Height, Width];
			for (int i = 0; i < Height; i++) {
				for (int j = 0; j < Width; j++) {
					_field [i, j] = new SmallField();
				}
			}
		}
        public void SetSymbol(int x, int y, char symbol)
        {
            _field[y, x].SetSymbol(symbol);
        }
		public SmallField GetField(int x, int y){
			return _field [y, x];
		}
        public char GetSymbol(int x, int y)
        {
            return _field[y, x].GetSymbol();
        }
    }
}

