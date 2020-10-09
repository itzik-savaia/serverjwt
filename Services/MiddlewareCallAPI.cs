using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JWT.Services
{
    public class MiddlewareCallAPI
    {
        public static string CallApi(string url_project)
        {
            try
            {
                string res;
                if (url_project != null)
                {
                    WebRequest request = WebRequest.Create(url_project);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        return res = reader.ReadToEnd();
                    }
                }
                else
                {
                    return res = "Samting is worng!";
                }
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }        
        }

    }
}
