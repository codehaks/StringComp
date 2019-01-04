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
            var termLength = termAsByte.Length;
            int count = 0;
            var tempName = new byte[32];

            var termByte = Encoding.ASCII.GetBytes("jackjackjackjackjackjackjackjack");
            var size = Vector<byte>.Count;
            var dest = new byte[size];
            var sw = Stopwatch.StartNew();

            for (int r = 0; r < numberOfRequests; r++)
            {


                for (int i = 0; i < usersAsByte.Count; i++)
                {
                    if (termLength != usersAsByte[i].Length)
                    {
                        continue;
                    }
                    else
                    if (tempName.Length < Vector<byte>.Count)
                    {
                        tempName.SetValue(usersAsByte[i], tempName.Length - 1);
                    }
                    else
                    {
                        var vterm = new Vector<byte>(termByte);
                        var vuser = new Vector<byte>(tempName);


                        var vresult = Vector.Equals(vterm, vuser);

                        vresult.CopyTo(dest);
                        count += GetCount(dest);
                    }
                }
            }

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

        public static void Execute2(int numberOfRequests, IList<string> users, string term)
        {
            List<BitArray> usersAsBits = new List<BitArray>();
            foreach (var item in users)
            {
                var bits = new BitArray(Encoding.ASCII.GetBytes(item.Trim()));
                usersAsBits.Add(bits);
            }

            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());
            var termAsBits = new BitArray(termAsByte);

            int count = 0;

            var sw = Stopwatch.StartNew();
            usersAsBits = usersAsBits.Where(u => u.Length == termAsBits.Length).ToList();

            var totalCount = usersAsBits.Count;
            var rem = totalCount % 8;
            var rounds = totalCount / 8;

            var vuser = new Vector<bool>();

            var baseBin = ToBinary(termAsBits);
            var bin = new bool[256];

            for (int i = 0; i < 8; i++)
            {
                bin.SetValue(baseBin, i);
            }
            var vterm = new Vector<bool>();

            var dest = new bool[256];

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;

                for (int j = 0; j < usersAsBits.Count; j++)
                {
                    var baseVec = new bool[256];
                    for (int k = j; k < j + 8; k++)
                    {
                        baseVec.SetValue(usersAsBits[k], k);
                    }
                    vuser = new Vector<bool>(baseVec);
                    var vresult = Vector.Xor(vuser, vterm);
                    vresult.CopyTo(dest);
                    count += GetNumberOfTerms(dest);
                }


            }

            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static byte GetNumberOfTerms(bool[] bits)
        {
            byte count = 0;
            for (int k = 0; k < 8; k++)
            {
                var same = false;
                for (int i = 0; i < 32; i++)
                {
                    //Console.WriteLine(bits[i]);
                    if (bits[k * 8 + i] == true)
                    {
                        same = true;
                        break;
                    }
                    else
                    {
                        same = true;
                    }
                }
                if (same)
                {
                    count++;
                }
            }

            return count;
        }
        private static bool IsSame(BitArray bits)
        {

            for (int i = 0; i < bits.Length; i++)
            {
                //Console.WriteLine(bits[i]);
                if (bits[i] == true)
                {
                    //  Console.WriteLine(i);
                    return false;
                }
            }
            return true;
        }
        private static bool CompareBites(BitArray bit1, BitArray bit2)
        {
            for (int i = 0; i < bit1.Length; i++)
            {
                if (bit1[i] != bit2[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static void Execute1(int numberOfRequests, IList<string> users, string term)
        {
            int count = 0;

            List<BitArray> usersAsBits = new List<BitArray>();
            foreach (var item in users)
            {
                var bits = new BitArray(Encoding.ASCII.GetBytes(item.Trim()));
                usersAsBits.Add(bits);
            }

            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());
            var termAsBits = new BitArray(termAsByte);
            var totalCount = usersAsBits.Count;
            var rem = totalCount % 8;
            var rounds = totalCount / 8;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;


                for (int j = 0; j < usersAsBits.Count; j++)
                {
                    if (termAsBits.Length != usersAsBits[j].Length)
                    {
                        continue;
                    }
                    if (CompareBites(termAsBits, usersAsBits[j]))

                    {
                        count++;
                    }
                }
            }

            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }
    }
}
