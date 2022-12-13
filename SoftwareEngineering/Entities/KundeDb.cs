using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineering.Entities
{
    public class KundeDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Vorname { get; set; }

        [Required]
        public string Nachname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password_Hash { get; set; }
    }
}
