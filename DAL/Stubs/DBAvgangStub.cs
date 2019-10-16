using System.Collections.Generic;
using Model;

namespace DAL.Stubs
{
    public class DBAvgangStub : IDBAvgang
    {
        private List<Avgang> avganger = new List<Avgang>()
        {
            new Avgang()
            {
                AId = 1,
                Avgangstid = "10:30",
                Spor = 1,
                Linje = "L1",
                SId = 1
            },
            new Avgang()
            {
                AId = 2,
                Avgangstid = "11:30",
                Spor = 2,
                Linje = "L2",
                SId = 2
            },
            new Avgang()
            {
                AId = 3,
                Avgangstid = "12:30",
                Spor = 2,
                Linje = "L3",
                SId = 3
            }
        };
        public List<Avgang> HentAlleAvganger()
        {
            return avganger;
        }

        public List<Avgang> HentAvgangerForStasjon(int SId)
        {
            var avgang = avganger.FindAll(a => a.SId == SId);
            
            return avgang;
        }

        public bool SettInnAvgang(Avgang innAvgang)
        {
            if(innAvgang != null
               && innAvgang.Avgangstid != null
               && innAvgang.Linje != null)
            {
                avganger.Add(innAvgang);
                return true;
            }

            return false;
        }

        public bool SlettAvgang(int AId)
        {
            var avgang = avganger.Find(a => a.AId == AId);
            if (avgang != null)
            {
                avganger.Remove(avgang);
                return true;
            }
            return false;
        }

        public Avgang HentEnAvgang(int AId)
        {
            var avgang = avganger.Find(a => a.AId == AId);
            if (avgang != null)
            {
                return avgang;
            }

            return null;
        }

        public bool EndreAvgang(Avgang innAvgang)
        {
            if (innAvgang != null
                && innAvgang.Avgangstid != null
                && innAvgang.Linje != null)
            {
                var avgang = avganger.Find(a => a.AId == innAvgang.AId);
                if (avgang != null)
                {
                    avgang.Avgangstid = innAvgang.Avgangstid;
                    avgang.Spor = innAvgang.Spor;
                    avgang.Linje = innAvgang.Linje;

                    return true;
                }

                return false;
            }
            return false;
        }

        public List<Avgang> HentUtreiseAvganger(Strekning valgtStasjon)
        {
            throw new System.NotImplementedException();
        }

        public List<Avgang> HentReturAvganger(Strekning valgtStasjon)
        {
            throw new System.NotImplementedException();
        }
    }
}