using System;

namespace TicTacToe
{
	public class BigField : MapField
	{
		public const int Width = 3, Height = 3;
		private SmallField[,] _field;
        private int _lastFieldX = -1, _lastFieldY = -1;
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
        public virtual int GetLastField()
        {
            if (_lastFieldY == -1 || _lastFieldX == -1 || symbol != Nol) return -1;
            else return _lastFieldY * Width + _lastFieldX;
        }
        public void SetSymbol(int x, int y, char symbol)
        {
            _field[y, x].SetSymbol(symbol);
        }
        public void SetSymbol(int bx, int by, int x, int y, char symbol)
        {
            _lastFieldX = x;
            _lastFieldY = y;
            _field[by, bx].SetSymbol(x, y, symbol);
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

