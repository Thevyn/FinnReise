using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Stasjon
    {
        public int SId { get; set; }
        [Required(ErrorMessage = "Skriv inn stasjon")]
        public string StasjonNavn { get; set; }
    }
}