using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public interface IOrdreBLL
    {
        List<DBOrdre> HentAlleOrdre();
        bool SettInnOrdre(Ordre innOrdre);
    }
}