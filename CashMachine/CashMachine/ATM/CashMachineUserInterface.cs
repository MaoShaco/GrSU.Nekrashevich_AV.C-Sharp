using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachine.ATM 
{
    class CashMachineUserInterface
    {
        private readonly CashMachineSystem _atmSystem = new CashMachineSystem();

        public void InputCassetes(List<Cassete> cassetes)
        {
            _atmSystem.SetCassetes(cassetes);
            Console.WriteLine("Cassetes inserted Succsessfully");
        }

        public bool NotEmptyAtm
        {
            get { return _atmSystem.MaxSum > 0; }
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

        private void ShowCash(IReadOnlyList<Cassete> moneySet)
        {
            foreach (Cassete item in moneySet.Where(item => item.Amount != 0))
            {
                Console.WriteLine("{0} : {1}", item.Value, item.Amount);
            }
        }


        public void GiveMoney(int money = 0)
        {
            switch (_atmSystem.CheckStates(money))
            {
                case 0:
                {
                    Console.WriteLine("Not Enough Money");
                    break;
                }
                case 1:
                {
                    WrongCombination(money, _atmSystem.Cassetes);
                    break;
                }
                case 2:
                {
                    ShowCash(_atmSystem.MoneySet);
                    break;
                }
            }
        }
    }
}
