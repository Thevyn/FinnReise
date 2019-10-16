using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL;
using DAL.Stubs;
using Gruppeoppgave1.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Model;
using Moq;
using Xunit;

namespace TestProject1
{
    public class AdminControllerTest
    {
        [Fact]
        public void LoginView()
        {
            var identitet = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Admin")
            });

            var principal = new ClaimsPrincipal(identitet);

            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = principal
                }
            };
            var controller = HentController();
            controller.ControllerContext = context;
            var actionResult = (ViewResult)controller.Login();

            Assert.Equal("Login", actionResult.ViewName);
        }

        [Fact]
        public void Login_Post_RedirectToAction()
        {
            var controller = HentController();

            controller.ControllerContext = AddAuthentication();
            ;
            var login = new Login()
            {
                Brukernavn = "Admin",
                Passord = "Admin"
            };
            var actionResult = (RedirectToActionResult) controller.Login(login);
            Assert.Equal("AdminPanel", actionResult.ActionName);
        }

        [Fact]
        public void Login_feil_validering_Post()
        {
            var controller = HentController();

            controller.ControllerContext = AddAuthentication();
            ;
            var login = new Login();
            controller.ViewData.ModelState.AddModelError("Brukernavn", "Ikke oppgitt brukernavn");
            var actionResult = (ViewResult) controller.Login(login);

            Assert.True(actionResult.ViewData.ModelState.Count == 1);
            Assert.Equal("Login", actionResult.ViewName);
        }

        [Fact]
        public void Login_feil_Post()
        {
            var controller = HentController();
            controller.ControllerContext = AddAuthentication();
            var login = new Login();
            login.Brukernavn = "";
            var actionResult = (ViewResult) controller.Login(login);

            Assert.Equal("Login", actionResult.ViewName);
        }


        [Fact]
        public void Vis_AdminPanel_View()
        {
            var controller = HentController();
            var actionResult = (ViewResult) controller.AdminPanel();

            Assert.Equal("AdminPanel", actionResult.ViewName);
        }

        [Fact]
        public void Vis_NyStasjon_View()
        {
            var controller = HentController();

            var actionResult = (ViewResult)controller.NyStasjon();

            Assert.Equal("NyStasjon", actionResult.ViewName);
        }

        [Fact]
        public void NyStasjon_validering_Post()
        {
            var controller = HentController();

            var stasjon = new Stasjon()
            {
                SId = 1,
                StasjonNavn = "Bergen"
            };
            var actionResult = (ViewResult)controller.NyStasjon(stasjon);

            Assert.Equal(stasjon.StasjonNavn + " har blitt lagt til", actionResult.ViewData["OK"]);
        }

        [Fact]
        public void NyStasjon_feil_validering_Post()
        {
            var controller = HentController();
            var stasjon = new Stasjon();
            controller.ViewData.ModelState.AddModelError("StasjonsNavn", "Ikke oppgitt stasjonsnavn");
            var actionResult = (ViewResult) controller.NyStasjon(stasjon);

            Assert.True(actionResult.ViewData.ModelState.Count == 1);
            Assert.Equal("NyStasjon", actionResult.ViewName);
        }

        [Fact]
        public void NyStasjon_feil_Post()
        {
            var controller = HentController();
            var stasjon = new Stasjon();
            stasjon.StasjonNavn = "";

            var actionResult = (ViewResult) controller.NyStasjon(stasjon);

            Assert.Equal(stasjon.StasjonNavn + " kunne ikke bli lagt til, prøv igjen", actionResult.ViewData["Error"]);
        }

        [Fact]
        public void Vis_PartialView_med_liste_AlleStasjoner()
        {
            var controller = HentController();

            var forventetResultat = new List<Stasjon>()
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

            var actionResult = (PartialViewResult) controller.HentAlleStasjoner();
            var resultatListe = (List<Stasjon>) actionResult.Model;
            // Assert
            Assert.Equal("AlleStasjoner", actionResult.ViewName);
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.Equal(forventetResultat[i].SId, resultatListe[i].SId);
                Assert.Equal(forventetResultat[i].StasjonNavn, resultatListe[i].StasjonNavn);
            }
        }

        [Fact]
        public void Vis_PartialView_med_liste_AlleAvganger()
        {
            var controller = HentController();

            var forventetResultat = new List<Avgang>()
            {
                new Avgang()
                {
                    AId = 1,
                    Avgangstid = "10:30",
                    Spor = 1,
                    Linje = "L1",
                    SId = 1
                },
                new Avgang()
                {
                    AId = 2,
                    Avgangstid = "11:30",
                    Spor = 2,
                    Linje = "L2",
                    SId = 2
                },
                new Avgang()
                {
                    AId = 3,
                    Avgangstid = "12:30",
                    Spor = 2,
                    Linje = "L3",
                    SId = 3
                }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SId"] = 1;
            controller.TempData = tempData;

            var actionResult = (PartialViewResult) controller.HentAlleAvganger(1);
            var resultatListe = (List<Avgang>) actionResult.Model;
            // Assert
            Assert.Equal("AlleAvganger", actionResult.ViewName);
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.Equal(forventetResultat[i].AId, resultatListe[i].AId);
                Assert.Equal(forventetResultat[i].Avgangstid, resultatListe[i].Avgangstid);
                Assert.Equal(forventetResultat[i].Spor, resultatListe[i].Spor);
                Assert.Equal(forventetResultat[i].Linje, resultatListe[i].Linje);
            }
        }

        [Fact]
        public void Slett_stasjon_OK()
        {
            var controller = HentController();

            var actionResult = (RedirectToActionResult) controller.SlettStasjon(1);

            // Assert
            Assert.Equal("HentAlleStasjoner", actionResult.ActionName);
        }

        [Fact]
        public void Slett_stasjon_feil_validering()
        {
            var controller = HentController();

            var actionResult = (RedirectToActionResult) controller.SlettStasjon(0);

            // Assert
            Assert.Equal("AdminPanel", actionResult.ActionName);
        }

        [Fact]
        public void Endre_Stasjon_View()
        {
            var controller = HentController();

            var actionResult = (ViewResult) controller.EndreStasjon(1);

            Assert.Equal("EndreStasjon", actionResult.ViewName);
        }

        [Fact]
        public void Endre_Stasjon_Post()
        {
            var controller = HentController();

            var stasjon = new Stasjon()
            {
                SId = 1,
                StasjonNavn = "Bergen"
            };
            var actionResult = (ViewResult) controller.EndreStasjon(stasjon);

            Assert.Equal(stasjon.StasjonNavn + " har blitt endret", actionResult.ViewData["OK"]);
        }

        [Fact]
        public void Endre_Stasjon_feil_validering_Post()
        {
            var controller = HentController();

            var stasjon = new Stasjon();
            stasjon.StasjonNavn = "";
            var actionResult = (ViewResult) controller.EndreStasjon(stasjon);

            Assert.Equal(stasjon.StasjonNavn + " kunne ikke bli endret, prøv igjen", actionResult.ViewData["Error"]);
        }

        [Fact]
        public void Vis_NyAvgang_View()
        {
            var controller = HentController();
            var resultat = (ViewResult) controller.NyAvgang(1);

            Assert.Equal("NyAvgang", resultat.ViewName);
        }

        [Fact]
        public void NyAvgang_validering_Post()
        {
            var controller = HentController();

            var avgang = new Avgang()
            {
                SId = 1,
                AId = 1,
                Avgangstid = "12:30",
                Spor = 2,
                Linje = "L4"
            };
            var actionResult = (ViewResult) controller.NyAvgang(avgang);

            Assert.Equal("Avgangen har blitt lagt til", actionResult.ViewData["OK"]);
        }

        [Fact]
        public void NyAvgang_feil_validering_Post()
        {
            var controller = HentController();
            var avgang = new Avgang();
            controller.ViewData.ModelState.AddModelError("Avgangstid", "Ikke oppgitt Avgangstid");
            var actionResult = (ViewResult) controller.NyAvgang(avgang);

            Assert.True(actionResult.ViewData.ModelState.Count == 1);
            Assert.Equal("NyAvgang", actionResult.ViewName);
        }

        [Fact]
        public void NyAvgang_feil_Post()
        {
            var controller = HentController();
            var avgang = new Avgang();
            avgang.Avgangstid = "";

            var actionResult = (ViewResult) controller.NyAvgang(avgang);

            Assert.Equal("Avgangen kunne ikke bli lagt til, prøv igjen", actionResult.ViewData["Error"]);
        }

        [Fact]
        public void Slett_Avgang_OK()
        {
            var controller = HentController();
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SId"] = 1;
            controller.TempData = tempData;
            var actionResult = (RedirectToActionResult) controller.SlettAvgang(1);

            // Assert
            Assert.Equal("HentAlleAvganger", actionResult.ActionName);
        }

        [Fact]
        public void Slett_Avgang_feil_validering()
        {
            var controller = HentController();
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SId"] = 1;
            controller.TempData = tempData;
            var actionResult = (RedirectToActionResult) controller.SlettAvgang(0);

            // Assert
            Assert.Equal("HentAlleStasjoner", actionResult.ActionName);
        }

        [Fact]
        public void Endre_Avgang_View()
        {
            var controller = HentController();

            var actionResult = (ViewResult) controller.EndreAvgang(1);

            Assert.Equal("EndreAvgang", actionResult.ViewName);
        }

        [Fact]
        public void Endre_Avgang_Post()
        {
            var controller = HentController();

            var avgang = new Avgang()
            {
                SId = 2,
                AId = 2,
                Avgangstid = "19:32",
                Spor = 5,
                Linje = "L3"
            };

            var actionResult = (ViewResult) controller.EndreAvgang(avgang);

            Assert.Equal("Avgangen har blitt endret", actionResult.ViewData["OK"]);
        }

        [Fact]
        public void Endre_Avgang_feil_validering_Post()
        {
            var controller = HentController();

            var avgang = new Avgang();
            avgang.Avgangstid = "";
            var actionResult = (ViewResult) controller.EndreAvgang(avgang);

            Assert.Equal("Avgangen kunne ikke bli endret, prøv igjen", actionResult.ViewData["Error"]);
        }

        public AdminController HentController()
        {
            var stasjonStub = new DBStasjonStub();
            var avgangStub = new DBAvgangStub();
            var adminStub = new DBAdminStub();
            var controller = new AdminController(new AvgangBLL(avgangStub), new StasjonBLL(stasjonStub),
                new AdminBLL(adminStub));

            return controller;
        }

        private ControllerContext AddAuthentication()
        {
            var context = new ControllerContext();
            context.HttpContext = new DefaultHttpContext();


            var authManager = new Mock<IAuthenticationService>();
            authManager.Setup(s => s.SignOutAsync(It.IsAny<HttpContext>(),
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    It.IsAny<Microsoft.AspNetCore.Authentication.AuthenticationProperties>()))
                .Returns(Task.FromResult(true));
            var servicesMock = new Mock<IServiceProvider>();
            servicesMock.Setup(sp => sp.GetService(typeof(IAuthenticationService))).Returns(authManager.Object);
            servicesMock.Setup(sp => sp.GetService(typeof(IUrlHelperFactory))).Returns(new UrlHelperFactory());
            servicesMock.Setup(sp => sp.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(new TempDataDictionaryFactory(new SessionStateTempDataProvider()));

            context.HttpContext.RequestServices = servicesMock.Object;

            return context;
        }
    }
}