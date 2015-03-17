using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachine.ATM
{
    class CashMachineSystem
    {
        private string _pathToCassettes;
        private bool _wrongInput = true;

        protected List<Cassete> RealCassetes { get; private set; }
        protected List<List<int>> Sets { get; private set; }
        private List<int> _newSet = new List<int>();

        protected void InsertCassetes(string path)
        {
            _pathToCassettes = path;
            var inputCassetes = new CassetteReader();
            RealCassetes = inputCassetes.GetCassetes(path);
        }

        public CashMachineSystem()
        {
            Sets = new List<List<int>>();
        }

        private void Refresh()
        {
            Console.WriteLine();
            Sets.Clear();
            _newSet.Clear();
            _wrongInput = true;
            SaveCassetes();
        }

        private void SaveCassetes()
        {
            using (var casseteWriter = new System.IO.StreamWriter(_pathToCassettes))
            {
                foreach (var item in RealCassetes)
                {
                    casseteWriter.WriteLine(item.ToString());
                }
            }
        }


        protected short CheckStates(int target)
        {
            Refresh();
            var bills = new List<int>();

            if (target > MaxSum())
            {
                return 0;
            }

            if (CashBack(ref bills, RealCassetes, 0, 0, target))
            {
                return 1;
            }

            for (int i = 0; i < RealCassetes.Count; i++)
            {
                RealCassetes[i].Withdraw(Sets[Sets.Count - 1].ElementAt(i));
            }
            return 2;
        }

        private bool CashBack(ref List<int> cash, List<Cassete> realCassetes, int highest, int sum, int target)
        {
            if (Sets.Count >= 1)
            {
                return _wrongInput;
            }
            if (sum == target && CheckBillsSet(ref cash, out _newSet))
            {
                Sets.Add(_newSet);
                _wrongInput = false;
                return _wrongInput;
            }

            if (sum > target)
            {
                return _wrongInput;
            }


            foreach (Cassete value in realCassetes)
            {
                if (value.Value >= highest)
                {
                    var cashCopy = new List<int>(cash) {value.Value};
                    CashBack(ref cashCopy, realCassetes, value.Value, sum + value.Value, target);
                }
            }
            return _wrongInput;
        }

        private bool CheckBillsSet(ref List<int> cash, out List<int> newSet)
        {
            var set = new List<int>();
            foreach (var par in RealCassetes)
            {
                int count = cash.Count(value => value == par.Value);
                set.Add(count);
            }
            newSet = set;

            int i = 0;
            int enough = 0;
            foreach (Cassete item in RealCassetes)
            {
                if (set[i] <= item.Amount)
                    enough++;
                i++;
            }
            return enough.Equals(set.Count);
        }

        protected int MaxSum()
        {
            return RealCassetes.Sum(item => item.Value*item.Amount);
        }
    }
}
