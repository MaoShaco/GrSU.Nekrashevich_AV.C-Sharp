using System;
using System.Collections.Generic;
using System.IO;

namespace StartDispencer
{
    class CassetteReader
    {

        private readonly List<Cassete> _cassettes;

        private static StreamReader _reader;

        public List<Cassete> GetCassetes()
        {
                string bufferLine;
                const char delimiter = ':';
                while ((bufferLine = _reader.ReadLine()) != null)
                {
                    bufferLine = bufferLine.Replace(" ", "");
                    string[] valueAmount = bufferLine.Split(delimiter);
                    _cassettes.Add(new Cassete(int.Parse(valueAmount[0]), int.Parse(valueAmount[1])));
                }
                _reader.Close();

            return _cassettes;
        }

        public CassetteReader(string path)
        {
            _cassettes = new List<Cassete>();
            try
            {
                _reader = new StreamReader(path);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("There aren't any _cassettes");
                Environment.Exit(0);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Wrong Casset inserted");
                Environment.Exit(0);
            }
        }
    }
}
