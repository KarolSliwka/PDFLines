using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDFLines.Models
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Klient")]
        public int? ClientId { get; set; }
        public Client? Clients { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Nazwa Projektu")]
        [Column(TypeName = "VARCHAR(128)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Typ")]
        public int Type { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Nazwa Pliku")]
        [Column(TypeName = "VARCHAR(128)")]
        public string FileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Lokalizacja Pliku")]
        [Column(TypeName = "VARCHAR(1028)")]
        public string FileLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Aktywny")]
        public bool Active { get; set; } = false;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Order")]
        public int Order { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Utworzony Przez")]
        public int? UserId { get; set; }
        public User? Users { get; set; }

        public List<Maintenance>? Maintenances { get; set; }
    }
}