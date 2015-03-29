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

        private bool NotEmptyAtm
        {
            get { return _atmSystem.MaxSum > 0; }
        }

        private void WrongCombination(int target, List<Cassete> outBillSet)
        {
            Console.Write("The combination - {0} is wrong for this ATM \n \t Choose from the following banknotes ",
                target);
            foreach (var item in outBillSet.Where(item => item.Amount > 0))
            {
                Console.Write("{0}'s ", item.Value);
            }
            Console.WriteLine();
        }

        public void Run()
        {
            int money;
            do
            {
                if (int.TryParse(Console.ReadLine(), out money) && money != 0)
                    GiveMoney(money);
            } while (NotEmptyAtm && money != 0);
        }

        private Money _money; 

        private void GiveMoney(int money = 0)
        {
            switch (_atmSystem.TryWithdrawMoney(money, ref _money))
            {
                case States.NotEnoughMoney:
                {
                    Console.WriteLine("Not Enough Money");
                    break;
                }
                case States.WrongInput:
                {
                    WrongCombination(money, _atmSystem.Cassetes);
                    break;
                }
                case States.MoneyReturned:
                {
                    Console.WriteLine(_money);
                    break;
                }
            }
        }
    }
}
