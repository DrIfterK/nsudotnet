using System;

namespace TicTacToe
{
    public class BigFieldController
    {
        private int _count = 0;
        private int _lastFieldX = -1, _lastFieldY = -1;
        private char[] _symbols;
        protected BigField field;

        public delegate void UpdateMethod(BigField field);
        public event UpdateMethod Update;
        public void OnUpdate()
        {
            Update(field);
        }
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
        public BigFieldController(BigField field)
        {
            _symbols = new char[2] { Field.Tic, Field.Tac };
            this.field = field;
        }

        public void NewGame()
        {
            _count = 0;
            _lastFieldY = -1;
            _lastFieldX = -1;
            field = new BigField();
            OnUpdate();
        }
        public virtual int GetLastField()
        {
            if (_lastFieldY == -1 || _lastFieldX == -1) return -1;
            else return _lastFieldX * BigField.Width + _lastFieldY;
        }
        public virtual char GetSymbol()
        {
            return field.GetSymbol();
        }
        public virtual void SetSymbol(int bigFieldX, int bigFieldY, int smallFieldX, int smallFieldY)
        {
            if ((bigFieldX == _lastFieldY && bigFieldY == _lastFieldX) || (_lastFieldY == -1 && _lastFieldX == -1))
            {
                
                SmallField current = field.GetField(bigFieldX, bigFieldY);
                current.SetSymbol(smallFieldX, smallFieldY, _symbols[_count % _symbols.Length]);
                IsSmallGameOver(current);
                IsGameOver();
                _lastFieldY = smallFieldX;
                _lastFieldX = smallFieldY;
                if (field.GetField(_lastFieldY, _lastFieldX).GetSymbol() != Field.Nol)
                {
                    _lastFieldY = -1;
                    _lastFieldX = -1;
                }
                _count++;
                OnUpdate();
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
                    currentField[i, j] = field.GetSymbol(j, i);
                    if (currentField[i, j] == Field.Nol) hasNol = true;
                }
            }
            char symbol = GetWinSymbol(currentField);
            field.SetSymbol(symbol);
            if (symbol == Field.Nol && hasNol == false) field.SetSymbol(Field.Nor);
            return symbol;
        }
    }
}

