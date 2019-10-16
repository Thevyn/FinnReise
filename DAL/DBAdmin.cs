using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Model;

namespace DAL
{
    public class DBAdmin : IDBAdmin
    {
        [Key] public string Brukernavn { get; set; }
        public string Passord { get; set; }

        private readonly DBContext _db;

        public DBAdmin(DBContext db)
        {
            _db = db;
        }
        

        // Valider admin innlogging
        public bool ValiderLogin(Login innLogin)
        {
            try
            {
                if (_db.Admin.Any(b => b.Brukernavn == innLogin.Brukernavn && b.Passord == innLogin.Passord))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når ValiderLogin-metoden prøvde å validere admin login",
                    "DBAdmin:ValiderLogin",
                    e);
            }

            return false;
        }
    }
}