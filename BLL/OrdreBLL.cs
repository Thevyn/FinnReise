using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class OrdreBLL : IOrdreBLL
    {
        private IDBOrdre _ordre;

        public OrdreBLL(DBContext db)
        {
            _ordre = new DBOrdre(db);
        }

        public OrdreBLL(IDBOrdre stub)
        {
            _ordre = stub;
        }

        public List<DBOrdre> HentAlleOrdre()
        {
            return _ordre.HentAlleOrdre();
        }

        public bool SettInnOrdre(Ordre innOrdre)
        {
            return _ordre.SettInnOrdre(innOrdre);
        }
    }
}