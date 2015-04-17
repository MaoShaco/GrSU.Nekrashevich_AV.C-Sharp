using System.Collections.Generic;
using System.Linq;

namespace CashMachine.ATM
{
    internal class Algorithm
    {
        private static bool _wrongInput;
        private static List<List<int>> _trySets;
        public static bool TrySelect(List<Cassete> cassetes, int requiedSum, ref List<List<int> > trySets)
        {
            _wrongInput = true;
            List<int> moneySet = new List<int>();
            _trySets = trySets;
            return CashBack(ref moneySet, cassetes, 0, 0, requiedSum);
        }

        private static bool CashBack(ref List<int> cash, List<Cassete> cassetes, int highest, int sum, int requiedSum)
        {
            List<int> newSet;
            if (_trySets.Count >= 1)
            {
                return _wrongInput;
            }
            if (sum == requiedSum && CheckBillsSet(cassetes, ref cash, out newSet))
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
                var cashCopy = new List<int>(cash) { value.Value };
                CashBack(ref cashCopy, cassetes, value.Value, sum + value.Value, requiedSum);
            }
            return _wrongInput;
        }

        private static bool CheckBillsSet(List<Cassete> cassetes ,ref List<int> cash, out List<int> newSet)
        {
            var set = new List<int>();
            foreach (var par in cassetes)
            {
                int count = cash.Count(value => value == par.Value);
                set.Add(count);
            }
            newSet = set;

            int i = 0;
            int enough = 0;
            foreach (var item in cassetes)
            {
                if (set[i] <= item.Amount)
                    enough++;
                i++;
            }
            return enough.Equals(set.Count);
        }
    }
}
