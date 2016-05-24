using System;

namespace TicTacToe
{
	public abstract class View
	{
        public delegate void SetSymbolMethod(int bigFieldX, int bigFieldY, int smallFieldX, int smallFieldY);
        public abstract event SetSymbolMethod SetSymbol;
        public delegate void NewGameMethod();
        public abstract event NewGameMethod NewGame;
        public abstract void Update(BigField field);
	}
}

