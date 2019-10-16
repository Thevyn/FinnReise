using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class KortBLL : IKortBLL
    {
        private IDBKort _kort;

        public KortBLL(DBContext db)
        {
            _kort = new DBKort(db);
        }

        public KortBLL(IDBKort stub)
        {
            _kort = stub;
        }

        public List<DBKort> ListAlleKort()
        {
            return _kort.HentAlleKort();
        }
    }
}