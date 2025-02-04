using System.Diagnostics;

namespace CS_Audio_Player;

class Program
{
    static void Main(string[] args)
    {
        AudioPlayer audioPlayer = new AudioPlayer();
        HandleSampleRate handleSampleRate = new HandleSampleRate();

        Console.WriteLine("Enter the filepath of the song you wish to play");

        string? filePath = Console.ReadLine();

        int sampleRate = handleSampleRate.GetSampleRate(filePath);
        Console.WriteLine($"Playing sample {filePath} rate: {sampleRate} hz");


        Console.WriteLine("Commands: 'play', 'pause', 'stop'");
        while (true)
        {
            string? commands = Console.ReadLine().ToLower();
            if (string.IsNullOrEmpty(commands))
            {
                Console.WriteLine("There was an error processing the file..");
                break;
            }
            switch (commands)
            {
                case "play":
                    audioPlayer.PlayAudio(filePath);
                    break;
                case "pause":
                    audioPlayer.PauseAudio();
                    break;
                case "stop":
                    audioPlayer.StopAudio();
                    break;
                default:
                    Console.WriteLine("Unknown command! Exiting program..");
                    break;
            }
        }
    }
}
