using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


public class AudioPlayer
{
    private Process? ffmpegProcess;
    private bool isPaused = false;
    private bool isPlaying = false;
    private bool stopRequested = false;
    private Task visualizationTask;

    public void PlayAudio(string filePath)
    {
        if (ffmpegProcess != null && !ffmpegProcess.HasExited)
        {
            Console.WriteLine("A song is already playing..");
            return;
        }

        stopRequested = false;
        isPlaying = true;

        ffmpegProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "ffplay",
                Arguments = $"-nodisp -autoexit \"{filePath}\"",
                // Enable controls via input stream
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        ffmpegProcess.Start();

        visualizationTask = Task.Run(() =>
        {
            VisualizeWaveForm(filePath);
        });

        Task.Run(() =>
        {
            ffmpegProcess.WaitForExit();
            isPlaying = false;
            stopRequested = true;
        });
    }

    public void PauseAudio()
    {
        if (ffmpegProcess != null && !ffmpegProcess.HasExited)
        {
            if (!isPaused)
            {
                ffmpegProcess.StandardInput.WriteLine("p");
                isPaused = true;
                Console.WriteLine("Paused!");
            }
            else
            {
                ffmpegProcess.StandardInput.WriteLine("p");
                isPaused = false;
                Console.WriteLine("Resumed playing...");
            }
        }
    }

    public void StopAudio()
    {
        if (ffmpegProcess != null && !ffmpegProcess.HasExited)
        {
            ffmpegProcess.Kill();
            ffmpegProcess.Dispose();
            ffmpegProcess = null;
            Console.WriteLine("Player stopped!");
        }
    }

    private void VisualizeWaveForm(string filePath)
    {
        Random random = new Random();
        //Console.CancelKeyPress();
    }

    // Code below discarded!

    /*
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
        */
}
