using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAL
{
    public class DBKort : IDBKort
    {
        [Key] public int KortID { get; set; }
        public byte[] Kortnummer { get; set; }
        public string Navn { get; set; }
        public string Gyldighet { get; set; }
        public byte[] CVC { get; set; }

        public virtual List<DBOrdre> Ordre { get; set; }

        private readonly DBContext _db;

        public DBKort(DBContext db)
        {
            _db = db;
        }

        public DBKort()
        {
        }
        // Hent alle kort og legg det i en liste og returner listen

        public List<DBKort> HentAlleKort()
        {
            try
            {
                var alleKort = _db.Kort.ToList();

                return alleKort;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentAlleKort-metoden skulle hente ut alle kort",
                    "DBKort:HentAlleKort", feil);
                return null;
            }
        }
    }
}