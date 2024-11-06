using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDFLines.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Nazwa")]
        [Column(TypeName = "VARCHAR(128)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Aktywny")]
        public bool Active { get; set; } = false;

        public List<Project>? Projects { get; set; }
    }
}