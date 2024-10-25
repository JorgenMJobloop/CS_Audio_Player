using System;
using System.Diagnostics;
using System.Threading;


public class AudioPlayer
{
    public void PlayAndVisualizeAudio(string filePath)
    {
        var ffmpegProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "ffplay", // use ffmpeg to play the audio files
                Arguments = $"-autoexit -nodisp \"{filePath}\"",
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        ffmpegProcess.Start();

        VisualizeWaveform(filePath);

        ffmpegProcess.WaitForExit();
    }


    private void VisualizeWaveform(string filePath)
    {
        float maxAmplitude = 0.0f;
        float minAmplitude = 0.0f;

        for (int i = 0; i < 100; i++) // Run 100 visualizations as a simulation
        {
            // generate random amplitude
            maxAmplitude = (float)(new Random().NextDouble());
            minAmplitude = (float)(-new Random().NextDouble());

            Console.Clear();
            Console.WriteLine($"Currently playing: {filePath.ToString()}");
            Console.WriteLine("Max amplitude: " + maxAmplitude.ToString("F2"));
            Console.WriteLine("Min amplitude: " + minAmplitude.ToString("F2"));

            // Visualize amplitude
            int maxBarWidth = (int)(maxAmplitude * 50);
            int minBarWidth = (int)(Math.Abs(minAmplitude) * 50);

            Console.WriteLine(new string('#', maxBarWidth));
            Console.WriteLine(new string('-', minBarWidth));
            // pause between visualisation
            Thread.Sleep(100);
        }
    }
}
