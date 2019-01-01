using System;
using System.Collections;
using System.Text;

namespace ByteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte byteValue1 = 201;
            //Console.WriteLine(byteValue1);

            //byte byteValue2 = 0x00C9;
            //Console.WriteLine(byteValue2);

            var bits1 = new BitArray(Encoding.ASCII.GetBytes("jack"));
            var bits2 = new BitArray(Encoding.ASCII.GetBytes("jack"));
            var bits3 = bits1.And(bits2);
            ShowBits(bits1,bits3);

        }

        private static void ShowBits(BitArray bits1,BitArray bits2)
        {
            for (int i = 0; i < bits1.Length; i++)
            {
                Console.WriteLine($" {bits1[i]} - {bits2[i]}");
            }

            Console.WriteLine("--------------");
        }
    }
}
