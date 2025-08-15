using System;
using System.Collections.Generic;
using System.Linq;
using Q2_HealthSystem.Models;
using Q2_HealthSystem.Repository;

namespace Q2_HealthSystem
{
    public class HealthSystemApp
    {
        private readonly Repository<Patient> _patientRepo = new();
        private readonly Repository<Prescription> _prescriptionRepo = new();

        // Group prescriptions by patient id for fast lookup
        private Dictionary<int, List<Prescription>> _prescriptionMap = new();

        public void Run()
        {
            Console.WriteLine("=== Q2_HealthSystem Demo ===\n");

            SeedData();
            BuildPrescriptionMap();

            Console.WriteLine("All patients:");
            PrintAllPatients();
            Console.WriteLine();

            // Print prescriptions for each seeded patient
            foreach (var patient in _patientRepo.GetAll())
            {
                Console.WriteLine($"Prescriptions for patient {patient.Id} - {patient.Name}:");
                PrintPrescriptionsForPatient(patient.Id);
                Console.WriteLine();
            }

            // Demo: add a new prescription and show map updated
            var newPrescription = new Prescription
            {
                Id = 999,
                PatientId = 2,
                Date = DateTime.Today,
                Medication = "Azithromycin",
                Dosage = "500mg daily x3",
                Notes = "Take after food"
            };

            Console.WriteLine("Adding a new prescription for PatientId=2...");
            _prescriptionRepo.Add(newPrescription);
            // update map incrementally (or rebuild)
            AddPrescriptionToMap(newPrescription);

            Console.WriteLine("Prescriptions for patient 2 after adding new prescription:");
            PrintPrescriptionsForPatient(2);
            Console.WriteLine();

            // Demo: remove a patient
            Console.WriteLine("Removing patient with Id = 3 (if exists) and associated prescriptions...");
            var removed = _patientRepo.Remove(p => (p as Patient) != null && ((Patient)(object)p).Id == 3);
            // above generic cast is not needed - we will remove using predicate below in a simple way:
            // (we'll use the repository's Remove correctly below)
            if (!_patientRepo.Remove(p => p.Id == 3))
            {
                Console.WriteLine("Patient Id=3 not found (that's okay).");
            }
            else
            {
                // remove prescriptions for that patient
                var keysToRemove = _prescriptionMap.Keys.Where(k => k == 3).ToList();
                foreach (var k in keysToRemove)
                    _prescriptionMap.Remove(k);

                Console.WriteLine("Patient 3 and their prescriptions removed from in-memory map.");
            }

            Console.WriteLine("\nDemo complete.");
        }

        private void SeedData()
        {
            // Seed some patients
            _patientRepo.Add(new Patient { Id = 1, Name = "Alice Mensah", DateOfBirth = new DateTime(1990, 5, 12), Gender = "Female" });
            _patientRepo.Add(new Patient { Id = 2, Name = "Kwame Ofori", DateOfBirth = new DateTime(1985, 2, 20), Gender = "Male" });
            _patientRepo.Add(new Patient { Id = 3, Name = "Grace Boateng", DateOfBirth = new DateTime(2000, 11, 1), Gender = "Female" });

            // Seed prescriptions (some multiple per patient)
            _prescriptionRepo.Add(new Prescription { Id = 101, PatientId = 1, Date = DateTime.Today.AddDays(-10), Medication = "Paracetamol", Dosage = "500mg", Notes = "3 times a day" });
            _prescriptionRepo.Add(new Prescription { Id = 102, PatientId = 1, Date = DateTime.Today.AddDays(-5), Medication = "Ibuprofen", Dosage = "200mg", Notes = "Take if pain persists" });

            _prescriptionRepo.Add(new Prescription { Id = 201, PatientId = 2, Date = DateTime.Today.AddDays(-1), Medication = "Amoxicillin", Dosage = "500mg TID", Notes = "7 days course" });
            _prescriptionRepo.Add(new Prescription { Id = 202, PatientId = 2, Date = DateTime.Today.AddDays(-3), Medication = "Cough Syrup", Dosage = "10ml", Notes = "At night" });

            _prescriptionRepo.Add(new Prescription { Id = 301, PatientId = 3, Date = DateTime.Today.AddDays(-2), Medication = "Vitamin D", Dosage = "400 IU", Notes = "Once daily" });
        }

        // Build prescription map from the repository
        private void BuildPrescriptionMap()
        {
            _prescriptionMap = _prescriptionRepo
                .GetAll()
                .GroupBy(p => p.PatientId)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Add single prescription to the map (keeps the map up-to-date)
        private void AddPrescriptionToMap(Prescription p)
        {
            if (_prescriptionMap.TryGetValue(p.PatientId, out var list))
            {
                list.Add(p);
            }
            else
            {
                _prescriptionMap[p.PatientId] = new List<Prescription> { p };
            }
        }

        // Print all patients
        public void PrintAllPatients()
        {
            foreach (var p in _patientRepo.GetAll())
            {
                Console.WriteLine(p);
            }
        }

        // Print prescriptions for a patient by id
        public void PrintPrescriptionsForPatient(int patientId)
        {
            if (_prescriptionMap.TryGetValue(patientId, out var prescriptions) && prescriptions.Count > 0)
            {
                foreach (var rx in prescriptions.OrderBy(r => r.Date))
                    Console.WriteLine("  " + rx);
            }
            else
            {
                Console.WriteLine("  No prescriptions found for patient id " + patientId);
            }
        }
    }
}
