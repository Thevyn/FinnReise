using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Model;

namespace DAL
{
    public class DBOrdre : IDBOrdre
    {
        [Key] public int BId { get; set; }
        public string FraStasjon { get; set; }
        public string TilStasjon { get; set; }
        public string BillettType { get; set; }
        public string Avgangtid { get; set; }
        public string Dato { get; set; }


        public string ReturAvgangtid { get; set; }
        public string ReturDato { get; set; }
        public int? AntallVoksen { get; set; }
        public int? AntallStudent { get; set; }
        public int? AntallBarn { get; set; }
        public int? AntallUngdom { get; set; }
        public int? Pris { get; set; }
        public int KortId { get; set; }
        public virtual DBKort Kort { get; set; }


        public readonly DBContext _db;

        public DBOrdre(DBContext db)
        {
            _db = db;
        }

        public DBOrdre()
        {
        }

        // Legger ordren inn i databasen
        public bool SettInnOrdre(Ordre innOrdre)
        {
            try
            {
                byte[] kortDb = lagHash(innOrdre.Kort.Kortnummer);
                byte[] cvcDb = lagHash(innOrdre.Kort.CVC);

                var kort = new DBKort()
                {
                    Kortnummer = kortDb,
                    Navn = innOrdre.Kort.Navn,
                    CVC = cvcDb,
                    Gyldighet = innOrdre.Kort.GyldighetsManed + "/" + innOrdre.Kort.GyldighetsAr
                };

                var ordre = new DBOrdre()
                {
                    FraStasjon = innOrdre.Rute.Strekning.FraStasjon,
                    TilStasjon = innOrdre.Rute.Strekning.TilStasjon,
                    BillettType = innOrdre.Rute.Strekning.BillettType,
                    AntallVoksen = innOrdre.Rute.Strekning.AntallVoksen,
                    AntallUngdom = innOrdre.Rute.Strekning.AntallUngdom,
                    AntallStudent = innOrdre.Rute.Strekning.AntallStudent,
                    AntallBarn = innOrdre.Rute.Strekning.AntallBarn,
                    Pris = innOrdre.Rute.Strekning.Pris,

                    Avgangtid = innOrdre.Rute.Avgang.Avgangstid,
                    Dato = innOrdre.Rute.Strekning.Dato.ToString("d"),
                    ReturAvgangtid = innOrdre.Rute.Avgang.AvgangstidRetur,
                    ReturDato = innOrdre.Rute.Strekning.ReturDato.ToString("d"),
                };

                kort.Ordre = new List<DBOrdre>();
                kort.Ordre.Add(ordre);

                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En ny ordre har blitt lagt til: ",
                    Endring = $"{kort.KortID}, {kort.Kortnummer} {kort.CVC}"
                };

                _db.Endring.Add(endring);
                _db.Kort.Add(kort);
                _db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en ny ordre skulle blitt lagt til", "DBOrdre:SettInnOrdre", feil);
                return false;
            }
        }

        // Hent alle ordre og legg det i en liste og returner listen
        public List<DBOrdre> HentAlleOrdre()
        {
            try
            {
                var alleOrdre = _db.Ordre.ToList();

                return alleOrdre;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentAlleOrdre-metoden skulle hente ut alle ordre",
                    "DBOrdre:HentAlleOrdre", feil);
                return null;
            }
        }

        // Lager en hash av Kontonr og CVC
        private static byte[] lagHash(string innPassord)
        {
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }
    }
}