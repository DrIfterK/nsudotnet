using System;

namespace TicTacToe
{
	public class TicTacToe
	{
		public static void Main(string[] args){
            BigField field = new BigField();
            BigFieldController controller = new BigFieldController(field);
            ConsoleView view = new ConsoleView(controller);
            controller.NewGame();
		}
	}
}

