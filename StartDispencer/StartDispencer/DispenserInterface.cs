using System;
using System.Collections.Generic;
using System.Linq;

namespace StartDispencer
{
    class DispenserInterface
    {

        private readonly string _casseteSlot;
        private bool _wrongInput = true;

        private List<Cassete> _realCassetes;
        private readonly List<List<int>> _sets;
        private List<int> _newSet = new List<int>();

        public DispenserInterface(string path = "")
        {
            _casseteSlot = path;
            var inputCassetes = new CassetteReader(path);
            _realCassetes = inputCassetes.Cassettes;
            _realCassetes.Sort();
            _realCassetes.Reverse();
            _sets = new List<List<int>>();
        }

        private void Refresh()
        {
            Console.WriteLine();
            _sets.Clear();
            _newSet.Clear();
            _wrongInput = true;
            SaveCassetes();
        }

        public void SaveCassetes()
        {
            using (var casseteWriter = new System.IO.StreamWriter(_casseteSlot))
            {
                foreach (var item in _realCassetes)
                {
                    casseteWriter.WriteLine(item.ToString());
                }
            }
        }


        public void GiveMoney(int target)
        {
            var cash = new List<int>();

            if (target > MaxSum())
            {
                Console.WriteLine("Not Enough Money");
                return;
            }

            if (CashBack(ref cash, ref _realCassetes, 0, 0, target))
            {
                WrongCombination(target);
            }

            for(int i = 0; i < _realCassetes.Count; i++)
            {
                _realCassetes[i].Withdraw(_sets[_sets.Count - 1].ElementAt(i));
                Console.WriteLine("{0} : {1}", _realCassetes[i].Value, _sets[_sets.Count - 1].ElementAt(i));
            }
            Refresh();
        }

        private bool CashBack(ref List<int> cash, ref List<Cassete> realCassetes, int highest, int sum, int goal)
        {
            if (_sets.Count == 1)
            {
                return _wrongInput;
            }
            if (sum == goal && CheckBillsSet(ref cash, out _newSet))
            {
                _sets.Add(_newSet);
                _wrongInput = false;
                return _wrongInput;
            }

            if (sum > goal)
            {
                return _wrongInput;
            }


            foreach (Cassete value in realCassetes)
            {
                if (value.Value >= highest)
                {
                    var copy = new List<int>(cash) { value.Value };
                    CashBack(ref copy, ref realCassetes, value.Value, sum + value.Value, goal);
                }
            }
            return _wrongInput;
        }

        private bool CheckBillsSet(ref List<int> cash, out List<int> newSet)
        {
            var set = new List<int>();
            foreach (var par in _realCassetes)
            {
                int count = cash.Count(value => value == par.Value);
                set.Add(count);
            }
            newSet = set;

            int i = 0;
            int enough = 0;
            foreach (Cassete item in _realCassetes)
            {
                if (set[i] <= item.Amount)
                    enough++;
                i++;
            }
            return enough.Equals(set.Count);
        }


        public int MaxSum()
        {
            return _realCassetes.Sum(item => item.Value * item.Amount);
        }

        private void WrongCombination(int target)
        {
            Console.Write("The combination - {0} is wrong for this ATM \n \t Choose from the following banknotes", target);
            foreach (Cassete item in _realCassetes)
            {
                if (item.Amount > 0)
                    Console.Write("{0}'s ", item.Value);
            }
        }

    }
}
