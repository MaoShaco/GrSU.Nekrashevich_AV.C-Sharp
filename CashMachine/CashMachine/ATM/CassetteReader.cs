using System;
using System.Collections.Generic;
using System.IO;

using log4net;
namespace CashMachine.ATM
{
    class CassetteReader
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CashMachineUserInterface));
        private readonly List<Cassete> _cassettes = new List<Cassete>();

        private static StreamReader _reader;

        public bool CheckCassetes(FileInfo fileWithCassetes)
        {
            return fileWithCassetes.Exists;
        }

        public bool LoadCassettes(FileInfo fileWithCassetes)
        {
            _cassettes.Clear();
            const char delimiter = ':';
            if (CheckCassetes(fileWithCassetes))
                _reader = new StreamReader(fileWithCassetes.OpenRead());
            else
            {
                Logger.Info(string.Format("There is no file in the following direction {0}", fileWithCassetes.FullName));
                return false;
            }
            try
            {
                string bufferLine;
                while ((bufferLine = _reader.ReadLine()) != null)
                {
                    bufferLine = bufferLine.Replace(" ", "");
                    var valueAmount = bufferLine.Split(delimiter);
                    _cassettes.Add(new Cassete(int.Parse(valueAmount[0]), int.Parse(valueAmount[1])));
                }
            }
            catch (FormatException)
            {
                Logger.Info(string.Format("The format of inputed cassetes is wrong in [{0}]", fileWithCassetes.FullName));
                _cassettes.Clear();
                return false;
            }
            _reader.Close();

            _cassettes.Sort();
            return true;
        }

        public bool TryGetCassetes(FileInfo fileWithCassetes, out List<Cassete> cassetes)
        {
            bool formatCheck = LoadCassettes(fileWithCassetes);
            cassetes = _cassettes;
            return formatCheck;

        }
    }
}
