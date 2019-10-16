using System.Collections.Generic;

namespace DAL
{
    public interface IDBKort
    {
        List<DBKort> HentAlleKort();
    }
}