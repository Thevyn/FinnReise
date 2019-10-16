using System;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace Model
{
    public class Strekning
    {

        public int SId { get; set; }
        [Required(ErrorMessage = "Velg en stasjon")]
        [AssertThat("FraStasjon != TilStasjon", ErrorMessage = "Du har valgt å reise til samme stasjon")]
        [Remote("StasjonGyldig", "Home", ErrorMessage = "Du kan ikke reise fra denne stasjonen")]
        public string FraStasjon { get; set; }
        [Required(ErrorMessage = "Velg en stasjon")]
        [Remote("StasjonGyldig", "Home", ErrorMessage = "Du kan ikke reise fra denne stasjonen")]
        public string TilStasjon { get; set; }

        [Required(ErrorMessage = "Vennligst velg billett type")]
        public string BillettType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Dato { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Tid { get; set; }
        [Required]
        public DateTime ReturDato { get; set; }
        [Required]
        public DateTime ReturTid { get; set; }
        [AssertThat("(AntallVoksen == 0 && AntallBarn == 0 && AntallStudent == 0 && AntallUngdom ==0) ? false : AntallVoksen != 0 || " +
                    "AntallBarn != 0 || AntallStudent != 0 || AntallUngdom != 0", ErrorMessage = "Vennligst velg minst en passasjer")]
        public int? AntallVoksen { get; set; }
        public int? AntallStudent { get; set; }
        public int? AntallBarn { get; set; }
        public int? AntallUngdom { get; set; }
        public int? Pris { get; set; }

        
        
        public void SettStrekning(Strekning valgtStrekning)
        {
            FraStasjon = valgtStrekning.FraStasjon;
            TilStasjon = valgtStrekning.TilStasjon;
            BillettType = valgtStrekning.BillettType;
            Dato = valgtStrekning.Dato;
            Tid = valgtStrekning.Tid;
            AntallVoksen = valgtStrekning.AntallVoksen;
            AntallBarn = valgtStrekning.AntallBarn;
            AntallStudent = valgtStrekning.AntallStudent;
            AntallUngdom = valgtStrekning.AntallUngdom;
            
            if (BillettType == "TurRetur")
            {
                ReturDato = valgtStrekning.ReturDato;
                ReturTid = valgtStrekning.ReturTid;
            }
        }
        public int SettPris()
        {
            var prisVoksen= Pris * AntallVoksen;
            // Studenter og ungdom halvpris
            var prisStudent= Pris * AntallStudent * 0.5;
            var prisUngdom = Pris * AntallUngdom * 0.5;
            // Barn 75% avslag
            var prisBarn= Pris * AntallBarn * 0.25;
            // Totalprisen for antall pasasjerer
            var totalPris = prisVoksen + prisStudent + prisUngdom + prisBarn;

            if (totalPris != null) return (int) totalPris;

            return 0;
        }
        

        
    }

    

}
