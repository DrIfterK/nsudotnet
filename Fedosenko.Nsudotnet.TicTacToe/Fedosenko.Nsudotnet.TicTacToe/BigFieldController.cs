using System;

namespace TicTacToe
{
	public class BigFieldController
	{
		private int cy = -1, cx = -1;
		private View view;
		protected BigField field;
		private char getWinSymbol(char[][] current_field){
			int width = BigField.WIDTH, height = BigField.HEIGHT;
			for (int i = 0; i < width * height; i++) {
				if (current_field [i / width] [0] == current_field [i / width] [1] &&
					current_field [i / width] [1] == current_field [i / width] [2] &&
					current_field [i / width] [2] != Field.NOL) {
					return current_field [i / width] [i];
				}
				else if (current_field [0] [i / height] == current_field [1] [i / height] &&
					current_field [1] [i / height] == current_field [2] [i / height] &&
					current_field [2] [i / height] != Field.NOL) {
					return current_field [i] [i / height];
				}
			}
			if ((current_field [0] [0] == current_field [1] [1] &&
				current_field [1] [1] == current_field [2] [2]) ||
				(current_field [0] [2] == current_field [1] [1] &&
					current_field [1] [1] == current_field [2] [0]) &&
				current_field [1] [1] != Field.NOL) {
				field.setSymbol(1, 1, current_field[1][1]);
				return current_field [1] [1];
			}
		}
		public BigFieldController (
			field)
		{
			this.field = field;
		}
		public void setView(View view){
			this.view = view;
			view.update (field);
		}
		public virtual char getSymbol(int x, int y){
			return field.getSymbol(x, y);
		}
		public virtual SmallField getField(int x, int y);	
		public virtual void setSymbol(int bx, int by, int sx, int sy, char symbol){
			SmallField current = field.getField(bx, by);
			current.setSymbol (sx, sy, symbol);
			isSmallGameOver (current);
			symbol = isGameOver ();
		}
		public char isSmallGameOver(SmallField field){
			char[][] current_field = new char[height][width];
			int width = SmallField.WIDTH, height = SmallField.HEIGHT;
			for (int i = 0; i < height; i++) {
				for (int j = 0; i < width; j++) {
					current_field [i] [j] = field [i] [j].getSymbol();
				}
			}
			char symbol = getWinSymbol (current_field);
			field.setSymbol (symbol);
			return symbol;
		}
		public char isGameOver(){
			char[][] current_field = new char[height][width];
			int width = SmallField.WIDTH, height = SmallField.HEIGHT;
			for (int i = 0; i < height; i++) {
				for (int j = 0; i < width; j++) {
					current_field [i] [j] = field [i] [j].getSymbol();
				}
			}
			char symbol = getWinSymbol (current_field);
			field.setSymbol (current_field);
			return symbol;
		}
	}
}

