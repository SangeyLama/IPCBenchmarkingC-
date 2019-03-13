using System;

namespace IPCBenchmarkingExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            //mark0();
            //mark1();
            //mark2();
            //mark3();
            //mark4();
            mark5();
            Console.ReadLine();
        }

        private static double multiply(int i)
        {
            double x = 1.1 * (double)(i & 0xFF);
            return x * x * x * x * x * x * x * x * x * x * x
                     * x * x * x * x * x * x * x * x * x;
        }

        public static void mark0()
        { // USELESS
            Timer t = new Timer();
            double dummy = multiply(10);
            double time = t.Check() * 1e9;
            Console.WriteLine("mark0:");
            Console.Write(time.ToString("#.00") + "\n");
        }

        public static void mark1()
        { // NEARLY USELESS
            Timer t = new Timer();
            int count = 100_000;
            for (int i = 0; i < count; i++)
            {
                double dummy = multiply(i);
            }
            double time = t.Check() * 1e9 / count;
            Console.WriteLine("mark1:");
            Console.Write(time.ToString("#.00") + "\n");
        }

        public static double mark2()
        {
            Timer t = new Timer();
            int count = 100_000_000;
            double dummy = 0.0;
            for (int i = 0; i < count; i++)
                dummy += multiply(i);
            double time = t.Check() * 1e9 / count;
            Console.WriteLine("mark2:");
            Console.Write(time.ToString("#.00") + "\n");
            return dummy;
        }

        public static double mark3()
        {
            int n = 10;
            int count = 100_000_000;
            double dummy = 0.0;
            Console.WriteLine("mark3:");
            for (int j = 0; j < n; j++)
            {
                Timer t = new Timer();
                for (int i = 0; i < count; i++)
                    dummy += multiply(i);
                double time = t.Check() * 1e9 / count;
                Console.Write(time.ToString("0.00") + "\n");
            }
            return dummy / n;
        }

        public static double mark4()
        {
            int n = 10;
            int count = 100_000_000;
            double dummy = 0.0;
            double st = 0.0, sst = 0.0;
            for (int j = 0; j < n; j++)
            {
                Timer t = new Timer();
                for (int i = 0; i < count; i++)
                    dummy += multiply(i);
                double time = t.Check() * 1e9 / count;
                st += time;
                sst += time * time;
            }
            double mean = st / n, sdev = Math.Sqrt((sst - mean * mean * n) / (n - 1));
            Console.WriteLine("mark4:");
            Console.Write(mean.ToString("#.0") + " ns +/- " + sdev.ToString("#.000") + "\n");
            return dummy / n;
        }

        public static double mark5()
        {
            int n = 10, count = 1, totalCount = 0;
            double dummy = 0.0, runningTime = 0.0, st = 0.0, sst = 0.0;
            Console.WriteLine("mark5");
            Console.WriteLine(" Mean\t\tSTD\t\tcount");
            do
            {
                count *= 2;
                st = sst = 0.0;
                for (int j = 0; j < n; j++)
                {
                    Timer t = new Timer();
                    for (int i = 0; i < count; i++)
                        dummy += multiply(i);
                    runningTime = t.Check();
                    double time = runningTime * 1e9 / count;
                    st += time;
                    sst += time * time;
                    totalCount += count;
                }
                double mean = st / n, sdev = Math.Sqrt((sst - mean * mean * n) / (n - 1));
                Console.Write(mean.ToString("#.0") + "\t\t" + sdev.ToString("#.000") + "\t\t" + count + "\n");
            } while (runningTime < 0.25 && count < Int32.MaxValue / 2);
            Console.WriteLine("mark5 - done");
            return dummy / totalCount;
        }
    }
}
