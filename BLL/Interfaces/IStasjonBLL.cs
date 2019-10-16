using System.Collections.Generic;
using System.Linq;
using DAL;
using Model;

namespace BLL
{
    public interface IStasjonBLL
    {
        bool SlettStasjon(int SId);
        Stasjon HentEnStasjon(int SId);
        bool EndreStasjon(Stasjon innStasjon);
        bool StasjonFinnes(string fraStasjon, string tilStasjon);
        IQueryable<string> HentStasjon(string prefix);
        bool SettInnStasjon(Stasjon innStasjon);
        List<Stasjon> HentAlleStasjoner();
    }
}