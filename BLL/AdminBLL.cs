using DAL;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BLL
{
    public class AdminBLL : IAdminBLL
    {
        private IDBAdmin _admin;

        public AdminBLL(DBContext db)
        {
            _admin = new DBAdmin(db);
        }

        public AdminBLL(IDBAdmin stub)
        {
            _admin = stub;
        }

        public bool ValiderLogin(Login innLogin)
        {
            return _admin.ValiderLogin(innLogin);
        }
    }
}