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

		public static string CheckExist(Dictionary<string, string> owner, string fileName)
		{
			return owner.ContainsKey(fileName) ? owner[fileName] : null;
		}

		public static Dictionary<string, string> FilesInFolder(string path)
		{
			Dictionary<string, string> files = null;
			if (Directory.Exists(path))
			{
				string[] fileEntries = Directory.GetFiles(path);
				files = new Dictionary<string, string>();
				foreach (string item in fileEntries)
				{
					files.Add(Path.GetFileNameWithoutExtension(item), $"{item}" );
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
