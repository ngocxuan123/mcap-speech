using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpeechTextVietNamese.Helper
{
	class Recorder
	{
		WaveIn sourceStream;
		WaveFileWriter waveWriter;
		readonly string FilePath;
		readonly string FileName;
		readonly int InputDeviceIndex;
		Chart Chart;

		public Recorder(string filePath, string fileName, Chart chart,  int inputDeviceIndex = 0)
		{
			InputDeviceIndex = inputDeviceIndex;
			FileName = fileName;
			FilePath = filePath;
			Chart = chart;
		}

		public void StartRecording()
		{
			sourceStream = new WaveIn
			{
				DeviceNumber = this.InputDeviceIndex,
				WaveFormat =
					new WaveFormat(44100, WaveIn.GetCapabilities(InputDeviceIndex).Channels)
			};

			sourceStream.DataAvailable += SourceStreamDataAvailable;

			if (!Directory.Exists(FilePath))
			{
				Directory.CreateDirectory(FilePath);
			}

			waveWriter = new WaveFileWriter(FilePath + FileName, sourceStream.WaveFormat);
			sourceStream.StartRecording();
		}

		public void SourceStreamDataAvailable(object sender, WaveInEventArgs e)
		{
			if (waveWriter == null) return;
			waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
			Chart.Series["wave"].Points.Add(BitConverter.ToSingle(e.Buffer, 1));
			waveWriter.Flush();
		}

		public void RecordEnd()
		{
			if (sourceStream != null)
			{
				sourceStream.StopRecording();
				sourceStream.Dispose();
				sourceStream = null;
			}
			if (waveWriter == null)
			{
				return;
			}
			waveWriter.Dispose();
			waveWriter = null;
		}
	}
}
