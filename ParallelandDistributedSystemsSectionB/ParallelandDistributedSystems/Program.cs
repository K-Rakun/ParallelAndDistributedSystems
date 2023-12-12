using System.Diagnostics;

namespace ParallelandDistributedSystems
{
    public class Circle
    {
        public bool Drawn { get; set; }
        public Circle(bool drawn)
        {
            Drawn = drawn;
        }
    }

    internal class Program
    {
        static void Main()
        {
            var circles = new List<Circle>();
            var threads = new List<Thread>();
            Stopwatch stopwatch = new Stopwatch();

            for (var i = 0; i < 1000; i++)
            {
                var circle = new Circle(false);
                circles.Add(circle);
            }

            Console.WriteLine("How many threads?");
            int threadNumber = int.Parse(Console.ReadLine());
            stopwatch.Start();

            for (int i = 0; i < threadNumber; i++)
            {
                int iCopy = i;
                Thread thread = new Thread(() => Draw(threadNumber, iCopy, circles));
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            stopwatch.Stop();
            Console.WriteLine($"Time spent {stopwatch.Elapsed}");

        }


        static void Draw(int threadNumber, int threadCount, List<Circle> circles)
        {
            for (var i = threadCount; i < 1000; i += threadNumber)
            {
                Console.WriteLine($"circle {i + 1} is drawn by thread number {threadCount + 1}");
                circles[i].Drawn = true;
                Thread.Sleep(20);

            }
        }
    }
}