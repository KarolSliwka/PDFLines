using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDFLines.Models
{
    [Table("Maintenance")]
    public class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Linia")]
        public int? ProjectId { get; set; }
        public Project Projects { get; set; } = null!;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Opis Przeglądu")]
        [Column(TypeName = "NVARCHAR(1024)")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Ropoczęcie Przeglądu")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Zakończenie Przeglądu")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Data Utworzenia")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Utworzony Przez")]
        public int? UserId { get; set; }
        public User? Users { get; set; }
    }
}