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

            Console.WriteLine(ToBinary(bits1));
            Console.WriteLine(ToBinary(bits2));

            //Console.WriteLine(ToBinary(bits1.Xor(bits2)));
            var bits3 = bits1.Xor(bits2);
            Console.WriteLine(ToBinary(bits3));
            
            
            Console.WriteLine(IsSame(bits3));
        }

        private static bool IsSame(BitArray bits)
        {

            for (int i = 0; i < bits.Length; i++)
            {
                //Console.WriteLine(bits[i]);
                if (bits[i]==true)
                {
                  //  Console.WriteLine(i);
                    return false;
                }
            }
            return true;
        }

        private static string ToBinary(BitArray bits)
        {
            //var bytes = Encoding.ASCII.GetBytes(term);
            //            var bits = new BitArray(bytes);
            var result = "";

            foreach (var bit in bits)
            {
                result = result + Convert.ToByte(bit).ToString();
            }

            return result;
        }

        private static void ShowBits(BitArray bits1, BitArray bits2)
        {
            for (int i = 0; i < bits1.Length; i++)
            {
                Console.WriteLine($" {Convert.ToByte(bits1[i])} - {Convert.ToByte(bits2[i])}");
            }

            Console.WriteLine("--------------");
        }
    }
}
