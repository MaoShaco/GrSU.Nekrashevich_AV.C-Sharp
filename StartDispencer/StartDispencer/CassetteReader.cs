using System;
using System.Collections.Generic;
using System.IO;

namespace StartDispencer
{
    class CassetteReader
    {

        public List<Cassete> Cassettes { get; private set; }

        public CassetteReader(string path)
        {
            const char delimiter = ':';
            Cassettes = new List<Cassete>();
            try
            {
                var reader = new StreamReader(path);

                string bufferLine;
                while ((bufferLine = reader.ReadLine()) != null)
                {
                    bufferLine = bufferLine.Replace(" ", "");
                    string[] valueAmount = bufferLine.Split(delimiter);
                    Cassettes.Add(new Cassete(int.Parse(valueAmount[0]), int.Parse(valueAmount[1])));
                }
                reader.Close();

            }
            catch (ArgumentException)
            {
                Console.WriteLine("There aren't any Cassettes");
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
