using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCentrifugo;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSCategories
{
    public static class CMSCentrifugoFactory
    {
        public static bool SendSMSToCentri(string method, string url, string apiKey, string channel, object data)
        {
            try
            {
                HttpClient client = new HttpClient();
                CentrifugoParamModel param = new CentrifugoParamModel() { channel = channel, data = data };
                CentrifugoModel cenMod = new CentrifugoModel() { method = method, param = param };
                client.DefaultRequestHeaders.Add("Authorization", string.Format("apikey {0}", apiKey));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-type", "application/json");
                var response = client.PostAsJsonAsync(url, cenMod).Result;
                NSLog.Logger.Info("SendSMSToCentri: " + response.Content.ReadAsStringAsync().ToString());
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("SendSMSToCentri: ",ex);
                return false;
            }

            
        }
    }
}
