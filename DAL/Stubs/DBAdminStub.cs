using System;
using Model;

namespace DAL.Stubs
{
    public class DBAdminStub : IDBAdmin
    {
        public bool ValiderLogin(Login innLogin)
        {
            if (innLogin.Brukernavn == "Admin" && innLogin.Passord == "Admin")
            {
                return true;
            }

            return false; 
        }
    }
}