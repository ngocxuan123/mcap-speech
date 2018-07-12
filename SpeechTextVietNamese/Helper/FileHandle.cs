using AxWMPLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTextVietNamese.Helper
{
	public class FileHandle
	{
		public static bool SaveStreamToFile(string url, string fileName)
		{
			using (FileStream file = new FileStream($"{GlobalVar.SpeechFolder}{fileName}.mp3", FileMode.Create, FileAccess.Write))
			{
				try
				{
					Stream ms = GetStreamFromUrl(url);
					byte[] bytes = new byte[ms.Length];
					ms.Read(bytes, 0, (int)ms.Length);
					file.Write(bytes, 0, bytes.Length);
					ms.Close();
					return true;
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public static string CheckExist(string fileName)
		{
			if (GlobalVar.SpeechFiles == null) return null;
			foreach (string item in GlobalVar.SpeechFiles)
			{
				if (TryHash.VerifyHashedPassword(item, fileName) == PasswordVerificationResult.Success)
				{
					return item;
				}
			}
			return null;
		}

		public static List<string> FilesInFolder(string path)
		{
			List<string> files = null;
			if (Directory.Exists(path))
			{
				string[] fileEntries = Directory.GetFiles(path);
				files = new List<string>();
				foreach (string item in fileEntries)
				{
					files.Add(item.Substring(0, item.LastIndexOf('.')));
				}
			}
			return files;
		}
		#region "Private"

		private static Stream GetStreamFromUrl(string url)
		{
			byte[] data = null;

			using (var wc = new System.Net.WebClient())
				data = wc.DownloadData(url);

			return new MemoryStream(data);
		}
		#endregion
	}
}
