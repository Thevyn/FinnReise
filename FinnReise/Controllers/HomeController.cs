using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using BLL;
using DAL;
using Gruppeoppgave1.api;
using Microsoft.AspNetCore.Authorization;
using Model;


namespace Gruppeoppgave1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAvgangBLL _avgang;
        private readonly IStasjonBLL _stasjon;
        private readonly IOrdreBLL _ordre;

        public HomeController(DBContext db)
        {
            _avgang = new AvgangBLL(db);
            _stasjon = new StasjonBLL(db);
            _ordre = new OrdreBLL(db);
        }

        public IActionResult Index()
        {
            return View();
        }

        // Lagrer all informasjonen og redirecter til avganger
        [HttpPost]
        public ActionResult VisAvganger(Strekning valgtStrekning)
        {
            var strekning = new Strekning();
            strekning.SettStrekning(valgtStrekning);
            //Henter Pris fra API
            strekning.Pris = GetDistance.GetDistanceFromApi(valgtStrekning.FraStasjon, valgtStrekning.TilStasjon);
            //Setter pris mtp. antall passasjerer og forskjellig pris på ulike billett typer. 
            strekning.Pris = strekning.SettPris();

            // Lagrer temporary data i en Session Cookie slik at vi kan bruke dataen/infoen ved redirection til Avganger
            TempData["Strekning"] = JsonConvert.SerializeObject(strekning);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Avganger", strekning);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Avganger()
        {
            var strekningModell = HentTempData();
            return View(strekningModell);
        }

        // Returnerer et partial view etter at brukeren har valgt avgang siden vi sender over data med ajax. 
        [HttpPost]
        public ActionResult VisBetal([FromBody] Avgang valgtAvgang)
        {
            var strekningModell = HentTempData();

            var avgang = new Avgang();
            avgang.SettRute(valgtAvgang);


            var rute = new Rute()
            {
                Avgang = avgang,
                Strekning = strekningModell
            };

            TempData["ValgtRute"] = JsonConvert.SerializeObject(rute);

            if (ModelState.IsValid)
            {
                return PartialView("Betale");
            }

            return RedirectToAction("Avganger");
        }

        // Viser kvitteringen. 
        public IActionResult BillettKvittering()
        {
            var rute = JsonConvert.DeserializeObject<Rute>(TempData["ValgtRute"].ToString());
            TempData.Keep("ValgtRute");
            return View(rute);
        }


        public IActionResult Betale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VisKvittering(Kort Kort)
        {
            var rute = JsonConvert.DeserializeObject<Rute>(TempData["ValgtRute"].ToString());
            var kort = new Kort();
            kort.SettKort(Kort);

            var ordre = new Ordre();
            {
                ordre.Kort = kort;
                ordre.Rute = rute;
            }

            bool settInnOK = _ordre.SettInnOrdre(ordre);

            if (settInnOK)
            {
                return RedirectToAction("BillettKvittering");
            }

            return RedirectToAction("Betale");
        }


        // Hent alle utreise avganger som matcher kriteriene og returner en liste.
        [HttpGet]
        public JsonResult HentAvganger()
        {
            var strekningModell = HentTempData();
            List<Avgang> alleAvganger = _avgang.HentUtreiseAvganger(strekningModell);

            return Json(alleAvganger);
        }

        // Hent alle retur avganger som matcher kriteriene og returner en liste.
        [HttpGet]
        public JsonResult HentAvgangerRetur()
        {
            var strekningModell = HentTempData();
            List<Avgang> alleAvganger = _avgang.listReturAvganger(strekningModell);

            return Json(alleAvganger);
        }

        // Hent Alle stasjoner som matcher det man skriver inn i fra stasjon og til stasjon
        [HttpGet]
        public JsonResult HentStasjon(string prefix)
        {
            var stasjoner = _stasjon.HentStasjon(prefix);

            return Json(stasjoner);
        }

        // Velg den avgangen som passer best.
        public string VelgAvgang(int AId)
        {
            var valgtAvgang = _avgang.HentEnAvgang(AId);
            var modell = HentTempData();

            string ut = "<table>";

            ut += "<tr><td>" + valgtAvgang.Avgangstid + "</td>" + "<td>" + valgtAvgang.Spor + "</td>"
                  + "<td>" + valgtAvgang.Linje + "</td>" + "<td>" + modell.Pris + "</td></table>";

            return ut;
        }

        // Validering om stasjonene finnes i databasen.
        public JsonResult StasjonGyldig(Strekning strekning)
        {
            return _stasjon.StasjonFinnes(strekning.FraStasjon, strekning.TilStasjon)
                ? Json(true)
                : Json(false);
        }

        public Strekning HentTempData()
        {
            var strekningModell =
                JsonConvert.DeserializeObject<Strekning>(TempData["Strekning"]
                    .ToString()); // Lagre alle data valgt stasjon i en cookie.
            // Hold på dataen ved refresh.
            TempData.Keep("Strekning");

            return strekningModell;
        }
    }
}