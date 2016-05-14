using System;
using System.Text;
using System.Threading;

namespace TicTacToe
{
	public class ConsoleView : View
	{
		Thread thread;
        private static string SET = "set", EXIT = "exit", NEW_GAME = "new", HELP = "help", PRINT = "print";
        private BigFieldController controller;
        public ConsoleView (BigFieldController controller)
		{
            this.controller = controller;
			controller.SetView (this);
			thread = new Thread (this.Read);
			thread.Start ();
            Console.WriteLine("Game started. First is X!");
        }
        private void Read(){

            while (true)
            {
                string line = Console.ReadLine();
                string[] commands = line.Split(' ');
                if (commands[0].Equals(SET))
                {
                    int lastField = controller.GetLastField();
                    int bx = 0, by = 0, sx = 0, sy = 0;
                    if (lastField == -1 && commands.Length == 5)
                    {
                        bx = Int32.Parse(commands[1]);
                        by = Int32.Parse(commands[2]);
                        sx = Int32.Parse(commands[3]);
                        sy = Int32.Parse(commands[4]);
                    }
                    else if (lastField != -1 && commands.Length == 3)
                    {
                        bx = lastField % 3;
                        by = lastField / 3;
                        sx = Int32.Parse(commands[1]);
                        sy = Int32.Parse(commands[2]);
                    }
                    else
                    {
                        if (lastField == -1)
                            Console.WriteLine("Wrong args. Type \"set bx by sx sy\".");
                        else
                            Console.WriteLine("Wrong args. Type only \"set sx sy\" cause you locked on field " + bx + " " + by + ".");
                        continue;
                    }
                    try
                    {

                        controller.SetSymbol(bx, by, sx, sy);
                        char win;
                        if ((win = controller.GetSymbol()) != Field.NOL)
                        {
                            Console.WriteLine("Game is over! Winner is " + win + "!");
                            Console.WriteLine("If you want to start a new game type \"new\".");
                            break;
                        }
                    }
                    catch (Field.ChangingNotNOLSynbolException e)
                    {
                        Console.WriteLine("Wrong Field selected " + bx + " " + by + " : " + sx + " " + sy);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                    }
                }
                else if (commands[0].Equals(HELP)) Help();
                else if (commands[0].Equals(NEW_GAME)) controller.NewGame();
                else if (commands[0].Equals(EXIT)) break;
                else Console.WriteLine("Wrong Command!");
            }
        }
        public void Help()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("    help - this text;");
            Console.WriteLine("    set - set symbol on field in pos bx by sx sy (0 <= for all < 3 and the same);");
            Console.WriteLine("    exit - close game;");
            Console.WriteLine("    new - start new game.");
        }
        public void Update(BigField field){
			char bigSymbol = field.GetSymbol();
			if (bigSymbol != Field.NOL) {
				Console.WriteLine ("Game is over. Winner is " + bigSymbol + ".");
				return;
			}
			StringBuilder[] lines = new StringBuilder[BigField.HEIGHT * SmallField.HEIGHT + 3];
            for(int i = 0; i < lines.Length; i++)
            {
                lines[i] = new StringBuilder();
            }
            int last = controller.GetLastField();
			for (int i = 0; i < BigField.HEIGHT; i++) {
				for (int j = 0; j < BigField.WIDTH; j++) {
					SmallField smallField = field.GetField (i, j);
					for (int ii = 0; ii < SmallField.HEIGHT; ii++) {
						for (int jj = 0; jj < SmallField.WIDTH; jj++) {
							CharField charField = smallField.GetField (ii, jj);
							lines [i * BigField.HEIGHT + ii].Append (charField.GetSymbol());
						}
                        if(i == last/BigField.HEIGHT && j == last/BigField.WIDTH && last != -1)
                            lines[i * BigField.HEIGHT + ii].Append('|');
                        else if(smallField.GetSymbol() != Field.NOL)
                            lines[i * BigField.HEIGHT + ii].Append('#');
                        else
                            lines[i * BigField.HEIGHT + ii].Append(' ');         
                    }
                }
			}
            for (int i = 0; i < BigField.HEIGHT; i++)
            {
                for (int j = 0; j < SmallField.HEIGHT; j++)
                {
                    Console.WriteLine(lines[i*BigField.HEIGHT + j].ToString());
                }
                Console.WriteLine();
            }
		}
	}
}

