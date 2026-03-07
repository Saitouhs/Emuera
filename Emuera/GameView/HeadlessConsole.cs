using System;
using MinorShift.Emuera.Sub;

namespace MinorShift.Emuera.GameView
{
	internal sealed class HeadlessConsole : IConsoleHost
	{
		private bool running = true;
		private string windowTitle = string.Empty;

		public bool Enabled { get { return true; } }
		public bool IsRunning { get { return running; } }

		public void Print(string text)
		{
			WriteJson("print", text);
		}

		public void PrintSingleLine(string text, bool temporary = false)
		{
			WriteJson("print_single_line", text, "temporary", temporary ? "true" : "false");
		}

		public void PrintSystemLine(string text)
		{
			WriteJson("print_system_line", text);
		}

		public void PrintError(string text)
		{
			WriteJson("print_error", text);
		}

		public void PrintErrorButton(string text, ScriptPosition pos)
		{
			if (pos == null)
			{
				WriteJson("print_error_button", text);
				return;
			}
			WriteJson("print_error_button", text,
				"filename", pos.Filename,
				"line", pos.LineNo.ToString());
		}

		public void PrintFlush(bool force)
		{
			WriteJson("print_flush", null, "force", force ? "true" : "false");
		}

		public void RefreshStrings(bool forcePaint)
		{
			WriteJson("refresh_strings", null, "force_paint", forcePaint ? "true" : "false");
		}

		public void SetWindowTitle(string text)
		{
			windowTitle = text ?? string.Empty;
			WriteJson("set_window_title", windowTitle);
		}

		public void setStBar(string barStr)
		{
			WriteJson("set_st_bar", barStr);
		}

		public void ReadAnyKey(bool anykey = false, bool stopMesskip = false)
		{
			WriteJson("read_any_key", null,
				"anykey", anykey ? "true" : "false",
				"stop_messkip", stopMesskip ? "true" : "false");
		}

		public void Quit()
		{
			running = false;
			WriteJson("quit", null);
		}

		public void ThrowError(bool playSound)
		{
			running = false;
			WriteJson("throw_error", null, "play_sound", playSound ? "true" : "false");
		}

		private static void WriteJson(string type, string text)
		{
			WriteJson(type, text, null, null);
		}

		private static void WriteJson(string type, string text, string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				if (text == null)
					Console.Out.WriteLine("{\"type\":\"" + Escape(type) + "\"}");
				else
					Console.Out.WriteLine("{\"type\":\"" + Escape(type) + "\",\"text\":\"" + Escape(text) + "\"}");
				return;
			}
			if (text == null)
				Console.Out.WriteLine("{\"type\":\"" + Escape(type) + "\",\"" + Escape(key) + "\":\"" + Escape(value) + "\"}");
			else
				Console.Out.WriteLine("{\"type\":\"" + Escape(type) + "\",\"text\":\"" + Escape(text) + "\",\"" + Escape(key) + "\":\"" + Escape(value) + "\"}");
		}

		private static void WriteJson(string type, string text, string key1, string value1, string key2, string value2)
		{
			Console.Out.WriteLine("{\"type\":\"" + Escape(type)
				+ "\",\"" + Escape(key1) + "\":\"" + Escape(value1)
				+ "\",\"" + Escape(key2) + "\":\"" + Escape(value2)
				+ (text == null ? "\"}" : "\",\"text\":\"" + Escape(text) + "\"}"));
		}

		private static string Escape(string value)
		{
			if (value == null)
				return string.Empty;
			return value
				.Replace("\\", "\\\\")
				.Replace("\"", "\\\"")
				.Replace("\r", "\\r")
				.Replace("\n", "\\n")
				.Replace("\t", "\\t");
		}
	}
}
