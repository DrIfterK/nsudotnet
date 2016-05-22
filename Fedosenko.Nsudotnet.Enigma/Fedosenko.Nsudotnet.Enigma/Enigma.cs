using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fedosenko.Nsudotnet.Enigma
{
    class Enigma
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            if(args.Length != 4 && args.Length != 5)
            {
                Console.WriteLine("Not enough arguments");
                return;
            }
            try
            {
                String whatToDo = args[0];
                Worker worker;
                switch (whatToDo)
                {
                    case Worker.TaskEncrypt:
                        worker = new Encoder(args[2], args[1], args[3]);
                        break;
                    case Worker.TaskDecrypt:
                        worker = new Decoder(args[2], args[1], args[3], args[4]);
                        break;
                    default:
                        throw new Worker.NoSuchTaskException();
                }
                worker.DoWork();
            }
            catch (Worker.NoSuchTaskException e)
            {

            }
        }
    }
}
