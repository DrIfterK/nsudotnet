using System;

namespace TicTacToe
{
	public abstract class Field
	{
//		private const int width = 3, height = 3;
//		Field[][] field;
        public class ChangingNotNOLSynbolException : Exception
        {
            public ChangingNotNOLSynbolException()
            {

            }
        }
        public static char NOL = '0', TIC = 'X', TAC = 'O', NOR = '-';
		protected char symbol;
		public void setSymbol (char symbol){
            if (this.symbol == NOL)
                this.symbol = symbol;
            else throw new ChangingNotNOLSynbolException();
		}
		public char getSymbol (){
			return symbol;
		}
	}
}

