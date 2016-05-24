using System;

namespace TicTacToe
{
	public class TicTacToe
	{
		public static void Main(string[] args){
            BigField field = new BigField();
            ConsoleView view = new ConsoleView(field);
            BigFieldController controller = new BigFieldController(view, field);

            controller.NewGame();
		}
	}
}

