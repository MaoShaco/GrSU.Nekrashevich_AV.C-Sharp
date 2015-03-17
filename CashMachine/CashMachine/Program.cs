using System;
using CashMachine.ATM;

namespace CashMachine
{
    class Program
    {
        private static void Main()
        {
            var atm = new CashMachineUserInterface();
            string path;
            do
            {
                path = Console.ReadLine();
            } while (!atm.InputCassetes(path));

            int money;
            do
            {
                if (int.TryParse(Console.ReadLine(), out money) && money != 0)
                    atm.GiveMoney(money);

            } while (money != 0);
            
        }
    }
}
