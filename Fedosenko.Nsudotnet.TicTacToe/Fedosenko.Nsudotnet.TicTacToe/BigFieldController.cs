using System;

namespace TicTacToe
{
    public class BigFieldController
    {
        private int _count = 0;
        private char[] _symbols;
        protected BigField _field;
        private View _view;

        private char GetWinSymbol(char[,] currentField)
        {
            int width = BigField.Width, height = BigField.Height;
            for (int i = 0; i < width * height; i++)
            {
                if (currentField[i / width, 0] == currentField[i / width, 1] &&
                    currentField[i / width, 1] == currentField[i / width, 2] &&
                    currentField[i / width, 2] != Field.Nol)
                {
                    return currentField[i / width, 1];
                }
                if (currentField[0, i / height] == currentField[1, i / height] &&
                    currentField[1, i / height] == currentField[2, i / height] &&
                    currentField[2, i / height] != Field.Nol)
                {
                    return currentField[1, i / height];
                }
            }
            if (((currentField[0, 0] == currentField[1, 1] &&
                currentField[1, 1] == currentField[2, 2]) ||
                (currentField[0, 2] == currentField[1, 1] &&
                    currentField[1, 1] == currentField[2, 0])) &&
                currentField[1, 1] != Field.Nol)
            {
                return currentField[1, 1];
            }
            else return Field.Nol;
        }
        public BigFieldController(View view, BigField field)
        {
            this._view = view;
            this._view.SetSymbol += this.SetSymbol;
            this._view.NewGame += this.NewGame;
            _symbols = new char[2] { Field.Tic, Field.Tac };
            this._field = field;
        }

        public void NewGame()
        {
            _count = 0;
            _field = new BigField();
            _view.Update(_field);
        }
        
        public virtual char GetSymbol()
        {
            return _field.GetSymbol();
        }
        public virtual void SetSymbol(int bigFieldX, int bigFieldY, int smallFieldX, int smallFieldY)
        {
            int lastField = _field.GetLastField();
            int lastFieldX = lastField % BigField.Width;
            int lastFieldY = lastField / BigField.Height;
            if ((bigFieldX == lastFieldX && bigFieldY == lastFieldY) || (lastField == -1))
            {
                
                SmallField current = _field.GetField(bigFieldX, bigFieldY);
                _field.SetSymbol(bigFieldX, bigFieldY, smallFieldX, smallFieldY, _symbols[_count % _symbols.Length]);
                IsSmallGameOver(current);
                IsGameOver();
                _count++;
                _view.Update(_field);
            }
            else
            {
                Console.WriteLine("WHAAAAT!");
            }
        }
        public char IsSmallGameOver(SmallField field)
        {
            int width = SmallField.Width, height = SmallField.Height;
            char[,] currentField = new char[height, width];
            bool hasNol = false;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    currentField[i, j] = field.GetSymbol(j, i);
                    if (currentField[i, j] == Field.Nol) hasNol = true;
                }
            }
            char symbol = GetWinSymbol(currentField);
            field.SetSymbol(symbol);
            if (symbol == Field.Nol && hasNol == false) field.SetSymbol(Field.Nor);
            return symbol;
        }
        public char IsGameOver()
        {
            int width = SmallField.Width, height = SmallField.Height;
            char[,] currentField = new char[height, width];
            bool hasNol = false;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    currentField[i, j] = _field.GetSymbol(j, i);
                    if (currentField[i, j] == Field.Nol) hasNol = true;
                }
            }
            char symbol = GetWinSymbol(currentField);
            _field.SetSymbol(symbol);
            if (symbol == Field.Nol && hasNol == false) _field.SetSymbol(Field.Nor);
            return symbol;
        }
    }
}

