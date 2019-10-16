using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Gruppeoppgave1.api
{
    public class GetDistance
    {
        public static int GetDistanceFromApi(string origin, string destination)
        {
            //Addressen til API og nøkkelen
            string url = @"https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin +
                         "Norge&destinations=" + destination + " Norge&mode=transit&sensor=false&language=en-EN&units=metric&key=AIzaSyDG4SLSjocdjnDdcDTi24xz-ulgMrnsaQk";

            //GET forespørsel om å hente xmldoc fra url
            WebRequest request = WebRequest.Create(url);
            var response = request.GetResponse().GetResponseStream();
            string sreader = new StreamReader(response).ReadToEnd();
            response.Close();

            //oppretter ny doc med informasjon hentet fra doc
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(sreader);

            //Iterer nedover til informasjonen vi trenger og henter det ut
            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                return Int32.Parse(distance[0].ChildNodes[0].InnerText) / 1000 * 2;
            }

            return 0;
        }

    }
}