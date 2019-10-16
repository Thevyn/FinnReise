using System.Collections.Generic;
using DAL;

namespace BLL
{
    public interface IKortBLL
    {
        List<DBKort> ListAlleKort();
    }
}