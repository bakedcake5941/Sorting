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

        public static void Split(object o)
        {
            List<int> L = (List<int>)o;

            foreach (int E in L)
            {
                Console.WriteLine(E);
            }
        }
    }

    class Program
    {
        static string compareTimes(int input)
        {
            Console.WriteLine($"\n\nSorting length {input}");

            Insertion.handles = new WaitHandle[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };

            Insertion insertion = new Insertion(input);
            Insertion insertion2 = new Insertion(1);
            insertion2.Elements = insertion.Elements;
            DateTime time = DateTime.Now;
            insertion.AdvancedSort();
            
            double timeTaken = (DateTime.Now - time).TotalSeconds;


            Threading.Finished = false;
            Threading.Runs = 0;
            time = DateTime.Now;
            insertion2.Sort();
            return $"Normal Sorting : { (DateTime.Now - time).TotalSeconds}\nAdvanced sorting : {timeTaken}";
        }

        static void Main(string[] args)
        {
            int[] bruh = new int[] {  100000 };


            foreach (int b in bruh)
                compareTimes(b);
        }
    }

    class Insertion
    {
        public List<int> Elements = new List<int>();
        public readonly int TotalElements;

        public Insertion(int Elements, bool Random)
        {
            this.TotalElements = Elements;
            generateNumbers(Random);
        }
        public Insertion(int Elements)
        {
            this.TotalElements = Elements;
            generateNumbers(false);
        }

        private void generateNumbers(bool Rand)
        {
            Random random = new Random();

            if (Rand)
            {

                for (int i = 0; i < TotalElements; i++)
                {
                    Elements.Add(random.Next());
                }
            }
            else
            {
                List<int> temp = new List<int>();
                for (int i = 0; i < TotalElements; i++)
                {
                    temp.Add(i);
                }




                for (int i = 0; i < Elements.Count + temp.Count; i++)
                {
                    int S = random.Next(temp.Count);

                    Elements.Add(temp[S]);
                    temp.RemoveAt(S);
                }
            }
        }

        static List<int> L1 ;
        static List<int> L2 ;

        private static void SortSplit(object In)
        {

            if (In == null)
                return;

            List<int> Elements = (List<int>)In;

            int pos = Elements[Elements.Count - 1];

            Elements.RemoveAt(Elements.Count-1);


            AutoResetEvent eventt = (AutoResetEvent)handles[pos];

            while (true)
            {
                bool fullRun = true;

                for (int i = 1; i < Elements.Count; i++)
                {
                    if (Elements[i - 1] > Elements[i])
                    {

                        fullRun = false;

                        int Temp = Elements[i - 1];
                        Elements[i - 1] = Elements[i];
                        Elements[i] = Temp;

                        for (int I = i - 1; I > 0; I--)
                        {
                            if (Elements[I] > Elements[I + 1])
                            {
                                Temp = Elements[I + 1];
                                Elements[I + 1] = Elements[I];
                                Elements[I] = Temp;
                            }
                        }

                        Threading.Runs++;
                    }
                }

                if (fullRun)
                    break;
                Threading.Runs++;
            }

            if (L1 == null)
                L1 = Elements;
            else
                L2 = Elements;

            eventt.Set();
        }

        public static WaitHandle[] handles = new WaitHandle[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };

        public void Sort()
        {
            Threading.Runs = 0;
            Thread thread = new Thread(new ThreadStart(Threading.Read));
            thread.Start();

            

            while (true)
            {
                bool fullRun = true;

                for (int i = 1; i < Elements.Count; i++)
                {
                    if (Elements[i - 1] > Elements[i])
                    {

                        fullRun = false;

                        int Temp = Elements[i - 1];
                        Elements[i - 1] = Elements[i];
                        Elements[i] = Temp;

                        for (int I = i - 1; I > 0; I--)
                        {
                            if (Elements[I] > Elements[I + 1])
                            {
                                Temp = Elements[I + 1];
                                Elements[I + 1] = Elements[I];
                                Elements[I] = Temp;
                            }
                        }

                        Threading.Runs++;
                    }
                }

                if (fullRun)
                    break;
                Threading.Runs++;
            }

            Threading.Finished = true;
        }

        public void AdvancedSort()
        {


            Thread thread = new Thread(new ThreadStart(Threading.Read));
            thread.Start();
            Threading.Runs = 0;

            List<int> Split1 = new List<int>();
            List<int> Split2= new List<int>();

            int Pivot = Elements[Elements.Count / 2];

            

            for (int i = 0; i < Elements.Count; i++)
            {
                if (Elements[i] < Pivot)
                    Split1.Add(Elements[i]);
                else
                    Split2.Add(Elements[i]);
            }


            Split1.Add(0);
            Split2.Add(1);

            Thread thread2 = new Thread(new ParameterizedThreadStart(SortSplit));


            ThreadPool.QueueUserWorkItem(new WaitCallback(SortSplit), Split1);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SortSplit), Split2);


            WaitHandle.WaitAll(handles);

            Threading.Finished = true;


            List<int> Finished = new List<int>();
            foreach (int L in L1[0] > L2[0] ? L2 : L1)
            {
                Finished.Add(L);
            }
            foreach (int L in L1[0] > L2[0] ? L1 : L2)
            {
                Finished.Add(L);
            }

            /*while (true)
            {
                bool FullRun = true;

                for (int i = 1; i < Finished.Count; i++)
                {
                    if (Finished[i-1] > Finished[i])
                    {
                        int Temp = Finished[i - 1];
                        Finished[i - 1] = Finished[i];
                        Finished[i] = Temp;
                        FullRun = false;
                    }
                }

                if (FullRun)
                    break;
            }*/

            
        }
    }

    class Bubble
    {
        public readonly int Elements;
        public List<int> Generated = new List<int>();

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
