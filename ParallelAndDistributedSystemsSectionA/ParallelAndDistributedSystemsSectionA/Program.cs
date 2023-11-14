using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParallelAndDistributedSystemsSectionA
{

    public class Tool
    {
        public int Barcode { get; set; }
        public int Type { get; set; }
        public Tool(int barcode, int type)
        {
            Barcode = barcode;
            Type = type;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            int[] numbers = new int[100000];
            var tools = new List<Tool>();

            Random rand = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rand.Next(1, 100000);
            }

            for (var i = 0; i < 100000; i++)
            {
                int number = rand.Next(1, 101);
                var tool = new Tool(i + 1, number);
                tools.Add(tool);
            }

            Task2(tools);
            Task1(numbers);


        }

        static public List<int> SortArray(List<int> list)
        {
            var n = list.Count;

            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (list[j] > list[j + 1])
                    {
                        var tempVar = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = tempVar;
                    }

            return list;
        }

        static public List<Tool> FindItem(List<Tool> list, int item1Count, int item2Count, int item3Count)
        {
            int n = 0;
            var result = new List<Tool>();
            for (int i = 0; n < item1Count; i++)
            {
                if (list[i].Type == 1)
                {
                    result.Add(list[i]);
                    n++;
                    Console.WriteLine($"{list[i].Barcode} is Type {list[i].Type}");
                }
            }
            n = 0;
            for (int i = 0; n < item2Count; i++)
            {
                if (list[i].Type == 7)
                {
                    result.Add(list[i]);
                    n++;
                    Console.WriteLine($"{list[i].Barcode} is Type {list[i].Type}");
                }
            }
            n = 0;
            for (int i = 0; n < item3Count; i++)
            {
                if (list[i].Type == 10)
                {
                    result.Add(list[i]);
                    n++;
                    Console.WriteLine($"{list[i].Barcode} is Type {list[i].Type}");
                }
            }


            return result;

        }

        static void Task1(int[] array)
        {
            var part1 = new List<int>();
            var part2 = new List<int>();


            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] < 50000)
                {
                    part1.Add(array[i]);
                }
                else
                {
                    part2.Add(array[i]);
                }
            }

            var thread1 = new Thread((state) => SortArray(part1));
            thread1.Start();
            thread1.Join();
            var thread2 = new Thread((state) => SortArray(part2));
            thread2.Start();
            thread2.Join();

            var result = part1.Concat(part2).ToList();
            for (var i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }

        }

        static void Task2(List<Tool> list)
        {
            var part1 = new List<Tool>();
            var part2 = new List<Tool>();


            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Barcode < 50000)
                {
                    part1.Add(list[i]);
                }
                else
                {
                    part2.Add(list[i]);
                }
            }

            var thread1 = new Thread((state) => FindItem(part1, 15, 8, 4));
            thread1.Start();
            thread1.Join();
            var thread2 = new Thread((state) => FindItem(part2, 15, 7, 4));
            thread2.Start();
            thread2.Join();
        }
    }
}
