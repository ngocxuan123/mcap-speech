using NAudio.Wave;
using SpeechTextVietNamese.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechTextVietNamese
{
	public partial class Form1 : Form
	{
		SpeechGoogle Speecher = new SpeechGoogle();
		private Recorder recorder;
		public Form1()
		{
			InitializeComponent();
			chart.Series.Add("wave");
			chart.Series["wave"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			chart.Series["wave"].ChartArea = "ChartArea1";
			GlobalVar.SpeechFiles = FileHandle.FilesInFolder(GlobalVar.SpeechFolder);
		}
		private void btnSpeech_Click(object sender, EventArgs e)
		{
			Speecher.Speech(textTTL.Text.Trim());
		}

		private void btnRecorder_Click(object sender, EventArgs e)
		{
			recorder.RecordEnd();
		}

		private void speechSignal()
		{
			WaveChannel32 wave = new WaveChannel32(new WaveFileReader($"{GlobalVar.SpeechFolder}abc.mp3"));
			int sampleSize = 1024;
			var bufferSize = 16384 * sampleSize;
			var buffer = new byte[bufferSize];
			int read = 0;
			chart.Series.Add("wave");
			chart.Series["wave"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			chart.Series["wave"].ChartArea = "ChartArea1";
			while (wave.Position < wave.Length)
			{
				read = wave.Read(buffer, 0, bufferSize);
				for (int i = 0; i < read / sampleSize; i++)
				{
					var point = BitConverter.ToSingle(buffer, i * sampleSize);

					chart.Series["wave"].Points.Add(point);
				}
			}
		}
		private void btnStart_Click(object sender, EventArgs e)
		{
			int waveInDevices = WaveIn.DeviceCount;
			var a = WaveIn.GetCapabilities(0);
			recorder = new Recorder(GlobalVar.SpeechFolder, "abc.mp3", chart, 0);
			recorder.StartRecording();
		}
	}
}
