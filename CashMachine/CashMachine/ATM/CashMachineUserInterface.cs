using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CashMachine.ATM 
{
    class CashMachineUserInterface
    {
        private readonly string[] _uiText;
        public CashMachineUserInterface(EnumLanguages language = EnumLanguages.English)
        {
            _uiText = (ConfigurationManager.AppSettings[language.ToString()]).Split('%');

        }

        private readonly CashMachineSystem _atmSystem = new CashMachineSystem();

        public void InputCassetes(List<Cassete> cassetes)
        {
            _atmSystem.SetCassetes(cassetes);
            //Console.WriteLine("Cassetes inserted Succsessfully");
            Console.WriteLine(_uiText[0]);

        }

        private bool NotEmptyAtm
        {
            get { return _atmSystem.MaxSum > 0; }
        }

        private void WrongCombination(int target, List<Cassete> outBillSet)
        {
            Console.Write(_uiText[1], target);
            foreach (var item in outBillSet.Where(item => item.Amount > 0))
            {
                Console.Write(_uiText[2], item.Value);
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
                case AtmSystemState.NotEnoughMoney:
                {
                    Console.WriteLine(_uiText[3]);
                    break;
                }
                case AtmSystemState.WrongInput:
                {
                    WrongCombination(money, _atmSystem.Cassetes);
                    break;
                }
                case AtmSystemState.MoneyWithdrawed:
                {
                    Console.WriteLine(_money);
                    break;
                }
            }
        }
    }
}
