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
            public const string Width = "/width";
            public const string Height = "/height";
            public const string Timeout = "/timeout";
            public const string ClearScreen = "/clear";
            public const string UpdateOnResize = "/resize";
            public const string Range = "/range";
            public const string Run = "/run";
            public const string Run2_FileOfLines = "/linesFile";
            public const string Run2_MinLinesLength = "/minLinesLength";
            public const string Run2_MaxLinesLength = "/maxLinesLength";
            public const string Run2_LinesCount = "/linesCount";
            public const string Run2_GenerateTimeout = "/lineTimeout";
            public const string Run2_LinesCharMin = "/linesCharCodeMin";
            public const string Run2_LinesCharMax = "/linesCharCodeMax";
            public const string Run2_LineSpeed = "/linesSpeed";

            /// Yeah, I could use [,] instead of [][], but...
            /// I had some reasons about that weird form...
            /// And I couldn't remember them...
            /// Anyway, it doesn't look good...
            /// It doesn't look right eather...
            private string[][] _parametrs = 
                {
                    new string[] {Width, "smth"}, 
                    new string[] {Height, "smth"}, 
                    new string[] {Timeout, "default"},
                    new string[] {ClearScreen, "true"},
                    new string[] {UpdateOnResize, "true"},
                    new string[] {Range, "2"},
                    new string[] {Run, "0"},
                    new string[] {Run2_FileOfLines, "none"},
                    new string[] {Run2_MinLinesLength, "default"},
                    new string[] {Run2_MaxLinesLength, "default"},
                    new string[] {Run2_LinesCount, "defalt"},
                    new string[] {Run2_GenerateTimeout, "default"},
                    new string[] {Run2_LinesCharMin, "default"},
                    new string[] {Run2_LinesCharMax, "default"},
                    new string[] {Run2_LineSpeed, "default"}
                };

            public Setting()
            { }

            public void Set(string[] args)
            {
                if (args == null)
                    return;
                for (int i = 0; (i < args.Length); i++)
                {
                    string parametr = args[i];
                    if ((parametr == null) || (parametr.Length == 0))
                        continue;
                    string[] split_parametr = parametr.Split(new char[] { value_separator }, 2);
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
            public const char Filler = ' ';

            const int MaxWidth = 256;
            const int MaxHeight = 128;

            char[,] matrix;
            int width;
            int height;
            List<Run_2_Line> lines;

            public Run_2_Matrix(int width, int height)
            {
                this.width = width;
                this.height = height;
                matrix = new char[MaxHeight, MaxWidth];
                for (int i = 0; i < MaxHeight; i++)
                    for (int j = 0; j < MaxWidth; j++)
                        matrix[i, j] = Filler;
                lines = new List<Run_2_Line> { };
            }

            private bool CheckColumnIndex(int i)
            {
                return (i >= 0) && (i < MaxWidth);
            }

            private bool CheckRowIndex(int i)
            {
                return (i >= 0) && (i < MaxHeight);
            }

            private bool CheckIndexes(int row, int column)
            {
                return CheckColumnIndex(column) && CheckRowIndex(row);
            }

            public char this[int index1, int index2]
            {
                get
                {
                    return CheckIndexes(index1, index2) ? matrix[index1, index2] : Filler;
                }

                set
                {
                    if (CheckIndexes(index1, index2))
                        matrix[index1, index2] = value;
                }
            }

            public bool IsLineCross(Run_2_Line line)
            {
                bool output = false;
                for (int i = 0; (i < lines.Count) && (!output); i++)
                {
                    output = output || line.IsCrossWith(lines[i]);
                }
                return output;
            }

            //public void AddNewLine(int column, int start, int length, string random_line)
            //{
            //    Run_2_Line line = new Run_2_Line(column, start, length, random_line);
            //    this.AddNewLine(line);
            //}

            public void AddNewLine(Run_2_Line line)
            {
                for (int i = 0, j = line.start; (j <= line.end); i++, j++)
                {
                    this[j, line.column] = line.GetSymbol();
                }
                this.lines.Add(line);
            }

            public void ShiftLines()
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    Run_2_Line line = lines[i];
                    int index1 = line.start;
                    int index2 = line.column;
                    int index1_2 = line.end + 1;
                    for (int j = 0; j < line.speed; j++, index1++, index1_2++)
                    {
                        this[index1, index2] = Filler;
                        this[index1_2, index2] = line.GetSymbol();
                    }
                    line.ShiftDown();
                }
            }

        }

        public class Run_2_Line
        {
            public int column;
            public int start;
            public int end;
            public string random_line;
            public int random_line_position;
            public int speed;

            public int Length
            {
                get { return end - start + 1; }
            }

            public Run_2_Line(int column, int start, int length, string random_line, int speed)
            {
                this.column = column;
                this.start = start;
                this.end = start + length - 1;
                this.random_line = random_line;
                this.random_line_position = 0;
                this.speed = speed;
            }

            public Run_2_Line(int column, string random_line, int speed)
            {
                this.column = column;
                this.start = -random_line.Length;
                this.end = 0;
                this.random_line = random_line;
                this.random_line_position = 0;
                this.speed = speed;
            }

            public void ShiftDown()
            {
                start += speed;
                end += speed;
            }

            private bool CheckLinePosition()
            {
                return random_line_position < random_line.Length;
            }

            public char GetSymbol()
            {
                if (!CheckLinePosition())
                    this.random_line_position = 0;
                return this.random_line[this.random_line_position++]; ///Not sure;
            }

            private bool IsBelong(int digit, int diapason_start, int diapason_end)
            {
                return (digit >= diapason_start) && (digit <= diapason_end);
            }

            public bool IsCrossWith(Run_2_Line line)
            {
                return IsBelong(this.start, line.start, line.end) || IsBelong(this.end, line.start, line.end);
            }

        }

        private static void Run_2_Print(Run_2_Matrix matrix, int width, int height)
        {
            string text = "";
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    text += matrix[i, j];
                }
            }
            Console.WriteLine(text);
        }

        private static string GenerateRandomString(Random random, int length, int min, int max)
        {
            string output = "";
            for (int i = 0; i < length; i++)
            {
                output += (char)random.Next(min, max);
            }
            return output;
        }

        private static void Run_2(Setting setting)
        {

            //public const string Run2_FileOfLines = "/linesFile";
            int width = setting.GetInteger(Setting.Width, Console.WindowWidth);
            int height = setting.GetInteger(Setting.Height, Console.WindowHeight);
            int timeout = setting.GetInteger(Setting.Timeout, 50);
            bool isClearScreen = setting.GetBoolean(Setting.ClearScreen, true);
            bool isUpdateOnResize = setting.GetBoolean(Setting.UpdateOnResize, true);
            int linesAmount = setting.GetInteger(Setting.Run2_LinesCount, 2713);
            int generateTimeout = setting.GetInteger(Setting.Run2_GenerateTimeout, -7);
            Run_2_Matrix matrix = new Run_2_Matrix(width, height);
            Random random = new Random();
            int min = setting.GetInteger(Setting.Run2_MinLinesLength, 1);
            int max = setting.GetInteger(Setting.Run2_MaxLinesLength, 13);
            int charMin = setting.GetInteger(Setting.Run2_LinesCharMin, 1024*1024 + 48);
            int charMax = setting.GetInteger(Setting.Run2_LinesCharMax, 1024*1024 + 50);
            int maxSpeed = setting.GetInteger(Setting.Run2_LineSpeed, 13);
            for (int linesCounter = 0; true; linesCounter++)
            {

                if (isUpdateOnResize)
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                }

                if ((generateTimeout < 0) || ((linesCounter % generateTimeout == 0) && (linesAmount > 0)))
                {
                    for (int i = 0; i < ((generateTimeout < 0) ? -generateTimeout : 1); i++)
                    {
                        Run_2_Line line = new Run_2_Line(random.Next(0, width), GenerateRandomString(random, random.Next(min, max), charMin, charMax), random.Next(1, maxSpeed));
                        matrix.AddNewLine(line);
                        linesAmount--;
                    }
                }

                matrix.ShiftLines();

                if (isClearScreen)
                    Console.Clear();
                Run_2_Print(matrix, width, height);



                System.Threading.Thread.Sleep(timeout);
            }
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