using System.Diagnostics;

namespace MinorShift.Emuera
{
	internal static class ExternalBridge
	{
		internal static void SendCharaId(int id)
		{
			Debug.WriteLine($"[EXT] CHARA {id}");
		}
	}
}
