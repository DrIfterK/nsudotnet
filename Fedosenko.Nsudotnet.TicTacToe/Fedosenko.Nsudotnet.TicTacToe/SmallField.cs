using System;

namespace TicTacToe
{
	public class SmallField : MapField
	{
		public const int Width = 3, Height = 3;
		private CharField[,] _field;

		public SmallField ()
		{
			symbol = Nol;
			this._field = new CharField[Height, Width];
			for (int i = 0; i < Height; i++) {
				for (int j = 0; j < Width; j++) {
					_field [i, j] = new CharField ();
				}
			}
		}

		public void SetSymbol(int x, int y, char symbol){
            if (this.symbol == Nol)
                _field[y, x].SetSymbol(symbol);
            else throw new ChangingNotNolSymbolException();
		}
        public char GetSymbol(int x, int y)
        {
            return _field[y, x].GetSymbol();
        }
        public CharField GetField(int x, int y){
			return _field [y, x];
		}
	}
}

