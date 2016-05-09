using System;

namespace TicTacToe
{
	public abstract class Field
	{
//		private const int width = 3, height = 3;
//		Field[][] field;
		public static char NOL = '0', TIC = 'X', TAC = 'O';
		protected char symbol;
		public void setSymbol (char symbol){
			this.symbol = symbol;
		}
		public char getSymbol (){
			return symbol;
		}
		virtual public char isGameOver ();
	}
}

