using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDBOrdre
    {
        bool SettInnOrdre(Ordre innOrdre);
        List<DBOrdre> HentAlleOrdre();
    }
}