using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MinorShift.Emuera.GameView
{
    internal static class ExternalInput
    {
        private static Thread _thread;
        public static Action<string> OnLine;

        public static void Start()
        {
            if (_thread != null)
                return;

            _thread = new Thread(ReadLoop);
            _thread.IsBackground = true;
            _thread.Name = "ExternalInputReader";
            _thread.Start();
        }

        private static void ReadLoop()
        {
            try
            {
                while (true)
                {
                    string line = Console.ReadLine();
                    if (line == null)
                        break;

                    try
                    {
                        OnLine?.Invoke(line);
                    }
                    catch
                    {
                        // 这里先吞掉，避免回调炸死线程
                    }
                }
            }
            catch
            {
                // stdin 不可用时先静默失败
            }
        }
    }
}