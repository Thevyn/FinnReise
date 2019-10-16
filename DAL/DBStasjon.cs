using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Model;

namespace DAL
{
    public class DBStasjon : IDBStasjon
    {
        [Key] public int SId { get; set; }
        public string Stasjon { get; set; }

        public virtual List<DBAvgang> Avganger { get; set; }

        public readonly DBContext _db;

        public DBStasjon(DBContext db)
        {
            _db = db;
        }

        public DBStasjon()
        {
        }

        public bool SettInnStasjon(Stasjon innStasjon)
        {
            try
            {
                var stasjon = new DBStasjon()
                {
                    Stasjon = innStasjon.StasjonNavn
                };

                if (_db.Strekning.Any(s => s.Stasjon == innStasjon.StasjonNavn))
                {
                    return false;
                }

                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En ny stasjon har blitt lagt til: ",
                    Endring = $"{stasjon.Stasjon}"
                };

                _db.Endring.Add(endring);

                _db.Strekning.Add(stasjon);
                _db.SaveChanges();

                return true;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en stasjon skulle blitt lagt til", "DBStasjon:SettInnStasjon",
                    feil);
                return false;
            }
        }

        public bool StasjonFinnes(string fraStasjon, string tilStasjon)
        {
            try
            {
                if (_db.Strekning.Any(c => c.Stasjon == fraStasjon))
                {
                    return true;
                }

                if (_db.Strekning.Any(c => c.Stasjon == tilStasjon))
                {
                    return true;
                }

                return false;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når StasjonFinnes-metoden skulle sjekke om stasjonene eksisterte",
                    "DBStasjon:SettInnStasjon", feil);
                return false;
            }
        }


        // Hent Alle stasjoner som matcher det man skriver inn i fra stasjon og til stasjon
        public IQueryable<string> VisStasjon(string prefix)
        {
            try
            {
                var stasjoner = _db.Strekning
                    .Where(c => c.Stasjon.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    .Select(s => s.Stasjon);
                return stasjoner;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når VisStasjon-metoden skulle hente ut stasjonsnavnet",
                    "DBStasjon:VisStasjon", feil);
                return null;
            }
        }

        public bool SlettStasjon(int SId)
        {
            try
            {
                DBStasjon stasjon = _db.Strekning.FirstOrDefault(s => s.SId == SId);

                if (stasjon == null)
                {
                    return false;
                }

                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En stasjon har blitt slettet: ",
                    Endring = $"[{stasjon.SId}] {stasjon.Stasjon}"
                };

                _db.Endring.Add(endring);
                _db.Strekning.Remove(stasjon);
                _db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en stasjon skulle blitt slettet.", "DBStasjon:SlettStasjon", feil);
                return false;
            }
        }

        public Stasjon HentEnStasjon(int SId)
        {
            try
            {
                DBStasjon stasjon = _db.Strekning.FirstOrDefault(s => s.SId == SId);
                var enStasjon = new Stasjon()
                {
                    SId = stasjon.SId,
                    StasjonNavn = stasjon.Stasjon
                };

                return enStasjon;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentEnStasjon-metoden skulle hente ut en stasjon.",
                    "DBStasjon:HentEnStasjon", feil);
                return null;
            }
        }

        //Endre stasjon
        public bool EndreStasjon(Stasjon innStasjon)
        {
            try
            {
                DBStasjon stasjon = _db.Strekning.FirstOrDefault(s => s.SId == innStasjon.SId);

                if (stasjon == null)
                {
                    return false;
                }

                stasjon.Stasjon = innStasjon.StasjonNavn;

                if (_db.Strekning.Any(s => s.Stasjon == innStasjon.StasjonNavn))
                {
                    return false;
                }

                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En stasjon har blitt endret: ",
                    Endring = $"[{stasjon.SId}] {stasjon.Stasjon}"
                };

                _db.Endring.Add(endring);
                _db.Strekning.Update(stasjon);
                _db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en stasjon skulle blitt endret", "DBStasjon:EndreStasjon", feil);
                return false;
            }
        }

        public List<Stasjon> HentAlleStasjoner()
        {
            try
            {
                var alleStasjoner = _db.Strekning.ToList();
                var stasjoner = new List<Stasjon>();

                foreach (var stasjon in alleStasjoner)
                {
                    var enStasjon = new Stasjon()
                    {
                        SId = stasjon.SId,
                        StasjonNavn = stasjon.Stasjon
                    };
                    stasjoner.Add(enStasjon);
                }

                return stasjoner;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en HentAlleStasjoner-metoden skulle hente ut alle stasjonene",
                    "DBStasjon:HentAlleStasjoner", feil);
                return null;
            }
        }
    }
}