using System;

namespace CashMachine.ATM
{
    class Cassete : IComparable
    {
        public int Value { get; private set; }
        public int Amount { get; set; }

        public Cassete(int value, int amount)
        {
            Value = value;
            Amount = amount;
        }

        public void Withdraw(int count)
        {
            Amount -= count;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Value, Amount);
        }

        public int CompareTo(object obj)
        {
            if (Value < ((Cassete) obj).Value) return 1;
            if (Value > ((Cassete) obj).Value) return -1;
            return 0;
        }
    }
}
