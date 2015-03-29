using System.Collections.Generic;
using CashMachine.ATM;

namespace CashMachine
{
    class Program
    {
        private static void Main()
        {
            const string path = @"Cassete.txt";
            var atm = new CashMachineUserInterface();
            var casseteReader = new CassetteReader();
            List<Cassete> cassetes;

            if (casseteReader.TryGetCassetes(path, out cassetes))
                atm.InputCassetes(cassetes);

            atm.Run();
        }
    }
}
