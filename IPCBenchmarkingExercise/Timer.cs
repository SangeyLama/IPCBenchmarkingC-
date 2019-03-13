using System;
using System.Diagnostics;

namespace IPCBenchmarkingExercise
{
    public class Timer
    {
        private long start, spent = 0;

        public Timer()
        {
            play();
        }

        public double Check()
        {
            var time = (nanoTime() - start + spent) / 1e9;
            return time;
        }

        public void pause()
        {
            spent += nanoTime() - start;
        }

        public void play()
        {
            start = nanoTime();
        }

        private static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
