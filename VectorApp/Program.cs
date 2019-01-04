using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace VectorApp
{
    class Program
    {
        public static int numberOfRequests = 100000;

        static void Main(string[] args)
        {
            Console.WriteLine("\n\n SIMD Sample");
            var n = (int)Math.Pow(8, 4);
            var nums = new int[n];

            for (int i = 0; i < n; i++)
            {
                nums[i] = i;
            }

            Sum(nums,out long secSum);
            SumVector(nums, out long secVec);

            Console.WriteLine($"\n vec is {secSum/secVec }x faster!");
            Console.WriteLine();
        }
       
        private static void Sum(int[] n, out long sec)
        {
            long result = 0;
            var s = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                result = 0;
                for (int j = 0; j < n.Length; j++)
                {
                    result += n[j] + n[j];
                }
            }

            s.Stop();
            sec = s.ElapsedMilliseconds;
            Console.WriteLine($" Sum : {result} => {s.ElapsedMilliseconds,5:N0} ms");
        }

        private static void SumVector(int[] nums,out long sec)
        {
            long result = 0;
            var vecSize = Vector<int>.Count;
            var vr = new Vector<int>();
            var s = Stopwatch.StartNew();

            var vresult = new int[nums.Length];
            for (int i = 0; i < numberOfRequests; i++)
            {
                result = 0;
                for (int j = 0; j <= nums.Length - vecSize; j += vecSize)
                {
                    var vn = new Vector<int>(nums, j);
                    vr = vn + vn;
                    vr.CopyTo(vresult, j);
                }
            }

            result = vresult.Sum();
            s.Stop();
            sec = s.ElapsedMilliseconds;

            Console.WriteLine($" Vec : {result} => {s.ElapsedMilliseconds,5:N0} ms (size:{vecSize}x{sizeof(int)}x8={vecSize* sizeof(int)*8} bits)");


        }
    }
}
