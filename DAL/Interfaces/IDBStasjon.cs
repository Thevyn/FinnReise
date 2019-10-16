using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL
{
    public interface IDBStasjon
    {
        bool SettInnStasjon(Stasjon innStasjon);
        bool StasjonFinnes(string fraStasjon, string tilStasjon);
        IQueryable<string> VisStasjon(string prefix);
        bool SlettStasjon(int SId);
        Stasjon HentEnStasjon(int SId);
        bool EndreStasjon(Stasjon innStasjon);
        List<Stasjon> HentAlleStasjoner();
    }
}