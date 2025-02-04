using System.IO;
using System;

public class HandleSampleRate
{
    public int GetSampleRate(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!");
            return -1;
        }

        byte[] header = new byte[44];
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            fileStream.Read(header, 0, 44);
        }

        int sampleRate = BitConverter.ToInt32(header, 24);
        return sampleRate;
    }
}