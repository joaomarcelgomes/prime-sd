using System.Net;
using System.Text;
using System.Xml;

namespace OrderSystem.Order.API.Infrastructure.ExternalServices
{
    public class RpcClient
    {
        private readonly string _url;

        public RpcClient(string url)
        {
            _url = url;
        }

        public string Call(string method, params object[] parameters)
        {
            var xmlRequest = new XmlDocument();
            xmlRequest.LoadXml($"<?xml version='1.0'?>" +
                $"<methodCall><methodName>{method}</methodName><params>" +
                $"{string.Join("", Array.ConvertAll(parameters, p => $"<param><value><string>{p}</string></value></param>"))}" +
                $"</params></methodCall>");

            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "text/xml";

            byte[] bytes = Encoding.UTF8.GetBytes(xmlRequest.OuterXml);
            request.ContentLength = bytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                var xmlResponse = new XmlDocument();
                xmlResponse.LoadXml(responseStream.ReadToEnd());
                return xmlResponse.SelectSingleNode("//string")?.InnerText ?? "Erro na resposta RPC";
            }
        }
    }
}
