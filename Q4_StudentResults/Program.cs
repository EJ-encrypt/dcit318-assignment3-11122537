using System;
using System.Collections.Generic;
using System.IO;
using Q4_StudentResults.Models;

namespace Q4_StudentResults
{
    class Program
    {
        static void Main(string[] args)
        {
            // Allow passing input and output paths via command-line; otherwise use defaults in project folder
            var inputPath = args.Length > 0 ? args[0] : "input.txt";
            var outputPath = args.Length > 1 ? args[1] : "report.txt";

            Console.WriteLine("Q4_StudentResults - Student grading from text file");
            Console.WriteLine($"Input file: {Path.GetFullPath(inputPath)}");
            Console.WriteLine($"Output file: {Path.GetFullPath(outputPath)}");
            Console.WriteLine();

            // If input does not exist, create a sample input file to help you get started
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file not found. Creating a sample input file: input.txt\n");
                File.WriteAllLines(inputPath, new[]
                {
                    "# Sample input -- Id,Name,Score",
                    "101, Alice Smith, 84",
                    "102, Bob Joe, 71",
                    "103, Charlie K, 45",
                    "104, Dana, 59",
                    "105, Ed , 38",
                    "badline without enough fields",
                    "106, Frank, notanumber"
                });
                Console.WriteLine("Sample input.txt created. Rerun the program or continue to process this sample automatically.\n");
            }

            var processor = new StudentResultProcessor();
            var students = processor.ReadStudentsFromFile(inputPath, out List<string> errors);

            // Print any parsing errors
            if (errors.Count > 0)
            {
                Console.WriteLine("Parsing errors encountered:");
                foreach (var e in errors)
                    Console.WriteLine(" - " + e);
                Console.WriteLine();
            }

            // Print parsed students
            Console.WriteLine($"Parsed {students.Count} valid student(s):");
            foreach (var s in students)
            {
                Console.WriteLine(" - " + s);
            }
            Console.WriteLine();

            // Write report
            try
            {
                processor.WriteReportToFile(students, outputPath);
                Console.WriteLine($"Report written successfully to: {Path.GetFullPath(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to write report: " + ex.Message);
            }

            Console.WriteLine("\nDone. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
