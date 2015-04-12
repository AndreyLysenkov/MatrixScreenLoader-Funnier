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
            const string Width = "width";
            const string Height = "height";
            const string Timeout = "timeout";
            /// Yeah, I could use [,] instead of [][], but...
            /// I had some reasons about that weird form...
            /// And I couldn't remember them...
            private string[][] _parametrs = 
                {
                    new string[] {"width", "smth"}, 
                    new string[] {"height", "smth"}, 
                    new string[] {"timeout", "100"}
                };

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

            private int GetIndexByName(string name, int null_index = -1)
            {
                bool isFounded = false;
                int index = null_index;
                for (int i = 0; (i < _parametrs.Length) && (!isFounded); i++)
                {
                    if (name.ToLower() == _parametrs[i][0])
                    {
                        isFounded = true;
                        index = null_index;
                    }
                }
                return isFounded ? index : null_index;
            }
            
            private bool SetParametr(string name, object value)
            {
                int index = GetIndexByName(name, -1);
                if (index == -1)
                    return false;
                _parametrs[index][1] = value.ToString();
                return true;
            }
        
            public string Get(string name, string default_value)
            {
                int index = GetIndexByName(name, -1);
                return index == -1 ? default_value : _parametrs[index][1];
            }
        
            public int GetInteger(string name, int default_value)
            {
                string output_string = Get(name, default_value.ToString());
                int output = 0;
                /// Some bad thing downthere;
                return int.TryParse(output_string, out output) ? output : default_value;
            }
        }

        public static void Run(Setting setting)
        {
            while (true)
            {
                int[] lineLength = new int[setting.GetIntegerwidth];
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
