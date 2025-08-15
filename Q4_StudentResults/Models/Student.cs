// Q4_StudentResults/Models/Student.cs
using System;

namespace Q4_StudentResults.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }

        public Student() { }

        public Student(int id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        /// <summary>
        /// Grade mapping:
        /// 80-100 => A
        /// 70-79  => B
        /// 60-69  => C
        /// 50-59  => D
        /// below 50 => F
        /// </summary>
        public string GetGrade()
        {
            if (Score >= 80 && Score <= 100) return "A";
            if (Score >= 70 && Score <= 79) return "B";
            if (Score >= 60 && Score <= 69) return "C";
            if (Score >= 50 && Score <= 59) return "D";
            return "F";
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, Score={Score}, Grade={GetGrade()}";
        }
    }
}
