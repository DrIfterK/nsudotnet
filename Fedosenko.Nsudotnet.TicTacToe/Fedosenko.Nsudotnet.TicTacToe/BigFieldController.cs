using System;

namespace TicTacToe
{
	public class BigFieldController
	{
        private int count = 0;
		private int cy = -1, cx = -1;
		private View view;
        private char[] tic_tac;
        protected BigField field;
		private char GetWinSymbol(char[,] current_field){
			int width = BigField.WIDTH, height = BigField.HEIGHT;
			for (int i = 0; i < width * height; i++) {
				if (current_field [i / width, 0] == current_field [i / width, 1] &&
					current_field [i / width, 1] == current_field [i / width, 2] &&
					current_field [i / width, 2] != Field.NOL) {
					return current_field [i / width, i];
				}
				else if (current_field [0, i / height] == current_field [1, i / height] &&
					current_field [1, i / height] == current_field [2, i / height] &&
					current_field [2, i / height] != Field.NOL) {
					return current_field [i, i / height];
				}
			}
            if ((current_field[0, 0] == current_field[1, 1] &&
                current_field[1, 1] == current_field[2, 2]) ||
                (current_field[0, 2] == current_field[1, 1] &&
                    current_field[1, 1] == current_field[2, 0]) &&
                current_field[1, 1] != Field.NOL)
            {
                field.setSymbol(1, 1, current_field[1, 1]);
                return current_field[1, 1];
            }
            else return Field.NOL;
		}
		public BigFieldController (BigField field)
		{
            tic_tac = new char[2] { Field.TIC, Field.TAC };
            this.field = field;
		}

        public void NewGame()
        {
            count = 0;
            cx = -1;
            cy = -1;
            field = new BigField();
            view.update(field);
        }
		public void SetView(View view){
			this.view = view;
			view.update (field);
		}
        public virtual int GetLastField()
        {
            if (cx == -1 || cy == -1) return -1;
            else return cy*3 + cx;
        }
		public virtual char GetSymbol(){
			return field.getSymbol();
		}
        public virtual void SetSymbol(int bx, int by, int sx, int sy){
                if ((bx == cx && by == cy) || (cx == -1 && cy == -1))
                {
                    cx = sx;
                    cy = sy;
                    if(field.getField(cx, cy).getSymbol() != Field.NOL)
                    {
                        cx = -1;
                        cy = -1;
                    }
                    SmallField current = field.getField(bx, by);
                    current.setSymbol(sx, sy, tic_tac[count % tic_tac.Length]);
                    IsSmallGameOver(current);
                    IsGameOver();
                    count++;
                    view.update(field);
                }
                else
                {

                }
		}
		public char IsSmallGameOver(SmallField field){
			int width = SmallField.WIDTH, height = SmallField.HEIGHT;
            char[,] current_field = new char[height, width];
            bool hasNol = false;
            for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
                    current_field[i, j] = field.getSymbol(j, i);
                    if (current_field[i, j] == Field.NOL) hasNol = true;
				}
			}
			char symbol = GetWinSymbol (current_field);
			field.setSymbol (symbol);
            if (symbol == Field.NOL && hasNol == false) field.setSymbol(Field.NOR);
			return symbol;
		}
		public char IsGameOver(){
			int width = SmallField.WIDTH, height = SmallField.HEIGHT;
            char[,] current_field = new char[height, width];
            bool hasNol = false;
            for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					current_field [i,j] = field.getSymbol(j, i);
                    if (current_field[i, j] == Field.NOL) hasNol = true;
                }
            }
			char symbol = GetWinSymbol (current_field);
			field.setSymbol (symbol);
            if (symbol == Field.NOL && hasNol == false) field.setSymbol(Field.NOR);
            return symbol;
		}
	}
}

