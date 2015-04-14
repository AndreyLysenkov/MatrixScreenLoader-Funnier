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

    public class MatrixScreen
    {

        public delegate void Runner(Setting setting);

        public Runner Run;

        /// Note, what height and width set not in pixels... in smth else... he-he
        public class Setting
        {

            const char value_separator = '=';
            public const string Width = "/w";
            public const string Height = "/h";
            public const string Timeout = "/t";
            public const string ClearScreen = "/c";
            public const string UpdateOnResize = "/r";
            public const string Range = "/d";
            public const string Run = "/run";

            /// Yeah, I could use [,] instead of [][], but...
            /// I had some reasons about that weird form...
            /// And I couldn't remember them...
            /// Anyway, it doesn't look good...
            /// It doesn't look right eather...
            private string[][] _parametrs = 
                {
                    new string[] {Width, "smth"}, 
                    new string[] {Height, "smth"}, 
                    new string[] {Timeout, "50"},
                    new string[] {ClearScreen, "true"},
                    new string[] {UpdateOnResize, "true"},
                    new string[] {Range, "2"},
                    new string[] {Run, "0"}
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

            public void Apply(MatrixScreen matrix)
            {
                Console.WindowHeight = this.GetInteger(Height, Console.WindowHeight);
                Console.WindowWidth = this.GetInteger(Width, Console.WindowWidth);
                switch (GetInteger(Run, 0))
                {
                    case 1:
                        matrix.Run = Run_1;
                        break;
                    case 2:
                        matrix.Run = Run_2;
                        break;
                    default:
                        matrix.Run = Run_2;
                        break;
                }
            }
        
        }

        private static void Run_1(Setting setting)
        {
            int width = setting.GetInteger(Setting.Width, Console.WindowWidth);
            int height = setting.GetInteger(Setting.Height, Console.WindowHeight);
            int timeout = setting.GetInteger(Setting.Timeout, 100);
            bool isClearScreen = setting.GetBoolean(Setting.ClearScreen, true);
            bool isUpdateOnResize = setting.GetBoolean(Setting.UpdateOnResize, true);
            int range = setting.GetInteger(Setting.Range, 2);
            while (true)
            {
                ///Renew width and height;
                if (isUpdateOnResize)
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                }

                int[] lineLength = new int[width];
                Random random = new Random();

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
                        text += ((i <= lineLength[j]) ? random.Next(range).ToString() : " ");
                    }
                }
                Console.WriteLine(text);
                System.Threading.Thread.Sleep(timeout);
            }
        }

        public class Run_2_Matrix
        {
            const int MaxWidth = 128;
            const int MaxHeight = 128;

            char[,] matrix;
            int width;
            int height;
            List<Run_2_Line> lines;

            public Run_2_Matrix(int width, int height)
            {
                this.width = width;
                this.height = height;
                matrix = new char[MaxWidth, MaxHeight];
                lines = new List<Run_2_Line> { };
            }



            public void AddNewLine(int column, int start, int length, string random_line)
            {
                Run_2_Line line = new Run_2_Line(column, start, length);
                for (int i = 0, j = line.start; (j <= line.end); i++, j++)
                {
                    if (true)
                    {
                        matrix[j, line.column] = random_line[i];
                    }
                }
                this.lines.Add(line);
            }


        }

        public class Run_2_Line
        {
            public int column;
            public int start;
            public int end;

            public int Length
            {
                get { return end - start + 1; }
            }

            public Run_2_Line(int column, int start, int length)
            {
                this.column = column;
                this.start = start;
                this.end = start + length - 1;
            }
        
            public void ShiftDown(int shift_length)
            {
                start -= shift_length;
                end -= shift_length;
            }
        
        }

        private static void Run_2_Print(char[,] matrix, int width, int height)
        {
            string text = "";
            for(int i = 0; i < height; i++)
            {
                for(int j=0; j < width; j++)
                {
                    text += matrix[i, j];
                }
            }
            Console.WriteLine(text);
        }

        private static void Run_2(Setting setting)
        {
            int width = setting.GetInteger(Setting.Width, Console.WindowWidth);
            int height = setting.GetInteger(Setting.Height, Console.WindowHeight);
            int timeout = setting.GetInteger(Setting.Timeout, 100);
            bool isClearScreen = setting.GetBoolean(Setting.ClearScreen, true);
            bool isUpdateOnResize = setting.GetBoolean(Setting.UpdateOnResize, true);
            int range = setting.GetInteger(Setting.Range, 2);
            char[,] matrix = new char [128, 128]; ///(Don't ask me why);
            ///(new Run_2_Line(5, 3, 7)).AddToMatrix(ref matrix, "Hell Welcomes You");
            Run_2_Print(matrix, width, height);


            
        }

    }

    public static class Program
    {

        static void Main(string[] args)
        {
            MatrixScreen matrix = new MatrixScreen();
            MatrixScreen.Setting current_setting = new MatrixScreen.Setting();
            current_setting.Set(args);
            //current_setting.DEBUG_ShowParametrs();
            //Console.ReadLine();
            current_setting.Apply(matrix);
            matrix.Run(current_setting);
            Console.ReadLine();
        }
    
    }

}