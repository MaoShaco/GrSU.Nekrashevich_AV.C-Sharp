using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachine.ATM
{
    class CashMachineSystem
    {
        public List<Cassete> MoneySet;

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
            MoneySet = new List<Cassete>();
            for (int i = 0; i < Cassetes.Count; i++)
            {
                MoneySet.Add(new Cassete(Cassetes[i].Value, _trySets[_trySets.Count - 1].ElementAt(i)));
            }
        }

        private void Refresh()
        {
            Console.WriteLine();
            _trySets.Clear();
            _wrongInput = true;
        }

        public short CheckStates(int target)
        {
            Refresh();
            var bills = new List<int>();

            if (target > MaxSum)
            {
                return 0;
            }

            if (CashBack(ref bills, Cassetes, 0, 0, target))
            {
                return 1;
            }

            for (int i = 0; i < Cassetes.Count; i++)
            {
                Cassetes[i].Withdraw(_trySets[_trySets.Count - 1].ElementAt(i));
                ReturnMoneySet();
            }
            return 2;
        }

        private bool CashBack(ref List<int> cash, List<Cassete> cassetes, int highest, int sum, int target)
        {
            List<int> newSet;
            if (_trySets.Count >= 1)
            {
                return _wrongInput;
            }
            if (sum == target && CheckBillsSet(ref cash, out newSet))
            {
                _trySets.Add(newSet);
                _wrongInput = false;
                return _wrongInput;
            }

            if (sum > target)
            {
                return _wrongInput;
            }


            foreach (Cassete value in cassetes)
            {
                if (value.Value >= highest)
                {
                    var cashCopy = new List<int>(cash) {value.Value};
                    CashBack(ref cashCopy, cassetes, value.Value, sum + value.Value, target);
                }
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
            foreach (Cassete item in Cassetes)
            {
                if (set[i] <= item.Amount)
                    enough++;
                i++;
            }
            return enough.Equals(set.Count);
        }
    }
}
