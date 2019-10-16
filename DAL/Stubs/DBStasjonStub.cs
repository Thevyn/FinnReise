using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL.Stubs
{
    public class DBStasjonStub : IDBStasjon
    {
        private List<Stasjon> stasjoner = new List<Stasjon>()
        {
            new Stasjon()
            {
                SId = 1,
                StasjonNavn = "Oslo S"
            },
            new Stasjon()
            {
                SId = 2,
                StasjonNavn = "Bergen"
            },
            new Stasjon()
            {
                SId = 3,
                StasjonNavn = "Fredrikstad"
            }
        };
        public bool SettInnStasjon(Stasjon innStasjon)
        {
            if (innStasjon.StasjonNavn == "")
            {
                return false;
            }

            return true;
        }

        public bool StasjonFinnes(string fraStasjon, string tilStasjon)
        {
            if (fraStasjon != "Oslo S")
            {
                return false;
            }

            if (tilStasjon != "Bergen")
            {
                return false;
            }

            return true;


        }

        public IQueryable<string> VisStasjon(string prefix)
        {
            throw new NotImplementedException();
        }

        public bool SlettStasjon(int SId)
        {
            if (SId == 0)
            {
                return false;
            }

            return true;
        }

        public Stasjon HentEnStasjon(int SId)
        {
            if (SId == 0)
            {
                var stasjon = new Stasjon();
                stasjon.SId = 0;
                return stasjon;
            }
            else
            {
                var stasjon = new Stasjon()
                {
                    SId = 1,
                    StasjonNavn = "Oslo S"
                };
                return stasjon;
            }
        }

        public bool EndreStasjon(Stasjon innStasjon)
        {
            if (innStasjon != null
                && innStasjon.StasjonNavn != null)
            {
                var stasjon = stasjoner.Find(s => s.SId == innStasjon.SId);
                if (stasjon != null)
                {
                    stasjon.StasjonNavn = innStasjon.StasjonNavn;
                    return true;
                }

                return false;
            }
            return false;
        }

        public List<Stasjon> HentAlleStasjoner()
        {
            return stasjoner;
        }
    }
}