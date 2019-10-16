using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public interface IAvgangBLL
    {
        List<Avgang> HentAlleAvganger();
        List<Avgang> HentAvgangerForStasjon(int SId);
        bool SettInnAvgang(Avgang innAvgang);
        bool SlettAvgang(int AId);
        Avgang HentEnAvgang(int AId);
        bool EndreAvgang(Avgang innAvgang);
        List<Avgang> HentUtreiseAvganger(Strekning valgtStasjon);
        List<Avgang> listReturAvganger(Strekning valgtStasjon);
    }
}