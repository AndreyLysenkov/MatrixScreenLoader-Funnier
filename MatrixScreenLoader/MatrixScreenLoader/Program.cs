using System;
using System.Collections.Generic;
using System.Text;

/*
 * Downthere complete s*cks is happening;
 * Please, make sure, you took children away from the screen;
 * Also current code unrecommended to view for person with weak psyhological health;
 * And remember: I did warn you;
 * Best wishes,
 *          Allan Awake;
 */

namespace MatrixScreenLoader
{

    public static class MatrixScreen
    {

        /// Note, what height and width set not in pixels... in smth else... he-he
        public class Setting
        {

            const char value_separator = '=';
            public const string Width = "/w";
            public const string Height = "/h";
            public const string Timeout = "/t";
            public const string ClearScreen = "/c";
            public const string UpdateOnResize = "/r";

            /// Yeah, I could use [,] instead of [][], but...
            /// I had some reasons about that weird form...
            /// And I couldn't remember them...
            /// Anyway, it doesn't look good...
            /// It doesn't look right eather...
            private string[][] _parametrs = 
                {
                    new string[] {Width, "smth"}, 
                    new string[] {Height, "smth"}, 
                    new string[] {Timeout, "100"},
                    new string[] {ClearScreen, "true"},
                    new string[] {UpdateOnResize, "true"}
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
                    string[] split_parametr = parametr.Split(new char[] {value_separator}, 2);
                    this.SetParametr(name: split_parametr[0], value: split_parametr[1]);
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
                    if (name == _parametrs[i][0])
                    {
                        isFounded = true;
                        index = i;
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
        
            public bool GetBoolean(string name, bool default_value)
            {
                string output_string = Get(name, default_value.ToString());
                bool output = false;
                /// Some sh*t downthere;
                return bool.TryParse(output_string, out output) ? output : default_value;
            }

            public void Apply()
            {
                Console.WindowHeight = this.GetInteger(Height, Console.WindowHeight);
                Console.WindowWidth = this.GetInteger(Width, Console.WindowWidth);
            }
        }

        public static void Run(Setting setting)
        {
            int width = setting.GetInteger(Setting.Width, Console.WindowWidth);
            int height = setting.GetInteger(Setting.Height, Console.WindowHeight);
            int timeout = setting.GetInteger(Setting.Timeout, 100);
            bool isClearScreen = setting.GetBoolean(Setting.ClearScreen, true);
            bool isUpdateOnResize = setting.GetBoolean(Setting.UpdateOnResize, true);
            while (true)
            {
                int[] lineLength = new int[width];
                Random random = new Random();

                ///Renew width and height;
                if (isUpdateOnResize)
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                }

                if (isClearScreen)
                    Console.Clear();

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
            //current_setting.DEBUG_ShowParametrs();
            //Console.ReadLine();
            current_setting.Apply();
            MatrixScreen.Run(current_setting);
            Console.ReadLine();
        }
    
    }

}