using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDBEndring
    {
        List<Endring> HentAlleEndringer();
    }
}