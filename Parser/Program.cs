using System;
using System.IO;

public class RobloxUserIdParser
{
    public static void Main(string[] args)
    {
        // Define file names (assuming they are in the same directory as Program.cs)
        string inputFileName = "input.txt";
        string outputFileName = "AccountList.lua";

        // Get the full file paths (combine directory and file names)
        string inputFilePath = Path.Combine(Environment.CurrentDirectory, inputFileName);
        string outputFilePath = Path.Combine(Environment.CurrentDirectory, outputFileName);

        // Process the file
        ProcessFile(inputFilePath, outputFilePath);
    }

    private static void ProcessFile(string inputPath, string outputPath)
    {
        // Read the input file line by line
        List<string> processedLines = new List<string>();
        try
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    processedLines.Add(ProcessLine(line));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading input file: " + ex.Message);
            return;
        }

        // Write the processed lines to the output file
        try
        {
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("local module = {");
                foreach (string line in processedLines)
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine("}");
                writer.WriteLine("");
                writer.WriteLine("return module");
            }
            Console.WriteLine("Successfully processed and wrote user IDs to output file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to output file: " + ex.Message);
        }
    }

    private static string ProcessLine(string line)
    {
        // Remove "https://www.roblox.com/users/" from the beginning
        line = line.Substring(29);

        // Find and remove the next "/" and everything after it
        int slashIndex = line.IndexOf('/');
        if (slashIndex > -1)
        {
            line = line.Substring(0, slashIndex);
        }

        // Add quotes and comma
        return "\t\"" + line + "\",";
    }
}
