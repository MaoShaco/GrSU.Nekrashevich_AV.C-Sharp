using System;

namespace StartDispencer
{
    class Cassete  : IComparable
    {
        public int Value { get; private set; }
        public int Amount { get; private set; }
        
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
            return Value.CompareTo(((Cassete)obj).Value);
        }
    }
}
