using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTextVietNamese.Helper
{
	public static class GlobalVar
	{
		/// <summary>
		/// Global variable that is list files.
		/// </summary>
		public static Dictionary<string, string> SpeechFiles = new Dictionary<string, string>();

		/// <summary>
		/// Forder speech
		/// </summary>
		public static string SpeechFolder = @"E:\Speech\";
	}
}
