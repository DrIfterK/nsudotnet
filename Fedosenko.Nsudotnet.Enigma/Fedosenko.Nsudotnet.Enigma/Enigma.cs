using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fedosenko.Nsudotnet.Enigma
{
    class Enigma
    {
        public const string CmdHelp = "-h", CmdAltHelp = "--help";
        public const string MsgHelp = Decoder.HelpDecode + "\n" + Encoder.HelpEncode;

        static void Main(string[] args)
        {
            if(args[0].Equals(CmdHelp) || args[0].Equals(CmdAltHelp))
            {
                Console.WriteLine(MsgHelp);
                return;
            }
            try
            {
                String whatToDo = args[0];
                Worker worker;
                switch (whatToDo.ToLower())
                {
                    case Worker.TaskEncrypt:
                        worker = new Encoder(args);
                        break;
                    case Worker.TaskDecrypt:
                        worker = new Decoder(args);
                        break;
                    default:
                        throw new Worker.WrongArgsException("Wrong Task: " + whatToDo);
                }
                worker.DoWork();
            }
            catch (Worker.WrongArgsException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
