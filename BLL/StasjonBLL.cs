using System.Collections.Generic;
using System.Linq;
using DAL;
using Model;

namespace BLL
{
    public class StasjonBLL : IStasjonBLL
    {
        private IDBStasjon _stasjon;

        public StasjonBLL(DBContext db)
        {
            _stasjon = new DBStasjon(db);
        }

        public StasjonBLL(IDBStasjon stub)
        {
            _stasjon = stub;
        }

        public bool SlettStasjon(int SId)
        {
            return _stasjon.SlettStasjon(SId);
        }

        public Stasjon HentEnStasjon(int SId)
        {
            return _stasjon.HentEnStasjon(SId);
        }

        public bool EndreStasjon(Stasjon innStasjon)
        {
            return _stasjon.EndreStasjon(innStasjon);
        }

        public bool StasjonFinnes(string fraStasjon, string tilStasjon)
        {
            return _stasjon.StasjonFinnes(fraStasjon, tilStasjon);
        }


        public IQueryable<string> HentStasjon(string prefix)
        {
            return _stasjon.VisStasjon(prefix);
        }

        public bool SettInnStasjon(Stasjon innStasjon)
        {
            return _stasjon.SettInnStasjon(innStasjon);
        }

        public List<Stasjon> HentAlleStasjoner()
        {
            return _stasjon.HentAlleStasjoner();
        }
    }
}