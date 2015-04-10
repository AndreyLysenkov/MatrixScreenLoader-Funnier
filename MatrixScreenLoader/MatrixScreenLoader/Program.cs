using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixScreenLoader
{

    public static class MatrixScreen
    {

        public static void Run(int width, int height, int timeout)
        {
            while (true)
            {
                int[] lineLength = new int[width];
                Random random = new Random();
                for (int i = 0; i < width; i++)
                {
                    lineLength[i] = 2 + random.Next(height);
                }
                string text = null;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        text += ((i <= lineLength[j]) ? random.Next(10).ToString() : " ");
                    }
                }
                Console.WriteLine(text);
                System.Threading.Thread.Sleep(timeout);
            }
        }

    }

    public static class Program
    {
        static void Main(string[] args)
        {

            MatrixScreen.Run(Console.WindowWidth, Console.WindowHeight, 100);
        }
    }
}
