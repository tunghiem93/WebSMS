using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCentrifugo;
using CMS_Entity;
using CMS_Entity.Entity;
using Newtonsoft.Json;
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
        public static bool PublishApiToCentri(string method, string url, string apiKey, string channel, object data)
        {
            try
            {
                HttpClient client = new HttpClient();
                CentrifugoParamModel param = new CentrifugoParamModel() { channel = channel, data = data };
                CentrifugoModel cenMod = new CentrifugoModel() { method = method, param = param };
                client.DefaultRequestHeaders.Add("Authorization", string.Format("apikey {0}", apiKey));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-type", "application/json");
                var response = client.PostAsJsonAsync(url, cenMod).Result;
                var result = response.Content.ReadAsStringAsync();
                NSLog.Logger.Info("PublishApiToCentri: " + response.StatusCode + "-" + result.Result);
                NSLog.Logger.Info("PublishApiToCentri: " + JsonConvert.SerializeObject(cenMod));
                if (result.Result.Contains("error"))
                {
                    return false;
                }
                else
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("PublishApiToCentri: ", ex);
                return false;
            }


        }
       
    }
}
