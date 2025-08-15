using System;
using System.Collections.Generic;
using System.IO;
using Q4_StudentResults.Models;
using Q4_StudentResults.Exceptions;

namespace Q4_StudentResults
{
    public class StudentResultProcessor
    {
        /// <summary>
        /// Reads students from a text file where each line contains:
        /// Id,Name,Score
        /// Lines that cannot be parsed are reported in the 'errors' out parameter.
        /// </summary>
        /// <param name="path">Path to the input text file</param>
        /// <param name="errors">Out list of error messages encountered while parsing</param>
        /// <returns>List of successfully parsed Student objects</returns>
        public List<Student> ReadStudentsFromFile(string path, out List<string> errors)
        {
            var students = new List<Student>();
            errors = new List<string>();

            if (!File.Exists(path))
            {
                errors.Add($"Input file not found: {Path.GetFullPath(path)}");
                return students;
            }

            var lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                var lineNumber = i + 1;
                var raw = lines[i].Trim();

                if (string.IsNullOrEmpty(raw)) continue; // skip empty lines
                if (raw.StartsWith("#")) continue;       // allow comment lines starting with #

                // We expect comma-separated fields: Id,Name,Score
                var parts = raw.Split(',');

                if (parts.Length < 3)
                {
                    // missing field(s)
                    errors.Add($"Line {lineNumber}: missing field(s). Expected format: Id,Name,Score. Line: \"{raw}\"");
                    continue;
                }

                // Trim each part
                var idPart = parts[0].Trim();
                var namePart = parts[1].Trim();
                var scorePart = parts[2].Trim();

                // Validate fields
                if (string.IsNullOrEmpty(idPart) || string.IsNullOrEmpty(namePart) || string.IsNullOrEmpty(scorePart))
                {
                    errors.Add($"Line {lineNumber}: one or more fields are empty. Line: \"{raw}\"");
                    continue;
                }

                // Parse ID
                if (!int.TryParse(idPart, out int id))
                {
                    errors.Add($"Line {lineNumber}: invalid Id '{idPart}'. Line: \"{raw}\"");
                    continue;
                }

                // Parse Score
                if (!int.TryParse(scorePart, out int score))
                {
                    // throw or report custom exception - here we report and continue
                    errors.Add($"Line {lineNumber}: invalid score format '{scorePart}'. Line: \"{raw}\"");
                    continue;
                }

                // Score range check (optional): ensure 0..100
                if (score < 0 || score > 100)
                {
                    errors.Add($"Line {lineNumber}: score out of range 0-100: {score}. Line: \"{raw}\"");
                    continue;
                }

                // All good -> create Student
                var student = new Student(id, namePart, score);
                students.Add(student);
            }

            return students;
        }

        /// <summary>
        /// Writes a CSV-style report of students to outputPath.
        /// Columns: Id,Name,Score,Grade
        /// </summary>
        public void WriteReportToFile(IEnumerable<Student> students, string outputPath)
        {
            using var writer = new StreamWriter(outputPath, false);
            writer.WriteLine("Id,Name,Score,Grade"); // header

            foreach (var s in students)
            {
                writer.WriteLine($"{s.Id},{EscapeCsv(s.Name)},{s.Score},{s.GetGrade()}");
            }
        }

        // Simple CSV escape: wrap name in quotes if it contains comma/quote, double any quotes inside
        private static string EscapeCsv(string input)
        {
            if (input.Contains(",") || input.Contains("\""))
            {
                return $"\"{input.Replace("\"", "\"\"")}\"";
            }
            return input;
        }
    }
}
