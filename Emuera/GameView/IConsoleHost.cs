using MinorShift.Emuera.Sub;

namespace MinorShift.Emuera.GameView
{
	internal interface IConsoleHost
	{
		bool Enabled { get; }
		bool IsRunning { get; }
		void Print(string text);
		void PrintSingleLine(string text, bool temporary = false);
		void PrintSystemLine(string text);
		void PrintError(string text);
		void PrintErrorButton(string text, ScriptPosition pos);
		void PrintFlush(bool force);
		void RefreshStrings(bool forcePaint);
		void SetWindowTitle(string text);
		void setStBar(string barStr);
		void ReadAnyKey(bool anykey = false, bool stopMesskip = false);
		void Quit();
		void ThrowError(bool playSound);
	}
}
