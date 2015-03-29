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

        private bool _wrongInput = true;

        public List<Cassete> Cassetes { get; private set; }
        private readonly List<List<int>> _trySets = new List<List<int>>();
        

        public void SetCassetes(List<Cassete> cassetes)
        {
            Cassetes = cassetes;
        }

        private void ReturnMoneySet()
        {
            var moneySet = Cassetes.Select((t, i) => new Cassete(t.Value, _trySets[_trySets.Count - 1].ElementAt(i))).ToList();
            _money = new Money();
            _money.GetMoneyFromCassettes(moneySet);
        }

        private void Refresh()
        {
            Console.WriteLine();
            _trySets.Clear();
            _wrongInput = true;
        }

        public States TryWithdrawMoney(int target, ref Money money)
        {
            Refresh();
            var bills = new List<int>();

            if (target > MaxSum)
            {
                return States.NotEnoughMoney;
            }

            if (CashBack(ref bills, Cassetes, 0, 0, target))
            {
                return States.WrongInput;
            }

            for (int i = 0; i < Cassetes.Count; i++)
            {
                Cassetes[i].Withdraw(_trySets[_trySets.Count - 1].ElementAt(i));
                ReturnMoneySet();
                money = _money;
            }
            return States.MoneyReturned;
        }

        private bool CashBack(ref List<int> cash, List<Cassete> cassetes, int highest, int sum, int requiedSum)
        {
            List<int> newSet;
            if (_trySets.Count >= 1)
            {
                return _wrongInput;
            }
            if (sum == requiedSum && CheckBillsSet(ref cash, out newSet))
            {
                _trySets.Add(newSet);
                _wrongInput = false;
                return _wrongInput;
            }

            if (sum > requiedSum)
            {
                return _wrongInput;
            }


            foreach (var value in cassetes.Where(value => value.Value >= highest))
            {
                var cashCopy = new List<int>(cash) {value.Value};
                CashBack(ref cashCopy, cassetes, value.Value, sum + value.Value, requiedSum);
            }
            return _wrongInput;
        }

        private bool CheckBillsSet(ref List<int> cash, out List<int> newSet)
        {
            var set = new List<int>();
            foreach (var par in Cassetes)
            {
                int count = cash.Count(value => value == par.Value);
                set.Add(count);
            }
            newSet = set;

            int i = 0;
            int enough = 0;
            foreach (var item in Cassetes)
            {
                if (set[i] <= item.Amount)
                    enough++;
                i++;
            }
            return enough.Equals(set.Count);
        }
    }
}
