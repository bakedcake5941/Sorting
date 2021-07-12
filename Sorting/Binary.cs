using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    class Binary
    {
        public List<int> Elements = new List<int>();
        public int TotalElements { get; private set; }

        public Binary(int TotalElements, bool Random)
        {
            this.TotalElements = TotalElements;
            generateNumbers(Random);
        }

        public Binary(int TotalElements)
        {
            this.TotalElements = TotalElements;
            generateNumbers(true);

            foreach (int i in Elements)
            {
                Console.WriteLine(i);
            }
        }

        public void Sort()
        {
            //I have no idea on how I'm going to do this
            //I simply need to use recursive sorting with the pivot

            Console.Clear();

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int pivotIndex = (int)Math.Round((double)Elements.Count / 2);
            int pivotValue = Elements[pivotIndex];
            Console.WriteLine($"{pivotIndex} : {pivotValue}");

            for (int i = 0; i < Elements.Count; i++)
            {
                if (i == pivotIndex)
                    continue;

                if (Elements[i] < pivotValue)
                    left.Add(Elements[i]);
                else
                    right.Add(Elements[i]);
            }

            left.Add(pivotValue);

            Elements = Reassemble(left, right);
            foreach (int i in Elements)
            {
                Console.WriteLine(i);
            }
        }

        private List<int> Reassemble(List<int> _1, List<int> _2)
        {
            List<int> toReturn = new List<int>();

            foreach (int _ in _1)
            {
                toReturn.Add(_);
            }
            foreach (int _ in _2)
            {
                toReturn.Add(_);

            }
            return toReturn;
        }


        private void generateNumbers(bool Rand)
        {
            Random random = new Random();

            if (Rand)
            {

                for (int i = 0; i < TotalElements; i++)
                {
                    Elements.Add(random.Next(Int32.MinValue,Int32.MaxValue));
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
    }
}
