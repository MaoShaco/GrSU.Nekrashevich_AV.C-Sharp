using System;
using System.Collections.Generic;
using System.Linq;

namespace StartDispencer
{
    class DispenserInterface
    {

        private readonly string _casseteSlot;
        private bool _wrongInput = true;

        public List<Cassete> RealCassetes { get; private set; }
        public List<List<int>> Sets { get; private set; }
        private List<int> _newSet = new List<int>();

        public DispenserInterface(string path = "")
        {
            _casseteSlot = path;
            var inputCassetes = new CassetteReader(path);
            RealCassetes = inputCassetes.GetCassetes();
            RealCassetes.Sort();
            RealCassetes.Reverse();
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
            using (var casseteWriter = new System.IO.StreamWriter(_casseteSlot))
            {
                foreach (var item in RealCassetes)
                {
                    casseteWriter.WriteLine(item.ToString());
                }
            }
        }


        public short GiveMoney(int target)
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

            for(int i = 0; i < RealCassetes.Count; i++)
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
                    var copy = new List<int>(cash) { value.Value };
                    CashBack(ref copy, realCassetes, value.Value, sum + value.Value, target);
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


        public int MaxSum()
        {
            return RealCassetes.Sum(item => item.Value * item.Amount);
        }
    }
}
