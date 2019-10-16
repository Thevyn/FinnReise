using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Model;

namespace DAL
{
    public class DBAvgang : IDBAvgang
    {
        [Key] public int AId { get; set; }
        public string Avgangstid { get; set; }
        public int Spor { get; set; }
        public string Linje { get; set; }
        public int SId { get; set; }
        public virtual DBStasjon Stasjon { get; set; }


        public readonly DBContext _db;

        public DBAvgang(DBContext db)
        {
            _db = db;
        }

        public DBAvgang()
        {
        }

        // Hent alle avganger og legg det i en liste og returner listen

        public List<Avgang> HentAlleAvganger()
        {
            try
            {
                var alleAvganger = _db.Avgang.ToList();
                var avganger = new List<Avgang>();
                foreach (var avgang in alleAvganger)
                {
                    var enAvgang = new Avgang()
                    {
                        AId = avgang.AId,
                        Avgangstid = avgang.Avgangstid,
                        Spor = avgang.Spor,
                        Linje = avgang.Linje,
                        SId = avgang.SId
                    };
                    avganger.Add(enAvgang);
                }

                return avganger;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentAlleAvganger-metoden prøvde å hente alle avgangene",
                    "DBAvgang:HentAlleAvganger",
                    feil);
                return null;
            }
        }

        public List<Avgang> HentAvgangerForStasjon(int SId)
        {
            try
            {
                var stasjon = _db.Strekning.FirstOrDefault(s => s.SId == SId);
                var alleAvganger = new List<DBAvgang>();
                var avganger = new List<Avgang>();
                if (stasjon != null)
                {
                    foreach (var avgang in stasjon.Avganger)
                    {
                        alleAvganger.Add(avgang);
                    }
                }

                foreach (var avgang in alleAvganger)
                {
                    var enAvgang = new Avgang()
                    {
                        AId = avgang.AId,
                        Avgangstid = avgang.Avgangstid,
                        Spor = avgang.Spor,
                        Linje = avgang.Linje
                    };
                    avganger.Add(enAvgang);
                }

                return avganger;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile(
                    "Feil oppstått når HentAlleAvgangerForStasjon-metoden prøvde å hente alle avgangene for valgt stasjon",
                    "DBAvgang:HentAvgangerForStasjon",
                    feil);
                return null;
            }
        }

