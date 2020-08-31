using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace hackdetaco
{
    class KeysToPress
    {
        string[] KeysL = {"{f}", "{d}"};
        string[] KeysR = {"{j}", "{k}"};
        bool RightHand;

        public KeysToPress() {
            RightHand = false;
        }

        public string GetNext(bool isColorBlue) {
            int i = 0;
            if (isColorBlue) {
                i = 1;
            }
            string r;
            if (RightHand) r = KeysR[i];
            else r = KeysL[i];
            RightHand = !RightHand;
            return r;
        }

    }
    class Program
    {
        static bool didPressKey;
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
            KeysToPress ktp = new KeysToPress();
            didPressKey = false;

            while (kontinue) {
                color = GetPixel(System.Windows.Forms.Cursor.Position);
                s = color.ToString();
                //blue
                if (color.R >= 67 * 0.9 && color.R <= 67 * 1.1
                    && color.G >= 142 * 0.9 && color.G <= 142 * 1.1
                    && color.B >= 172 * 0.9 && color.B <= 172 * 1.1
                    && Keyboard.IsKeyDown(Key.LeftCtrl)) {
                    key = ktp.GetNext(true);
                    s = s +" "+ key;
                    PressKey(key);
                    //didPressKey = false;
                } 
                //red
                else if (color.R >= 235 * 0.9 && color.R <= 235 * 1.1
                    && color.G >= 69 * 0.9 && color.G <= 69 * 1.1
                    && color.B >= 44 * 0.9 && color.B <= 44 * 1.1
                    && Keyboard.IsKeyDown(Key.LeftCtrl)) {
                    key = ktp.GetNext(false);
                    s = s + " " + key;
                    PressKey(key);
                    //didPressKey = false;
                }

                /*if (didPressKey) { 
                    Thread.Sleep(10);
                    didPressKey = false;
                }*/

                if (Keyboard.IsKeyDown(Key.P)) kontinue = false;
                Console.WriteLine(s);
            }

        }

        public static void PressKey(String key) {
            int timeToWait = 1;
            SendKeys.SendWait(key);
            Console.WriteLine("Pressed "+key+"; Sleeping for "+timeToWait+" milliseconds...");
            Thread.Sleep(timeToWait);
            //didPressKey = true;
        }
    }
}
