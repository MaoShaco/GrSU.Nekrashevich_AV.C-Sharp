using System;

namespace StartDispencer
{
    class Program
    {
        static void Main()
        {
            var atm = new DispenserInterface("Cassete.txt");
            int target = -1;
            while (target != 0)
            {
                if (int.TryParse(Console.ReadLine(), out target))
                    atm.GiveMoney(target);
            } 
        }
    }
}

