namespace CS_Audio_Player;

class Program
{
    static void Main(string[] args)
    {
        AudioPlayer audioPlayer = new AudioPlayer();

        if (args.Length < 1)
        {
            Console.WriteLine("Usage: dotnet run <path_to_audio_file>");
            return;
        }
        string filePath = args[0];

        audioPlayer.PlayAndVisualizeAudio(filePath);
    }
}
