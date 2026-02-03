using System.Diagnostics;

namespace MinorShift.Emuera
{
	internal static class ExternalBridge
	{
		internal static void SendCharaId(int id)
		{
			Debug.WriteLine($"[EXT] CHARA {id}");
		}

		internal static void SendString(string text)
		{
			Debug.WriteLine($"[EXT] STR {text}");
		}
	}
}
