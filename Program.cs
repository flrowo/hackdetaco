using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace hackdetaco
{
    class Program
    {
        static Color GetPixel(Point position)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }

        [STAThread]
        static void Main()
        {
            string key;
            String s;
            Color color;
            bool kontinue = true;
            while (kontinue) {
                color = GetPixel(System.Windows.Forms.Cursor.Position);
                s = color.ToString();
                //blue
                if (color.R >= 67 * 0.9 && color.R <= 67 * 1.1
                    && color.G >= 142 * 0.9 && color.G <= 142 * 1.1
                    && color.B >= 172 * 0.9 && color.B <= 172 * 1.1
                    && Keyboard.IsKeyDown(Key.LeftCtrl)) {
                    key = "{k}";
                    s = s +" "+ key;
                    PressKey(key);
                } 
                //red
                else if (color.R >= 235 * 0.9 && color.R <= 235 * 1.1
                    && color.G >= 69 * 0.9 && color.G <= 69 * 1.1
                    && color.B >= 44 * 0.9 && color.B <= 44 * 1.1
                    && Keyboard.IsKeyDown(Key.LeftCtrl)) {
                    key = "{j}";
                    s = s + " " + key;
                    PressKey(key);
                }
                if (Keyboard.IsKeyDown(Key.P)) kontinue = false;
                Console.WriteLine(s);
                //String path = @"C:\Users\ryzen2700x\Desktop\temp\WriteLines.txt";
                //string[] lines = { s };
                //System.IO.File.WriteAllLines(path, lines);
            }

        }

        public static void PressKey(String key) {
            int timeToWait = 1;
            SendKeys.SendWait(key);
            Console.WriteLine("Pressed "+key+"; Sleeping for "+timeToWait+" milliseconds...");
            Thread.Sleep(timeToWait);
        }
    }
}

