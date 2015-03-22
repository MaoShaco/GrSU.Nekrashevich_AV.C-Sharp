using System;
using System.Collections.Generic;
using System.IO;

namespace CashMachine.ATM
{
    class CassetteReader
    {

        private readonly List<Cassete> _cassettes = new List<Cassete>();

        private static StreamReader _reader;

        public bool CheckCassetes(string pathToCassettes)
        {
            return File.Exists(pathToCassettes);
        }

        public bool CheckCassetteFormat(string pathToCassettes)
        {
            _cassettes.Clear();
            const char delimiter = ':';
            _reader = new StreamReader(pathToCassettes);

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
                _cassettes.Clear();
                return false;
            }
            _reader.Close();

            _cassettes.Sort();
            return true;
        }

        public bool TryGetCassetes(string pathToCassettes, out List<Cassete> cassetes)
        {
            bool formatCheck = CheckCassetteFormat(pathToCassettes);
            cassetes = _cassettes;
            return formatCheck;

        }
    }
}