        public bool SettInnAvgang(Avgang innAvgang)
        {
            try
            {
                var avgang = new DBAvgang()
                {
                    Avgangstid = innAvgang.Avgangstid,
                    Spor = innAvgang.Spor,
                    Linje = innAvgang.Linje,
                    SId = innAvgang.SId
                };

                if (_db.Avgang.Any(a => a.Avgangstid == innAvgang.Avgangstid))
                {
                    return false;
                }

                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En ny avgang har blitt lagt til: ",
                    Endring = $"[{avgang.AId}] {avgang.Avgangstid} <br> {avgang.Spor} <br> {avgang.Linje}"
                };
                _db.Endring.Add(endring);
                _db.Avgang.Add(avgang);
                _db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en avgang skulle bli lagt til", "DBAvgang:SettInnAvgang", feil);
                return false;
            }
        }

        public bool SlettAvgang(int AId)
        {
            try
            {
                var avgang = _db.Avgang.FirstOrDefault(a => a.AId == AId);

                if (avgang == null)
                {
                    return false;
                }

                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En avgang har blitt slettet: ",
                    Endring = $"[{avgang.AId}] {avgang.Avgangstid}"
                };
                _db.Endring.Add(endring);
                _db.Avgang.Remove(avgang);
                _db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en avgang skulle bli slettet til", "DBAvgang:SlettAvgang", feil);
                return false;
            }
        }

        public Avgang HentEnAvgang(int AId)
        {
            try
            {
                DBAvgang avgang = _db.Avgang.FirstOrDefault(a => a.AId == AId);
                var enAvgang = new Avgang()
                {
                    AId = avgang.AId,
                    Avgangstid = avgang.Avgangstid,
                    Spor = avgang.Spor,
                    Linje = avgang.Linje,
                    SId = avgang.SId
                };

                return enAvgang;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentEnAvgang-metoden prøvde å hente en avgangene",
                    "DBAvgang:HentAvgangerForStasjon", feil);
                return null;
            }
        }

        //Endre Avgang
        public bool EndreAvgang(Avgang innAvgang)
        {
            try
            {
                DBAvgang avgang = _db.Avgang.FirstOrDefault(a => a.AId == innAvgang.AId);


                if (avgang == null)
                {
                    return false;
                }

                avgang.Avgangstid = innAvgang.Avgangstid;
                avgang.Spor = innAvgang.Spor;
                avgang.Linje = innAvgang.Linje;


                var endring = new DBEndring()
                {
                    Tidspunkt = DateTime.Now,
                    EndringOperasjon = "En avgang har blitt endret: ",
                    Endring = $"{avgang.AId}, {avgang.Avgangstid} {avgang.Spor}, {avgang.Linje}"
                };

                _db.Endring.Add(endring);
                _db.Avgang.Update(avgang);
                // Save changes in database
                _db.SaveChanges();
                return true;
            }


            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når en avgang skulle endres", "DBAvgang:EndreAvgang", feil);
                return false;
            }
        }

        // Hent alle avganger fra  utreise stasjon med utreise tid.

        public List<Avgang> HentUtreiseAvganger(Strekning valgtStasjon)
        {
            try
            {
                var stasjoner = _db.Strekning.Where(s => s.Stasjon == valgtStasjon.FraStasjon);
                List<Avgang> alleAvganger = new List<Avgang>();

                foreach (var stasjon in stasjoner)
                {
                    foreach (var avgang in stasjon.Avganger)
                    {
                        if (stasjon.SId == avgang.SId && DateTime.Parse(avgang.Avgangstid) > valgtStasjon.Tid)
                        {
                            var enAvgang = new Avgang();
                            enAvgang.AId = avgang.AId;
                            enAvgang.Avgangstid = avgang.Avgangstid;
                            enAvgang.Spor = avgang.Spor;
                            enAvgang.Linje = avgang.Linje;
                            alleAvganger.Add(enAvgang);
                        }
                    }
                }

                var sortertAvganger = alleAvganger.OrderBy(x =>
                    DateTime.Parse(x.Avgangstid)).ToList();

                return sortertAvganger;
            }
            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentUtreiseAvganger-metoden prøvde å hente utreise avgangene",
                    "DBAvgang:HentUtreiseAvganger", feil);
                return null;
            }
        }

        // Hent alle avganger fra retur stasjon med retur tid.
        public List<Avgang> HentReturAvganger(Strekning valgtStasjon)
        {
            try
            {
                var stasjoner = _db.Strekning.Where(s => s.Stasjon == valgtStasjon.TilStasjon);
                List<Avgang> alleAvganger = new List<Avgang>();

                foreach (var stasjon in stasjoner)
                {
                    foreach (var avgang in stasjon.Avganger)
                    {
                        if (stasjon.SId == avgang.SId && DateTime.Parse(avgang.Avgangstid) > valgtStasjon.ReturTid)
                        {
                            var enAvgang = new Avgang();
                            enAvgang.AId = avgang.AId;
                            enAvgang.Avgangstid = avgang.Avgangstid;
                            enAvgang.Spor = avgang.Spor;
                            enAvgang.Linje = avgang.Linje;
                            alleAvganger.Add(enAvgang);
                        }
                    }
                }

                var sortertAvganger = alleAvganger.OrderBy(x =>
                    DateTime.Parse(x.Avgangstid)).ToList();

                return sortertAvganger;
            }

            catch (Exception feil)
            {
                DBLog.ErrorToFile("Feil oppstått når HentReturAvganger-metoden prøvde å hente retur avgangene",
                    "DBAvgang:HentReturAvganger", feil);
                return null;
            }
        }
    }
}