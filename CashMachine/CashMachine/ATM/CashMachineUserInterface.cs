using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CashMachine.ATM 
{
    class CashMachineUserInterface : CashMachineSystem
    {
        public bool InputCassetes(string path = "")
        {

            if (!File.Exists(path))
            {
                Console.WriteLine("Wrong Casset inserted");
                return false;
            }
            InsertCassetes(path);
            Console.WriteLine("Cassetes inserted Succsessfully");
            return true;
        }

        private void WrongCombination(int target, List<Cassete> outBillSet)
        {
            Console.Write("The combination - {0} is wrong for this ATM \n \t Choose from the following banknotes",
                target);
            foreach (var item in outBillSet.Where(item => item.Amount > 0))
            {
                Console.Write("{0}'s ", item.Value);
            }
            Console.WriteLine();
        }

        private void ShowCash(IReadOnlyList<Cassete> realCassetes, IReadOnlyList<List<int>> sets)
        {
            for (int i = 0; i < realCassetes.Count; i++)
            {
                Console.WriteLine("{0} : {1}", realCassetes[i].Value, sets[sets.Count - 1].ElementAt(i));
            }
        }


        public void GiveMoney(int Money = 0)
        {
            switch (CheckStates(Money))
            {
                case 0:
                {
                    Console.WriteLine("Not Enough Money");
                    break;
                }
                case 1:
                {
                    WrongCombination(Money, RealCassetes);
                    break;
                }
                case 2:
                {
                    ShowCash(RealCassetes, Sets);
                    break;
                }
            }
        }
    }
}
