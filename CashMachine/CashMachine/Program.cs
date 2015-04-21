using System.Collections.Generic;
using System.IO;
using CashMachine.ATM;

namespace CashMachine
{
    class Program
    {
        private static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();    
            const string path = @"Cassete.txt";
            var fileWithCassettes = new FileInfo(path);
            var atm = new CashMachineUserInterface(EnumLanguages.English);
            var casseteReader = new CassetteReader();
            List<Cassete> cassetes;
            

            if (casseteReader.TryGetCassetes(fileWithCassettes, out cassetes))
                atm.InputCassetes(cassetes);

            atm.Run();
        }
    }
}
