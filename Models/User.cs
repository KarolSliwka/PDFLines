using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDFLines.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public Guid EmployeeId { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "AD Użytkownika")]
        [Column(TypeName = "VARCHAR(128)")]
        public string Alias { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Imię & Nazwisko")]
        [Column(TypeName = "NVARCHAR(512)")]
        public string NameSurname { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Email")]
        [Column(TypeName = "NVARCHAR(512)")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [Display(Name = "Poziom Dostępu")]
        [Column(TypeName = "VARCHAR(128)")]
        public string? AccessLevel { get; set; }

        [NotMapped]
        public List<SelectListItem>? AccessLevels { get; set; }
    }
}
