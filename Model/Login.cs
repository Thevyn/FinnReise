using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Login
    {
        [Required(ErrorMessage = "Skriv inn brukernavn")]
        public string Brukernavn { get; set; }
        [Required(ErrorMessage = "Skriv inn passord")]

        public string Passord { get; set; }
        
    }
}