using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachine.ATM
{
    class CashMachineSystem
    {
        private Money _money;
        public int MaxSum 
        {
            get
            {
                return Cassetes.Sum(item => item.Value*item.Amount);
            }
        }

        public List<Cassete> Cassetes { get; private set; }
        private List<List<int>> _trySets = new List<List<int>>();

        public void SetCassetes(List<Cassete> cassetes)
        {
            Cassetes = cassetes;
        }

        private void ReturnMoneySet()
        {
            var moneySet = Cassetes.Select((t, i) => new KeyValuePair<int,int>(t.Value, _trySets[0].ElementAt(i))).ToList();
            _money = new Money();
            _money.GetMoneyFromCassettes(moneySet);
        }

        private void Refresh()
        {
            Console.WriteLine();
            _trySets.Clear();
        }

        public AtmSystemState TryWithdrawMoney(int target, ref Money money)
        {
            {
                Refresh();

                if (target > MaxSum)
                {
                    return AtmSystemState.NotEnoughMoney;
                }

                if (Algorithm.TrySelect(Cassetes, target, ref _trySets))
                {
                    return AtmSystemState.WrongInput;
                }

                for (int i = 0; i < Cassetes.Count; i++)
                {
                    Cassetes[i].Withdraw(_trySets[0].ElementAt(i));
                    ReturnMoneySet();
                    money = _money;
                }
                return AtmSystemState.MoneyWithdrawed;
            }
        }
    }
}
