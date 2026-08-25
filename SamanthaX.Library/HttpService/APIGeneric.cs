using SamanthaX.Library.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.HttpService {
    
    public class APIGeneric {

        public APIGeneric() { }

        public (
            HttpStatusCode IsSuccess,
            string stObjectStruct,
            string ErrorMessage
        ) GetAPI<T>(string url, T objectRequest) {
            string result = string.Empty;
            try {
                //serializamos el objeto
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);

                //peticion
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = "POST";
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Timeout = 60000;
                //
                var username = Variable.APIUsername;
                var password = Variable.APIPassword;
                byte[] basicBytes = Encoding.ASCII.GetBytes($"{username}:{password}");
                string basicB64 = Convert.ToBase64String(basicBytes);
                string encoded =
                    System.Convert.ToBase64String(
                        Encoding
                        .GetEncoding("ISO-8859-1")
                        .GetBytes($"{username} : {password}")
                    );
                request.Headers.Add("Authorization", "Basic " + basicB64);
                //
                using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    result = streamReader.ReadToEnd();
                }
                return (httpResponse.StatusCode, result, string.Empty);
            } catch (Exception e) {
                result = e.Message;
            }
            return (HttpStatusCode.BadGateway, null, result);
        }
    }
}
