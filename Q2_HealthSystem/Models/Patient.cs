using System;

namespace Q2_HealthSystem.Models
{
    public class Patient
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateTime DateOfBirth { get; init; }
        public string Gender { get; init; } = "Unknown";

        public int Age => (int)((DateTime.Today - DateOfBirth).TotalDays / 365.25);

        public override string ToString()
        {
            return $"[{Id}] {Name} (Age: {Age}, Gender: {Gender})";
        }
    }
}
