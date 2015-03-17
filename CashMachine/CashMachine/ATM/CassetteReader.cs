using System.Collections.Generic;
using System.IO;

namespace CashMachine.ATM
{
    class CassetteReader
    {

        private readonly List<Cassete> _cassettes;

        private static StreamReader _reader;

        public List<Cassete> GetCassetes(string pathToCassettes)
        {
            const char delimiter = ':';
            _reader = new StreamReader(pathToCassettes);
            
            string bufferLine;
            while ((bufferLine = _reader.ReadLine()) != null)
            {
                bufferLine = bufferLine.Replace(" ", "");
                string[] valueAmount = bufferLine.Split(delimiter);
                _cassettes.Add(new Cassete(int.Parse(valueAmount[0]), int.Parse(valueAmount[1])));
            }
            _reader.Close();

            _cassettes.Sort();
            _cassettes.Reverse();
            return _cassettes;
        }

        public CassetteReader()
        {
            _cassettes = new List<Cassete>();
        }
    }
}
