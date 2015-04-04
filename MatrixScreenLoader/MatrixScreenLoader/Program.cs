using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixScreenLoader
{

    public static class MatrixScreen
    {

        public static void Run(int width, int height)
        {
            while (true)
            {
                int[] lineLength = new int[width];
                Random random = new Random();
                for (int i = 0; i < width; i++)
                {
                    lineLength[i] = random.Next(height);
                }
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write((i < lineLength[j]) ? (char)(150 + random.Next(105)) : ' ');
                    }
                }
            }
        }

    }

    public static class Program
    {
        static void Main(string[] args)
        {
            MatrixScreen.Run(Console.WindowWidth, Console.WindowHeight);
        }
    }
}
