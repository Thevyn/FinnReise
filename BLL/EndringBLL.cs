using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class EndringBLL : IEndringBLL
    {
        private IDBEndring _endring;

        public EndringBLL(DBContext db)
        {
            _endring = new DBEndring(db);
        }

        public List<Endring> HentAlleEndringer()
        {
            return _endring.HentAlleEndringer();
        }
    }
}