using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL;
using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace Gruppeoppgave1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAvgangBLL _avgang;
        private readonly IStasjonBLL _stasjon;
        private readonly IAdminBLL _admin;
        private readonly IEndringBLL _endring;
        private readonly IKortBLL _kort;
        private readonly IOrdreBLL _ordre;
        
        [ActivatorUtilitiesConstructor]
        public AdminController(DBContext db)
        {
            _avgang = new AvgangBLL(db);
            _stasjon = new StasjonBLL(db);
            _admin = new AdminBLL(db);
            _endring = new EndringBLL(db);
            _kort = new KortBLL(db);
            _ordre = new OrdreBLL(db);
        }

        public AdminController(IAvgangBLL avgangStub, IStasjonBLL stasjonStub, IAdminBLL adminStub)
        {
            _avgang = avgangStub;
            _stasjon = stasjonStub;
            _admin = adminStub;
        }


        [HttpPost]
        public IActionResult Login(Login innLogin)
        {
            if (ModelState.IsValid)
            {
                var innloggingOK = _admin.ValiderLogin(innLogin);

                if (innloggingOK)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, innLogin.Brukernavn)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("AdminPanel");
                }
            }

            ViewBag.Error = "Feil brukernavn eller password";
            return View("Login");
        }

        [HttpGet]
        public async Task<IActionResult> LoggUt()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AdminPanel");
            }
            return View("Login");
        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            return View("AdminPanel");
        }

        [Authorize]
        public IActionResult NyStasjon()
        {
            return View("NyStasjon");
        }

        [Authorize]
        [HttpPost]
        public ActionResult NyStasjon(Stasjon innStasjon)
        {
            var settInnOK = _stasjon.SettInnStasjon(innStasjon);

            if (ModelState.IsValid)
            {
                if (settInnOK)
                {
                    ViewBag.OK = innStasjon.StasjonNavn + " har blitt lagt til";
                    ModelState.Clear();
                    return View("NyStasjon");
                }
            }

            ViewBag.Error = innStasjon.StasjonNavn + " kunne ikke bli lagt til, prøv igjen";
            return View("NyStasjon");
        }

        [Authorize]
        public IActionResult HentAlleStasjoner()
        {
            List<Stasjon> stasjoner = _stasjon.HentAlleStasjoner();

            return PartialView("AlleStasjoner", stasjoner);
        }

        [Authorize]
        public IActionResult HentAlleAvganger(int SId)
        {
            var alleAvgangerForStasjon = _avgang.HentAvgangerForStasjon(SId);

            TempData["SId"] = SId;
            TempData.Keep("SId");
            return PartialView("AlleAvganger", alleAvgangerForStasjon);
        }


        [Authorize]
        [HttpPost]
        public IActionResult SlettStasjon(int SId)
        {
            var slettOK = _stasjon.SlettStasjon(SId);
            if (slettOK)
            {
                return RedirectToAction("HentAlleStasjoner");
            }

            return RedirectToAction("AdminPanel");
        }

        [Authorize]
        public IActionResult EndreStasjon(int SId)
        {
            var enStasjon = _stasjon.HentEnStasjon(SId);
            var stasjon = new Stasjon()
            {
                SId = enStasjon.SId,
                StasjonNavn = enStasjon.StasjonNavn
            };

            return View("EndreStasjon", stasjon);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EndreStasjon(Stasjon innStasjon)
        {
            var endreOK = _stasjon.EndreStasjon(innStasjon);
            if (ModelState.IsValid)
            {
                if (endreOK)
                {
                    ViewBag.OK = innStasjon.StasjonNavn + " har blitt endret";
                    return View(innStasjon);
                }
            }

            ViewBag.Error = innStasjon.StasjonNavn + " kunne ikke bli endret, prøv igjen";
            return View(innStasjon);
        }

        [Authorize]
        public IActionResult NyAvgang(int SId)
        {
            var avgang = new Avgang()
            {
                SId = SId
            };

            return View("NyAvgang", avgang);
        }

        [Authorize]
        [HttpPost]
        public ActionResult NyAvgang(Avgang innAvgang)
        {
            var settInnOK = _avgang.SettInnAvgang(innAvgang);

            if (ModelState.IsValid)
            {
                if (settInnOK)
                {
                    ViewBag.OK = "Avgangen har blitt lagt til";
                    ModelState.Clear();
                    return View("NyAvgang", innAvgang);
                }
            }

            ViewBag.Error = "Avgangen kunne ikke bli lagt til, prøv igjen";
            return View("NyAvgang", innAvgang);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SlettAvgang(int AId)
        {
            var Id = Int32.Parse(TempData["SId"].ToString());

            var slettOK = _avgang.SlettAvgang(AId);
            if (slettOK)
            {
                return RedirectToAction("HentAlleAvganger", new {SId = Id});
            }

            return RedirectToAction("HentAlleStasjoner");
        }

        [Authorize]
        public IActionResult EndreAvgang(int AId)
        {
            var avgang = _avgang.HentEnAvgang(AId);
           
            return View("EndreAvgang", avgang);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EndreAvgang(Avgang innAvgang)
        {
            var endreOK = _avgang.EndreAvgang(innAvgang);

            if (ModelState.IsValid)
            {
                if (endreOK)
                {
                    ViewBag.OK = "Avgangen har blitt endret";
                    return View("EndreAvgang", innAvgang);
                }
            }

            ViewBag.Error = "Avgangen kunne ikke bli endret, prøv igjen";
            return View("EndreAvgang", innAvgang);
        }

        [Authorize]
        public IActionResult VisDatabase()
        {
            return PartialView("VisDatabase");
        }

        // Hent og vis data fra databasen
        public string HentData(string database)
        {
            switch (database)
            {
                case "Strekning":

                    var alleStasjoner = _stasjon.HentAlleStasjoner();
                    string stasjonUt = "<table class='table'><th>SId</th><th>Stasjon</th>";

                    foreach (var stasjon in alleStasjoner)
                    {
                        stasjonUt += "<tr><td>" + stasjon.SId + "</td><td>" + stasjon.StasjonNavn + "</td></tr>";
                    }

                    stasjonUt += "</table>";

                    return stasjonUt;
                case "Avganger":
                    var alleAvganger = _avgang.HentAlleAvganger();
                    string avgangUt = "<table class='table'><th>AId</th><th>Avgangstid</th>" +
                                      "<th>Spor</th><th>Linje</th><th>SId</th>";

                    foreach (var avgang in alleAvganger)
                    {
                        avgangUt += "<tr><td>" + avgang.AId + "</td><td> " + avgang.Avgangstid + "</td><td>" +
                                    avgang.Spor + "</td>"
                                    + "<td>" + avgang.Linje + "</td><td>" + avgang.SId + "</td>";
                    }

                    avgangUt += "</table>";
                    return avgangUt;
                case "Kort":
                    var alleKort = _kort.ListAlleKort();
                    string kortUt = "<table class='table'><th>KortID</th><th>Kortnummer</th><th>CVC</th>" +
                                    "<th>Navn</th><th>Gylidghet</th>";

                    foreach (var kort in alleKort)
                    {
                        kortUt += "<tr><td>" + kort.KortID + "</td><td>" + kort.Kortnummer + "</td>"
                                  + "<td>" + kort.CVC + "</td><td>" + kort.Navn + "</td><td>" + kort.Gyldighet +
                                  "</td>";
                    }

                    kortUt += "</table>";
                    return kortUt;
                case "Ordre":
                    var alleOrdre = _ordre.HentAlleOrdre();
                    string ordreUt = "<table class='table'><th>BId</th><th>KortId</th><th>FraStasjon</th>" +
                                     "<th>TilStasjon</th><th>BillettType</th>"
                                     + "<th>Dato</th><th>Avgangtid</th><th>ReturDato</th><th>ReturAvgangtid</th>" +
                                     "<th>AntallVoksen</th><th>AntallStudent</th><th>AntallUngdom</th><th>AntallBarn</th>";
                    foreach (var ordre in alleOrdre)
                    {
                        ordreUt += "<tr><td>" + ordre.BId + "</td><td>" + ordre.KortId + "</td><td> " +
                                   ordre.FraStasjon + "</td>"
                                   + "<td>" + ordre.TilStasjon + "</td><td>" + ordre.BillettType + "</td><td>" +
                                   ordre.Dato + "</td>"
                                   + "<td>" + ordre.Avgangtid + "</td><td>" + ordre.ReturDato + "</td>"
                                   + "<td>" + ordre.ReturAvgangtid + "</td><td>" + ordre.AntallVoksen + "</td><td>" +
                                   ordre.AntallStudent + "</td>"
                                   + "<td>" + ordre.AntallUngdom + "</td><td>" + ordre.AntallBarn + "</td>";
                    }

                    ordreUt += "</table>";
                    return ordreUt;
                case "Endringer":
                    var alleEndrigner = _endring.HentAlleEndringer();
                    string endringUt =
                        "<table class='table'><th>EndringOperasjon</th><th>Endring</th><th>Tidspunkt</th>";
                    if (alleEndrigner != null)
                    {
                        foreach (var endring in alleEndrigner)
                        {
                            endringUt += "<tr><td>" + endring.EndringOperasjon + "</td><td>" + endring.endring +
                                         "</td><td>"
                                         + endring.Tidspunkt + "</td>";
                        }
                    }

                    endringUt += "</table>";
                    return endringUt;
            }

            return "ingen verdi";
        }
    }
}