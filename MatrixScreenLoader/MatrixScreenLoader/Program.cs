using System;
using System.Collections.Generic;
using System.Text;

/*
 * Downthere complete sucks is happening;
 * Please, make sure, you took children away from the screen;
 * Also current code unrecommended to view for person with weak psyhological health;
 * And remember: I did warn you;
 * Best wishes,
 *          Andrey Lysenkov (Allan Awake);
 */

namespace MatrixScreenLoader
{

    public static class MatrixScreen
    {

        public class Setting
        {

            const char value_separator = '=';

            private string[][] _parametrs = 
                {new string[] {"width", "smth"}, {"height", "smth"}, {"timeout", "100"}};

            public Setting()
            {   }

            public void Set(string[] args)
            {
                if (args == null)
                    return;
                for (int i = 0; (i < args.Length); i++)
                {
                    string parametr = args[i];
                    if ((parametr == null) || (parametr.Length == 0))
                        continue;
                    string[] split_parametrs = parametr.Split(new char[] {value_separator}, 2);
                    this.SetParametr(name: split_parametrs[0], value: split_parametrs[1]);
                }
            }

            public void DEBUG_ShowParametrs(string template = "{0} = {1}")
            {
                for (int i = 0; i < _parametrs.Length; i++)
                {
                    Console.WriteLine(template, _parametrs[i][0], _parametrs[i][1]);
                }
            }

            private bool SetParametr(string name, object value)
            {
                bool isFounded = false;
                for (int i = 0; (i < _parametrs.Length) && (!isFounded); i++)
                {
                    if (name.ToLower() == _parametrs[i, 0])
                    {
                        _parametrs[i, 1] = value.ToString();
                        isFounded = true;
                    }
                }
                return isFounded;
            }
        
        }

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
            MatrixScreen.Setting current_setting = new MatrixScreen.Setting();
            current_setting.Set(args);
            current_setting.DEBUG_ShowParametrs();
            ///MatrixScreen.Run(Console.WindowWidth, Console.WindowHeight, 100);
            Console.ReadLine();
        }
    }
}
