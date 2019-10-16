
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Kort
    {

        public int KortID { get; set; }
        [Required(ErrorMessage = "Oppgi kortnr")]
        [RegularExpression("^4[0-9]{12}(?:[0-9]{3})?$", ErrorMessage = "Ugyldig kortnr")]
        public string Kortnummer { get; set; }
        [Required(ErrorMessage = "Oppgi navn")]
        public string Navn { get; set; }
        [Required(ErrorMessage = "Oppgi utløpsdato")]

        public string GyldighetsManed { get; set; }

        public string GyldighetsAr { get; set; }
        [Required(ErrorMessage = "Oppgi CVC")]
        public string CVC { get; set; }


        public void SettKort(Kort valgtKort)
        {
            Kortnummer = valgtKort.Kortnummer;
            Navn = valgtKort.Navn;
            GyldighetsManed = valgtKort.GyldighetsManed;
            GyldighetsAr = valgtKort.GyldighetsAr;
            CVC = valgtKort.CVC;
        }


    }
}
