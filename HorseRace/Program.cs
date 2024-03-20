using HorseRace;
using System;
using System.Threading;

namespace HorseRace
{
    public class Horse
    {
        public string Name { get; private set; }
        public int Distance { get; private set; }
        public bool IsFinished { get; private set; }

        public Horse(string name)
        {
            Name = name;
            Distance = 0;
            IsFinished = false;
        }

        public void Run()
        {
            Random random = new Random();
            while (!IsFinished)
            {
                Distance += random.Next(1, 11);

                Thread.Sleep(100);
            }
        }

        public void Finish()
        {
            IsFinished = true;
        }
    }

    public class RaceSimulation
    {
        private Horse[] horses;
        private int[] results;

        public RaceSimulation(string[] horseNames)
        {
            horses = new Horse[horseNames.Length];
            results = new int[horseNames.Length];

            for (int i = 0; i < horseNames.Length; i++)
            {
                horses[i] = new Horse(horseNames[i]);
            }
        }

        public void StartRace()
        {
            Console.WriteLine("Race started!");

            Thread[] threads = new Thread[horses.Length];
            for (int i = 0; i < horses.Length; i++)
            {
                threads[i] = new Thread(horses[i].Run);
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Race finished!");

            Array.Sort(horses, (h1, h2) => h2.Distance.CompareTo(h1.Distance));
            for (int i = 0; i < horses.Length; i++)
            {
                results[i] = Array.IndexOf(horses, horses[i]) + 1;
            }
        }

        public void DisplayResults()
        {
            Console.WriteLine("Race Results:");
            for (int i = 0; i < horses.Length; i++)
            {
                Console.WriteLine($"#{i + 1}: {horses[i].Name} - Distance: {horses[i].Distance} - Position: {results[i]}");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            string[] horseNames = { "Horse 1", "Horse 2", "Horse 3", "Horse 4", "Horse 5" };
            var raceSimulation = new RaceSimulation(horseNames);

            raceSimulation.StartRace();

            raceSimulation.DisplayResults();
        }
    }
}

