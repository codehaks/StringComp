using System;
using System.Diagnostics;
using System.Numerics;

namespace VectorApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var n = 100000000;

            var nums = new int[n];
            for (int i = 0; i < n; i++)
            {
                nums[i] = i;
            }

            Sum(nums);
            SumVector(nums);

            Console.WriteLine();
        }

        private static void Sum(int[] n)
        {
            long result = 0;
            var s = Stopwatch.StartNew();
            for (int i = 0; i < n.Length; i++)
            {
                result += (n[i] + n[i]);
            }
            s.Stop();
            Console.WriteLine($" {result} => {s.ElapsedMilliseconds}");
        }

        private static void SumVector(int[] nums)
        {
            long result = 0;
            var vecSize = Vector<int>.Count;
            var vr = new Vector<int>();
            Console.WriteLine(vecSize);
            
            var s = Stopwatch.StartNew();

            for (int i = 0; i < nums.Length; i += vecSize)
            {
                var vn = new Vector<int>(nums, i);
                vr = vn + vn;
                for (int j = 0; j < vecSize; j++)
                {
                    result = result + vr[j];
                }
                
            }
            s.Stop();
           
            Console.WriteLine($" {result} => {s.ElapsedMilliseconds}");
        }
    }
}
