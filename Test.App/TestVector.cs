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
            var data = Array.Empty<byte>();
            foreach (var item in users)
            {
                usersAsByte.Add(Encoding.ASCII.GetBytes(item.Trim()));
                //data.Concat
                data = data.Concat(Encoding.ASCII.GetBytes(item.Trim())).ToArray();
            }
            //var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());

            var termAsByte = Encoding.ASCII.GetBytes("jackjackjackjackjackjackjackjack");

            var vterm = new Vector<byte>(termAsByte);
            var dest = new byte[32];
            var count = 0;

            var sw = Stopwatch.StartNew();

            for (int r = 0; r < numberOfRequests; r++)
            {
                count = 0;
                for (int i = 0; i < data.Length / 32; i++)
                {
                    //Console.WriteLine(i);
                    var vuser = new Vector<byte>(data.Skip(i * 32).Take(32).ToArray());
                    var vresult = Vector.Equals(vterm, vuser);

                    vresult.CopyTo(dest);
                    count += GetCount(dest);
                }
            }



            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static int GetCount(byte[] model)
        {
            var count = 0;

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
