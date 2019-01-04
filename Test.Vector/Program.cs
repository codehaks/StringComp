using System;
using System.Collections;
using System.Numerics;
using System.Text;

namespace Test.VectorApp
{
    class Program
    {
        private static bool[] ToBinary(BitArray bits)
        {
         
            var result = new bool[bits.Length];

            for (int i = 0; i < bits.Length; i++)
            {
                result[i] = Convert.ToBoolean(bits[i]);
            }

            return result;
        }

        static void Main(string[] args)
        {
            var term = Encoding.ASCII.GetBytes("jackjackjackjackjackjackjackjack");
            var user = Encoding.ASCII.GetBytes("omidjackpopopopopopopopojackpopo");
            var size = Vector<byte>.Count;
            //new Vector<byte>()
            var vterm = new Vector<byte>(term);
            var vuser = new Vector<byte>(user);

            var dest = new byte[32];
            var vresult = Vector.Equals(vterm, vuser);

            vresult.CopyTo(dest);

            Console.WriteLine(GetCount(dest));

          
        }

        private static int GetCount(byte[] model)
        {
            var count = 0;
            //var index = 0;

            for (int i = 0; i < 8; i++)
            {
                var same = true;
                for (int k = 0; k < 4; k++)
                {
                  
                    if (model[i*4+k] != 255)
                    {
                        same = false;
                        break;
                    }

                    //index++;
                }

                if (same)
                {
                    count++;
                }
            }
            return count;
        }

        private static string ToBinaryChar(bool[] bits)
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
    }
}
