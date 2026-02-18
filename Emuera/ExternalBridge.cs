using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace MinorShift.Emuera
{
	internal static class ExternalBridge
    {
		internal static void SendCharaId(int id)
		{
			Debug.WriteLine($"[EXT] CHARA {id}");

            using (var clientPipe = new NamedPipeClientStream(
                ".",
                "testpipe",
                PipeDirection.InOut,
				PipeOptions.None)){
                try
                {
                    clientPipe.Connect(500);

                    // AutoFlush=true：确保立刻发出去
                    using (var writer = new StreamWriter(clientPipe))
                    {
                        writer.WriteLine($"[EXT] CHARA {id}");
                        writer.Flush(); // 旧写法里建议显式 Flush
                    }

                    // 写完就结束 using，clientPipe 关闭——server 应该能正常 Disconnect 后继续等
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("[client] Pipe connection timed out.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"[client] Pipe IO error: {ex.Message}");
                }
            }

            
        }

		internal static void SendString(string text)
		{
			Debug.WriteLine($"[EXT] STR {text}");
		}

	}
}
/*
 using System.IO.Pipes;
using System.Text;
class Program
{
    static void Main()
    {
        using var pipeStream = new NamedPipeServerStream(
            "testpipe",
            PipeDirection.InOut,
            1,
            PipeTransmissionMode.Byte,  // 行协议更稳
            PipeOptions.None);

        Console.WriteLine("Pipe server waiting for connection ...");

        while (true)
        {
            try
            {
                pipeStream.WaitForConnection();
                Console.WriteLine("Client connected.");

                // leaveOpen=true：不要让 reader 把 pipeStream 关了
                using var rdr = new StreamReader(pipeStream, Encoding.UTF8, false, 1024, leaveOpen: true);

                // ReadLine: client 断开时可能返回 null
                var line = rdr.ReadLine();
                if (line == null)
                {
                    Console.WriteLine("Client disconnected (no data).");
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now}: {line}");
                }
            }
            catch (IOException ex)
            {
                // client 提前断开 / 管道状态变化时常见
                Console.WriteLine($"[server] IO: {ex.Message}");
            }
            finally
            {
                if (pipeStream.IsConnected)
                    pipeStream.Disconnect(); // 关键：断开本次连接，准备下一次 WaitForConnection
            }
        }
    }
}
*/