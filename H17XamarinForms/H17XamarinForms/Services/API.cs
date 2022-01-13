using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace H17XamarinForms.Services
{
    public class API<R> : IDisposable
    {
        public R Data { get; private set; }
        public bool IsError { get; private set; }
        public string ErrorMessage { get; private set; }
        public string ServerMessage { get; private set; }

        static string _baseaServerAdress = "https://localhost:44378/";

        public void Dispose()
        {
            
        }

        public void Get(string area, string controller, string action)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    System.Net.ServicePointManager.
                            ServerCertificateValidationCallback += RemoteCertValidate;

                    client.BaseAddress = _baseaServerAdress;
                    ServerMessage = client.DownloadString($"{area}/{controller}/{action}");

                    Data = JsonConvert.DeserializeObject<R>(ServerMessage);
                }
                catch (Exception ex)
                {
                    SetErrorInformation(ex.Message);
                }

            }
            void SetErrorInformation(string mesage)
            {
                IsError = true;
                ErrorMessage = mesage;
            }
        }
        public void Get(string area, string controller, string action, Dictionary<string,string> queryProperties)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    System.Net.ServicePointManager.
                            ServerCertificateValidationCallback += RemoteCertValidate;


                    foreach (var item in queryProperties)
                    {
                        client.QueryString.Add(item.Key, item.Value);
                    }


                    client.BaseAddress = _baseaServerAdress;
                    ServerMessage = client.DownloadString($"{area}/{controller}/{action}");

                    Data = JsonConvert.DeserializeObject<R>(ServerMessage);
                }
                catch (Exception ex)
                {
                    SetErrorInformation(ex.Message);
                }

            }
            void SetErrorInformation(string mesage)
            {
                IsError = true;
                ErrorMessage = mesage;
            }
        }
        public void Post(string area, string controller, string action, object @obj)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    System.Net.ServicePointManager.
                            ServerCertificateValidationCallback += RemoteCertValidate;


                    client.Headers.Add("Content-Type", "application/json");
                    client.BaseAddress = _baseaServerAdress;


                    ServerMessage = client.UploadString($"{area}/{controller}/{action}", JsonConvert.SerializeObject(obj));

                    Data = JsonConvert.DeserializeObject<R>(ServerMessage);
                }
                catch (Exception ex)
                {
                    SetErrorInformation(ex.Message);
                }

            }
            void SetErrorInformation(string mesage)
            {
                IsError = true;
                ErrorMessage = mesage;
            }
        }



        private static bool RemoteCertValidate(object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }
    }
}
//TODO: before send request need to check is server avaible or not