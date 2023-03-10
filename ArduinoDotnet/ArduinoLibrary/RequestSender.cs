using Nancy.Json;
using System.Net;

namespace ArduinoLibrary
{
    internal class RequestSender
    {
        public bool SendPinStateRequest(string url, string pinName, int type, double state)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                if (type == 1)
                {
                    if (state > 1 || state < 0)
                    {
                        Console.WriteLine("Wrong State value ignoring");
                        return true;
                    }
                }

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        Pin = pinName,
                        State = state,
                        Type = type
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SendGenericRequest(string url, string json)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
