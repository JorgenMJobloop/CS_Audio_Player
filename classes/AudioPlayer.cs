using NAudio.Wave;


public class AudioPlayer
{
    public void PlayAndVisualizeAudio(string filePath)
    {
        using (var audioFile = new AudioFileReader(filePath))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();

            // buffer that reads the amplitudes
            float[] buffer = new float[1024];
            int samplesRead;

            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                // reads the amplitude from the audiofile
                samplesRead = audioFile.Read(buffer, 0, buffer.Length);

                if (samplesRead > 0)
                {
                    VisualizeWaveform(buffer, samplesRead);
                }
                // limit the virtualization rate
                Thread.Sleep(50);
            }
        }
    }

    public void VisualizeWaveform(float[] buffer, int samplesRead)
    {
        // find max amplitude and visualize it
        float maxAmplitude = 0;

        for (int i = 0; i < samplesRead; i++)
        {
            maxAmplitude = Math.Max(maxAmplitude, Math.Abs(buffer[i]));
        }

        // convert amplitude to a size that can be displayed in the terminal
        int visualizeWidth = (int)(maxAmplitude * 50); // scale to width between 0 and 50

        // draw the amplitude as a line
        Console.Clear();
        Console.Write($"Amplitude: {maxAmplitude.ToString("F2")}");
        Console.WriteLine(new string("#"), visualizeWidth);
    }
}
