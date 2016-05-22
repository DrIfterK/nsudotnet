using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Fedosenko.Nsudotnet.Enigma
{
    abstract class Worker
    {
        public const string TaskDecrypt = "decrypt", TaskEncrypt = "encrypt";
        public const string AlgAES = "AES", AlgRC2 = "RC2", AlgDES = "DES", AlgRijndael = "RIJNDAEL";
        public abstract void DoWork();
        public class NoSuchAlgorithmException : Exception
        {

        };
        public class NoSuchTaskException : Exception
        {

        };
        public SymmetricAlgorithm parseAlgorithm(String algorithmName)
        {
            algorithmName = algorithmName.ToUpper();
            switch (algorithmName)
            {
                case AlgAES:
                    return new AesCryptoServiceProvider();
                case AlgRC2:
                    return new RC2CryptoServiceProvider();
                case AlgDES:
                    return new DESCryptoServiceProvider();
                case AlgRijndael:
                    return new RijndaelManaged();
                default: throw new NoSuchAlgorithmException();
            }
        }
    }
}
