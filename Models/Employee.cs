namespace PDFLines.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? Alias { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public int HRID { get; set; }
        public string? PositionPL { get; set; }
        public int? HID { get; set; }
        public string? PositionEN { get; set; }
        public string? Department { get; set; }
        public Guid? SuperiorId { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsAnonymized { get; set; }
        public string? Building { get; set; }
        public int? PreviousHRID { get; set; }
        public bool IsLTA { get; set; }
        public DateTime? RemoveDate { get; set; }
        public bool IsDirectLabor { get; set; }
        public bool IsAgency { get; set; }
    }
}
