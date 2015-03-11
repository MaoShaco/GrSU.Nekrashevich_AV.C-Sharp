using System;
using System.Collections.Generic;
using System.Linq;

namespace StartDispencer
{
    class Program
    {
        public static void WrongCombination(int target, List<Cassete> realCassetes)
        {
            Console.Write("The combination - {0} is wrong for this ATM \n \t Choose from the following banknotes", target);
            foreach (Cassete item in realCassetes)
            {
                if (item.Amount > 0)
                    Console.Write("{0}'s ", item.Value);
            }
            Console.WriteLine();
        }

        public static void ShowCash(List<Cassete> realCassetes, List<List<int>> sets)
        {
            for (int i = 0; i < realCassetes.Count; i++)
            {
                Console.WriteLine("{0} : {1}", realCassetes[i].Value, sets[sets.Count - 1].ElementAt(i));
            }
        }

        static void Main()
        {
            var atm = new DispenserInterface("Cassete.txt");
            int target = -1;
            while (target != 0)
            {
                if (int.TryParse(Console.ReadLine(), out target) && target != 0)
                {
                    switch (atm.GiveMoney(target))
                    {
                        case 0 :
                        {
                            Console.WriteLine("Not Enough Money");
                            break;
                        }
                        case 1 :
                        {
                            WrongCombination(target, atm.RealCassetes);
                            break;
                        }
                        case 2 :
                        {
                            ShowCash(atm.RealCassetes, atm.Sets);
                            break;
                        }
                    }
                }

            } 
        }
    }
}

