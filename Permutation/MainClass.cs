using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permutation
{
    class Program
    {
        static void Main(string[] args)
        {
            NthPermutation lll = new NthPermutation();

            string ABDC = lll.GetNthPermutation("ABCD", 2);// Expected  : ABDC
            Console.WriteLine("Trying for k= 2 : " + ABDC);

            string BACD = lll.GetNthPermutation("ABCD", 7);// Expected  : BACD
            Console.WriteLine("Trying for k= 7 : " + BACD);

            string CBAD = lll.GetNthPermutation("ABCD", 15);// Expected : CBAD
            Console.WriteLine("Trying for k=15 : " + CBAD);

            string DABC = lll.GetNthPermutation("ABCD", 19);// Expected : DABC
            Console.WriteLine("Trying for k=19 : " + DABC);

            string DCBA = lll.GetNthPermutation("ABCD", 24);
            Console.WriteLine("Trying for k=24 : " + DCBA);

            string CDAA = lll.GetNthPermutation("AACD", 9);
            string DAAC = lll.GetNthPermutation("AACD", 10);
            string DCAA = lll.GetNthPermutation("AACD", 12);

            string ADAA = lll.GetNthPermutation("AAAD", 3);
            string DAAA = lll.GetNthPermutation("AAAD", 4);

            string ABAB = lll.GetNthPermutation("AABB", 2);
            string ABBA = lll.GetNthPermutation("AABB", 3);

            Console.ReadLine();
        }

    }
}
