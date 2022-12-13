using SoftwareEngineering.Entities;
using System.ComponentModel.DataAnnotations;
using BC = BCrypt.Net.BCrypt;

namespace SoftwareEngineering.DataObjects
{
    public class Kunde
    {
        [Required]
        public string Vorname { get; set; }

        [Required]
        public string Nachname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Kunde() { }

        public Kunde(KundeDb entity)
        {
            Vorname = entity.Vorname;
            Nachname = entity.Nachname;
            Email = entity.Email;
        }
    }

    public class KundeRegister : Kunde
    {
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public KundeRegister() : base() { }

        public KundeDb ToEntity()
        {
            return new KundeDb()
            {
                Vorname = Vorname,
                Nachname = Nachname,
                Email = Email,
                Password_Hash = BC.HashPassword(Password),
            };
        }
    }
}
