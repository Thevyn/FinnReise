using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDBAvgang
    {
        List<Avgang> HentAlleAvganger();
        List<Avgang> HentAvgangerForStasjon(int SId);
        bool SettInnAvgang(Avgang innAvgang);
        bool SlettAvgang(int AId);
        Avgang HentEnAvgang(int AId);
        bool EndreAvgang(Avgang innAvgang);
        List<Avgang> HentUtreiseAvganger(Strekning valgtStasjon);
        List<Avgang> HentReturAvganger(Strekning valgtStasjon);
    }
}