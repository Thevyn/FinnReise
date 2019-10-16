using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Model;

namespace DAL
{
    public class DBEndring : IDBEndring
    {
        [Key] public int EId { get; set; }
        public string EndringOperasjon { get; set; }
        public string Endring { get; set; }
        public DateTime Tidspunkt { get; set; }


        public readonly DBContext _db;

        public DBEndring(DBContext db)
        {
            _db = db;
        }

        public DBEndring()
        {
        }


        public List<Endring> HentAlleEndringer()
        {
            try
            {
                var dbEndringer = _db.Endring.ToList();
                var alleEndringer = new List<Endring>();

                foreach (var endring in dbEndringer)
                {
                    var enEndring = new Endring()
                    {
                        EId = endring.EId,
                        EndringOperasjon = endring.EndringOperasjon,
                        endring = endring.Endring,
                        Tidspunkt = endring.Tidspunkt
                    };

                    alleEndringer.Add(enEndring);
                }

                alleEndringer.Reverse();
                return alleEndringer;
            }

            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når HentAlleEndringer-metoden prøvde å hente alle endringene",
                    "DBEndring:HentAlleEndringer",
                    e);
                return null;
            }
        }
    }
}