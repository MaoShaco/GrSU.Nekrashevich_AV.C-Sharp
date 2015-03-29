using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashMachine.ATM
{
    class Money
    {
        public Dictionary<int, int> Bills { get; private set; }

        public int TotalSum {
            get
            {
                var sum = Bills.Sum(bill => bill.Value*bill.Key);
                return sum;
            }
        }

        public Money()
        {
            Bills = new Dictionary<int, int>();
        }

        public void GetMoneyFromCassettes(List<Cassete> cassetes)
        {
            foreach (var item in cassetes)
            {
                Bills.Add(item.Value, item.Amount);
            }
        }

        public override string ToString()
        {
            var moneyString = new StringBuilder();
            foreach (var item in Bills.Where(item => item.Value != 0))
            {
                moneyString.Append(string.Format("{0} : {1} \n", item.Key, item.Value));
            }
            moneyString.Append(string.Format("Total Sum of Bills equal {0}", TotalSum));

            return moneyString.ToString();
        }
    }
}
