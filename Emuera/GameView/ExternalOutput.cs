using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinorShift.Emuera.GameView
{
    internal static class ExternalOutput
    {
        public static bool Enabled = true;

        public static void Send(string type, string text)
        {
            if (!Enabled || text == null)
                return;

            try
            {
                // 第一阶段先最土：直接打到标准输出
                Console.WriteLine($"[EMUERA-{type}] {text}");
                Console.Out.Flush();
            }
            catch
            {
                // 旁路失败不能影响原程序
            }
        }
    }
}