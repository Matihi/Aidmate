namespace AidMate.Models
{
    public class PatientModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Condition { get; set; }
        public bool isCritical { get; set; }
    }
}