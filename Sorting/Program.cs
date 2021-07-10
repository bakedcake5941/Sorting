using System;
using System.Collections.Generic;
using System.Threading;

namespace Sorting
{
    static class Threading //This is implemented so that the sorting speed isn't impacted by the console writing
    {
        public static int Runs;
        public static bool Finished;
        public static void Read()
        {
            while (true)
            {
                if (Finished)
                    return;

                Console.SetCursorPosition(0, 0);
                Console.Write("Times run through : " + Runs);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WriteLine("What do you want the size of the (to be) sorted array to be?");
            Int32 Size = Int32.Parse(Console.ReadLine());

            Console.Clear();
            Bubble bubble = new Bubble(Size, true);

            bubble.Sort();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine($"The array has been sorted in {Threading.Runs} attempts!");
            Thread.Sleep(5000);
            Console.WriteLine(bubble.getElements());
        }
    }

    class Bubble
    {
        public readonly int Elements;
        private List<int> Generated = new List<int>();

        public Bubble(int Elements, bool random)
        {
            this.Elements = Elements;
            GenerateElements(random);
        }

        public Bubble(int Elements)
        {
            this.Elements = Elements;
            GenerateElements(true);
        }

        public String getElements()
        {
            String toReturn = "";
            foreach (int s in Generated)
            {
                toReturn += $"{s}\n";
            }
            return toReturn;
        }

        public void Sort()
        {
            Thread thread = new Thread(new ThreadStart(Threading.Read));
            thread.Start();

            while (true)
            {
                bool FullRun = true;

                for (int i = 1; i < Generated.Count; i++)
                {
                    if (Generated[i - 1] > Generated[i])
                    {
                        FullRun = false;

                        int R = Generated[i];
                        Generated[i] = Generated[i - 1];
                        Generated[i - 1] = R;
                    }
                }

                if (FullRun)
                    break;

                Threading.Runs++;
            }

            Threading.Finished = true; //This aborts the reading thread


        }

        private void GenerateElements(bool Rand)
        {
            Random random = new Random();

            if (Rand)
            {

                for (int i = 0; i < Elements; i++)
                {
                    Generated.Add(random.Next());
                }
            }
            else
            {
                List<int> temp = new List<int>();
                for (int i = 0; i < Elements; i++)
                {
                    temp.Add(i);
                }




                for (int i = 0; i < Generated.Count + temp.Count; i++)
                {
                    int S = random.Next(temp.Count);

                    Generated.Add(temp[S]);
                    temp.RemoveAt(S);
                }
            }
        }
    }
}
