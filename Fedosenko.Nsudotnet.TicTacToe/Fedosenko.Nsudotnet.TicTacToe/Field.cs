using System;

namespace TicTacToe
{
	public abstract class Field
	{
        public class ChangingNotNolSymbolException : Exception
        {
            public ChangingNotNolSymbolException()
            {

            }
        }
        public const char Nol = '_', Tic = 'X', Tac = 'O', Nor = '-';
		protected char symbol;
		public void SetSymbol (char symbol){
            if (this.symbol == Nol)
                this.symbol = symbol;
            else throw new ChangingNotNolSymbolException();
		}
		public char GetSymbol (){
			return symbol;
		}
	}
}

