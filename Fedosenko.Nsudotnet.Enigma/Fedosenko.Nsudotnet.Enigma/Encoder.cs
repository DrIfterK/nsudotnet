using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Fedosenko.Nsudotnet.Enigma
{
    class Encoder : Worker
    {
        String _algorithmName, _fileFromName, _fileToName;
        public Encoder(String algorithmName, String fileFromName, String fileToName)
        {
            this._algorithmName = algorithmName;
            this._fileFromName = fileFromName;
            this._fileToName = fileToName;
        }

        public override void DoWork()
        {
            FileStream fileToStream, fileFromStream, fileKeyStream;
            CryptoStream cryptoStream;
            try
            {
                SymmetricAlgorithm symmetricAlgorithm = parseAlgorithm(_algorithmName);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes("MYKEYHEH");
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes("MYKEYHEH");
                using (fileToStream = new FileStream(_fileToName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (fileFromStream = new FileStream(_fileFromName, FileMode.Open, FileAccess.Read))
                    {
                        using (cryptoStream = new CryptoStream(fileToStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            fileFromStream.CopyTo(cryptoStream);
                        }
                    }
                    using (fileKeyStream = new FileStream(_fileFromName.Insert(_fileFromName.LastIndexOf('.'), ".key"), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fileKeyStream.Write(symmetricAlgorithm.Key, 0, symmetricAlgorithm.Key.Length);
                    }

                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
