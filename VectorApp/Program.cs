using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace VectorApp
{
    class Program
    {
        public static int numberOfRequests = 10000000;

        static void Main(string[] args)
        {

            var n = 8;
            var nums = new int[n];

            for (int i = 0; i < n; i++)
            {
                nums[i] = i;
            }

            Sum(nums);
            SumVector(nums);
            SumParallel(nums);

            Console.WriteLine();
        }

        private static void SumParallel(int[] n)
        {
            long result = 0;
            var s = Stopwatch.StartNew();

            Parallel.For(0, numberOfRequests, i =>
            {
                result = 0;
                for (int j = 0; j < n.Length; j++)
                {
                    result += n[j] + n[j];
                }
            });

            s.Stop();
            Console.WriteLine($" {result} => {s.ElapsedMilliseconds}");
        }
        private static void Sum(int[] n)
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
            Console.WriteLine($" {result} => {s.ElapsedMilliseconds}");
        }

        private static void SumVector(int[] nums)
        {
            long result = 0;
            var vecSize = Vector<int>.Count;
            var vr = new Vector<int>();
            //Console.WriteLine(vecSize);

            var vn = new Vector<int>(nums);
            var s = Stopwatch.StartNew();

            var IsHardwareAccelerated = false;

            for (int i = 0; i < numberOfRequests; i++)
            {
                result = 0;
                vr = vn + vn;
          


            }

            IsHardwareAccelerated = Vector.IsHardwareAccelerated;

            var vresult = new int[nums.Length];
            vr.CopyTo(vresult);
            
            result = vresult.Sum();

            s.Stop();


            Console.WriteLine("IsHardwareAccelerated :"+ IsHardwareAccelerated);
            Console.WriteLine($" {result} => {s.ElapsedMilliseconds}");
        }
    }
}
