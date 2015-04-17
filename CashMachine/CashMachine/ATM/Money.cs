using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashMachine.ATM
{
    class Money
    {
        private readonly Dictionary<int, int> _bills = new Dictionary<int, int>();

        public int TotalSum {
            get
            {
                var sum = _bills.Sum(bill => bill.Value*bill.Key);
                return sum;
            }
        }

        public void GetMoneyFromCassettes(List<KeyValuePair<int,int>> cassetes)
        {
            foreach (var item in cassetes)
            {
                _bills.Add(item.Key, item.Value);
            }
        }

        public override string ToString()
        {
            var moneyString = new StringBuilder();
            foreach (var item in _bills.Where(item => item.Value != 0))
            {
                moneyString.Append(string.Format("{0} : {1} \n", item.Key, item.Value));
            }
            moneyString.Append(string.Format("Total Sum of Bills equal {0}", TotalSum));

            return moneyString.ToString();
        }
    }
}
