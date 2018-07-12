using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechTextVietNamese
{
	public static class GlobalVar
	{
		/// <summary>
		/// Global variable that is list files.
		/// </summary>
		public static List<string> SpeechFiles = new List<string>();

		/// <summary>
		/// Forder speech
		/// </summary>
		public static string SpeechFolder = @"E:\Speech\";
	}

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
