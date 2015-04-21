using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using log4net;

namespace CashMachine.ATM 
{
    class CashMachineUserInterface
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CashMachineUserInterface));

        private readonly string[] _uiText;
        public CashMachineUserInterface(EnumLanguages language = EnumLanguages.English)
        {
            _uiText = (ConfigurationManager.AppSettings[language.ToString()]).Split('%');   
        }

        private readonly CashMachineSystem _atmSystem = new CashMachineSystem();

        public void InputCassetes(List<Cassete> cassetes)
        {
            
            _atmSystem.SetCassetes(cassetes);
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
                Logger.Info(string.Format("Existing banknotes [{0}]", item.Value));
            }
            Console.WriteLine();
        }

        public void Run()
        {
            if (_atmSystem.Cassetes == null)
                return;
            Logger.Info(string.Format("ATM started to work"));

            while (NotEmptyAtm)
            {
                var buffer = Console.ReadLine();
                Logger.Info(string.Format("Input string equal - [{0}]", buffer));

                int money;
                if (int.TryParse(buffer, out money))
                    GiveMoney(money);
                else
                {
                    Logger.Info(string.Format("ShutDowning the ATM the reason of inputted string [{0}]", buffer));
                    return;
                }
            }
            Logger.Info(string.Format("ShutDowning the empty ATM"));
        }

        private Money _money; 

        private void GiveMoney(int money = 0)
        {
            switch (_atmSystem.TryWithdrawMoney(money, ref _money))
            {
                case AtmSystemState.NotEnoughMoney:
                {
                    Logger.Info(string.Format("Not enough money in ATM for [{0}]", money));
                    Console.WriteLine(_uiText[3]);
                    break;
                }
                case AtmSystemState.WrongInput:
                {
                    Logger.Info(string.Format("The inputted string [{0}] is bad combination because there are only", money));
                    WrongCombination(money, _atmSystem.Cassetes);
                    break;
                }
                case AtmSystemState.MoneyWithdrawed:
                {
                    Logger.Info(string.Format("The money [{0}] has been withdrawed Succsessfully ", money));
                    Console.WriteLine(_money);
                    break;
                }
            }
        }
    }
}
