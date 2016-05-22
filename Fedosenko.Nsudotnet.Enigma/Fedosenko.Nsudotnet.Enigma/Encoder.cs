using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Fedosenko.Nsudotnet.Enigma
{
    class Encoder : Worker
    {
        public const string HelpEncode = "For encode: enigma.exe encrypt fileIn algorithmName fileOut";
        public const int ArgsLength = 4;
        private SymmetricAlgorithm _symmetricAlgorithm;
        private String _algorithmName, _fileFromName, _fileToName;
        public Encoder(String[] args)
        {
            if (args.Length != ArgsLength) throw new WrongArgsException(WrongArgs + "\n" + HelpEncode);
            this._algorithmName = args[2];
            this._symmetricAlgorithm = parseAlgorithm(_algorithmName);
            this._fileFromName = args[1];
            this._fileToName = args[3];
        }

        public override void DoWork()
        {
            FileStream fileToStream, fileFromStream, fileKeyStream;
            CryptoStream cryptoStream;
            try
            {
                _symmetricAlgorithm.GenerateKey();
                _symmetricAlgorithm.GenerateIV();
                using (fileToStream = new FileStream(_fileToName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (fileFromStream = new FileStream(_fileFromName, FileMode.Open, FileAccess.Read))
                    {
                        using (cryptoStream = new CryptoStream(fileToStream, _symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            fileFromStream.CopyTo(cryptoStream);
                        }
                    }
                    int dotIndex = _fileFromName.LastIndexOf('.');
                    if (dotIndex == -1) dotIndex = _fileFromName.Length;
                    using (fileKeyStream = new FileStream(_fileFromName.Insert(dotIndex, ".key"), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(fileKeyStream))
                        {
                            binaryWriter.Write(_symmetricAlgorithm.Key.Length);
                            binaryWriter.Write(_symmetricAlgorithm.Key, 0, _symmetricAlgorithm.Key.Length);
                            binaryWriter.Write(_symmetricAlgorithm.IV.Length);
                            binaryWriter.Write(_symmetricAlgorithm.IV, 0, _symmetricAlgorithm.IV.Length);
                        }
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
