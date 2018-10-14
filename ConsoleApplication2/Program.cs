using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication2
{
    class Program
    {
        /// <summary>
        /// Execute a Soap WebService call
        /// </summary>
        public static void Execute()
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <SOAP-ENV:Envelope
                    xmlns:ns0=""http://schemas.xmlsoap.org/soap/envelope/""
                    xmlns:ns1=""http://www.airservicesaustralia.com/naips/xsd""
                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <SOAP-ENV:Header/>
                    <ns0:Body>
                    <ns1:loc-brief-rqs password =""Hawk0524"" requestor =""Hawk0524"" source =""atis"">
                    <ns1:loc>YSSY</ns1:loc>
                    <ns1:loc>YBBN</ns1:loc>
                    <ns1:loc>YSCB</ns1:loc>
                    <ns1:loc>YMML</ns1:loc>
                    <ns1:loc>YPAD</ns1:loc>
                    <ns1:loc>YPPH</ns1:loc>
                    <ns1:loc>YBCS</ns1:loc>
                    <ns1:flags met=""true""/>
                    </ns1:loc-brief-rqs>
                    </ns0:Body>
                    </SOAP-ENV:Envelope >");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                }
            }
        }
        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"https://www.airservicesaustralia.com/naips/briefing-service?wsdl");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        static void Main(string[] args)
        {
            Execute();
            Console.ReadLine();
        }
    }
}
