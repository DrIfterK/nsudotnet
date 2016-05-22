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
        public const string WrongArgs = "Wrong arguments!";
        public const string TaskDecrypt = "decrypt", TaskEncrypt = "encrypt";
        public const string AlgAES = "AES", AlgRC2 = "RC2", AlgDES = "DES", AlgRijndael = "RIJNDAEL";
        public const int KeyLength = 8;

        public abstract void DoWork();
        public class NoSuchAlgorithmException : Exception
        {
            public NoSuchAlgorithmException() { }
            public NoSuchAlgorithmException(string message) : base(message) { }
        };
        public class WrongArgsException : Exception
        {
            public WrongArgsException() { }
            public WrongArgsException(string message) : base(message) { }
        };
        
        public SymmetricAlgorithm parseAlgorithm(String algorithmName)
        {
            switch (algorithmName.ToUpper())
            {
                case AlgAES:
                    return new AesCryptoServiceProvider();
                case AlgRC2:
                    return new RC2CryptoServiceProvider();
                case AlgDES:
                    return new DESCryptoServiceProvider();
                case AlgRijndael:
                    return new RijndaelManaged();
                default:
                    throw new WrongArgsException("NoSuchAlgorithm: " + algorithmName);
            }
        }
    }
}
