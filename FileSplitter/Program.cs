using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSplitter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var inputFolder = ConfigurationManager.AppSettings["inputFolder"];
                var outputFolder = ConfigurationManager.AppSettings["outputFolder"];
                var searchPattern = ConfigurationManager.AppSettings["searchPattern"];
                var files = Directory.GetFiles(inputFolder, searchPattern);

                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                foreach (var file in files)
                {
                    Console.WriteLine("Reading file: " + file);
                    var lines = File.ReadAllLines(file);

                    if (lines.Length == 0)
                    {
                        Console.WriteLine("File: " + file + " is empty");
                        continue;
                    }
                    var header = lines[0];

                    using (var outputFile1 = File.CreateText(outputFolder + "\\1" + Path.GetFileName(file)))
                    {
                        using (var outputFile2 = File.CreateText(outputFolder + "\\2" + Path.GetFileName(file)))
                        {
                            outputFile1.WriteLine(header);
                            outputFile2.WriteLine(header);
                            for (int i = 1; i < lines.Length; i++)
                            {
                                if (i <= lines.Length / 2)
                                {
                                    outputFile1.WriteLine(lines[i]);
                                }
                                else
                                {
                                    outputFile2.WriteLine(lines[i]);
                                }
                            }
                        }
                    }
                    Console.WriteLine("Created file: " + outputFolder + "\\1" + Path.GetFileName(file));
                    Console.WriteLine("Created file: " + outputFolder + "\\2" + Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
            }
        }
    }
}
