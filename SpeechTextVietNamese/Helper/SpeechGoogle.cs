using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxWMPLib;
using System.Threading;

namespace SpeechTextVietNamese.Helper
{
	public class SpeechGoogle : ISpeech
	{
		private AxWindowsMediaPlayer _player;
		private string _source = $"https://translate.google.com/translate_tts?ie=UTF-8&tl=vi-VN&client=tw-ob&q=";
		public AxWindowsMediaPlayer Player
		{
			get
			{
				if (_player == null)
				{
					_player = new AxWindowsMediaPlayer();
					_player.CreateControl();
					_player.settings.volume = 100;
				}
				return _player;
			}
		}
		public void Speech(string source)
		{
			string[] names = source.Trim().Split(' ');
			WMPLib.IWMPPlaylist playlist;
			WMPLib.IWMPMedia media;
			try
			{
				//Player.playlistCollection.setDeleted(Player.playlistCollection.getByName("speech").Item(0), true);
				playlist = Player.playlistCollection.newPlaylist("speech");
				playlist.clear();
				foreach (string name in names)
				{
					//string hashName = FileHandle.CheckExist(name);
					if (GlobalVar.SpeechFiles != null && GlobalVar.SpeechFiles.Count > 0 && GlobalVar.SpeechFiles.ContainsKey(name.ToLower()))
					{
						media = Player.newMedia(GlobalVar.SpeechFiles[name.ToLower()]);
						playlist.appendItem(media);
					} else
					{
						if (FileHandle.SaveStreamToFile(_source + name, name.ToLower()))
						{
							GlobalVar.SpeechFiles.Add(name.ToLower(), $"{GlobalVar.SpeechFolder}{name.ToLower()}.mp3");
							media = Player.newMedia(GlobalVar.SpeechFiles[name.ToLower()]);
							playlist.appendItem(media);
						}
					}
				}
				Player.currentPlaylist = playlist;
				Speech();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private void Speech()
		{
			try
			{
				Player.Ctlcontrols.play();
			}
			catch (Exception e)
			{
				throw e;
			}
			
		}
	}
}
