using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Test.App
{
    public static class TestVector
    {
        private static bool[] ToBinary(BitArray bits)
        {
            //var bytes = Encoding.ASCII.GetBytes(term);
            //            var bits = new BitArray(bytes);
            var result = new bool[bits.Length];

            for (int i = 0; i < bits.Length; i++)
            {
                result[i] = Convert.ToBoolean(bits[i]);
            }

            return result;
        }

        public static void Execute(int numberOfRequests, IList<string> users, string term)
        {
            List<byte[]> usersAsByte = new List<byte[]>();
            foreach (var item in users)
            {
                usersAsByte.Add(Encoding.ASCII.GetBytes(item.Trim()));
            }
            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());

            int count = 0;
   
            var sw = Stopwatch.StartNew();

         

            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
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

                    if (model[i * 4 + k] != 255)
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

        
    }
}
