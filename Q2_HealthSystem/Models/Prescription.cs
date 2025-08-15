using System;

namespace Q2_HealthSystem.Models
{
    public class Prescription
    {
        public int Id { get; init; }
        public int PatientId { get; init; }           // link to Patient.Id
        public DateTime Date { get; init; }
        public string Medication { get; init; } = string.Empty;
        public string Dosage { get; init; } = string.Empty;
        public string Notes { get; init; } = string.Empty;

        public override string ToString()
        {
            return $"#{Id} {Date:d} - {Medication} ({Dosage}) - Notes: {Notes}";
        }
    }
}
