using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Avgang
    {
        public int AId { get; set; }
        public int SId { get; set; }

        [Required(ErrorMessage = "Skriv inn avgangstid")]
        public string Avgangstid { get; set; }

        [Required(ErrorMessage = "Skriv inn spor")]

        public int Spor { get; set; }

        [Required(ErrorMessage = "Skriv inn linje")]

        public string Linje { get; set; }

        public string AvgangstidRetur { get; set; }
        public int SporRetur { get; set; }
        public string LinjeRetur { get; set; }


        public void SettRute(Avgang valgtReise)
        {
            Avgangstid = valgtReise.Avgangstid;
            Spor = valgtReise.Spor;
            Linje = valgtReise.Linje;
            AvgangstidRetur = valgtReise.AvgangstidRetur;
            SporRetur = valgtReise.SporRetur;
            LinjeRetur = valgtReise.LinjeRetur;
        }
    }
}