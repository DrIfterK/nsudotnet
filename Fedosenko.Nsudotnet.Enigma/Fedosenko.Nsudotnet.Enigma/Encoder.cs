﻿using System;
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
            this._symmetricAlgorithm = ParseAlgorithm(_algorithmName);
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
                    
                    FileInfo currentFile = new FileInfo(_fileFromName);
                    using (fileKeyStream = new FileStream(Path.GetFileNameWithoutExtension(currentFile.FullName) + ".key" + currentFile.Extension, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileKeyStream))
                        {
                            streamWriter.WriteLine(Convert.ToBase64String(_symmetricAlgorithm.Key));
                            streamWriter.WriteLine(Convert.ToBase64String(_symmetricAlgorithm.IV));
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
