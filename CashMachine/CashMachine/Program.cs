using System;
using System.Collections.Generic;
using CashMachine.ATM;

namespace CashMachine
{
    class Program
    {
        private static void Main()
        {
            var atm = new CashMachineUserInterface();
            var casseteReader = new CassetteReader();
            List<Cassete> cassetes;

            if (casseteReader.TryGetCassetes("Cassete.txt", out cassetes))
                atm.InputCassetes(cassetes);

            do
            {
                int money;
                if (int.TryParse(Console.ReadLine(), out money) && money != 0)
                    atm.GiveMoney(money);
            } while (atm.NotEmptyAtm);  
        }
    }
}
