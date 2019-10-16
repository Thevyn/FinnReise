using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class AvgangBLL : IAvgangBLL
    {
        private IDBAvgang _avgang;

        public AvgangBLL(DBContext db)
        {
            _avgang = new DBAvgang(db);
        }

        public AvgangBLL(IDBAvgang stub)
        {
            _avgang = stub;
        }

        public List<Avgang> HentAlleAvganger()
        {
            return _avgang.HentAlleAvganger();
        }

        public List<Avgang> HentAvgangerForStasjon(int SId)
        {
            return _avgang.HentAvgangerForStasjon(SId);
        }

        public bool SettInnAvgang(Avgang innAvgang)
        {
            return _avgang.SettInnAvgang(innAvgang);
        }

        public bool SlettAvgang(int AId)
        {
            return _avgang.SlettAvgang(AId);
        }

        public Avgang HentEnAvgang(int AId)
        {
            return _avgang.HentEnAvgang(AId);
        }

        public bool EndreAvgang(Avgang innAvgang)
        {
            return _avgang.EndreAvgang(innAvgang);
        }

        // Hent alle avganger fra  utreise stasjon med utreise tid.
        public List<Avgang> HentUtreiseAvganger(Strekning valgtStasjon)
        {
            return _avgang.HentUtreiseAvganger(valgtStasjon);
        }

        public List<Avgang> listReturAvganger(Strekning valgtStasjon)
        {
            return _avgang.HentReturAvganger(valgtStasjon);
        }
    }
}