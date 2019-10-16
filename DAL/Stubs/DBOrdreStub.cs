using System.Collections.Generic;
using Model;

namespace DAL.Stubs
{
    public class DBOrdreStub : IDBOrdre
    {
        private List<DBOrdre> ordre = new List<DBOrdre>()
        {
            new DBOrdre()
            {
                BId = 1,
                FraStasjon = "Oslo S",
                TilStasjon = "Trondheim",
                Avgangtid = "08:23",
                Dato = "29/10/2019",
                AntallBarn = 1,
                AntallStudent = 1,
                AntallVoksen = 1,
                AntallUngdom = 1,
            },
            new DBOrdre()
            {
                BId = 2,
                FraStasjon = "Oslo S",
                TilStasjon = "Trondheim",
                Avgangtid = "08:23",
                Dato = "25/10/2019",
                AntallBarn = 1,
                AntallStudent = 1,
                AntallVoksen = 1,
                AntallUngdom = 1,
                ReturDato = "28/10/2019",
                ReturAvgangtid = "10:23"
            },
            new DBOrdre()
            {
                BId = 3,
                FraStasjon = "Oslo S",
                TilStasjon = "Trondheim",
                Avgangtid = "08:23",
                Dato = "17/11/2019",
                AntallBarn = 1,
                AntallStudent = 1,
                AntallVoksen = 1,
                AntallUngdom = 1,
            }
        };

        public bool SettInnOrdre(Ordre innOrdre)
        {
            if (innOrdre.Rute.Strekning.FraStasjon != null
                && innOrdre.Rute.Strekning.TilStasjon != null
                && innOrdre.Rute.Avgang.Avgangstid != null)
            {
                return true;
            }

            return false; 
        }

        public List<DBOrdre> HentAlleOrdre()
        {
            throw new System.NotImplementedException();
        }
    }
}