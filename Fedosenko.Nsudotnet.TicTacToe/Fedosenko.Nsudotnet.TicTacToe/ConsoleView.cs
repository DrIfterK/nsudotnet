using System;
using System.Text;
using System.Threading;

namespace TicTacToe
{
	public class ConsoleView : View
	{
		private Thread _thread;
        private const string CmdSet = "set", CmdExit = "exit", CmdNewGame = "new", CmdHelp = "help";
        private BigFieldController _controller;
        public ConsoleView (BigFieldController controller)
		{
            this._controller = controller;
            controller.Update += this.Update;
			_thread = new Thread (this.Read);
			_thread.Start ();
            Console.WriteLine("Game started. First is X!");
        }
        private void Read(){

            while (true)
            {
                string line = Console.ReadLine();
                string[] commands = line.Split(' ');
                if (commands[0].Equals(CmdSet))
                {
                    int lastField = _controller.GetLastField();
                    int bigFieldX = 0, bigFieldY = 0, smallFieldX = 0, smallFieldY = 0;
                    try {
                        if (lastField == -1 && commands.Length == 5)
                        {
                            bigFieldX = Int32.Parse(commands[1]);
                            bigFieldY = Int32.Parse(commands[2]);
                            smallFieldX = Int32.Parse(commands[3]);
                            smallFieldY = Int32.Parse(commands[4]);
                        }
                        else if (lastField != -1 && commands.Length == 3)
                        {
                            bigFieldX = lastField % BigField.Width;
                            bigFieldY = lastField / BigField.Width;
                            smallFieldX = Int32.Parse(commands[1]);
                            smallFieldY = Int32.Parse(commands[2]);
                        }
                        else
                        {
                            if (lastField == -1)
                                Console.WriteLine("Wrong args. Type \"set bigFieldX bigFieldY smallFieldX smallFieldY\".");
                            else
                                Console.WriteLine("Wrong args. Type only \"set smallFieldX smallFieldY\" cause you locked on field " + lastField % BigField.Width + " " + lastField / BigField.Width + ".");
                            continue;
                        }
                        try
                        {

                            _controller.SetSymbol(bigFieldX, bigFieldY, smallFieldX, smallFieldY);
                            char winnerSymbol;
                            if ((winnerSymbol = _controller.GetSymbol()) != Field.Nol)
                            {
                                Console.WriteLine("Game is over! Winner is " + winnerSymbol + "!");
                                Console.WriteLine("If you want to start a new game type \"new\".");
                            }
                        }
                        catch (System.IndexOutOfRangeException e)
                        {
                            Console.WriteLine("Wrong Field selected " + bigFieldX + " " + bigFieldY + " : " + smallFieldX + " " + smallFieldY);
                        }
                        catch (Field.ChangingNotNolSymbolException e)
                        {
                            Console.WriteLine("Wrong Field selected " + bigFieldX + " " + bigFieldY + " : " + smallFieldX + " " + smallFieldY);
                        }
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                }
                else if (commands[0].Equals(CmdHelp)) Help();
                else if (commands[0].Equals(CmdNewGame)) _controller.NewGame();
                else if (commands[0].Equals(CmdExit)) break;
                else Console.WriteLine("Wrong Command!");
            }
        }
        public void Help()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("    help - this text;");
            Console.WriteLine("    set - set symbol on field in position bigFieldX bigFieldY smallFieldX smallFieldY (0 <= for all <= 2). If you locked on field just type \"set smallFieldX smallFieldY\";");
            Console.WriteLine("    exit - close the game;");
            Console.WriteLine("    new - start new game.");
        }
        public void Update(BigField field){
			StringBuilder[] lines = new StringBuilder[BigField.Height * SmallField.Height + 3];
            for(int i = 0; i < lines.Length; i++)
            {
                lines[i] = new StringBuilder();
            }
            int last = _controller.GetLastField();
			for (int i = 0; i < BigField.Height; i++) {
				for (int j = 0; j < BigField.Width; j++) {
					SmallField smallField = field.GetField (j, i);
					for (int ii = 0; ii < SmallField.Height; ii++) {
						for (int jj = 0; jj < SmallField.Width; jj++) {
							CharField charField = smallField.GetField (jj, ii);
							lines [i * BigField.Height + ii].Append (charField.GetSymbol());
						}
                        if(i == last/BigField.Width && j == last%BigField.Width && last != -1)
                            lines[i * BigField.Height + ii].Append('|');
                        else if(smallField.GetSymbol() != Field.Nol)
                            lines[i * BigField.Height + ii].Append('#');
                        else
                            lines[i * BigField.Height + ii].Append(' ');         
                    }
                }
			}
            for (int i = 0; i < BigField.Height; i++)
            {
                for (int j = 0; j < SmallField.Height; j++)
                {
                    Console.WriteLine(lines[i*BigField.Height + j].ToString());
                }
                Console.WriteLine();
            }
		}
	}
}

