using System;
using System.Text;

namespace TicTacToe
{
	public class ConsoleView : View
	{
		Thread thread;
		private char[] ticTac;
		public ConsoleView (BigFieldController controller)
		{
			controller.setView (this);
			thread = new Thread (this.read);
			ticTac = new {BigFieldController.Tic, BigFieldController.Tac};
			thread.Start ();
		}
		private void read(){
		
		}
		public void update(BigField field){
			char bigSymbol = field.getSymbol();
			if (bigSymbol != Field.NOL) {
				Console.WriteLine ("Game is over. Winner is " + bigSymbol + ".");
				return;
			}
			StringBuilder[] lines = new StringBuilder[BigField.HEIGHT * SmallField.HEIGHT];
			for (int i = 0; i < BigField.HEIGHT; i++) {
				for (int j = 0; j < BigField.WIDTH; j++) {
					SmallField smallField = field.getField (i, j);
					for (int ii = 0; ii < SmallField.HEIGHT; ii++) {
						for (int jj = 0; jj < SmallField.WIDTH; jj++) {
							CharField charField = smallField.getField (ii, jj);
							lines [i * BigField.HEIGHT + ii].append (charField.getSymbol);
						}
					}
				}
			}
			foreach(StringBuilder sb in lines){
				Console.WriteLine (sb.ToString ());
			}
		}
	}
}

